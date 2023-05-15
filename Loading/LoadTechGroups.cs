using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadTechGroups(LoadingProgress progress, NodeFile technology)
        {
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.technology] > 0)
                    technology = new NodeFile(GlobalVariables.pathtomod + "common\\technology.txt");
                else
                    technology = new NodeFile(GlobalVariables.pathtogame + "common\\technology.txt");

                if (technology.LastStatus.HasError)
                    progress.ReportError($"Critical error: File '{technology.Path}' has an error in line {technology.LastStatus.LineError}");
                else
                {
                    Node groups = technology.MainNode.Nodes.Find(x => x.Name.ToLower() == "groups");
                    if (groups == null)
                    {
                        progress.ReportError($"Alert: No technology groups found!");
                    }
                    else
                    {
                        foreach (Node node in groups.Nodes)
                        {
                            GlobalVariables.TechGroups.Add(node.Name);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with techgroups! Program will exit after continuing!");
                progress.ReportError(e.ToString());
            }
        }
    }
}
