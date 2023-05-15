using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadClimate(LoadingProgress progress, NodeFile climate)
        {
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.climate] > 0)
                    climate = new NodeFile(GlobalVariables.pathtomod + "map\\climate.txt");
                else
                    climate = new NodeFile(GlobalVariables.pathtogame + "map\\climate.txt");

                if (climate.LastStatus.HasError)
                    progress.ReportError($"Critical error: File '{climate.Path}' has an error in line {climate.LastStatus.LineError}");
                else
                {
                    Node tropical = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "tropical");
                    if (tropical != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in tropical.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: tropical in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: tropical in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Climate = 1;
                                }
                            }
                        }
                    }
                    Node arid = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "arid");
                    if (arid != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in arid.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: arid in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: arid in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Climate = 2;
                                }
                            }
                        }
                    }
                    Node arctic = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "arctic");
                    if (arctic != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in arctic.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: arctic in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: arctic in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Climate = 3;
                                }
                            }
                        }
                    }


                    Node mild_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "mild_winter");
                    if (mild_winter != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in mild_winter.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: mild_winter in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: mild_winter in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Winter = 1;
                                }
                            }
                        }

                    }
                    Node normal_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "normal_winter");
                    if (normal_winter != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in normal_winter.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: normal_winter in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: normal_winter in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Winter = 2;
                                }
                            }
                        }
                    }
                    Node severe_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "severe_winter");
                    if (severe_winter != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in severe_winter.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: severe_winter in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: severe_winter in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Winter = 3;
                                }
                            }
                        }
                    }



                    Node impassable = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "impassable");
                    if (impassable != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in impassable.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: impassable in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: impassable in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Impassable = 1;
                                }
                            }
                        }
                    }



                    Node mild_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "mild_monsoon");
                    if (mild_monsoon != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in mild_monsoon.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: mild_monsoon in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: mild_monsoon in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Monsoon = 1;
                                }
                            }
                        }
                    }
                    Node normal_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "normal_monsoon");
                    if (normal_monsoon != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in normal_monsoon.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: normal_monsoon in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: normal_monsoon in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Monsoon = 2;
                                }
                            }
                        }
                    }
                    Node severe_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "severe_monsoon");
                    if (severe_monsoon != null)
                    {
                        int id = 0;
                        Province p = null;
                        foreach (PureValue pv in severe_monsoon.PureValues)
                        {
                            if (!int.TryParse(pv.Name, out id))
                            {
                                progress.ReportError($"Error: severe_monsoon in climate.txt has unexpected value '{id}'!");
                            }
                            else
                            {
                                p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
                                {
                                    progress.ReportError($"Error: severe_monsoon in climate.txt has invalid province ID '{id}'!");
                                }
                                else
                                {
                                    p.Monsoon = 3;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with climate! Program will exit after continuing!");
                progress.ReportError(e.ToString());
            }
        }
    }
}
