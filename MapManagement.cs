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
        public enum UpdateMapOptions { Provinces, Development, TradeGood, Culture, Religion, Political, Area, Region, TradeNode,
            HRE, Fort, Continent, Superregion, DiscoveredBy, TradeCompany, Government, Localisation, Climate, Winter, Terrain };

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
                        if (p.Lake || p.Sea)
                            c = Color.Black;
                        else
                        {
                            if (totaldev > 25)
                                c = Color.Lime;
                            else if (totaldev == 0)
                                c = Color.Blue;
                            else if (totaldev == 3)                            
                                c = Color.Red;                           
                            else                            
                                c = Color.FromArgb((int)(255 * (1 - totaldev / 25f)), (int)(30 + 225 * (totaldev / 25f)), 0);                  
                            

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

                case UpdateMapOptions.Government:
                    GlobalVariables.GovernmentLocked.LockBits();
                    foreach (Province p in provinces)
                    {
                        Color c = p.OwnerCountry?.Government?.Color ?? Color.White;                                            
                        if (p.Lake || p.Sea)
                            c = Color.Black;
                        foreach (Point pon in p.Pixels)
                            GlobalVariables.GovernmentLocked.SetPixel(pon.X, pon.Y, c);
                        foreach (Point borderpnt in p.BorderPixels)                        
                            GlobalVariables.GovernmentLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                    }
                    GlobalVariables.GovernmentLocked.UnlockBits();
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
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
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
                        if (p.OwnerCountry != null && p.OwnerCountry != Country.NoCountry)
                        {
                            c = p.OwnerCountry.Color;
                            if (p.OwnerCountry.Capital == p)
                            {
                                verticalstripes = true;
                                stripec = AdditionalElements.DimColor(p.OwnerCountry.Color);
                            }
                        }
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
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

                case UpdateMapOptions.Superregion:
                    GlobalVariables.SuperregionBitmap.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        if (p.Area != null)
                        {
                            if (p.Area.Region != null)
                            {
                                if(p.Area.Region.Superregion != null)
                                    c = p.Area.Region.Superregion.Color;
                            }
                                
                        }
                        if ((p.Lake || p.Sea) && !GlobalVariables.ShowSeaTilesAreaMapmode)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.SuperregionBitmap.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.SuperregionBitmap.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.SuperregionBitmap.UnlockBits();
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
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
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

                case UpdateMapOptions.TradeCompany:
                    GlobalVariables.TradeCompanyLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        if (p.TradeCompany != null)
                        {
                            c = p.TradeCompany.Color;
                        }
                        Color borderc = Color.Black;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.TradeCompanyLocked.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.TradeCompanyLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }

                    }


                    GlobalVariables.TradeCompanyLocked.UnlockBits();
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

                case UpdateMapOptions.DiscoveredBy:
                    GlobalVariables.DiscoveredByBitmap = new LockBitmap(new Bitmap(GlobalVariables.BaseWhiteProvincesBitmap.source, GlobalVariables.BaseWhiteProvincesBitmap.Width, GlobalVariables.BaseWhiteProvincesBitmap.Height));
                    GlobalVariables.DiscoveredByBitmap.LockBits();

                    foreach (Province p in provinces)
                    {
                        bool ctr = false;
                        if (p.OwnerCountry != null)
                        {
                            if (p.OwnerCountry.TechnologyGroup == GlobalVariables.SelectedDiscoveredByTechGroup)
                                ctr = true;
                            else if (!p.GetDiscoveredBy().Contains(GlobalVariables.SelectedDiscoveredByTechGroup))
                                continue;
                        }
                        else if (!p.GetDiscoveredBy().Contains(GlobalVariables.SelectedDiscoveredByTechGroup))
                            continue;

                        foreach (Point pon in p.Pixels)
                        {
                            if (ctr)
                            {
                                GlobalVariables.DiscoveredByBitmap.SetPixel(pon.X, pon.Y, Color.Green);
                            }
                            else
                            {
                                if ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3)
                                {
                                    GlobalVariables.DiscoveredByBitmap.SetPixel(pon.X, pon.Y, Color.Green);
                                }
                            }
                        }

                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.DiscoveredByBitmap.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.DiscoveredByBitmap.UnlockBits();
                    break;
                case UpdateMapOptions.Localisation:
                    GlobalVariables.LocalisationLocked.LockBits();

                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        int pt = 0;
                        if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV" + p.ID) || GlobalVariables.LocalisationEntries.Keys.Contains("PROV" + p.ID))
                            pt = 1;
                        if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV_ADJ" + p.ID) || GlobalVariables.LocalisationEntries.Keys.Contains("PROV_ADJ" + p.ID))
                            pt += 2;

                        if (pt == 1)
                            c = Color.LightGreen;
                        else if (pt == 2)
                            c = Color.LightBlue;
                        else if (pt == 3)
                            c = Color.Green;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            GlobalVariables.LocalisationLocked.SetPixel(pon.X, pon.Y, c);
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.LocalisationLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }

                    }
                    GlobalVariables.LocalisationLocked.UnlockBits();
                    break;
                case UpdateMapOptions.Climate:
                    GlobalVariables.ClimateLocked.LockBits();
                    foreach (Province p in provinces)
                    {
                        Color c = Color.White;
                        Color stripec = Color.Black;
                        bool stripes = false;
                        switch (p.Climate)
                        {
                            case 0:
                                c = Color.FromArgb(102, 127, 68);
                                break;
                            case 1:
                                c = Color.FromArgb(102, 178, 48);
                                break;
                            case 2:
                                c = Color.FromArgb(216, 214, 66);
                                break;
                            case 3:
                                c = Color.White;
                                break;
                        }

                        if (p.Impassable == 1)
                            stripes = true;
                        if (p.Lake || p.Sea)
                        {
                            c = Color.FromArgb(68, 107,163);
                            //borderc = Color.Black;
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            if (!stripes)
                            {
                                GlobalVariables.ClimateLocked.SetPixel(pon.X, pon.Y, c);
                            }
                            else
                            {
                                if ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3)
                                {
                                    GlobalVariables.ClimateLocked.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.ClimateLocked.SetPixel(pon.X, pon.Y, c);
                                }
                            }
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.ClimateLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.ClimateLocked.UnlockBits();
                    break;
                case UpdateMapOptions.Winter:
                    GlobalVariables.WinterLocked.LockBits();
                    foreach (Province p in provinces)
                    {
                        Color c = Color.FromArgb(30,30,30);
                        Color stripec = Color.Pink;
                        bool stripes = false;

                        if (p.Winter > 0)
                        {
                            switch (p.Winter)
                            {
                                case 1:
                                    c = Color.FromArgb(85, 85, 85);
                                    break;
                                case 2:
                                    c = Color.FromArgb(170, 170, 170);
                                    break;
                                case 3:
                                    c = Color.White;
                                    break;
                            }
                            if(p.Monsoon > 0)
                            {
                                stripes = true;
                                switch (p.Monsoon)
                                {
                                    case 1:
                                        stripec = Color.FromArgb(0, 0, 85);
                                        break;
                                    case 2:
                                        stripec = Color.FromArgb(0, 0, 170);
                                        break;
                                    case 3:
                                        stripec = Color.FromArgb(0, 0, 255);
                                        break;
                                }
                            }
                        }
                        else if (p.Monsoon > 0)
                        {
                            switch (p.Monsoon)
                            {
                                case 1:
                                    c = Color.FromArgb(0, 0, 85);
                                    break;
                                case 2:
                                    c = Color.FromArgb(0, 0, 170);
                                    break;
                                case 3:
                                    c = Color.FromArgb(0, 0, 255);
                                    break;
                            }
                        }
                        if (p.Lake || p.Sea)
                        {
                            c = Color.FromArgb(68, 107, 163);
                        }
                        foreach (Point pon in p.Pixels)
                        {
                            if (!stripes)
                            {
                                GlobalVariables.WinterLocked.SetPixel(pon.X, pon.Y, c);
                            }
                            else
                            {
                                if ((pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 2 || (pon.X + (int)Math.Floor(pon.Y / 2f)) % 8 == 3)
                                {
                                    GlobalVariables.WinterLocked.SetPixel(pon.X, pon.Y, stripec);
                                }
                                else
                                {
                                    GlobalVariables.WinterLocked.SetPixel(pon.X, pon.Y, c);
                                }
                            }
                        }
                        foreach (Point borderpnt in p.BorderPixels)
                        {
                            GlobalVariables.WinterLocked.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                        }
                    }
                    GlobalVariables.WinterLocked.UnlockBits();
                    break;
            }
        }

        public static void UpdateMap(Province p, UpdateMapOptions options)
        {
            UpdateMap(new List<Province> { p }, options);
        
            
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

        public static void ReloadProvince(List<Province> p)
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
            UpdateMap(p, UpdateMapOptions.DiscoveredBy);
            UpdateMap(p, UpdateMapOptions.Superregion);
        }

        public static void CreateBaseWhiteMap()
        {
            GlobalVariables.BaseWhiteProvincesBitmap.LockBits();
            foreach (Province p in GlobalVariables.Provinces)
            {
                Color c = Color.White;
                if (p.Sea || p.Lake || p.Wasteland)
                    c = Color.Gray;
                foreach (Point pon in p.Pixels)
                {
                    GlobalVariables.BaseWhiteProvincesBitmap.SetPixel(pon.X, pon.Y, c);
                }
                foreach (Point borderpnt in p.BorderPixels)
                {
                    GlobalVariables.BaseWhiteProvincesBitmap.SetPixel(borderpnt.X, borderpnt.Y, Color.Black);
                }
            }
            GlobalVariables.BaseWhiteProvincesBitmap.UnlockBits();
        }
    }
}
