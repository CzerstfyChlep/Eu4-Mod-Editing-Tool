﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    public class NodeItem
    {
        public string Name = "";
        public string Comment = "";
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
        public NodeFile()
        {
            MainNode = new Node("__MainNode");
        }
        public NodeFile(string file, bool readonl = false)
        {
            ReadFile(file);
            ReadOnly = readonl;
            Path = file;
        }


        char[] specialChars = { '=', '{', '}', '#' };

        public void ReadFile(string path)
        {
            if (!File.Exists(path))
                return;
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
                return;
            }

            bool comment = false;
            bool commentline = false;
            bool equals = false;
            string pretxt = "";
            bool insideAp = false;

            foreach (string line in File.ReadAllLines(path))
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
                                    CurrentNode.Variables.Add(v);
                                    CurrentNode.ItemOrder.Add(v);
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
            Console.ReadLine();
            /*
        StreamReader Reader = new StreamReader(path);
        string read = "";
        string name = "";
        string value = "";
        bool comment = false;
        bool commentLine = false;
        NodeItem lastObj = null;
        string commentText = "";
        while (!Reader.EndOfStream)
        {
            char character = (char)Reader.Read();

            if (character == '=' && !comment)
            {
                name = read.Trim();
                read = "";
            }
            else if (character == '{' && !comment)
            {
                Node newNode = new Node(name, CurrentNode);
                CurrentNode.Nodes.Add(newNode);
                CurrentNode = newNode;
                lastObj = null;
                name = "";
                value = "";
                read = "";

            }
            else if (character == '}' && !comment)
            {
                if (!read.Contains('=') && name == "" && value == "")
                {
                    foreach (string v in read.Replace("\t", " ").Split(' '))
                    {
                        if (v != "" && v != " " && !string.IsNullOrWhiteSpace(v))
                        {
                            CurrentNode.PureValues.Add(v.Trim());
                        }
                    }
                }
                lastObj = CurrentNode;
                CurrentNode.Parent.ItemOrder.Add(CurrentNode);
                CurrentNode = CurrentNode.Parent;
                name = "";
                value = "";
                read = "";
            }
            else if (character == '\n')
            {
                if (commentLine)
                {
                    CurrentNode.Comments.Add(new CommentLine(commentText, lastObj));
                }
                else
                {
                    if (name != "")
                    {
                        Variable v = new Variable(name, read.Trim());
                        v.Comment = commentText;
                        CurrentNode.Variables.Add(v);
                        CurrentNode.ItemOrder.Add(v);
                        lastObj = v;
                    }
                    else
                    {
                        foreach (string v in read.Replace("\t", " ").Split(' '))
                        {
                            if (v != "" && v != " " && !string.IsNullOrWhiteSpace(v))
                            {
                                CurrentNode.PureValues.Add(v.Trim());
                            }
                        }
                    }
                }
                name = "";
                value = "";
                read = "";

                comment = false;
                commentLine = false;
                commentText = "";
            }
            else if (character == '#')
            {
                comment = true;
                if (read.Trim() == "" || name.Trim() == "")
                {
                    commentLine = true;
                }
            }
            else if (comment)
            {
                commentText += character;
            }
            else
            {
                read += character;
            }
        }
        if (name != "")
        {
            if (name.Contains(" "))
            {
                foreach (string v in name.Split(' '))
                {
                    CurrentNode.Variables.Add(new Variable(v, ""));
                }
            }
            else
            {
                CurrentNode.Variables.Add(new Variable(name, read.Trim()));
            }
        }
        Reader.Close();
        */

        }
        public void SaveFile(string path)
        {
            File.WriteAllText(path, Node.NodeToText(MainNode));
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

    public class PureValue
    {
        public string Name = "";
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

            if (n.PureValues.Any())
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
            }
            else
            {
                foreach (NodeItem ni in n.ItemOrder)
                {
                    if (ni is Variable)
                    {
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
                }
            }
            return text;
        }
        public bool ChangeVariable(string name, string value, bool forceadd = false)
        {
            Variable v = Variables.Find(x => x.Name == name);
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
        public PureValue AddPureValue(string name, bool sep = false)
        {
            PureValue pv = new PureValue(name, sep);
            PureValues.Add(pv);
            return pv;
        }
        public string[] GetPureValuesAsArray()
        {
            List<string> s = new List<string>();
            foreach (PureValue pv in PureValues)
                s.Add(pv.Name);
            return s.ToArray();
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
