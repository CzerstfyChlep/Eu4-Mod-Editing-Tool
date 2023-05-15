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
        public static void LoadProvinces(LoadingProgress progress)
        {
            try
            {
                List<NodeFile> Files = new List<NodeFile>();
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.historyProvinces] != 0)
                {
                    if (!Directory.Exists(GlobalVariables.pathtomod + "history\\provinces\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "history\\provinces\\"}' not found!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "history\\provinces\\"))
                        {
                            if (file.Contains('.'))
                            {
                                if (file.Split('.').Last() == "txt")
                                {
                                    try
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            Files.Add(nf);
                                        }
                                    }
                                    catch
                                    {
                                        progress.ReportError($"File '{file}' is unusable!");
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.historyProvinces] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "history\\provinces\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "history\\provinces\\"}' not found!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "history\\provinces\\"))
                        {
                            if (file.Contains('.'))
                            {
                                if (file.Split('.').Last() == "txt")
                                {
                                    if (!Files.Any(x => x.Path.Split('\\').Last() == file.Split('\\').Last()))
                                    {
                                        try
                                        {
                                            NodeFile nf = new NodeFile(file, true);
                                            if (nf.LastStatus.HasError)
                                                progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                            else
                                                Files.Add(nf);
                                        }
                                        catch
                                        {
                                            progress.ReportError($"File '{file}' is unusable!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (NodeFile file in Files)
                {
                    string tocheck = file.Path.Split('\\').Last();
                    string created = "";

                    for (int a = 0; a < tocheck.Length; a++)
                    {
                        if (char.IsDigit(tocheck[a]))
                            created += tocheck[a];
                        else
                            break;
                    }
                    bool b = int.TryParse(created, out int id);
                    if (b)
                    {
                        Province province = GlobalVariables.Provinces.Find(x => x.ID == id);
                        if (province == null)
                        {
                            progress.ReportError($"Alert: File '{file.FileName}' couldn't be loaded because it's not in definition.csv!");
                            continue;
                        }

                        province.HistoryFile = file;
                        NodeFile nodefile = file;
                        if (nodefile.MainNode.Variables.Any())
                            GlobalVariables.TotalUsableProvinces++;

                        //CHECK
                        ReadProvinceValuesFromNode(province, GlobalVariables.CurrentDate, nodefile.MainNode, progress);


                        //TODO
                        //RESTRUCTURE DATE NODES HERE !!!!

                        foreach (Node dateNode in nodefile.MainNode.Nodes)
                        {
                            if (dateNode.Name.Contains("."))
                            {
                                try
                                {
                                    if (dateNode.Name.Where(x => x == '.').Count() >= 2)
                                    {

                                        int y = 0;
                                        int m = 0;
                                        int d = 0;

                                        if (!int.TryParse(dateNode.Name.Split('.')[0], out y))
                                            progress.ReportError($"Error: Date entry '{dateNode.Name.Split('.')[0]}.{dateNode.Name.Split('.')[1]}.{dateNode.Name.Split('.')[2]}' in province '{province.ID}' is incorrect!");
                                        else if (!int.TryParse(dateNode.Name.Split('.')[1], out m))
                                            progress.ReportError($"Error: Date entry '{dateNode.Name.Split('.')[0]}.{dateNode.Name.Split('.')[1]}.{dateNode.Name.Split('.')[2]}' in province '{province.ID}' is incorrect!");
                                        else if (!int.TryParse(dateNode.Name.Split('.')[2], out d))
                                            progress.ReportError($"Error: Date entry '{dateNode.Name.Split('.')[0]}.{dateNode.Name.Split('.')[1]}.{dateNode.Name.Split('.')[2]}' in province '{province.ID}' is incorrect!");
                                        else
                                        {
                                            DateTime date = new DateTime(y, m, d);
                                            ReadProvinceValuesFromNode(province, date, dateNode, progress);
                                        }
                                    }
                                }
                                catch
                                {
                                    progress.ReportError($"Alert: Date entry '{dateNode.Name}' in province {id} is incorrect! Ignoring.");
                                }
                            }
                        }


                        if (province.TradeGood != null)
                        {
                            province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                        }
                        if (province.LatentTradeGood != null)
                        {
                            province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                        }


                    }
                }

                if (GlobalVariables.pathtomod != "")
                {
                    foreach (Province p in GlobalVariables.Provinces)
                    {
                        if (p.HistoryFile == null)
                        {
                            string s = GlobalVariables.pathtomod + "history\\provinces\\" + (p.ID) + ".txt";
                            NodeFile nf = new NodeFile(s, dontread: true);
                            p.HistoryFile = nf;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with province history! Program will exit after continuing!");
                progress.ReportError(e.ToString());
                throw new Exception();
            }
        }
    }
}
