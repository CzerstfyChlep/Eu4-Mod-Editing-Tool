using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    public static class ChangeMapmode
    {

        public static void ChangeMapmodeDirectly(MapManagement.UpdateMapOptions op)
        {
            GlobalVariables.mapmode = op;
            MapManagement.UpdateProvinceColors(GlobalVariables.Provinces, GlobalVariables.mapmode);
            MapManagement.DrawPixelsOnMap(new List<System.Drawing.Rectangle> { new System.Drawing.Rectangle(GlobalVariables.CameraPosition, new System.Drawing.Size(GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight)) });
            ModEditor.UpdateMap();
        }

        public static void ChangeMapmodeVoid(object sender, EventArgs e)
        {
            Button snd = (Button)sender;
            switch (snd.Name)
            {
                case "ProvincesMapmodeButton":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Provinces;                   
                    break;
                case "DevelopmentMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Development;
                    break;
                case "TradeGoodsMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.TradeGood;
                    break;
                case "ReligionMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Religion;
                    break;
                case "CultureMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Culture;
                    break;
                case "PoliticalMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Political;
                    break;
                case "AreaMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Area;
                    break;
                case "RegionMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Region;
                    break;
                case "TradeNodeMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.TradeNode;
                    break;
                case "HREMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.HRE;
                    break;
                case "FortMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Fort;
                    break;
                case "ContinentMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Continent;
                    break;
                case "SuperregionMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Superregion;
                    break;
                case "DiscoveredByMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.DiscoveredBy;
                    break;
                case "TradeCompanyMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.TradeCompany;
                    break;
                case "GovernmentMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Government;
                    break;
                case "ProvinceLocalisationMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Localisation;
                    break;
                case "WinterMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Winter;
                    break;
                case "ClimateMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Climate;
                    break;
                case "TerrainMapmode":
                    GlobalVariables.mapmode = MapManagement.UpdateMapOptions.Terrain;
                    break;
            }
            MapManagement.UpdateProvinceColors(GlobalVariables.Provinces, GlobalVariables.mapmode);
            MapManagement.DrawPixelsOnMap(new List<System.Drawing.Rectangle> { new System.Drawing.Rectangle(GlobalVariables.CameraPosition, new System.Drawing.Size(GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight)) });
            ModEditor.UpdateMap();
        }
    }
}
