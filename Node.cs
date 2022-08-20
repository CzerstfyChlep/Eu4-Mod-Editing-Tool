using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class NodeFile
    {
        public Node MainNode;
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
        public void ReadFile(string path)
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
                return;
            }       
            StreamReader Reader = new StreamReader(path);
            string read = "";
            string name = "";
            string value = "";
            bool comment = false;
            bool commentLine = false;
            object lastObj = null;
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
                else if(character == '#')
                {
                    comment = true;
                    if(read.Trim() == "")
                    {
                        commentLine = true;
                    }
                }
                else if(comment)
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

        }
        public void SaveFile(string path)
        {
            File.WriteAllText(path, Node.NodeToText(MainNode));
        }      
    }
    public class CommentLine
    {
        public string Text = "";
        public object Below = null;
        public CommentLine(string text, object below)
        {
            Text = text;
            Below = below;
        }
    }
    public class Node
    {
        public string Name = "";
        public string PureInnerText = "";
        public bool UseInnerText = false;
        public Node Parent = null;
        public List<Node> Nodes = new List<Node>();
        public List<Variable> Variables = new List<Variable>();
        public List<string> PureValues = new List<string>();
        public List<CommentLine> Comments = new List<CommentLine>();
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
                if (cl.Below == null)
                    text += "#" + cl.Text + "\n";
            }

            if (n.PureValues.Any())
            {
                int count = 0;
                foreach(string s in n.PureValues)
                {                    
                    count++;
                    if(count == 10)
                    {
                        count = 0;
                        text += s + "\n";
                    }
                    else
                    {
                        text += s + " ";
                    }
                }
            }
            else
            {
                foreach (Variable v in n.Variables)
                {
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
                foreach (Node inner in n.Nodes)
                {
                    text += inner.Name + " = {";
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
                    text += "}\n";
                    foreach (CommentLine cl in n.Comments)
                    {
                        if (cl.Below == inner)
                            text += "#" + cl.Text + "\n";
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
                    Variables.Add(new Variable(name, value));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
    public class Variable
    {
        public string Name = "";
        public string Value = "";
        public string Comment = "";
        public bool StringVariable = false;
        public Variable(string name, string value, bool str = false)
        {
            Name = name;
            Value = value;
            StringVariable = str;
        }
    }
}
