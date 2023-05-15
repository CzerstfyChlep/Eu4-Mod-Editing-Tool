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
        public static void LoadDefinition(LoadingProgress progress)
        {
            int linen = 0;
            try
            {
                StreamReader Reader;
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.definition] > 0)
                    Reader = new StreamReader(GlobalVariables.pathtomod + "map\\definition.csv");
                else
                    Reader = new StreamReader(GlobalVariables.pathtogame + "map\\definition.csv");

                while (!Reader.EndOfStream)
                {
                    string data = Reader.ReadLine();
                    linen++;
                    if (data.Contains("province;red;"))
                        continue;
                    string[] values = data.Split(';');
                    if (values.Count() < 5)
                    {
                        progress.ReportError($"Error: Incorrect line number {linen} in definition.csv");
                        continue;
                    }
                    Province p = new Province(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), values[4]);
                    GlobalVariables.CubeArray[int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3])] = p;
                    GlobalVariables.Provinces.Add(p);
                }
            }
            catch
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected error with definition.csv! Program will exit after continuing!");
            }
        }
    }
}
