using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadDefault(LoadingProgress progress)
        {
            try
            {
                NodeFile defaultmap;
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.mapdefault] > 0)
                    defaultmap = new NodeFile(GlobalVariables.pathtomod + "map\\default.map");
                else
                    defaultmap = new NodeFile(GlobalVariables.pathtogame + "map\\default.map");

                if (defaultmap.LastStatus.HasError)
                    progress.ReportError($"Critical error: File '{defaultmap.Path}' has an error in line {defaultmap.LastStatus.LineError}");
                else
                {

                    Variable width = defaultmap.MainNode.Variables.Find(x => x.Name.ToLower() == "width");
                    Variable height = defaultmap.MainNode.Variables.Find(x => x.Name.ToLower() == "height");

                    if (width == null || height == null)
                    {
                        progress.ReportError($"Error: No width or heigh specified in default.map!");
                    }
                    else
                    {

                        if (!int.TryParse(width.Value, out GlobalVariables.MapWidth) || !int.TryParse(height.Value, out GlobalVariables.MapHeight))
                        {
                            progress.ReportError($"Error: Invalid values given for width or height in default.map!");
                        }
                    }

                    Node seastarts = defaultmap.MainNode.Nodes.Find(x => x.Name.ToLower() == "sea_starts");
                    if (seastarts == null)
                    {
                        progress.ReportError($"Alert: No sea_starts in default.map.");
                    }
                    else
                    {
                        foreach (string sea in seastarts.GetPureValuesAsArray())
                        {
                            if (int.TryParse(sea, out int id))
                            {
                                Province f = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (f == null)
                                    progress.ReportError($"Error: In default.map, sea_starts has invalid province ID!");
                                else
                                    f.Sea = true;
                            }
                            else
                            {
                                progress.ReportError($"Error: In default.map, sea_starts has unexpected value!");
                            }
                        }
                    }

                    foreach (string lake in defaultmap.MainNode.Nodes.Find(x => x.Name.ToLower() == "lakes").GetPureValuesAsArray())
                    {
                        if (int.TryParse(lake, out int id))
                        {
                            Province f = GlobalVariables.Provinces.Find(x => x.ID == id);
                            if (f == null)
                                progress.ReportError($"Error: In default.map, lakes has invalid province ID!");
                            else
                                f.Lake = true;
                        }
                        else
                        {
                            progress.ReportError($"Error: In default.map, lakes has unexpected value!");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Issue with default.map! Program will exit after continuing!");
                progress.ReportError(e.ToString());
                throw new Exception();
            }
        }
    }
}
