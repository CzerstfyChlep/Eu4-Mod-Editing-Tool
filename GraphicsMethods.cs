using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Eu4ModEditor
{
    public static class GraphicsMethods
    {
        public static void FloodFill(ref LockBitmap bmp, Point pt, Color targetColor, Color replacementColor, ref List<Point> pixelsChanged)
        {
            Stack<Point> pixels = new Stack<Point>(10000);
            targetColor = bmp.GetPixel(pt.X, pt.Y);
            pixels.Push(pt);
            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                if (a.X < bmp.Width && a.X > 0 &&
                        a.Y < bmp.Height && a.Y > 0)
                {
                    if (bmp.CmpPixel(a.X, a.Y, targetColor.R, targetColor.G, targetColor.B))
                    {
                        bmp.SetPixel(a.X, a.Y, replacementColor);
                        pixelsChanged.Add(new Point(a.X, a.Y));
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }
        }
        public static List<Point> CreateBorders(Province p)
        {
            List<Point> border = new List<Point>();
            int minx = GlobalVariables.MapWidth;
            int maxx = 0;
            int miny = GlobalVariables.MapHeight;
            int maxy = 0;
            if (!p.Pixels.Any())
                return border;
            foreach (Point pt in p.Pixels)
            {
                if (pt.X < minx)
                    minx = pt.X;
                if (pt.X > maxx)
                    maxx = pt.X;
                if (pt.Y < miny)
                    miny = pt.Y;
                if (pt.Y > maxy)
                    maxy = pt.Y;
            }
            Point[,] points = new Point[(maxy - miny) + 3, (maxx - minx) + 3];
            for (int y = 0; y < (maxy - miny) + 3; y++)
            {
                for (int x = 0; x < (maxx - minx) + 3; x++)
                {
                    points[y, x] = new Point(-1, 0);
                }
            }
            foreach (Point pt in p.Pixels)
            {
                points[(pt.Y - miny) + 1, (pt.X - minx) + 1] = pt;
            }
            for (int y = 1; y < maxy - miny + 2; y++)
            {
                for (int x = 1; x < maxx - minx + 2; x++)
                {
                    if (points[y, x].X != -1)
                    {
                        if (points[y - 1, x].X == -1 || points[y + 1, x].X == -1 || points[y, x + 1].X == -1 || points[y, x - 1].X == -1)
                        {
                            border.Add(points[y, x]);
                        }
                    }
                }
            }
            return border;
        }
        public static List<Point> CreateBordersTwo(ref LockBitmap bmp)
        {
            List<Point> border = new List<Point>(1000);
            List<Point> AlreadyAdded = new List<Point>(1000);
            for (int y = 1; y < bmp.Height - 1; y++)
            {
                for (int x = 1; x < bmp.Width - 1; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (!AlreadyAdded.Contains(new Point(x, y)))
                    {
                        if (!bmp.CmpPixel(x - 1, y, c.R, c.G, c.B))
                        {
                            border.Add(new Point(x, y));
                            border.Add(new Point(x - 1, y));
                            AlreadyAdded.Add(new Point(x - 1, y));
                            continue;
                        }
                        if (!bmp.CmpPixel(x + 1, y, c.R, c.G, c.B))
                        {
                            border.Add(new Point(x, y));
                            border.Add(new Point(x + 1, y));
                            AlreadyAdded.Add(new Point(x + 1, y));
                            continue;
                        }
                        if (!bmp.CmpPixel(x, y - 1, c.R, c.G, c.B))
                        {
                            border.Add(new Point(x, y));
                            border.Add(new Point(x, y - 1));
                            AlreadyAdded.Add(new Point(x, y - 1));
                            continue;
                        }
                        if (!bmp.CmpPixel(x, y + 1, c.R, c.G, c.B))
                        {
                            border.Add(new Point(x, y));
                            border.Add(new Point(x, y + 1));
                            AlreadyAdded.Add(new Point(x, y + 1));
                            continue;
                        }
                    }
                    else
                        AlreadyAdded.Remove(new Point(x, y));
                }
            }
            return border;
        }
    }
}
