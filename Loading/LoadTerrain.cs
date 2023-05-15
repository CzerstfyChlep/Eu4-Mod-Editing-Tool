using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor.Loading
{
    public static partial class LoadFilesClass
    {
        public static void LoadTerrain(LoadingProgress progress, NodeFile terrainFile)
        {
            //LOAD FILE
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.terrainTXT] > 0)
                    terrainFile = new NodeFile(GlobalVariables.pathtomod + "map\\terrain.txt");
                else
                    terrainFile = new NodeFile(GlobalVariables.pathtogame + "map\\terrain.txt");

                if (terrainFile.LastStatus.HasError)
                    progress.ReportError($"Critical error: File '{terrainFile.Path}' has an error in line {terrainFile.LastStatus.LineError}");
                else
                {
                    Node categories;
                    if (terrainFile.MainNode.TryGetNode("categories", out categories))
                    {
                        progress.ReportError($"Alert: No terrain types found!");
                    }
                    else
                    {
                        foreach (Node node in categories.Nodes)
                        {
                            TerrainType type = new TerrainType();
                            type.ReadFromNode(in node);
                            GlobalVariables.TerrainTypes.Add(type);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with terrains! Program will exit after continuing!");
                progress.ReportError(e.ToString());
            }

            //LOAD TERRAIN MAP
            try
            {
                Bitmap copiedBitmap = new Bitmap(GlobalVariables.ProvincesMapBitmap);
                LockBitmap bitmap = new LockBitmap(copiedBitmap);
                bitmap.LockBits();

                int heightInterval = bitmap.Height / 10;
                int heightValue = 0;
                int va = 10;

                for (int y = 1; y < bitmap.Height; y++)
                {
                    for (int x = 1; x < bitmap.Width; x += 2)
                    {
                        Color c = bitmap.GetPixel(x, y);
                        if (c != Color.FromArgb(1, 255, 255, 255))
                        {
                            Province p = GlobalVariables.CubeArray[c.R, c.G, c.B];
                            if (p != null)
                            {
                                p.Pixel = new Point(x, y);
                                GraphicsMethods.FloodFill(ref bitmap, new Point(x, y), c, Color.FromArgb(1, 255, 255, 255), ref p.Pixels);
                            }
                        }
                    }
                    heightValue++;
                    if (heightInterval == heightValue)
                    {
                        heightValue = 0;
                        va += 1;
                    }
                }

                bitmap.UnlockBits();
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Issue with the map! Program will exit after continuing!");
                progress.ReportError(e.ToString());
            }
        }
    }
}
