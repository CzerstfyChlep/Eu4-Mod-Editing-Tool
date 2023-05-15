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
        public static bool __DEBUG = false;
        public static string Version = "1.3.0";

        public static List<Province> Provinces = new List<Province>(5000);
        public static Dictionary<string, Province> ColorToProvince = new Dictionary<string, Province>(5000);
        public static MapManagement.UpdateMapOptions mapmode = MapManagement.UpdateMapOptions.Provinces;
        
        public static LockBitmap DevelopmentBitmapLocked;
        public static LockBitmap TradeGoodBitmapLocked;
        public static LockBitmap ReligionBitmapLocked;
        public static LockBitmap CultureBitmapLocked;
        public static LockBitmap PoliticalBitmapLocked;

        public static LockBitmap AreaBitmapLocked;
        public static LockBitmap RegionBitmapLocked;
        public static LockBitmap TradeNodeBitmap;
        public static LockBitmap HREBitmap;
        public static LockBitmap FortBitmap;
        public static LockBitmap ContinentBitmap;
        public static LockBitmap SuperregionBitmap;
        public static LockBitmap DiscoveredByBitmap;
        public static LockBitmap BaseWhiteProvincesBitmap;
        public static LockBitmap TradeCompanyLocked;
        public static LockBitmap GovernmentLocked;
        public static LockBitmap LocalisationLocked;
        public static LockBitmap ClimateLocked;
        public static LockBitmap WinterLocked;
        public static LockBitmap TerrainLocked;
        

        public static LockBitmap DrawingMain;
        public static LockBitmap ClickedMask;

        //public static List<Task<Dictionary<int, List<Point>>>> MapLines = new List<Task<Dictionary<int, List<Point>>>>();
        public static List<TradeGood> TradeGoods = new List<TradeGood>(50);
        public static List<TradeGood> LatentTradeGoods = new List<TradeGood>(5);
        public static Random GlobalRandom = new Random();
        public static string pathtomod = "";
        public static string pathtogame = "";
        public static Point CameraPosition = new Point(0, 0);
        public static Image ProvincesMap;
        public static Bitmap ProvincesMapBitmap;
        public static Bitmap DevelopmentBitmap;


       

        public static bool ShowSeaTilesAreaMapmode = false;

        public static bool CreateNewFilesReadOnly = false;
        public static bool NewObjectsNewFiles = false;

        public static List<Province> ClickedProvinces = new List<Province>(10);
        public static List<object> ToUpdate = new List<object>(50);
        public static Task UpdateDevInfo = null;
        public static List<Country> Countries = new List<Country>(1000);
        public static List<Country> RemovedCountries = new List<Country>();
        public static Country SelectedCountry;
        public static int TotalUsableProvinces = 0;
        public static List<int> PressedKeys = new List<int>();
        public static List<Area> Areas = new List<Area>(1000);
        public static List<Region> Regions = new List<Region>(500);
        public static List<Superregion> Superregions = new List<Superregion>(50);
        public static List<Continent> Continents = new List<Continent>(10);
        public static List<Tradenode> TradeNodes = new List<Tradenode>(50);
        public static bool TradeDestClickingMode = false;
        public static List<TradeCompany> TradeCompanies = new List<TradeCompany>(100);

        public static List<TerrainType> TerrainTypes = new List<TerrainType>(20);

        public static List<IdeaSet> IdeaGroups = new List<IdeaSet>(50);
        public static List<IdeaSet> NationalIdeas = new List<IdeaSet>(1000);

        public static List<Building> Buildings = new List<Building>(25);

        public static List<Government> Governments = new List<Government>(50);

        public static List<string> RebelTypes = new List<string>();

        public static List<CountryModifier> CountryScopeModifiers = new List<CountryModifier>() { };
        public static List<string> CountryModifiers = new List<string>() { };

        public static int[] UseMod = new int[26];
        public static bool[] ReadOnly = new bool[26];
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

        public static NodeFile GameTradeCompanyFile;
        public static List<NodeFile> ModTradeCompanyFiles = new List<NodeFile>();

        public static NodeFile GameCountryTagsFile;
        public static List<NodeFile> ModCountryTagsFiles = new List<NodeFile>();

        public static NodeFile GameGovernmentsFile;
        public static List<NodeFile> ModGovernmentsFiles = new List<NodeFile>();

        public static List<NodeFile> GameIdeaFiles = new List<NodeFile>();
        public static List<NodeFile> ModIdeaFiles = new List<NodeFile>();

        public enum ModNodeFileTypes { TradeGoods = 0, Prices = 1, Cultures, Religions, TradeNodes, TradeCompanies, CountryTags, Governments };

        public static ModEditor MainForm;

        public static string SelectedDiscoveredByTechGroup = "";

        public static int CurrentLoadingProgress = 0;

        public static Province[,,] CubeArray = new Province[256, 256, 256];


        public static bool InternalChanges = false;

        public enum Languages { English, French, German, Spanish };

        public static Languages LocalisationLanguage = Languages.English;

        public static Dictionary<string, string> LocalisationEntries = new Dictionary<string, string>();
        public static Dictionary<string, string> ModLocalisationEntries = new Dictionary<string, string>();


        public static bool BorderingMode = false;

        public static int MapHeight = 0;
        public static int MapWidth = 0;

        public static int AppSizeOption = 0;
        public static bool DarkMode = false;

        public static int MapDrawingPosX = 36;
        public static int MapDrawingPosY = 60;

        public static int MapDrawingWidth = 1090;
        public static int MapDrawingHeight = 730;

        public static bool Exited = false;

        public enum LoadFilesOrder { definition = 0, provincesBMP = 1, tradegoods = 2, prices = 3,
        cultures = 4, religions = 5, region = 6, commonCountries = 7, historyProvinces = 8,
        area = 9, mapdefault = 10, historyCountries = 11, tradenodes = 12,
        continent = 13, countrytags = 14, technology = 15, governments = 16,
        buildings = 17, superregion = 18, tradecompanies = 19, localisation = 20,
        climate = 21, rebeltypes = 22, ideas=23, terrainBMP = 24, terrainTXT = 25 }


        public static DateTime StartDate = new DateTime(1444, 11, 11);
        public static DateTime CurrentDate = new DateTime(1444, 11, 11);

        public static bool OldMapUpdatingStyle = false;

        public static bool NamesHidden = false;

        public static (string, int)[] FortBuildings = new (string, int)[] { };





        //#####################     OPTIONS     ###################        
        public static class Options
        {
            public static bool Autosaving = false;
            public static int AutosavingInterval = 5;
            public static string AutosavingPath = "";
            public static bool AutosavingOnExit = false;

            public static bool SaveCrash = false;
            public static string SaveCrashPath = "";

            public static bool RandomProvinceCustom = false;

            public static int RandomProvinceLowMinimum = 1;
            public static int RandomProvinceLowAverage = 1;
            public static int RandomProvinceLowMaximum = 1;

            public static int RandomProvinceMediumMinimum = 1;
            public static int RandomProvinceMediumAverage = 1;
            public static int RandomProvinceMediumMaximum = 1;

            public static int RandomProvinceHighMinimum = 1;
            public static int RandomProvinceHighAverage = 1;
            public static int RandomProvinceHighMaximum = 1;

            public static bool SameValueForAllProvinces = false;
        }
        //###################   THREADS    ########################

        public static class Threads
        {
            public static Thread UpdtGraphicsThread;
            public static Thread AutoSaveThread;
        }
    }
}















