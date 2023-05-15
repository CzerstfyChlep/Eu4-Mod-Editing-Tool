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
        public static void LoadRebels(LoadingProgress progress)
        {
            try
            {
                List<string> done = new List<string>();
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.rebeltypes] != 0)
                {
                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\rebel_types\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\rebel_types\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\rebel_types\\"))
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
                                        foreach (Node n in nf.MainNode.Nodes)
                                        {
                                            GlobalVariables.RebelTypes.Add(n.Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.rebeltypes] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\rebel_types\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\cultures\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\rebel_types\\"))
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
                                        foreach (Node n in nf.MainNode.Nodes)
                                        {
                                            GlobalVariables.RebelTypes.Add(n.Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                GlobalVariables.RebelTypes = GlobalVariables.RebelTypes.Distinct().ToList();
            }
            catch
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with rebel types! Program will exit after continuing!");
                throw new Exception();
            }
        }
    }
}
