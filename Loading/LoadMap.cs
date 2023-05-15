using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadMap(LoadingProgress progress)
        {
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
