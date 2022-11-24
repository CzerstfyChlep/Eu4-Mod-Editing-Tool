using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    public class NodeItem
    {
        public string Name = "";
        public string Comment = "";
    }

    public class NodeFileReadStatus
    {
        public int LineError = 0;
        public bool HasError = false;
    }

    public class NodeFile
    {
        public Node MainNode;
        public List<Node> AllNodes = new List<Node>();
        public List<Variable> Allvariables = new List<Variable>();
        public bool ReadOnly = false;
        public bool CreatedByEditor = false;
        public string FileName = "";
        public string Path = "";
        public NodeFileReadStatus LastStatus = new NodeFileReadStatus();
        public NodeFile()
        {
            MainNode = new Node("__MainNode");
        }
        public NodeFile(string file, bool readonl = false, bool dontread = false)
        {
            if (!dontread)
            {
                LastStatus = ReadFile(file);
            }
            else
            {
                MainNode = new Node("__MainNode");
            }
            ReadOnly = readonl;
            Path = file;
        }


        char[] specialChars = { '=', '{', '}', '#' };

        public NodeFileReadStatus ReadFile(string path, bool localPath = false)
        {
            Node CurrentNode = new Node("__MainNode");
            MainNode = CurrentNode;
            if (!localPath)
            {
                Path = path;
                FileName = path.Split('/').Last().Replace(".txt", "");
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(Path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Path));
                }
                if (!File.Exists(path))
                {
                    CreatedByEditor = true;
                    return new NodeFileReadStatus();
                }
                if (string.IsNullOrWhiteSpace(File.ReadAllText(path)))
                    return new NodeFileReadStatus();
            }
            int linen = 0;

            try
            {

                string read = File.ReadAllText(path);

                bool InComment = false;
                string ReadValue = "";
                string SavedValue = "";
                string Comment = "";
                bool Quote = false;
                int LastValueReadLocation = 0;

                bool EncounteredWhiteSpace = false;
                bool NothingButComment = true;
                for (int a = 0; a < read.Length; a++)
                {
                    char C = read[a];

                    if (InComment && C != '\n' && C != '\r')
                    {
                        Comment += C;
                        continue;
                    }
                    else if (InComment && C == '\n')
                    {
                        if (NothingButComment)
                        {
                            if (CurrentNode.ItemOrder.Any())
                            {
                                CommentLine cl = new CommentLine(Comment, CurrentNode.ItemOrder.Last());
                                CurrentNode.Comments.Add(cl);
                            }
                            else
                            {
                                CommentLine cl = new CommentLine(Comment, null);
                                CurrentNode.Comments.Add(cl);
                            }
                        }
                        else
                        {
                            if (CurrentNode.ItemOrder.Any())
                                CurrentNode.ItemOrder.Last().Comment = Comment;
                            else
                                CurrentNode.FirstBracketComment = Comment;
                        }
                        linen++;
                        InComment = false;
                        Comment = "";
                        EncounteredWhiteSpace = true;
                        continue;
                    }
                    else if (InComment)
                    {
                        continue;
                    }

                    if (Quote && C != '"')
                    {
                        if (C != '\r' && C != '\n')
                            ReadValue += C;
                        else if (C == '\n')
                            ReadValue += ' ';
                        continue;
                    }

                    if (C == ' ' || C == '\t')
                    {
                        EncounteredWhiteSpace = true;
                        continue;
                    }

                    switch (C)
                    {
                        case '\n':
                            EncounteredWhiteSpace = true;
                            linen++;
                            InComment = false;
                            NothingButComment = true;
                            continue;
                        case '=':
                            //SWITCH
                            SavedValue = ReadValue;
                            ReadValue = "";
                            NothingButComment = false;
                            break;
                        case '"':
                            if (Quote)
                            {
                                //SWITCH
                                if (ReadValue != "" && SavedValue == "")
                                {
                                    bool separate = false;
                                    if (LastValueReadLocation != a)
                                    {
                                        string LastRead = read.Substring(LastValueReadLocation, a - LastValueReadLocation);
                                        if (LastRead.Contains('\n'))
                                            separate = true;
                                    }
                                    CurrentNode.AddPureValue(ReadValue.Trim(), separate).Quoted = true;

                                }
                                else if (ReadValue != "")
                                {
                                    CurrentNode.AddVariable(SavedValue, ReadValue.Trim()).QuotedValue = true;
                                    SavedValue = "";
                                }
                                ReadValue = "";
                            }
                            else
                            {
                                if (ReadValue != "" && SavedValue == "")
                                {
                                    bool separate = false;
                                    if (LastValueReadLocation != a)
                                    {
                                        string LastRead = read.Substring(LastValueReadLocation, a - LastValueReadLocation);
                                        if (LastRead.Contains('\n'))
                                            separate = true;
                                    }
                                    CurrentNode.AddPureValue(ReadValue, separate);

                                }
                                else if (ReadValue != "")
                                {
                                    CurrentNode.AddVariable(SavedValue, ReadValue); ;
                                    SavedValue = "";
                                }
                                ReadValue = "";
                            }
                            Quote = !Quote;
                            NothingButComment = false;
                            break;
                        case '#':
                            //ENDLINE

                            if (ReadValue != "" && SavedValue == "")
                            {
                                bool separate = false;
                                if (LastValueReadLocation != a)
                                {
                                    string LastRead = read.Substring(LastValueReadLocation, a - LastValueReadLocation);
                                    if (LastRead.Contains('\n'))
                                        separate = true;
                                }
                                CurrentNode.AddPureValue(ReadValue, separate);

                            }
                            else if (ReadValue != "")
                            {
                                CurrentNode.AddVariable(SavedValue, ReadValue);
                                SavedValue = "";
                            }
                            ReadValue = "";
                            InComment = true;

                            break;
                        case '{':
                            if (ReadValue != "")
                                CurrentNode = CurrentNode.AddNode(ReadValue);
                            else if (SavedValue != "")
                                CurrentNode = CurrentNode.AddNode(SavedValue);
                            else
                                CurrentNode = CurrentNode.AddNode("MOD_EDITOR_EMPTY_NODE_ERROR");
                            SavedValue = "";
                            ReadValue = "";
                            NothingButComment = false;
                            EncounteredWhiteSpace = false;
                            break;
                        case '}':
                            if (ReadValue != "" && SavedValue == "")
                            {
                                bool separate = false;
                                if (LastValueReadLocation != a)
                                {
                                    string LastRead = read.Substring(LastValueReadLocation, a - LastValueReadLocation);
                                    if (LastRead.Contains('\n'))
                                        separate = true;
                                }
                                CurrentNode.AddPureValue(ReadValue, separate);
                            }
                            else if (ReadValue != "")
                            {
                                CurrentNode.AddVariable(SavedValue, ReadValue);
                                SavedValue = "";
                            }
                            ReadValue = "";

                            CurrentNode = CurrentNode.Parent;
                            ReadValue = "";
                            SavedValue = "";
                            EncounteredWhiteSpace = false;
                            NothingButComment = false;
                            break;
                        case '\r':
                            continue;
                        default:
                            if (EncounteredWhiteSpace)
                            {
                                EncounteredWhiteSpace = false;
                                if (ReadValue != "" && SavedValue == "")
                                {
                                    bool separate = false;
                                    if (LastValueReadLocation != a)
                                    {
                                        string LastRead = read.Substring(LastValueReadLocation, a - LastValueReadLocation);
                                        if (LastRead.Contains('\n'))
                                            separate = true;
                                    }
                                    CurrentNode.AddPureValue(ReadValue, separate);
                                }
                                else if (ReadValue != "")
                                {
                                    CurrentNode.AddVariable(SavedValue, ReadValue);
                                    SavedValue = "";
                                }
                                ReadValue = "";

                            }
                            NothingButComment = false;
                            ReadValue += C;
                            LastValueReadLocation = a;
                            break;

                    }
                }
                if (ReadValue != "" && SavedValue == "")
                {
                    bool separate = false;
                    if (LastValueReadLocation != read.Length)
                    {
                        string LastRead = read.Substring(LastValueReadLocation, read.Length - LastValueReadLocation);
                        if (LastRead.Contains('\n'))
                            separate = true;
                    }
                    CurrentNode.AddPureValue(ReadValue, separate);
                }
                else if (ReadValue != "")
                {
                    CurrentNode.AddVariable(SavedValue, ReadValue);
                    SavedValue = "";
                }
            }
            catch
            {
                NodeFileReadStatus status = new NodeFileReadStatus();
                status.HasError = true;
                status.LineError = linen;
                return status;
            }
            return new NodeFileReadStatus();
        }

        public void SaveFile(string path)
        {
            File.WriteAllText(path, Node.NodeToText(MainNode));
        }
        public void SaveFile()
        {
            File.WriteAllText(Path, Node.NodeToText(MainNode));
        }
    }
    public class CommentLine
    {
        public string Text = "";
        public NodeItem Below = null;
        public PureValue BelowPureValue = null;
        public CommentLine(string text, NodeItem below)
        {
            Text = text;
            Below = below;
        }
        public CommentLine(string text, PureValue below)
        {
            Text = text;
            BelowPureValue = below;
        }
    }

    public class PureValue : NodeItem
    {
        public bool SeparateLine = false;
        public bool Quoted = false;
        public override string ToString()
        {
            return Name;
        }
        public PureValue(string val, bool sep = false, bool quot = false)
        {
            Name = val;
            SeparateLine = sep;
            Quoted = quot;
        }
    }

    public class Node : NodeItem
    {
        public string PureInnerText = "";
        public bool UseInnerText = false;
        public string FirstBracketComment = "";
        public Node Parent = null;
        public List<Node> Nodes = new List<Node>();
        public List<Variable> Variables = new List<Variable>();
        public List<PureValue> PureValues = new List<PureValue>();
        public List<CommentLine> Comments = new List<CommentLine>();
        public List<NodeItem> ItemOrder = new List<NodeItem>();
        public Node(string name)
        {
            Name = name;
        }
        public Node(string name, Node parent)
        {
            Name = name;
            Parent = parent;
        }
        public static string NodeToText(Node n)
        {
            string text = "";
            foreach (CommentLine cl in n.Comments)
            {
                if (cl.Below == null && cl.BelowPureValue == null)
                    text += "#" + cl.Text + "\n";
            }

            int count = 0;
            bool lastwassep = false;
            bool lastwaspure = false;
            foreach (NodeItem ni in n.ItemOrder)
            {
                if (ni is Variable)
                {
                    if (lastwaspure && !lastwassep)
                        text += "\n";
                    lastwaspure = false;
                    count = 0;
                    lastwassep = true;
                    Variable v = (Variable)ni;
                    if (v.QuotedValue)
                        text += v.Name + " = \"" + v.Value + "\"";
                    else
                        text += v.Name + " = " + v.Value;
                    if (v.Comment != "")
                        text += "#" + v.Comment;
                    text += "\n";
                    foreach (CommentLine cl in n.Comments)
                    {
                        if (cl.Below == v)
                            text += "#" + cl.Text + "\n";
                    }
                }
                else if (ni is Node)
                {
                    if (lastwaspure && !lastwassep)
                        text += "\n";
                    lastwaspure = false;
                    count = 0;
                    lastwassep = true;
                    Node inner = (Node)ni;
                    text += inner.Name + " = {";
                    if (inner.FirstBracketComment != "")
                        text += " #" + inner.FirstBracketComment + "\n";
                    if (inner.UseInnerText && false)
                    {
                        text += " " + inner.PureInnerText + " ";
                    }
                    else
                    {
                        text += "\n";
                        string innertext = NodeToText(inner);
                        string tabbedtext = "";
                        foreach (string line in innertext.Split('\n'))
                        {
                            if (line != "")
                            {
                                tabbedtext += "\t" + line + "\n";
                            }
                        }
                        text += tabbedtext;
                    }
                    text += "}";
                    if (ni.Comment != "")
                        text += "#" + ni.Comment;
                    text += "\n";

                    foreach (CommentLine cl in n.Comments)
                    {
                        if (cl.Below == inner)
                            text += "#" + cl.Text + "\n";
                    }
                }
                else if (ni is PureValue)
                {
                    PureValue s = (PureValue)ni;
                    if (s.SeparateLine)
                    {
                        if (!lastwassep)
                            text += " ";
                        if (s.Quoted)
                            text += "\"" + s.Name + "\"\n";
                        else
                            text += s.Name + "\n";
                        lastwassep = true;
                        count = 0;
                    }
                    else
                    {
                        count++;
                        if (count == 10)
                        {
                            count = 0;
                            if (s.Quoted)
                                text += "\"" + s + "\"\n";
                            else
                                text += s + "\n";
                        }
                        else
                        {
                            if (s.Quoted)
                                text += "\"" + s + "\"";
                            else
                                text += s + " ";
                        }
                        lastwassep = false;
                    }
                    foreach (CommentLine cl in n.Comments.FindAll(x => x.BelowPureValue == s))
                    {
                        if (!lastwassep)
                            text += "\n";
                        text += "#" + cl.Text + "\n";
                        lastwassep = true;
                    }
                    lastwaspure = true;
                }
            }

            return text;
        }
        public bool ChangeVariable(string name, string value, bool forceadd = false)
        {
            Variable v = Variables.Find(x => x.Name.ToLower() == name.ToLower());
            if (v != null)
            {
                v.Value = value;
                return true;
            }
            else
            {
                if (forceadd)
                {
                    v = new Variable(name, value);
                    Variables.Add(v);
                    ItemOrder.Add(v);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public Node AddNode(string name)
        {
            Node n = new Node(name, this);
            Nodes.Add(n);
            ItemOrder.Add(n);
            return n;
        }
        public void AddNode(Node node)
        {
            Nodes.Add(node);
            ItemOrder.Add(node);
            node.Parent = this;
        }
        public PureValue AddPureValue(string name, bool sep = false, bool checkexists = false)
        {
            if (checkexists)
            {
                if (PureValues.Any(x => x.Name == name))
                    return null;
            }
            PureValue pv = new PureValue(name, sep);
            PureValues.Add(pv);
            ItemOrder.Add(pv);
            return pv;
        }
        public string[] GetPureValuesAsArray()
        {
            List<string> s = new List<string>();
            foreach (PureValue pv in PureValues)
                s.Add(pv.Name);
            return s.ToArray();
        }
        public Variable AddVariable(string name, string value)
        {
            Variable v = new Variable(name, value);
            Variables.Add(v);
            ItemOrder.Add(v);
            return v;
        }
        public void AddVariable(Variable v)
        {
            Variables.Add(v);
            ItemOrder.Add(v);
        }
        public void RemoveVariable(Variable v)
        {
            Variables.Remove(v);
            ItemOrder.Remove(v);
        }
        public void RemovePureValue(string v)
        {
            PureValue va = PureValues.Find(x => x.Name == v);
            if (va != null)
            {
                PureValues.Remove(va);
                ItemOrder.Remove(va);
            }
        }

        public void RemoveAllPureValues()
        {
            PureValues.Clear();
            ItemOrder.RemoveAll(x => x is PureValue);
        }

        public void RemoveNode(Node n)
        {
            Nodes.Remove(n);
            ItemOrder.Remove(n);
        }
    }
    public class Variable : NodeItem
    {
        public string Value = "";
        public bool QuotedValue = false;
        public Variable(string name, string value, bool quot = false)
        {
            Name = name;
            Value = value;
            QuotedValue = quot;
        }
    }
}
