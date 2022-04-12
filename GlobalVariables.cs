using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Eu4ModEditor;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    public static class GlobalVariables
    {
        public static List<Province> Provinces = new List<Province>();
        public static Dictionary<string, Province> ColorToProvince = new Dictionary<string, Province>();
        public static MapManagement.UpdateMapOptions mapmode = MapManagement.UpdateMapOptions.Provinces;
        public static LockBitmap DevelopmentBitmapLocked;
        public static LockBitmap TradeGoodBitmapLocked;
        public static LockBitmap ReligionBitmapLocked;
        public static LockBitmap CultureBitmapLocked;
        public static LockBitmap PoliticalBitmapLocked;
        public static LockBitmap ClickedMask;
        public static LockBitmap AreaBitmapLocked;
        public static LockBitmap RegionBitmapLocked;
        public static LockBitmap TradeNodeBitmap;
        public static LockBitmap HREBitmap;
        public static LockBitmap FortBitmap;
        public static LockBitmap ContinentBitmap;
        public static LockBitmap SuperregionBitmap;
        public static List<Task<Dictionary<int, List<Point>>>> MapLines = new List<Task<Dictionary<int, List<Point>>>>();
        public static List<TradeGood> TradeGoods = new List<TradeGood>();
        public static List<TradeGood> LatentTradeGoods = new List<TradeGood>();
        public static Random GlobalRandom = new Random();
        public static Province ClickedProvince;
        public static string pathtomod = "";
        public static string pathtogame = "";
        public static Point CameraPosition = new Point(0, 0);
        public static Image ProvincesMap;
        public static Bitmap ProvincesMapBitmap;
        public static Bitmap DevelopmentBitmap;
        public static Thread UpdtGraphicsThread;
        public static bool ChangedSomething = false;
        public static bool MultiProvinceMode = false;
        public static bool TaxInternalChange = false;
        public static bool ProductionInternalChange = false;
        public static bool ManpowerInternalChange = false;
        public static bool TradeGoodInternalChange = false;
        public static bool LatentTradeGoodInternalChange = false;
        public static bool ReligionInternalChange = false;
        public static bool CultureInternalChange = false;
        public static bool OwnerInternalChange = false;
        public static bool ControllerInternalChange = false;
        public static bool HREInternalChange = false;
        public static bool FortInternalChange = false;
        public static bool GovernmentTypeInternalChange = false;
        public static bool GovernmentReformInternalChange = false;
        public static bool CountryReligionInternalChange = false;
        public static bool CountryPrimaryCultureInternalChange = false;
        public static bool CountryGovernmentRankInternalChange = false;
        public static bool ProvinceCenterOfTradeInternalChange = false;
        public static bool CountryCapitalInternalChange = false;
        public static bool CountryTechGroupInternalChange = false;
        public static bool AreaInternalChange = false;
        public static bool RegionInternalChange = false;
        public static bool TradeNodeInternalChange = false;
        public static bool ShowSeaTilesAreaMapmode = false;
        public static bool ContinentInternalChange = false;

        public static List<Province> ClickedProvinces = new List<Province>();
        public static List<object> ToUpdate = new List<object>();
        public static Task UpdateDevInfo = null;
        public static List<Country> Countries = new List<Country>();
        public static Country SelectedCountry;
        public static int TotalUsableProvinces = 0;
        public static List<int> PressedKeys = new List<int>();
        public static List<Area> Areas = new List<Area>();
        public static List<Region> Regions = new List<Region>();
        public static List<Superregion> Superregions = new List<Superregion>();
        public static List<Continent> Continents = new List<Continent>();
        public static List<Tradenode> TradeNodes = new List<Tradenode>();
        public static bool TradeDestClickingMode = false;

        public static List<Building> Buildings = new List<Building>();

        public static List<Government> Governments = new List<Government>();

        public static List<CountryModifier> CountryScopeModifiers = new List<CountryModifier>() { };
        public static List<string> CountryModifiers = new List<string>() { };

        public static int[] UseMod = new int[19];
        public static bool[] ReadOnly = new bool[19];
        public static bool LoadedProperly = false;
        public static bool FullyLoaded = false;

        public static List<VariableChange> Changes = new List<VariableChange>();
        public static List<object> Saves = new List<object>();

        public static List<string> TechGroups = new List<string>();

        public static NodeFile GameTradeGoodsFile;
        public static List<NodeFile> ModTradeGoodsFiles = new List<NodeFile>();

        public static NodeFile GamePricesFile;
        public static List<NodeFile> ModPricesFiles = new List<NodeFile>();

        public static NodeFile GameCulturesFile;
        public static List<NodeFile> ModCulturesFiles = new List<NodeFile>();

        public static NodeFile GameReligionsFile;
        public static List<NodeFile> ModReligionsFiles = new List<NodeFile>();

        public static NodeFile GameTradeNodesFile;
        public static List<NodeFile> ModTradeNodesFiles = new List<NodeFile>();

        public static NodeFile GameCountryTagsFile;
        public static List<NodeFile> ModCountryTagsFiles = new List<NodeFile>();

        public static NodeFile GameGovernmentsFile;
        public static List<NodeFile> ModGovernmentsFiles = new List<NodeFile>();

        public static Form MainForm;
    }

   
}















