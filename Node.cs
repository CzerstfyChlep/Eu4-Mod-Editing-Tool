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
            if(!dontread)
                LastStatus = ReadFile(file);
            ReadOnly = readonl;
            Path = file;
        }


        char[] specialChars = { '=', '{', '}', '#' };

        public NodeFileReadStatus ReadFile(string path)
        {
            Path = path;
            FileName = path.Split('\\').Last().Replace(".txt", "");
            Node CurrentNode = new Node("__MainNode");
            MainNode = CurrentNode;
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(Path)))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Path));
            }
            if (!File.Exists(path))
            {
                CreatedByEditor = true;
                return new NodeFileReadStatus();
            }

            bool comment = false;
            bool commentline = false;
            bool equals = false;
            string pretxt = "";
            bool insideAp = false;

            if (string.IsNullOrWhiteSpace(File.ReadAllText(path)))
                return new NodeFileReadStatus();
            int linen = 0;

            foreach (string line in File.ReadAllLines(path))
            {
                linen++;
                try
                {
                    string nospaces = "";
                    int n = 0;
                    foreach (char c in line)
                    {
                        if (specialChars.Contains(c) && !insideAp)
                            nospaces += $" {c} ";
                        else if (c == '"' && n + 1 < line.Length)
                        {
                            if (line[n + 1] == '"')
                                nospaces += $"{c} ";
                            else
                                nospaces += c;
                        }
                        else
                            nospaces += c;
                        n++;
                        if (c == '"')
                            insideAp = !insideAp;

                    }

                    nospaces = Regex.Replace(nospaces.Replace("\t", " "), @"\s+", " ").Trim();

                    //Console.WriteLine(nospaces);

                    List<string> contentsl = nospaces.Split(' ').ToList();
                    contentsl.RemoveAll(x => x == " " || x == "");
                    string[] contents = contentsl.ToArray();

                    for (int a = 0; a < contents.Length; a++)
                    {
                        if (!comment)
                        {
                            switch (contents[a])
                            {
                                case "#":
                                    if (pretxt != "")
                                        CurrentNode.AddPureValue(pretxt, contents.Length == 1);
                                    comment = true;
                                    equals = false;
                                    pretxt = "";
                                    if (a == 0)
                                        commentline = true;
                                    break;
                                case "=":
                                    equals = true;
                                    break;
                                case "{":
                                    CurrentNode = CurrentNode.AddNode(pretxt);
                                    pretxt = "";
                                    equals = false;
                                    break;
                                case "}":
                                    if (pretxt != "")
                                        CurrentNode.AddPureValue(pretxt, contents.Length == 1);
                                    CurrentNode = CurrentNode.Parent;
                                    pretxt = "";
                                    break;
                                default:
                                    string textmem = "";
                                    if (contents[a].Count(x => x == '"') == 1)
                                    {
                                        textmem += contents[a] + " ";
                                        do
                                        {
                                            a++;
                                            textmem += contents[a] + " ";
                                        } while (a + 1 < contents.Length && !contents[a].Contains("\""));
                                        textmem.Trim();
                                    }

                                    if (!equals && pretxt != "")
                                    {
                                        CurrentNode.AddPureValue(pretxt, contents.Length == 1);
                                        CurrentNode.AddPureValue(textmem == "" ? contents[a] : textmem, contents.Length == 1);
                                        pretxt = "";
                                    }
                                    else if (!equals && pretxt == "")
                                    {
                                        if (a + 1 == contents.Length)
                                            CurrentNode.AddPureValue(contents[a], contents.Length == 1);
                                        else
                                            pretxt = textmem == "" ? contents[a] : textmem;

                                    }
                                    else if (equals)
                                    {
                                        Variable v = new Variable(pretxt, textmem == "" ? contents[a] : textmem);
                                        CurrentNode.AddVariable(v);
                                        equals = false;
                                        pretxt = "";
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            string textleft = "";
                            for (; a < contents.Length; a++)
                            {
                                textleft += contents[a] + " ";
                            }

                            if (commentline)
                            {
                                if (CurrentNode.ItemOrder.Any())
                                {
                                    CommentLine cl = new CommentLine(textleft, CurrentNode.ItemOrder.Last());
                                    CurrentNode.Comments.Add(cl);
                                }
                                else if (CurrentNode.PureValues.Any())
                                {
                                    CommentLine cl = new CommentLine(textleft, CurrentNode.PureValues.Last());
                                    CurrentNode.Comments.Add(cl);
                                }
                                else
                                {
                                    NodeItem nl = null;
                                    CommentLine cl = new CommentLine(textleft, nl);
                                    CurrentNode.Comments.Add(cl);
                                }


                            }
                            else
                            {
                                if (CurrentNode.ItemOrder.Any())
                                    CurrentNode.ItemOrder.Last().Comment = textleft;
                                else
                                    CurrentNode.FirstBracketComment = textleft;
                            }
                        }
                    }
                    comment = false;
                    commentline = false;
                    pretxt = "";
                }
                catch
                {
                    return new NodeFileReadStatus { HasError = true, LineError = linen };
                }
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
        public override string ToString()
        {
            return Name;
        }
        public PureValue(string val, bool sep = false)
        {
            Name = val;
            SeparateLine = sep;
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

            /*if (n.PureValues.Any())
            {
                int count = 0;
                bool lastwassep = false;
                foreach (PureValue s in n.PureValues)
                {
                    if (s.SeparateLine)
                    {
                        if (!lastwassep)
                            text += "\n";
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
                            text += s + "\n";
                        }
                        else
                        {
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
                }
                text += "\n";
            }
            */

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
                else if(ni is PureValue)
                {
                    PureValue s = (PureValue)ni;
                    if (s.SeparateLine)
                    {
                        if (!lastwassep)
                            text += "\n";
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
                            text += s + "\n";
                        }
                        else
                        {
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
            if(checkexists)
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
            if(va != null)
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

        public Variable(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
