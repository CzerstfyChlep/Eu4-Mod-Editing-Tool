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
        public static void LoadTradeGoods(LoadingProgress progress, List<NodeFile> tradegoodsfiles)
        {
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.tradegoods] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradegoods\\"))
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
                                    tradegoodsfiles.Add(nf);
                                    GlobalVariables.ModTradeGoodsFiles.Add(nf);
                                }
                            }
                        }
                    }
                }

                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.tradegoods] != 1)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\tradegoods\\"))
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
                                    if (!tradegoodsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                        tradegoodsfiles.Add(nf);
                                    GlobalVariables.GameTradeGoodsFile = nf;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with trade good files! Program will exit after continuing!");
                throw new Exception();
            }
        }
    }
}
