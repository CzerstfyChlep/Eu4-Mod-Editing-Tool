using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadIdeas(LoadingProgress progress, List<NodeFile> ideafiles)
        {
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.ideas] != 0)
                {
                    NodeFile[] files = ReadFiles(GlobalVariables.pathtomod + "common\\ideas\\", progress);

                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\ideas\\"))
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\ideas\\"}' doesn't exist!");
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\ideas\\"))
                        {
                            if (file.Contains('.'))
                            {
                                if (file.Split('.')[1] == "txt")
                                {
                                    NodeFile nf = new NodeFile(file);
                                    if (nf.LastStatus.HasError)
                                        progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                    else
                                    {
                                        ideafiles.Add(nf);
                                        GlobalVariables.ModIdeaFiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.ideas] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\ideas\\"))
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\ideas\\"}' doesn't exist!");
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\ideas\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.')[1] == "txt")
                            {
                                NodeFile nf = new NodeFile(file, true);
                                if (nf.LastStatus.HasError)
                                    progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                else
                                {
                                    if (!ideafiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                        ideafiles.Add(nf);
                                    GlobalVariables.GameIdeaFiles.Add(nf);
                                }
                            }
                        }
                    }
                }

                foreach (NodeFile nf in ideafiles)
                {
                    foreach (Node n in nf.MainNode.Nodes)
                    {
                        IdeaSet set;

                        if (n.Variables.Any(x => x.Name.ToLower() == "category"))
                        {
                            set = new BasicIdeas();
                            (set as BasicIdeas).Type = n.GetVariableValue("category");
                            IdeaSet lookset = GlobalVariables.IdeaGroups.Find(x => x.setName == n.Name);
                            if (lookset != null)
                                GlobalVariables.IdeaGroups.Remove(lookset);
                            GlobalVariables.IdeaGroups.Add(set);
                        }
                        else
                        {
                            set = new NationalIdeas();

                            IdeaSet lookset = GlobalVariables.NationalIdeas.Find(x => x.setName == n.Name);
                            if (lookset != null)
                                GlobalVariables.NationalIdeas.Remove(lookset);
                            GlobalVariables.NationalIdeas.Add(set);

                            foreach (Variable v in n.Nodes.Find(x => x.Name == "start")?.Variables)
                            {
                                (set as NationalIdeas).traditionModifiers.Add(new Modifier(v.Name, v.Value));
                            }
                        }
                        set.Trigger = TriggerConnector.GetTriggerConnectorFromNode(n.Nodes.Find(x => x.Name == "trigger"), Scope.Country);
                        set.Trigger.FileName = nf.FileName;
                        set.ParentFile = nf;
                        if (n.TryGetNode("ai_will_do", out Node aiwilldo))
                        {
                            set.AiWillDo = aiwilldo;
                        }
                        set.setName = n.Name;
                        foreach (Variable v in n.Nodes.Find(x => x.Name == "bonus")?.Variables)
                        {
                            set.ambitionModifiers.Add(new Modifier(v.Name, v.Value));
                        }
                        int N = 0;
                        foreach (Node innernode in n.Nodes)
                        {

                            if (innernode.Name == "ai_will_do" || innernode.Name == "trigger" || innernode.Name == "bonus" || innernode.Name == "start")
                                continue;
                            if (N == 7)
                                break;
                            set.ideaModifiers[N] = new List<Modifier>();
                            set.ideaNames[N] = innernode.Name;
                            foreach (Variable vr in innernode.Variables)
                            {
                                set.ideaModifiers[N].Add(new Modifier(vr.Name, vr.Value));
                            }
                            N++;
                        }
                    }
                }
            }
            catch
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with ideas! Program will exit after continuing!");
                throw new Exception();
            }
        }
    }
}
