using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static class MapManagement
    {
        public enum UpdateMapOptions { Provinces, Development, TradeGood, Culture, Religion, Political, Area, Region, TradeNode, HRE, Fort, Continent };

        public static void UpdateMap(List<Province> provinces, UpdateMapOptions options)
        {
            switch (options)
            {
                case UpdateMapOptions.Development:
                    GlobalVariables.DevelopmentBitmapLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        int totaldev = p.Tax + p.Manpower + p.Production;
                        Color c;
                        Color bordercolor = Color.Black;
                        if (p.Lake || p.Sea)
                            c = Color.Black;
                        else
                        {
                            if (totaldev > 25)
                                c = Color.Lime;
                            else if (totaldev == 0)
                                c = Color.Blue;
                            else if (totaldev == 3)
                            {
                                c = Color.Red;
                            }
                            else
                            {
                                //c = GetGradient(Color.Red, Color.Lime, totaldev / 30);
                                c = Color.FromArgb((int)(255 * (1 - totaldev / 25f)), (int)(30 + 225 * (totaldev / 25f)), 0);
                                //borderc = Color.FromArgb((int)(c.R * 0.66), (int)(c.G * 0.66), (int)(c.B * 0.66));                     
                            }

                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.DevelopmentBitmapLocked.SetPixel(pon.X, pon.Y, c);
                        }

                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.DevelopmentBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.DevelopmentBitmapLocked.UnlockBits();
                    break;
                case UpdateMapOptions.Religion:
                    GlobalVariables.ReligionBitmapLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.Black;
                        Color stripec = Color.White;
                        bool stripes = false;
                        if (p.Religion != null)
                            c = p.Religion.Color;
                        Color bordercolor = Color.Black;
                        if (p.OwnerCountry != null)
                        {
                            if (p.OwnerCountry.Religion != p.Religion)
                            {
                                if (p.OwnerCountry.Religion != null)
                                    stripec = p.OwnerCountry.Religion.Color;
                                else
                                    stripec = Color.White;
                                stripes = true;
                            }
                        }

                        if (p.Lake || p.Sea)
                            c = Color.Black;
                        foreach (Point pon in p.Pixels)
                        {
                            if (!stripes)
                            {
                                GlobalVariables.ReligionBitmapLocked.SetPixel(pon.X, pon.Y, c);
                            }
                            else
                            {
                                if ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3)
                                {
                                    GlobalVariables.ReligionBitmapLocked.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.ReligionBitmapLocked.SetPixel(pon.X, pon.Y, c);
                                }
                            }
                        }

                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.ReligionBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.ReligionBitmapLocked.UnlockBits();
                    break;
                case UpdateMapOptions.Culture:

                    GlobalVariables.CultureBitmapLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        if (p.Culture != null)
                        {
                            c = p.Culture.Color;
                        }
                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.CultureBitmapLocked.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.CultureBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.CultureBitmapLocked.UnlockBits();
                    break;
                case UpdateMapOptions.Political:
                    GlobalVariables.PoliticalBitmapLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        Color stripec = Color.White;
                        bool verticalstripes = false;
                        if (p.OwnerCountry != null)
                        {
                            c = p.OwnerCountry.Color;
                            if (p.OwnerCountry.Capital == p)
                            {
                                verticalstripes = true;
                                stripec = AdditionalElements.DimColor(p.OwnerCountry.Color);
                            }
                        }
                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }

                        foreach (Point pon in p.Pixels)
                        {
                            if (!verticalstripes)
                                GlobalVariables.PoliticalBitmapLocked.SetPixel(pon.X, pon.Y, c);
                            else
                            {
                                if (pon.X % 6 == 0 || pon.X % 6 == 1)
                                {
                                    GlobalVariables.PoliticalBitmapLocked.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.PoliticalBitmapLocked.SetPixel(pon.X, pon.Y, c);
                                }
                            }
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.PoliticalBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.PoliticalBitmapLocked.UnlockBits();
                    break;

                case UpdateMapOptions.TradeGood:
                    GlobalVariables.TradeGoodBitmapLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        Color stripec = Color.White;
                        bool stripes = false;
                        if (p.TradeGood != null)
                            c = p.TradeGood.Color;
                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.PapayaWhip;
                        }
                        if (p.LatentTradeGood != null)
                        {
                            stripec = p.LatentTradeGood.Color;
                            stripes = true;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            if (!stripes)
                            {
                                GlobalVariables.TradeGoodBitmapLocked.SetPixel(pon.X, pon.Y, c);
                            }
                            else
                            {
                                if ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3)
                                {
                                    GlobalVariables.TradeGoodBitmapLocked.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.TradeGoodBitmapLocked.SetPixel(pon.X, pon.Y, c);
                                }
                            }

                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.TradeGoodBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.TradeGoodBitmapLocked.UnlockBits();
                    break;

                case UpdateMapOptions.Area:
                    GlobalVariables.AreaBitmapLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        if (p.Area != null)
                        {
                            c = p.Area.Color;
                        }
                        Color borderc = Color.Black;
                        if ((p.Lake || p.Sea) && !GlobalVariables.ShowSeaTilesAreaMapmode)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.AreaBitmapLocked.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.AreaBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.AreaBitmapLocked.UnlockBits();
                    break;
                case UpdateMapOptions.Region:
                    GlobalVariables.RegionBitmapLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        if (p.Area != null)
                        {
                            if(p.Area.Region != null) 
                                c = p.Area.Region.Color;
                        }
                        Color borderc = Color.Black;
                        if ((p.Lake || p.Sea) && !GlobalVariables.ShowSeaTilesAreaMapmode)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.RegionBitmapLocked.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.RegionBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.RegionBitmapLocked.UnlockBits();
                    break;
                case UpdateMapOptions.TradeNode:
                    GlobalVariables.TradeNodeBitmap.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        Color stripec = Color.Gray;
                        Color stripevc = Color.White;
                        bool verticalstripes = false;
                        bool stripes = false;
                        if (p.TradeNode != null)
                        {
                            c = p.TradeNode.Color;
                            if (p.TradeNode.Location == p)
                            {
                                verticalstripes = true;
                                stripevc = AdditionalElements.DimColor(p.TradeNode.Color, 40);
                            }
                        }
                        if (p.CenterOfTrade > 0)
                            stripes = true;


                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            if (!verticalstripes && !stripes)
                                GlobalVariables.TradeNodeBitmap.SetPixel(pon.X, pon.Y, c);
                            else
                            {

                                if (verticalstripes && (pon.X % 6 == 0 || pon.X % 6 == 1))
                                    GlobalVariables.TradeNodeBitmap.SetPixel(pon.X, pon.Y, stripevc);
                                else if (stripes && ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3))                                
                                    GlobalVariables.TradeNodeBitmap.SetPixel(pon.X, pon.Y, stripec);                                              
                                else                                
                                    GlobalVariables.TradeNodeBitmap.SetPixel(pon.X, pon.Y, c);
                                
                            }
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.TradeNodeBitmap.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }

                    }


                    GlobalVariables.TradeNodeBitmap.UnlockBits();
                    break;


                case UpdateMapOptions.HRE:
                    GlobalVariables.HREBitmap.LockBits();
                    foreach (Province p in provinces)
                    {
                        Color c = Color.Green;
                        Color stripec = Color.Green;
                        bool stripes = false;

                        bool OwnerCountryInHRE = false;
                        if (p.OwnerCountry != null)
                            if (p.OwnerCountry.Capital != null)
                                OwnerCountryInHRE = p.OwnerCountry.Capital.HRE;
                        if (p.HRE && OwnerCountryInHRE)
                        {
                            c = Color.Green;
                        }
                        else if (p.HRE && !OwnerCountryInHRE)
                        {
                            stripes = true;
                            c = Color.White;
                        }
                        else
                            c = Color.White;
                                                            
                        Color bordercolor = Color.Black;
                        

                        if (p.Lake || p.Sea)
                            c = Color.Black;
                        foreach (Point pon in p.Pixels)
                        {
                            if (!stripes)
                            {
                                GlobalVariables.HREBitmap.SetPixel(pon.X, pon.Y, c);
                            }
                            else
                            {
                                if ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3)
                                {
                                    GlobalVariables.HREBitmap.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.HREBitmap.SetPixel(pon.X, pon.Y, c);
                                }
                            }
                        }

                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.HREBitmap.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.HREBitmap.UnlockBits();
                    break;
                case UpdateMapOptions.Fort:
                    GlobalVariables.FortBitmap.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        if (p.Fort)
                        {
                            c = Color.Green;
                        }
                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.FortBitmap.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.FortBitmap.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.FortBitmap.UnlockBits();
                    break;

                case UpdateMapOptions.Continent:
                    GlobalVariables.ContinentBitmap.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        if (p.Continent != null)
                            c = p.Continent.Color;
                           
                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.ContinentBitmap.SetPixel(pon.X, pon.Y, c);                            
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.ContinentBitmap.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }

                    }
                    GlobalVariables.ContinentBitmap.UnlockBits();
                    break;

            }
        }

        public static void UpdateMap(Province p, UpdateMapOptions options)
        {
            UpdateMap(new List<Province> { p }, options);
        
            /*



            Color bordercolor = Color.Black;
            Color c = Color.Black;
            Color stripec = Color.White;
            bool stripes = false;
            bool verticalstripes = false;
            switch (options)
            {
                case UpdateMapOptions.Religion:
                    if (p != null)
                    {
                        GlobalVariables.ReligionBitmapLocked.LockBits();
                        if (p.Religion != null)
                            c = p.Religion.Color;
                        if (p.Lake || p.Sea)
                            c = Color.Black;
                        if (p.OwnerCountry != null)
                        {
                            if (p.OwnerCountry.Religion != p.Religion)
                            {
                                if (p.OwnerCountry.Religion != null)
                                    stripec = p.OwnerCountry.Religion.Color;
                                else
                                    stripec = Color.White;
                                stripes = true;
                            }
                        }


                        foreach (Point pon in p.Pixels)
                        {
                            if (!stripes)
                            {
                                GlobalVariables.ReligionBitmapLocked.SetPixel(pon.X, pon.Y, c);
                            }
                            else
                            {
                                if ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3)
                                {
                                    GlobalVariables.ReligionBitmapLocked.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.ReligionBitmapLocked.SetPixel(pon.X, pon.Y, c);
                                }
                            }
                        }

                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.ReligionBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }

                        GlobalVariables.ReligionBitmapLocked.UnlockBits();
                    }
                    break;
                case UpdateMapOptions.Culture:
                    if (p != null)
                    {
                        GlobalVariables.CultureBitmapLocked.LockBits();
                        if (p.Culture != null)
                            c = p.Culture.Color;
                        if (p.Lake || p.Sea)
                            c = Color.Black;
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.CultureBitmapLocked.SetPixel(pon.X, pon.Y, c);
                        }

                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.CultureBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }

                        GlobalVariables.CultureBitmapLocked.UnlockBits();
                    }
                    break;
                case UpdateMapOptions.Development:
                    GlobalVariables.DevelopmentBitmapLocked.LockBits();
                    int totaldev = p.Tax + p.Manpower + p.Production;
                    if (p.Lake || p.Sea)
                        c = Color.Black;
                    else
                    {
                        if (totaldev > 25)
                            c = Color.Lime;
                        else if (totaldev == 0)
                            c = Color.Blue;
                        else if (totaldev == 3)
                        {
                            c = Color.Red;
                        }
                        else
                        {
                            //c = GetGradient(Color.Red, Color.Lime, totaldev / 30);
                            c = Color.FromArgb((int)(255 * (1 - totaldev / 25f)), (int)(30 + 225 * (totaldev / 25f)), 0);
                            //borderc = Color.FromArgb((int)(c.R * 0.66), (int)(c.G * 0.66), (int)(c.B * 0.66));                     
                        }

                    }
                    foreach (Point pon in p.Pixels)
                    {
                        GlobalVariables.DevelopmentBitmapLocked.SetPixel(pon.X, pon.Y, c);
                    }

                    foreach (Point borderpnt in p.BorderPixels)
                    {
                        GlobalVariables.DevelopmentBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                    }

                    GlobalVariables.DevelopmentBitmapLocked.UnlockBits();
                    break;
                case UpdateMapOptions.TradeGood:
                    if (p != null)
                    {
                        GlobalVariables.TradeGoodBitmapLocked.LockBits();
                        c = Color.White;
                        stripec = Color.White;
                        stripes = false;
                        if (p.TradeGood != null)
                            c = p.TradeGood.Color;
                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.PapayaWhip;
                        }
                        if (p.LatentTradeGood != null)
                        {
                            stripec = p.LatentTradeGood.Color;
                            stripes = true;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            if (!stripes)
                            {
                                GlobalVariables.TradeGoodBitmapLocked.SetPixel(pon.X, pon.Y, c);
                            }
                            else
                            {
                                if ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3)
                                {
                                    GlobalVariables.TradeGoodBitmapLocked.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.TradeGoodBitmapLocked.SetPixel(pon.X, pon.Y, c);
                                }
                            }

                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.TradeGoodBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                        GlobalVariables.TradeGoodBitmapLocked.UnlockBits();
                    }
                    break;
                case UpdateMapOptions.Political: 
                    if (p != null)
                    {
                        GlobalVariables.PoliticalBitmapLocked.LockBits();
                        c = Color.White;
                        if (p.OwnerCountry != null)
                        {
                            c = p.OwnerCountry.Color;
                            if(p.OwnerCountry.Capital == p)
                            {
                                verticalstripes = true;
                                stripec = AdditionalElements.DimColor(p.OwnerCountry.Color);
                            }
                        }
                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }

                        foreach (Point pon in p.Pixels)
                        {
                            if(!verticalstripes)
                                GlobalVariables.PoliticalBitmapLocked.SetPixel(pon.X, pon.Y, c);
                            else
                            {
                                if (pon.X % 6 ==  0 || pon.X % 6 == 1)
                                {
                                    GlobalVariables.PoliticalBitmapLocked.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.PoliticalBitmapLocked.SetPixel(pon.X, pon.Y, c);
                                }
                            }
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.PoliticalBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                        GlobalVariables.PoliticalBitmapLocked.UnlockBits();
                    }
                    break;
                case UpdateMapOptions.Area:
                    if (p != null)
                    {
                        GlobalVariables.AreaBitmapLocked.LockBits();
                        c = Color.White;
                        if (p.Area != null)
                        {
                            c = p.Area.Color;
                        }
                        if ((p.Lake || p.Sea) && !GlobalVariables.ShowSeaTilesAreaMapmode)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        Color borderc = Color.Black;                       
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.AreaBitmapLocked.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.AreaBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                        GlobalVariables.AreaBitmapLocked.UnlockBits();
                    }
                    break;
                case UpdateMapOptions.Region:
                    if (p != null)
                    {
                        GlobalVariables.RegionBitmapLocked.LockBits();
                        c = Color.White;
                        if (p.Area != null)
                        {
                            if(p.Area.Region != null)
                                c = p.Area.Region.Color;
                        }
                        if ((p.Lake || p.Sea) && !GlobalVariables.ShowSeaTilesAreaMapmode)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        Color borderc = Color.Black;
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.RegionBitmapLocked.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.RegionBitmapLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                        GlobalVariables.RegionBitmapLocked.UnlockBits();
                    }
                    break;
                case UpdateMapOptions.TradeNode:
                    if (p != null)
                    {
                        GlobalVariables.TradeNodeBitmap.LockBits();
                        c = Color.White;
                        if (p.TradeNode != null)
                        {
                            c = p.TradeNode.Color;
                            if(p.TradeNode.Location == p)
                            {
                                verticalstripes = true;
                                stripec = AdditionalElements.DimColor(p.TradeNode.Color);
                            }
                        }

                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            if(!verticalstripes)
                                GlobalVariables.TradeNodeBitmap.SetPixel(pon.X, pon.Y, c);
                            else
                            {
                                if (pon.X % 6 == 0 || pon.X % 6 == 1)
                                {
                                    GlobalVariables.TradeNodeBitmap.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.TradeNodeBitmap.SetPixel(pon.X, pon.Y, c);
                                }
                            }
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.TradeNodeBitmap.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }

                        GlobalVariables.TradeNodeBitmap.UnlockBits();
                    }
                    break;
            }
            */
        }

        public static void UpdateClickedMap(List<Province> provinces, Color c, bool add = true)
        {
            GlobalVariables.ClickedMask.LockBits();
            if (add)
            {
                foreach(Province p in provinces)
                {
                    foreach(Point point in p.BorderPixels)
                    {
                        GlobalVariables.ClickedMask.SetPixel(point.X, point.Y, c);
                    }
                }
            }
            else
            {
                foreach (Province p in provinces)
                {
                    foreach (Point point in p.BorderPixels)
                    {
                        GlobalVariables.ClickedMask.SetPixel(point.X, point.Y, Color.Transparent);
                    }
                }
            }
            GlobalVariables.ClickedMask.UnlockBits();
        }

        public static void CreateClickMask()
        {
            GlobalVariables.ClickedMask.LockBits();
            for (int y = 0; y < GlobalVariables.ClickedMask.Height; y++)
            {
                for (int x = 0; x < GlobalVariables.ClickedMask.Width; x++)
                {
                    GlobalVariables.ClickedMask.SetPixel(x, y, Color.Transparent);
                }
            }
            GlobalVariables.ClickedMask.UnlockBits();
        }

        public static void ReloadProvince(Province p)
        {
            UpdateMap(p, UpdateMapOptions.Area);
            UpdateMap(p, UpdateMapOptions.Continent);
            UpdateMap(p, UpdateMapOptions.Culture);
            UpdateMap(p, UpdateMapOptions.Development);
            UpdateMap(p, UpdateMapOptions.Fort);
            UpdateMap(p, UpdateMapOptions.HRE);
            UpdateMap(p, UpdateMapOptions.Political);
            UpdateMap(p, UpdateMapOptions.Provinces);
            UpdateMap(p, UpdateMapOptions.Region);
            UpdateMap(p, UpdateMapOptions.Religion);
            UpdateMap(p, UpdateMapOptions.TradeGood);
            UpdateMap(p, UpdateMapOptions.TradeNode);
        }
    }
}
