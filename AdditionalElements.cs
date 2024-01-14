using System;
using System.Collections.Generic;
using System.Drawing;

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

        public static string CreateStringFromStringList(List<string> input, string separator = ", ")
        {
            string toReturn = "";
            for(int a = 0; a < input.Count; a++)
            {
                toReturn += input[a];
                if(a != input.Count - 1)
                    toReturn += separator;
            }
            return toReturn;
      
        }

        /*public static int StringSimilarity(string s, string t)
        {
            int splitResult = 0;
            if (s.Length > t.Length)
            {
                splitResult = (int)(stringComparer.Similarity(s.Substring(0, t.Length), t) * 100);
            }
            else
                splitResult = (int)(stringComparer.Similarity(t.Substring(0, s.Length), s) * 100);
            int normalResult = (int)(stringComparer.Similarity(s, t) * 100);


            return Math.Max(splitResult, normalResult);
        }*/

        public static int CompareStrings(string s, string t, bool LeftLength = false)
        {

            if (LeftLength)
            {
                if (s.Length < t.Length)
                    t = t.Substring(s.Length);
            }



            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m] - Math.Abs(s.Length - t.Length);
        }

    }
}
