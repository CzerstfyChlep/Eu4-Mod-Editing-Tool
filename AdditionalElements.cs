using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static class AdditionalElements
    {
        public static T RandomChoice<T>(T[] input, int[] chances, Random r)
        {
            if (input.Length != chances.Length)
                throw new Exception("RandomChoice: Input array and Chances array have different ammount of elements!");


            if (input.Length == 0)
                throw new Exception("RandomChoice: Arrays can't be empty!");

            int sumchances = 0;
            int n = 0;
            int[] trsh = new int[chances.Length];
            int subch = 0;
            foreach (int ch in chances)
            {
                if (ch >= 0)
                    subch = ch;
                else
                    subch = 0;
                sumchances += subch;
                if (n == 0)
                    trsh[0] = subch;
                else
                    trsh[n] = subch + trsh[n - 1];
                n++;
            }
            int rand = r.Next(0, sumchances);
            if (sumchances > 0)
            {
                for (int a = 0; a < n; a++)
                {
                    if (rand < trsh[a] && chances[a] > 0)
                        return input[a];
                }
            }
            return input[n - 1];
        }
        public static Color GenerateColor(Random r)
        {
            return Color.FromArgb(r.Next(10, 240), r.Next(10, 240), r.Next(10, 240));
        }

        public static Color DimColor(Color c, int Value = 20)
        {
            int R = c.R - Value;
            int G = c.G - Value;
            int B = c.B - Value;
            if (R < 0)
                R = 0;
            if (G < 0)
                G = 0;
            if (B < 0)
                B = 0;
            return Color.FromArgb(R, G, B);
        }

        
    }
}
