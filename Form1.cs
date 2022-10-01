using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

namespace Eu4ModEditor
{
    public partial class ModEditor : Form
    {
        public ModEditor()
        {
            LanguageEngine.InitialiseLanguagePack();
            GlobalVariables.MainForm = this;
            /*LanguageWindow lg = new LanguageWindow();
            lg.ShowDialog();*/
            this.AutoScaleMode = AutoScaleMode.None;

            LoadingScreen sc = new LoadingScreen();
            sc.ShowDialog();

            if (!GlobalVariables.LoadedProperly)
                Environment.Exit(0);
            InitializeComponent();
            this.Text = "EUIV - Mod Editor - " + GlobalVariables.Version;
            form = this;
            graphics = this.CreateGraphics();
            //GlobalVariables.pathtomod = File.ReadAllText("path.txt");
            if (GlobalVariables.UseMod[1] == 1 || GlobalVariables.UseMod[1] == 2)
                GlobalVariables.ProvincesMap = Image.FromFile(GlobalVariables.pathtomod + "map/provinces.bmp");
            else if (GlobalVariables.UseMod[1] == 0)
                GlobalVariables.ProvincesMap = Image.FromFile(GlobalVariables.pathtogame + "map/provinces.bmp");
            GlobalVariables.ProvincesMapBitmap = new Bitmap(GlobalVariables.ProvincesMap);
            GlobalVariables.UpdtGraphicsThread = new Thread(UpdateGraphics);


            this.FormClosing += OnExitDo;

            GlobalVariables.ClickedMask = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.DrawingMain = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));     
            


            ProvincesMapmodeButton.Click += ChangeMapmode.ChangeMapmodeVoid;
            DevelopmentMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            TradeGoodsMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            ReligionMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            CultureMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            PoliticalMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            AreaMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            RegionMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            TradeNodeMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            HREMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            FortMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            ContinentMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            SuperregionMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            DiscoveredByMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            TradeCompanyMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            GovernmentMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            ProvinceLocalisationMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            ClimateMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            WinterMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            TerrainMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;

            LoadFilesClass.LoadFiles();

            MapManagement.DrawBordersOnMap();
            MapManagement.DrawPixelsOnMap(new List<Rectangle> { new Rectangle(GlobalVariables.CameraPosition, new Size(GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight)) });


            CheckForIllegalCrossThreadCalls = false;

            KeyDown += InputManagement.HandleButton;

            KeyUp += InputManagement.HandleKeyUp;          

            RightButton.MouseClick += InputManagement.HandleMoveButton;
            LeftButton.MouseClick += InputManagement.HandleMoveButton;
            UpButton.MouseClick += InputManagement.HandleMoveButton;
            DownButton.MouseClick += InputManagement.HandleMoveButton;

            MouseClick += MouseClickHandler;

            OwnerBox.KeyDown += InputManagement.IgnoreKeyPress;
            ReligionBox.KeyDown += InputManagement.IgnoreKeyPress;
            CultureBox.KeyDown += InputManagement.IgnoreKeyPress;
            TradeGoodBox.KeyDown += InputManagement.IgnoreKeyPress;
            ProvinceTaxNumeric.KeyDown += InputManagement.IgnoreKeyPress;
            ProvinceManpowerNumeric.KeyDown += InputManagement.IgnoreKeyPress;
            ProvinceProductionNumeric.KeyDown += InputManagement.IgnoreKeyPress;
            CountryBox.KeyDown += InputManagement.IgnoreKeyPress;
            CountryReligionBox.KeyDown += InputManagement.IgnoreKeyPress;
            AreaBox.KeyDown += InputManagement.IgnoreKeyPress;
            TradeNodeBox.KeyDown += InputManagement.IgnoreKeyPress;
            ProvinceTradeNodeBox.KeyDown += InputManagement.IgnoreKeyPress;
            AddTradeNodeDestinationBox.KeyDown += InputManagement.IgnoreKeyPress;
            AddCoreBox.KeyDown += InputManagement.IgnoreKeyPress;
            ContinentBox.KeyDown += InputManagement.IgnoreKeyPress;
            SuperregionBox.KeyDown += InputManagement.IgnoreKeyPress;
            TechnologyGroupBox.KeyDown += InputManagement.IgnoreKeyPress;
            CountryPrimaryCultureBox.KeyDown += InputManagement.IgnoreKeyPress;
            DiscoveredByBox.KeyDown += InputManagement.IgnoreKeyPress;
            BuildingsBox.KeyDown += InputManagement.IgnoreKeyPress;
            WinterBox.KeyDown += InputManagement.IgnoreKeyPress;
            MonsoonBox.KeyDown += InputManagement.IgnoreKeyPress;
            ClimateBox.KeyDown += InputManagement.IgnoreKeyPress;
            ImpassableBox.KeyDown += InputManagement.IgnoreKeyPress;

            MonarchNameBox.LostFocus += FocusLost;
            CountryNameLocalisationBox.LostFocus += FocusLost;
            CountryAdjLocalisationBox.LostFocus += FocusLost;
            CountryCapitalIDBox.LostFocus += FocusLost;
            ProvinceNameLocalisationBox.LostFocus += FocusLost;
            ProvinceAdjectiveLocalisationBox.LostFocus += FocusLost;
            AreaNameChangeBox.LostFocus += FocusLost;
            RegionNameChangeBox.LostFocus += FocusLost;
            ContinentNameChangeBox.LostFocus += FocusLost;
            SuperregionNameChangeBox.LostFocus += FocusLost;
            TradeCompanyNameChangeBox.LostFocus += FocusLost;
            ChangeTradeNodeNameBox.LostFocus += FocusLost;
            TradeNodeProvinceLocationBox.LostFocus += FocusLost;
            CountryTagBox.LostFocus += FocusLost;
            CountryNameBox.LostFocus += FocusLost;

            LeaderNamesBox.LostFocus += FocusLost;
            ShipNamesBox.LostFocus += FocusLost;
            ArmyNamesBox.LostFocus += FocusLost;
            FleetNamesBox.LostFocus += FocusLost;

            HideSeaTiles.Click += ShowHideSeaTilesAreaMapmode_Click;

            boxes.AddRange(new ComboBox[] { OwnerBox, ReligionBox, CultureBox, TradeGoodBox,
            CountryBox, CountryReligionBox, AreaBox, RegionBox, ProvinceTradeNodeBox, TradeNodeBox,
            AddTradeNodeDestinationBox, ContinentBox, AddCoreBox, TechnologyGroupBox,
            CountryPrimaryCultureBox, DiscoveredByBox, BuildingsBox, SuperregionBox, TradeCompanyBox });
            textboxes.AddRange(new TextBox[] { AreaNameChangeBox, AddNewAreaBox, AddNewRegionBox,
            RegionNameChangeBox, ChangeTradeNodeNameBox, TradeNodeNameBox, TradeNodeProvinceLocationBox,
            ContinentNameChangeBox, AddNewContinentBox, SuperregionNameChangeBox, AddNewSuperregionBox,
            TradeCompanyNameChangeBox, AddNewTradeCompanyBox, ProvinceNameLocalisationBox, ProvinceAdjectiveLocalisationBox,
            CountryNameLocalisationBox, CountryAdjLocalisationBox, MonarchNameBox, MonarchNameChancesTextBox, LeaderNamesBox,
            ShipNamesBox, ArmyNamesBox, FleetNamesBox, CountryTagBox, CountryNameBox, ConsoleInputBox });
            foreach (TradeGood tg in GlobalVariables.TradeGoods)
            {
                CreateTradeGoodsInfoBox(tg);
            }

            foreach (string s in GlobalVariables.TechGroups)
            {
                DiscoveredByBox.Items.Add(s);
            }

            MoveCameraTo(GlobalVariables.Provinces[0]);
            GlobalVariables.UpdtGraphicsThread.Start();

            CultureBox.Items.AddRange(Culture.Cultures.ToArray());
            CultureBox.Sorted = true;

            CountryPrimaryCultureBox.Items.AddRange(Culture.Cultures.ToArray());
            CountryPrimaryCultureBox.Sorted = true;

            ReligionBox.Items.AddRange(Religion.Religions.ToArray());
            ReligionBox.Sorted = true;

            CountryReligionBox.Items.AddRange(Religion.Religions.ToArray());
            CountryReligionBox.Sorted = true;

            OwnerBox.Items.AddRange(GlobalVariables.Countries.ToArray());
            OwnerBox.Sorted = true;

            ControllerBox.Items.AddRange(GlobalVariables.Countries.ToArray());
            ControllerBox.Sorted = true;

            CountryBox.Items.AddRange(GlobalVariables.Countries.ToArray());
            CountryBox.Sorted = true;

            AddCoreBox.Items.AddRange(GlobalVariables.Countries.ToArray());
            AddCoreBox.Sorted = true;

            TechnologyGroupBox.Items.AddRange(GlobalVariables.TechGroups.ToArray());
            TechnologyGroupBox.Sorted = true;

            MacroReligionBox.Items.AddRange(Religion.Religions.ToArray());
            MacroReligionBox.Sorted = true;

            MacroCultureBox.Items.AddRange(Culture.Cultures.ToArray());
            MacroCultureBox.Sorted = true;

            MacroAreaBox.Items.AddRange(GlobalVariables.Areas.ToArray());
            MacroAreaBox.Sorted = true;

            MacroRegionBox.Items.AddRange(GlobalVariables.Regions.ToArray());
            MacroRegionBox.Sorted = true;

            MacroSuperregionBox.Items.AddRange(GlobalVariables.Superregions.ToArray());
            MacroSuperregionBox.Sorted = true;

            MacroContinentBox.Items.AddRange(GlobalVariables.Continents.ToArray());
            MacroContinentBox.Sorted = true; 

            MacroTradeNodeBox.Items.AddRange(GlobalVariables.TradeNodes.ToArray());
            MacroTradeNodeBox.Sorted = true;

            MacroTechGroupBox.Items.AddRange(GlobalVariables.TechGroups.ToArray());
            MacroTechGroupBox.Sorted = true;

            GraphicalCultureBox.Items.AddRange(new string[] { "westerngfx", "easterngfx", "muslimgfx",
            "indiangfx", "asiangfx", "africangfx", "northamericagfx", "southamericagfx", "inuitgfx",
            "aboriginalgfx", "polynesiangfx", "southeastasiangfx"});
            //SIZING
            if(GlobalVariables.AppSizeOption == 1)
            {
                this.Width = 1290;
                GlobalVariables.MapDrawingWidth = 620;
                Tabs.Location = new Point(703, 12);
                UpButton.Width = 620;
                DownButton.Width = 620;
                RightButton.Location = new Point(665, RightButton.Location.Y);
            }

            //GIVE FOCUS

            MonarchNamePanel.MouseEnter += GainFocus;

            //TABS CHANGE

            Tabs.Selected += TabChanged;
            NamesTabs.Selected += TabChanged;
            ProvinceTabControl.Selected += TabChanged;

            }

        public void ProvUpdNum(int num)
        {
            label80.Text = num.ToString();
        }

        public void TabChanged(object sender, EventArgs e)
        {
            if(sender == Tabs)
            {
                if (Tabs.SelectedTab == CountryPage)
                {
                    UpdateCountryPage();
                    if (NamesTabs.SelectedTab == MonarchNamesTab)
                        UpdateMonarchNames();
                }
                else if (Tabs.SelectedTab == ProvinceTab)                
                    UpdateProvincePanel();              
                else if (Tabs.SelectedTab == TradeGoodsTab)
                    RefreshTradeGoodsTab();
                else if (Tabs.SelectedTab == TradeNodesTab)
                    UpdateTradeNodesPage();
                
            }
            else if(sender == NamesTabs)
            {
                if (NamesTabs.SelectedTab == MonarchNamesTab)
                    UpdateMonarchNames();
            }
            else if(sender == ProvinceTabControl)
            {
                if (ProvinceTabControl.SelectedTab == MainPage)
                    UpdateMainProvincePage();
                else if (ProvinceTabControl.SelectedTab == AreaRegionPage)
                    UpdateAreaAndRegionPage();
                else if (ProvinceTabControl.SelectedTab == LocalisationPage)
                    UpdateProvinceLocalisationPage();
            }
        }
        public static List<ComboBox> boxes = new List<ComboBox>();
        public static List<GroupBox> TradeGoodInfoBoxes = new List<GroupBox>();
        public static List<TextBox> textboxes = new List<TextBox>();
        private char[] nums = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        #region Graphical
        public static Graphics graphics;

        public void UpdateLab(string txt, int type)
        {
            switch (type)
            {
                case 0:
                    label80.Text = "Last draw time: " + txt;
                    break;
                case 1:
                    label81.Text = "Last set pixels time: " + txt;
                    break;
                case 2:
                    label82.Text = "Last set colours time: " + txt;
                    break;
                case 3:
                    label83.Text = "Provinces drawn: " + txt;
                    break;
            }
        }

        public static Stopwatch stopwatch = new Stopwatch();

        public static void UpdateMap()
        {
            stopwatch.Reset();
            stopwatch.Start();
            graphics.DrawImage(GlobalVariables.DrawingMain.source, new Rectangle(40, 40, GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight), new Rectangle(GlobalVariables.CameraPosition, new Size(GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight)), GraphicsUnit.Pixel);
            graphics.DrawImage(GlobalVariables.ClickedMask.source, new Rectangle(40, 40, GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight), new Rectangle(GlobalVariables.CameraPosition, new Size(GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight)), GraphicsUnit.Pixel);
            stopwatch.Stop();
            GlobalVariables.MainForm.UpdateLab(stopwatch.ElapsedMilliseconds.ToString(), 0);

        }
        public void UpdateGraphics()
        {

        }
        #endregion

        #region Creating Controls
        public void CreateTradeGoodsInfoBox(TradeGood tradegood)
        {
            GroupBox gb = new GroupBox
            {
                Name = tradegood.ReadableName,
                Text = tradegood.ReadableName,
                Tag = tradegood,
                Size = new Size(356, 63)
            };

            Button b = new Button
            {
                Tag = tradegood,
                Enabled = true
            };
            b.Click += SelectAllOfTradeGood;
            b.ForeColor = tradegood.Color;
            b.BackColor = tradegood.Color;
            b.Text = "";
            b.Size = new Size(50, 34);
            b.Location = new Point(6, 19);
            gb.Controls.Add(b);

            Label l1 = new Label();
            gb.Controls.Add(l1);
            l1.Location = new Point(62, 30);
            l1.AutoSize = true;
            l1.Text = "Provinces:" + tradegood.TotalProvinces;

            Label l2 = new Label();
            gb.Controls.Add(l2);
            l2.Location = new Point(163, 30);
            l2.AutoSize = true;
            l2.Text = "Share: " + RoundUp(((double)tradegood.TotalProvinces / GlobalVariables.TotalUsableProvinces), 2) * 100 + "%";

            Label l3 = new Label();
            gb.Controls.Add(l3);
            l3.Location = new Point(252, 30);
            l3.AutoSize = true;
            if (tradegood.TotalProvinces > 0)
                l3.Text = "Avg. Dev: " + RoundUp(((double)tradegood.TotalDev / tradegood.TotalProvinces), 1);
            else
                l3.Text = "Avg. Dev: 0";

            TradeGoodsInfoPanel.Controls.Add(gb);

        }
        #endregion

        #region Control Updaters
        public void UpdateMainProvincePage()
        {
            ChangeValueInternally(CultureBox, GlobalVariables.ClickedProvinces[0].Culture);
            ChangeValueInternally(ReligionBox, GlobalVariables.ClickedProvinces[0].Religion);
            ChangeValueInternally(OwnerBox, GlobalVariables.ClickedProvinces[0].OwnerCountry);
            ChangeValueInternally(TradeGoodBox, TradeGoodBox.Items.IndexOf(GlobalVariables.ClickedProvinces[0].TradeGood?.ReadableName ?? ""));
            ChangeValueInternally(LatentTradeGoodBox, LatentTradeGoodBox.Items.IndexOf(GlobalVariables.ClickedProvinces[0].LatentTradeGood?.ReadableName ?? ""));
            if (GlobalVariables.ClickedProvinces[0].Controller != null)            
                ChangeValueInternally(ControllerBox, GlobalVariables.ClickedProvinces[0].Controller);            
            else
                if (ControllerBox.SelectedIndex != 0)
                ChangeValueInternally(ControllerBox, 0);
            IsCityCheckbox.Checked = GlobalVariables.ClickedProvinces[0].City;
            if (GlobalVariables.ClickedProvinces[0].TradeNode != null)
            {
                if (GlobalVariables.TradeNodes.IndexOf(GlobalVariables.ClickedProvinces[0].TradeNode) + 1 != ProvinceTradeNodeBox.SelectedIndex)
                {
                    ChangeValueInternally(ProvinceTradeNodeBox, GlobalVariables.ClickedProvinces[0].TradeNode);
                }
                //TODO
                //Why is this here
                //TradeNodeBox.SelectedIndex = GlobalVariables.TradeNodes.IndexOf(p.TradeNode) + 1;
            }
            else
            {
                if (ProvinceTradeNodeBox.SelectedIndex != 0)
                    ChangeValueInternally(ProvinceTradeNodeBox, 0);
            }

            ChangeValueInternally(WinterBox, GlobalVariables.ClickedProvinces[0].Winter);
            ChangeValueInternally(MonsoonBox, GlobalVariables.ClickedProvinces[0].Monsoon);
            ChangeValueInternally(ClimateBox, GlobalVariables.ClickedProvinces[0].Climate);
            ChangeValueInternally(ImpassableBox, GlobalVariables.ClickedProvinces[0].Impassable);

        }
        public void UpdateAreaAndRegionPage()
        {
            if (GlobalVariables.ClickedProvinces[0].Area != null)
            {
                if (AreaBox.SelectedIndex != GlobalVariables.Areas.IndexOf(GlobalVariables.ClickedProvinces[0].Area) + 1)
                {
                    ChangeValueInternally(AreaBox, GlobalVariables.Areas.IndexOf(GlobalVariables.ClickedProvinces[0].Area) + 1);
                }

                if (GlobalVariables.ClickedProvinces[0].Area.Region != null)
                {
                    if (RegionBox.SelectedIndex != GlobalVariables.Regions.IndexOf(GlobalVariables.ClickedProvinces[0].Area.Region) + 1)
                    {
                        ChangeValueInternally(RegionBox, GlobalVariables.Regions.IndexOf(GlobalVariables.ClickedProvinces[0].Area.Region) + 1);
                    }
                    if (GlobalVariables.ClickedProvinces[0].Area.Region.Superregion != null)
                    {
                        if (SuperregionBox.SelectedIndex != GlobalVariables.Superregions.IndexOf(GlobalVariables.ClickedProvinces[0].Area.Region.Superregion) + 1)
                        {
                            ChangeValueInternally(SuperregionBox, GlobalVariables.Superregions.IndexOf(GlobalVariables.ClickedProvinces[0].Area.Region.Superregion) + 1);
                        }
                    }
                    else
                        ChangeValueInternally(SuperregionBox, 0);
                }
                else if (RegionBox.SelectedIndex != 0)
                    ChangeValueInternally(RegionBox, 0);


            }
            else
            {
                if (AreaBox.SelectedIndex != 0)
                    ChangeValueInternally(AreaBox, 0);
                if (RegionBox.SelectedIndex != 0)
                    ChangeValueInternally(RegionBox, 0);
            }
            if (GlobalVariables.ClickedProvinces[0].Continent != null)
            {
                if (GlobalVariables.Continents.IndexOf(GlobalVariables.ClickedProvinces[0].Continent) + 1 != ContinentBox.SelectedIndex)
                    ChangeValueInternally(ContinentBox, GlobalVariables.Continents.IndexOf(GlobalVariables.ClickedProvinces[0].Continent) + 1);
            }
            else
            {
                if (ContinentBox.SelectedIndex != 0)
                    ChangeValueInternally(ContinentBox, 0);
            }
            if (GlobalVariables.ClickedProvinces[0].TradeCompany != null)
            {
                if (GlobalVariables.TradeCompanies.IndexOf(GlobalVariables.ClickedProvinces[0].TradeCompany) + 1 != TradeCompanyBox.SelectedIndex)
                {
                    ChangeValueInternally(TradeCompanyBox, GlobalVariables.TradeCompanies.IndexOf(GlobalVariables.ClickedProvinces[0].TradeCompany) + 1);
                    TradeCompanyColorButton.BackColor = GlobalVariables.ClickedProvinces[0].TradeCompany.Color;
                }
            }
            else
            {
                if (TradeCompanyBox.SelectedIndex != 0)
                    ChangeValueInternally(TradeCompanyBox, 0);

            }
        }
        public void UpdateProvinceLocalisationPage()
        {
            if (GlobalVariables.ClickedProvinces.Count == 1)
            {
                ((Control)LocalisationPage).Enabled = true;

                if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV" + GlobalVariables.ClickedProvinces[0].ID))
                    ProvinceNameLocalisationBox.Text = GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID];
                else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV" + GlobalVariables.ClickedProvinces[0].ID))
                    ProvinceNameLocalisationBox.Text = GlobalVariables.LocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID];
                else
                    ProvinceNameLocalisationBox.Text = "";

                if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID))
                    ProvinceAdjectiveLocalisationBox.Text = GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID];
                else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID))
                    ProvinceAdjectiveLocalisationBox.Text = GlobalVariables.LocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID];
                else
                    ProvinceAdjectiveLocalisationBox.Text = "";
            }
            else
            {
                ((Control)LocalisationPage).Enabled = false;
            }
        }

        public void UpdateProvincePanel()
        {
            //TODO
            //make this whole thing work with ClickedProvinces
            //TODO 
            //Actually removed Clicked province entirely with a list with one element
            if (!GlobalVariables.ClickedProvinces.Any())
                return;
            //TODO
            //why is this here again?

            if (GlobalVariables.ClickedProvinces.Count == 1)
            {
                ProvinceLabelID.Text = "ID: " + GlobalVariables.ClickedProvinces[0].ID;
                ProvinceColorLabelR.Text = "R: " + GlobalVariables.ClickedProvinces[0].R;
                ProvinceColorLabelG.Text = "G: " + GlobalVariables.ClickedProvinces[0].G;
                ProvinceColorLabelB.Text = "B: " + GlobalVariables.ClickedProvinces[0].B;
                if (GlobalVariables.ClickedProvinces[0].Sea || GlobalVariables.ClickedProvinces[0].Lake)
                    ProvinceSeaLakeLabel.Text = "S/L: Yes";
                else
                    ProvinceSeaLakeLabel.Text = "S/L: No";
            }
            else
            {
                ProvinceLabelID.Text = "ID: N/A";
                ProvinceColorLabelR.Text = "R: N/A";
                ProvinceColorLabelG.Text = "G: N/A";
                ProvinceColorLabelB.Text = "B: N/A";
                ProvinceSeaLakeLabel.Text = "S/L: N/A";
            }
            //TODO
            //ADD AVERAGES!
            ChangeValueInternally(ProvinceTaxNumeric, GlobalVariables.ClickedProvinces[0].Tax);
            ChangeValueInternally(ProvinceProductionNumeric, GlobalVariables.ClickedProvinces[0].Production);
            ChangeValueInternally(ProvinceManpowerNumeric, GlobalVariables.ClickedProvinces[0].Manpower);
           
            ChangeValueInternally(HRECheckbox, GlobalVariables.ClickedProvinces[0].HRE);
            ChangeValueInternally(FortCheckbox, GlobalVariables.ClickedProvinces[0].Fort);
            ChangeValueInternally(CenterOfTradeNumeric, GlobalVariables.ClickedProvinces[0].CenterOfTrade);         

            // CountryBox.SelectedItem = GlobalVariables.ClickedProvinces[0].OwnerCountry;

            if (ProvinceTabControl.SelectedTab == MainPage)
                UpdateMainProvincePage();
            else if (ProvinceTabControl.SelectedTab == AreaRegionPage)
                UpdateAreaAndRegionPage();
            else if (ProvinceTabControl.SelectedTab == LocalisationPage)
                UpdateProvinceLocalisationPage();

        }
        public void UpdateChangesTab()
        {
            MergeChanges();
            ChangesLayoutPanel.Controls.Clear();
            int count = 0;
            foreach (VariableChange change in GlobalVariables.Changes)
            {
                if (count == 30)
                    break;
                count++;
                GroupBox gb = new GroupBox();
                if (change.Object is Province)
                {
                    Province cp = change.Object as Province;
                    gb.Text = "Province " + cp.ID;
                }
                else if (change.Object is Country)
                {
                    Country ct = change.Object as Country;
                    gb.Text = "Country " + ct.Tag;
                }
                gb.Size = new Size(507, 57);
                ChangesLayoutPanel.Controls.Add(gb);

                Label varlab = new Label();
                varlab.AutoSize = true;
                varlab.Location = new Point(6, 16);
                varlab.Text = "Variable: " + change.VariableName;
                gb.Controls.Add(varlab);

                Label oldlab = new Label();
                oldlab.Location = new Point(126, 16);
                oldlab.AutoSize = true;
                if (change.PreviousValue != null)
                {

                    if (change.PreviousValue is Religion)
                        oldlab.Text = "Old value: " + (change.PreviousValue as Religion).Name;
                    else if (change.PreviousValue is Country)
                        oldlab.Text = "Old value: " + (change.PreviousValue as Country).Tag;
                    else if (change.PreviousValue is TradeGood)
                        oldlab.Text = "Old value: " + (change.PreviousValue as TradeGood).ReadableName;
                    else if (change.PreviousValue is Tradenode)
                        oldlab.Text = "Old value: " + (change.PreviousValue as Tradenode).Name;
                    else if (change.PreviousValue is Region)
                        oldlab.Text = "Old value: " + (change.PreviousValue as Region).Name;
                    else if (change.PreviousValue is Area)
                        oldlab.Text = "Old value: " + (change.PreviousValue as Area).Name;
                    else if (change.PreviousValue is Continent)
                        oldlab.Text = "Old value: " + (change.PreviousValue as Continent).Name;
                    else if (change.PreviousValue is Government)
                        oldlab.Text = "Old value: " + (change.PreviousValue as Government).Type;
                    else if (change.PreviousValue is Building)
                        oldlab.Text = "Old value: " + (change.PreviousValue as Building).Name;
                    else
                        oldlab.Text = "Old value: " + change.PreviousValue.ToString();
                }
                else
                    oldlab.Text = "Old value: Null";
                gb.Controls.Add(oldlab);

                Label newlab = new Label();
                newlab.AutoSize = true;
                newlab.Location = new Point(126, 36);
                if (change.CurrentValue != null)
                {
                    if (change.CurrentValue is Religion)
                        newlab.Text = "New value: " + (change.CurrentValue as Religion).Name;
                    else if (change.CurrentValue is Country)
                        newlab.Text = "New value: " + (change.CurrentValue as Country).Tag;
                    else if (change.CurrentValue is TradeGood)
                        newlab.Text = "New value: " + (change.CurrentValue as TradeGood).ReadableName;
                    else if (change.CurrentValue is Tradenode)
                        newlab.Text = "New value: " + (change.CurrentValue as Tradenode).Name;
                    else if (change.CurrentValue is Region)
                        newlab.Text = "New value: " + (change.CurrentValue as Region).Name;
                    else if (change.CurrentValue is Area)
                        newlab.Text = "New value: " + (change.CurrentValue as Area).Name;
                    else if (change.CurrentValue is Continent)
                        newlab.Text = "New value: " + (change.CurrentValue as Continent).Name;
                    else if (change.CurrentValue is Government)
                        newlab.Text = "New value: " + (change.CurrentValue as Government).Type;
                    else if (change.CurrentValue is Building)
                        newlab.Text = "New value: " + (change.CurrentValue as Building).Name;
                    else
                        newlab.Text = "New value: " + change.CurrentValue.ToString();
                }
                else
                    newlab.Text = "New value: Null";
                gb.Controls.Add(newlab);

                Button keep = new Button();
                keep.Location = new Point(348, 10);
                keep.Text = "Keep";
                keep.Size = new Size(75, 41);
                keep.Tag = GlobalVariables.Changes.IndexOf(change);
                keep.Click += KeepAndSave;

                gb.Controls.Add(keep);

                //TODO
                //REVERTING CHANGES DOESN'T DO EVERYTHING
                //EXAMPLE: REVERTING OWNER DOESN'T REMOVE THE PROVINE FROM COUNTRY


                Button revert = new Button();
                revert.Location = new Point(428, 10);
                revert.Text = "Revert";
                revert.Size = new Size(75, 41);
                revert.Tag = GlobalVariables.Changes.IndexOf(change);
                revert.Click += Revert;
                gb.Controls.Add(revert);

                if (change.VariableName == "Owner tag" || change.VariableName == "Controller tag" ||
                    change.VariableName == "Discovered by tag" || change.VariableName == "Core tag" ||
                    change.VariableName == "Claim tag")
                    revert.Enabled = false;

            }
        }
        void UpdateTotalSelectedLabel()
        {
            MacroTotalSelected.Text = $"Selected: {GlobalVariables.ClickedProvinces.Count}/{GlobalVariables.Provinces.Count} ({((double)GlobalVariables.ClickedProvinces.Count / GlobalVariables.Provinces.Count).ToString("p")})";
        }
        public void RefreshTradeGoodsTab()
        {
            foreach (GroupBox gb in TradeGoodsInfoPanel.Controls)
            {
                TradeGood tg = ((TradeGood)gb.Tag);
                gb.Controls[1].Text = "Provinces:" + tg.TotalProvinces;
                gb.Controls[2].Text = "Share: " + RoundUp(((double)tg.TotalProvinces / GlobalVariables.TotalUsableProvinces), 2) * 100 + "%";
                if (tg.TotalProvinces > 0)
                    gb.Controls[3].Text = "Avg. Dev: " + RoundUp(((double)tg.TotalDev / tg.TotalProvinces), 1);
                else
                    gb.Controls[3].Text = "Avg. Dev: 0";
            }
        }
        public void UpdateCoresPanel()
        {
            CoresPanel.Controls.Clear();
            if (GlobalVariables.ClickedProvinces.Any())
            {
                List<string> added = new List<string>() { };
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    foreach (string core in p.GetCores())
                    {
                        if (!added.Contains(core))
                        {
                            added.Add(core);
                            Label corel = new Label();
                            corel.Text = core;
                            corel.MouseClick += CoreClick;
                            CoresPanel.Controls.Add(corel);
                            corel.Tag = core;
                            corel.BackColor = Color.DarkGray;
                            corel.ForeColor = Color.White;
                            corel.TextAlign = ContentAlignment.MiddleCenter;
                            corel.AutoSize = true;
                        }
                    }
                    foreach (string claim in p.GetClaims())
                    {
                        Label claiml = new Label();
                        claiml.Text = claim;
                        claiml.MouseClick += ClaimClick;
                        CoresPanel.Controls.Add(claiml);
                        claiml.Tag = claim;
                        claiml.BackColor = Color.Goldenrod;
                        claiml.ForeColor = Color.Black;
                        claiml.TextAlign = ContentAlignment.MiddleCenter;
                        claiml.AutoSize = true;
                    }
                }
            }
        }
        public void UpdateSavesTab()
        {
            SaveFilesPanel.Controls.Clear();

            int count = 0;
            foreach (object obj in GlobalVariables.Saves)
            {
                if (count == 30)
                    break;
                count++;
                string name = "";
                string path = "";
                string path2 = "";
                if (obj is Province)
                {
                    Province p = obj as Province;
                    name = "Province " + p.ID;
                    path = p.HistoryFile.Path;
                }
                else if (obj is Country)
                {
                    Country c = obj as Country;
                    name = "Country " + c.Tag;
                    path = c.HistoryFile.Path;
                    path2 = c.CommonFile.Path;
                }
                else if(obj is Saving.SpecialSavingObject)
                {
                    Saving.SpecialSavingObject so = obj as Saving.SpecialSavingObject;
                    switch (so.Type)
                    {
                        case Saving.SpecialSavingObject.SavingType.Area:
                            name = "Area file";
                            path = GlobalVariables.pathtomod + "map\\area.txt";
                            break;
                        case Saving.SpecialSavingObject.SavingType.Region:
                            name = "Region file";
                            path = GlobalVariables.pathtomod + "map\\region.txt";
                            break;
                        case Saving.SpecialSavingObject.SavingType.Continent:
                            name = "Continent file";
                            path = GlobalVariables.pathtomod + "map\\continent.txt";
                            break;
                        case Saving.SpecialSavingObject.SavingType.Superregion:
                            name = "Superregion file";
                            path = GlobalVariables.pathtomod + "map\\superregion.txt";
                            break;
                        case Saving.SpecialSavingObject.SavingType.TradeCompany:
                            name = "Trade company files";
                            path = GlobalVariables.pathtomod + "common\\tradecompanies\\";
                            break;
                        case Saving.SpecialSavingObject.SavingType.TagFile:
                            name = "Tag files";
                            path = GlobalVariables.pathtomod + "common\\country_tags\\";
                            break;
                        case Saving.SpecialSavingObject.SavingType.Climate:
                            name = "Climate file";
                            path = GlobalVariables.pathtomod + "map\\climate.txt";
                            break;
                        case Saving.SpecialSavingObject.SavingType.TradeNode:
                            name = "Tradenode files";
                            path = GlobalVariables.pathtomod + "common\\tradenodes\\";
                            break;
                    }
                }

                GroupBox gb = new GroupBox();
                gb.Text = name;
                gb.Size = new Size(507, 37);
                SaveFilesPanel.Controls.Add(gb);

                Label pathl = new Label();
                pathl.AutoSize = true;
                pathl.Location = new Point(8, 16);
                pathl.Text = "Path: " + path.Replace(GlobalVariables.pathtomod, "");
                pathl.Click += OpenFile;
                pathl.Tag = path;
                gb.Controls.Add(pathl);

                if (path2 != "")
                {
                    Label pathl2 = new Label();
                    pathl2.AutoSize = true;
                    pathl2.Location = new Point(140, 16);
                    pathl2.Text = "Path: " + path2.Replace(GlobalVariables.pathtomod, "");
                    pathl2.Click += OpenFile;
                    pathl2.Tag = path2;
                    gb.Controls.Add(pathl2);
                }

                Button save = new Button();
                save.Location = new Point(303, 10);
                save.Text = "Save";
                save.Size = new Size(75, 23);
                save.Tag = GlobalVariables.Saves.IndexOf(obj);
                save.Click += SaveFile;
                gb.Controls.Add(save);

                Button load = new Button();
                load.Location = new Point(384, 10);
                load.Text = "Load again";
                load.Size = new Size(115, 23);
                load.Tag = GlobalVariables.Saves.IndexOf(obj);
                load.Click += LoadFileAgain;
                gb.Controls.Add(load);


            }
        }
        public void UpdateDiscoveredBy()
        {
            //TODO
            //Make this faster
            DiscoveredByPanel.Controls.Clear();
            if (GlobalVariables.ClickedProvinces.Any())
            {
                List<string> added = new List<string>() { };

                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    foreach (string tech in p.GetDiscoveredBy())
                    {
                        if (!added.Contains(tech))
                        {
                            added.Add(tech);
                            Label techl = new Label();
                            techl.Text = tech;
                            techl.MouseClick += TechClick;
                            DiscoveredByPanel.Controls.Add(techl);
                            techl.Tag = tech;
                            techl.BackColor = Color.DarkGray;
                            techl.ForeColor = Color.White;
                            techl.TextAlign = ContentAlignment.MiddleCenter;
                            techl.AutoSize = true;
                        }
                    }
                }
            }
        }
        public void UpdateBuildings()
        {
            BuildingsPanel.Controls.Clear();
            if (GlobalVariables.ClickedProvinces.Any())
            {
                List<Building> added = new List<Building>() { };

                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    foreach (Building bl in p.GetBuildings())
                    {
                        if (!added.Contains(bl))
                        {
                            added.Add(bl);
                            Label bll = new Label();
                            bll.Text = bl.Name;
                            bll.MouseClick += BuildingClick;
                            BuildingsPanel.Controls.Add(bll);
                            bll.Tag = bl;
                            bll.BackColor = Color.DarkGray;
                            bll.ForeColor = Color.White;
                            bll.TextAlign = ContentAlignment.MiddleCenter;
                            bll.AutoSize = true;
                        }
                    }
                }
            }
        }

        public void UpdateTradeNodesPage()
        {
            if (TradeNodeBox.SelectedIndex > 0)
            {
                Tradenode tn = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1];
                ChangeTradeNodeNameBox.Text = tn.Name;
                ChangeTradeNodeColorButton.BackColor = tn.Color;
                AddTradeNodeDestinationBox.Items.Clear();
                TradeNodeDestinationsBox.Controls.Clear();

                foreach (Tradenode tr in GlobalVariables.TradeNodes)
                {
                    if (!tn.Destination.Any(x => x.TradeNode == tr) && tn != tr && !tn.Incoming.Contains(tr))
                        AddTradeNodeDestinationBox.Items.Add(tr.Name);
                    else if (tn.Destination.Any(x => x.TradeNode == tr))
                    {
                        Label tnl = new Label();
                        tnl.Text = tr.Name;
                        tnl.MouseClick += TradeNodeClick;
                        TradeNodeDestinationsBox.Controls.Add(tnl);
                        tnl.Tag = GlobalVariables.TradeNodes.IndexOf(tr);
                        tnl.BackColor = Color.DarkGray;
                        tnl.ForeColor = Color.White;
                        tnl.TextAlign = ContentAlignment.MiddleCenter;
                        tnl.AutoSize = true;
                    }
                }
                if (tn.Inland)
                    TradeNodeInlandCheckbox.Checked = true;
                else
                    TradeNodeInlandCheckbox.Checked = false;
                TradeNodeProvinceLocationBox.Text = "";
                if (tn.Location != null)
                    TradeNodeProvinceLocationBox.Text = tn.Location.ID + "";

                TradeNodeProvincesBox.Text = tn.Provinces.Count() + "";

            }
            else
            {
                ChangeTradeNodeNameBox.Text = "";
                ChangeTradeNodeColorButton.BackColor = Color.Transparent;
                AddTradeNodeDestinationBox.Items.Clear();
                TradeNodeDestinationsBox.Controls.Clear();
                TradeNodeInlandCheckbox.Checked = false;
                TradeNodeProvinceLocationBox.Text = "";
                TradeNodeProvincesBox.Text = "";
            }
        }

        public void UpdateCountryListUsingControls()
        {
            
            OwnerBox.Items.Clear();
            OwnerBox.Items.AddRange(GlobalVariables.Countries.ToArray());
            OwnerBox.Sorted = true;

            ControllerBox.Items.Clear();
            ControllerBox.Items.AddRange(GlobalVariables.Countries.ToArray());
            ControllerBox.Sorted = true;

            CountryBox.Items.Clear();
            CountryBox.Items.AddRange(GlobalVariables.Countries.ToArray());
            CountryBox.Sorted = true;

            AddCoreBox.Items.Clear();
            AddCoreBox.Items.AddRange(GlobalVariables.Countries.ToArray());
            AddCoreBox.Sorted = true;
            
        }

        public void UpdateCountryPage()
        {
            if (CountryBox.SelectedIndex != 0)
            {
                MoveToCapitalButton.Enabled = true;
                SelectAllProvincesButton.Enabled = true;
                CountryCapitalIDBox.Enabled = true;
                SaveCapitalIDButton.Enabled = true;
                CapitalSetClickedButton.Enabled = true;
                CountryReligionBox.Enabled = true;
                TechnologyGroupBox.Enabled = true;
                CountryPrimaryCultureBox.Enabled = true;
                GovernmentTypeBox.Enabled = true;
                GovernmentReformBox.Enabled = true;
                GovernmentRankNumeric.Enabled = true;
                GraphicalCultureBox.Enabled = true;
                CountryTagBox.Enabled = true;
                CountryNameBox.Enabled = true;

                if (GlobalVariables.SelectedCountry != null)
                {
                    CountryNameBox.Text = GlobalVariables.SelectedCountry.FullName;
                    CountryTagBox.Text = GlobalVariables.SelectedCountry.Tag;
                    TotalDevelopmentBox.Text = GlobalVariables.SelectedCountry.TotalDevelopment.ToString();
                    CountryProvinceCountBox.Text = GlobalVariables.SelectedCountry.Provinces.Count().ToString();
                    if (GlobalVariables.SelectedCountry.Capital != null)
                        CountryCapitalIDBox.Text = GlobalVariables.SelectedCountry.Capital.ID.ToString();
                    else
                        CountryCapitalIDBox.Text = "";

                    if (GlobalVariables.SelectedCountry.Religion != Religion.NoReligion)
                        ChangeValueInternally(CountryReligionBox, GlobalVariables.SelectedCountry.Religion);
                    else
                        ChangeValueInternally(CountryReligionBox, Religion.NoReligion);

                    if (GlobalVariables.SelectedCountry.TechnologyGroup != "")
                        ChangeValueInternally(TechnologyGroupBox, GlobalVariables.SelectedCountry.TechnologyGroup);
                    else
                        ChangeValueInternally(CountryReligionBox, 0);

                    if (GlobalVariables.SelectedCountry.PrimaryCulture != Culture.NoCulture)
                        ChangeValueInternally(CountryPrimaryCultureBox, GlobalVariables.SelectedCountry.PrimaryCulture);
                    else
                        ChangeValueInternally(CountryPrimaryCultureBox, Culture.NoCulture);

                    if (GlobalVariables.SelectedCountry.Capital != null)
                        ChangeValueInternally(CountryCapitalIDBox, GlobalVariables.SelectedCountry.Capital.ID.ToString());
                    else
                        ChangeValueInternally(CountryCapitalIDBox, "");

                    if (GlobalVariables.SelectedCountry.Government != null)
                    {
                        ChangeValueInternally(GovernmentTypeBox, GovernmentTypeBox.Items.IndexOf(GlobalVariables.SelectedCountry.Government.Type));
                        ChangeValueInternally(GovernmentReformBox, GovernmentReformBox.Items.IndexOf(GlobalVariables.SelectedCountry.GovernmentReform));
                    }
                    else
                        GovernmentTypeBox.SelectedIndex = 0;

                    ChangeValueInternally(GraphicalCultureBox, GlobalVariables.SelectedCountry.GraphicalCulture);

                    ChangeValueInternally(GovernmentRankNumeric, GlobalVariables.SelectedCountry.GovernmentRank);


                    SaveCountryAdj.Enabled = true;
                    SaveCountryName.Enabled = true;

                    if (GlobalVariables.ModLocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag))
                        CountryNameLocalisationBox.Text = GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag];
                    else if (GlobalVariables.LocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag))
                        CountryNameLocalisationBox.Text = GlobalVariables.LocalisationEntries[GlobalVariables.SelectedCountry.Tag];
                    else
                        CountryNameLocalisationBox.Text = "";

                    if (GlobalVariables.ModLocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag + "_ADJ"))
                        CountryAdjLocalisationBox.Text = GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"];
                    else if (GlobalVariables.LocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag + "_ADJ"))
                        CountryAdjLocalisationBox.Text = GlobalVariables.LocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"];
                    else
                        CountryAdjLocalisationBox.Text = "";
                    if (Tabs.SelectedTab == CountryPage && NamesTabs.SelectedTab == MonarchNamesTab)
                        UpdateMonarchNames();
                    //if (Tabs.SelectedTab == CountryPage && NamesTabs.SelectedTab == LeaderNamesTab)
                    UpdateNames(UpdateNamesOptions.Leader);
                    UpdateNames(UpdateNamesOptions.Ship);
                    UpdateNames(UpdateNamesOptions.Army);
                    UpdateNames(UpdateNamesOptions.Fleet);
                }

            }
            else
            {
                MoveToCapitalButton.Enabled = false;
                SelectAllProvincesButton.Enabled = false;
                CountryCapitalIDBox.Enabled = false;
                SaveCapitalIDButton.Enabled = false;
                CapitalSetClickedButton.Enabled = false;
                CountryReligionBox.Enabled = false;
                TechnologyGroupBox.Enabled = false;
                CountryPrimaryCultureBox.Enabled = false;
                GovernmentTypeBox.Enabled = false;
                GovernmentReformBox.Enabled = false;
                GovernmentRankNumeric.Enabled = false;
                GraphicalCultureBox.Enabled = false;
                SaveCountryAdj.Enabled = true;
                SaveCountryName.Enabled = true;
                CountryNameBox.Text = "";
                CountryTagBox.Text = "";
                CountryTagBox.Enabled = false;
                CountryNameBox.Enabled = false;
            }
        }

        public List<TextBox> MonarchNamesTXTBOXES = new List<TextBox>();

        public void UpdateMonarchNames()
        {
            MonarchNamePanel.Controls.Clear();
            textboxes.RemoveAll(x => MonarchNamesTXTBOXES.Contains(x));
            MonarchNamesTXTBOXES.Clear();
            if (GlobalVariables.SelectedCountry != null)
            { 
                foreach(MonarchName mn in GlobalVariables.SelectedCountry.MonarchNames)
                {
                    Panel p = new Panel();
                    p.Width = 240;
                    p.Height = 30;
                    p.Tag = mn;
                    MonarchNamePanel.Controls.Add(p);

                    TextBox tbname = new TextBox
                    {
                        Width = 140,
                        Height = 20,
                        Location = new Point(3, 5),
                        Tag = "name",
                        Text = mn.Name,               
                    };
                    tbname.LostFocus += FocusLost;
                    MonarchNamesTXTBOXES.Add(tbname);
                    p.Controls.Add(tbname);

                    TextBox tbchance = new TextBox
                    {
                        Width = 42,
                        Height = 20,
                        Location = new Point(150, 5),
                        Tag = "chance",
                        Text = mn.Chance.ToString(),

                    };
                    tbchance.LostFocus += FocusLost;
                    MonarchNamesTXTBOXES.Add(tbchance);
                    p.Controls.Add(tbchance);

                    Button b = new Button
                    {
                        Width = 34,
                        Height = 23,
                        Text = "X",
                        Location = new Point(200, 3),
                        Tag = mn
                    };
                    b.Click += RemoveMonarchNameClick;
                    p.Controls.Add(b);
                }
                textboxes.AddRange(MonarchNamesTXTBOXES);
            }
        }

        public enum UpdateNamesOptions { Leader, Ship, Army, Fleet }

        public void UpdateNames(UpdateNamesOptions option)
        {
            switch (option)
            {
                case UpdateNamesOptions.Leader:
                    if (GlobalVariables.SelectedCountry != null)
                    {
                        string towrite = "";
                        foreach (string name in GlobalVariables.SelectedCountry.LeaderNames)
                            towrite += name + ", ";
                        LeaderNamesBox.Text = towrite;
                    }
                    break;
                case UpdateNamesOptions.Ship:
                    if (GlobalVariables.SelectedCountry != null)
                    {
                        string towrite = "";
                        foreach (string name in GlobalVariables.SelectedCountry.ShipNames)
                            towrite += name + ", ";
                        ShipNamesBox.Text = towrite;
                    }
                    break;
                case UpdateNamesOptions.Army:
                    if (GlobalVariables.SelectedCountry != null)
                    {
                        string towrite = "";
                        foreach (string name in GlobalVariables.SelectedCountry.ArmyNames)
                            towrite += name + ", ";
                        ArmyNamesBox.Text = towrite;
                    }
                    break;
                case UpdateNamesOptions.Fleet:
                    if (GlobalVariables.SelectedCountry != null)
                    {
                        string towrite = "";
                        foreach (string name in GlobalVariables.SelectedCountry.FleetNames)
                            towrite += name + ", ";
                        FleetNamesBox.Text = towrite;
                    }
                    break;
            }
        }

        public void AutoSizeTextbox(object sender, EventArgs e)
        {
            TextBox pSender = (TextBox)sender;
            pSender.Width = (int)graphics.MeasureString(pSender.Text, pSender.Font).Width + 5;
        }
        #endregion

        #region Focus Gain
        public void GainFocus(object sender, EventArgs e)
        {
            //(sender as Control).Focus();
        }
        #endregion

        #region Click Handlers
        void MouseClickHandler(object sender, MouseEventArgs e)
        {
            //TODO
            //when scaling the game don't forget about this
            if (sender == this)
            {
                if (e.Location.X > 40 && e.Location.X < GlobalVariables.MapDrawingWidth + 40 && e.Location.Y > 40 && e.Location.Y < GlobalVariables.MapDrawingHeight + 40)
                {
                    Point truePosition = new Point(e.Location.X - 40 + GlobalVariables.CameraPosition.X, e.Location.Y - 40 + GlobalVariables.CameraPosition.Y);
                    Color c = GlobalVariables.ProvincesMapBitmap.GetPixel(truePosition.X, truePosition.Y);
                    Province p = GlobalVariables.CubeArray[c.R, c.G, c.B];
                    if (p != null)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            if (!GlobalVariables.ClickedProvinces.Contains(p))
                            {
                                if (p.HistoryFile != null)
                                {
                                    AddToClickedProvinces(p);
                                }
                            }
                            else
                            {
                                RemoveFromClickedProvinces(p);
                            }
                        }
                        else
                        {
                            if (GlobalVariables.TradeDestClickingMode)
                            {
                                GlobalVariables.TradeDestClickingMode = false;
                                if (GlobalVariables.mapmode != MapManagement.UpdateMapOptions.TradeNode)
                                    return;
                                if (p.TradeNode == null)
                                    return;
                                if (TradeNodeBox.SelectedIndex == 0)
                                    return;
                                AddTradeDestination(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1], p.TradeNode);
                            }
                            else
                            {
                                AddAndRemoveFromClickedProvinces(GlobalVariables.ClickedProvinces.ToList(), new List<Province> { p });
                                //BorderingDebugButton.Text = GlobalVariables.ClickedProvinces.Count + "";
                            }
                           
                        }
                    }
                    //UpdateMap();
                }
                
            }
            this.ActiveControl = null;
        }
        public void SelectAllOfTradeGood(object sender, EventArgs e)
        {
            TradeGood tg = (TradeGood)((Control)sender).Tag;
            if (!(GlobalVariables.PressedKeys.Contains(Keys.Shift.GetHashCode()) || GlobalVariables.PressedKeys.Contains(Keys.Control.GetHashCode())))
            {
                MapManagement.UpdateClickedMap(GlobalVariables.ClickedProvinces, Color.White, false);
                GlobalVariables.ClickedProvinces.Clear();
            }
            List<Province> toAdd = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.TradeGood == tg)
                    toAdd.Add(p);
            }
            AddToClickedProvinces(toAdd);
            UpdateMap();
        }
        private void CapitalSetClickedButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ClickedProvinces.Count == 1)
            {
                CountryCapitalIDBox.Text = GlobalVariables.ClickedProvinces[0].ID.ToString();
                if (CountryCapitalIDBox.Text.Any(x => !nums.Contains(x)))
                {
                    CountryCapitalIDBox.Text = CountryCapitalIDBox.Text.Where(x => nums.Contains(x)).ToString();
                }

                if (GlobalVariables.SelectedCountry != null)
                {
                    if (CountryCapitalIDBox.Text != "")
                    {
                        int n = int.Parse(CountryCapitalIDBox.Text);
                        Province p = GlobalVariables.Provinces.Find(x => x.ID == n);
                        if (p != null)
                        {
                            GlobalVariables.SelectedCountry.CapitalID = int.Parse(CountryCapitalIDBox.Text);
                            GlobalVariables.SelectedCountry.Capital = p;
                            //GlobalVariables.ToUpdate.Add(GlobalVariables.SelectedCountry);

                            MapManagement.UpdateMap(GlobalVariables.SelectedCountry.Provinces, MapManagement.UpdateMapOptions.Political);
                            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Political)
                                UpdateMap();

                            //Saving.SaveThingsToUpdate();

                        }
                    }
                }
            }

        }
        private void ReloadMapsButton_Click(object sender, EventArgs e)
        {
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Development);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.TradeGood);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Religion);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Culture);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Political);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Government);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Area);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Region);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Superregion);
            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();
        }
        private void RemoveDevButton_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.ClearDev();
            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();
        }
        private void DevRemoveAll_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.RemoveAll();
            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void DevIncreaseAll_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.DevIncreaseAll();
            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void RandomDevLow_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.RandomLowDev();
            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void RandomDevMed_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.RandomMedDev();
            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void RandomDevHigh_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.RandomHighDev();
            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void SelectAllProvincesButton_Click(object sender, EventArgs e)
        {

            if (GlobalVariables.SelectedCountry != null)
            {
                if (GlobalVariables.SelectedCountry.Provinces.Any())
                {
                    GlobalVariables.ClickedProvinces.Clear();
                    if (GlobalVariables.SelectedCountry.Provinces.Count > 1)
                    {
                        GlobalVariables.ClickedProvinces.AddRange(GlobalVariables.SelectedCountry.Provinces);
                        GlobalVariables.ClickedProvinces.RemoveAt(0);
                    }
                    AddToClickedProvinces(GlobalVariables.SelectedCountry.Provinces[0]);
                    if (GlobalVariables.SelectedCountry.Capital != null)
                        MoveCameraTo(GlobalVariables.SelectedCountry.Capital);
                    else
                        MoveCameraTo(GlobalVariables.SelectedCountry.Provinces[0]);
                }
            }

        }
        private void MoveToCapitalButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry != null)
            {
                if (GlobalVariables.SelectedCountry.Capital != null)
                {
                    MoveCameraTo(GlobalVariables.SelectedCountry.Capital);
                    AddAndRemoveFromClickedProvinces(GlobalVariables.ClickedProvinces.ToList(), new List<Province> { GlobalVariables.SelectedCountry.Capital });
                }
                else
                {
                    MoveCameraTo(GlobalVariables.SelectedCountry.Provinces[0]);
                    AddAndRemoveFromClickedProvinces(GlobalVariables.ClickedProvinces.ToList(), new List<Province> { GlobalVariables.SelectedCountry.Provinces[0]});
                }
            }
        }
        private void CreateCountryButton_Click(object sender, EventArgs e)
        {
            //TODO
            //Expand on this
            Eu4ModEditor.CreateCountryForm countryForm = new CreateCountryForm();
            countryForm.ShowDialog();
            if (!countryForm.Canceled)
            {

                if (!GlobalVariables.ModCountryTagsFiles.Any())
                {
                    NodeFile tagfile = new NodeFile(GlobalVariables.pathtomod + "common\\country_tags\\00_modeditor_countries.txt");
                    if (tagfile.LastStatus.HasError)
                    {
                        MessageBox.Show($"File '{tagfile.Path}' has an error in line {tagfile.LastStatus.LineError}");
                        return;
                    }
                    GlobalVariables.ModCountryTagsFiles.Add(tagfile);
                }

                Country c = new Country();
                NodeFile history = new NodeFile(GlobalVariables.pathtomod + "history\\countries\\" + countryForm.Tag + " - " + countryForm.Name + ".txt");
                c.HistoryFile = history;
                NodeFile common = new NodeFile(GlobalVariables.pathtomod + "common\\countries\\" + countryForm.Name + ".txt");
                c.CommonFile = common;
                history.MainNode.AddVariable("government", GlobalVariables.Governments[0].Type);
                history.MainNode.AddVariable("add_government_reform", GlobalVariables.Governments[0].reforms[0]);
                history.MainNode.AddVariable("government_rank", "1");
                history.MainNode.AddVariable("technology_group", GlobalVariables.TechGroups[0]);
                common.MainNode.AddVariable("graphical_culture", "westerngfx");
                Node color = new Node("color");
                color.AddPureValue(countryForm.CountryColor.R+"");
                color.AddPureValue(countryForm.CountryColor.G+"");
                color.AddPureValue(countryForm.CountryColor.B+"");
                common.MainNode.AddNode(color);                              
                
                GlobalVariables.ModCountryTagsFiles[0].MainNode.AddVariable(countryForm.Tag, $"\"countries/{countryForm.Name}.txt\"");
                GlobalVariables.ModCountryTagsFiles[0].SaveFile(GlobalVariables.ModCountryTagsFiles[0].Path);
                c.Color = countryForm.CountryColor;
                c.FullName = countryForm.Name;
                c.Government = GlobalVariables.Governments[0];
                c.GovernmentReform = c.Government.reforms[0];
                c.Tag = countryForm.Tag;
                c.TechnologyGroup = "western";
                GlobalVariables.Countries.Add(c);


                OwnerBox.Items.Add(c);
                ControllerBox.Items.Add(c);
                AddCoreBox.Items.Add(c);
                CountryBox.Items.Add(c);
            }
        }
        private void SaveCapitalIDButton_Click(object sender, EventArgs e)
        {
            if (CountryCapitalIDBox.Text.Any(x => !nums.Contains(x)))
            {
                CountryCapitalIDBox.Text = CountryCapitalIDBox.Text.Where(x => nums.Contains(x)).ToString();
            }

            if (GlobalVariables.SelectedCountry != null)
            {
                if (CountryCapitalIDBox.Text != "")
                {
                    int n = int.Parse(CountryCapitalIDBox.Text);
                    Province p = GlobalVariables.Provinces.Find(x => x.ID == n);
                    if (p != null)
                    {
                        GlobalVariables.SelectedCountry.CapitalID = int.Parse(CountryCapitalIDBox.Text);
                        GlobalVariables.SelectedCountry.Capital = p;
                        //GlobalVariables.ToUpdate.Add(GlobalVariables.SelectedCountry);

                        MapManagement.UpdateMap(GlobalVariables.SelectedCountry.Provinces, MapManagement.UpdateMapOptions.Political);
                        if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Political)
                            UpdateMap();

                        //Saving.SaveThingsToUpdate();

                    }
                }
            }
            form.Focus();
        }
        private void ShowHideSeaTilesAreaMapmode_Click(object sender, EventArgs e)
        {
            GlobalVariables.ShowSeaTilesAreaMapmode = !GlobalVariables.ShowSeaTilesAreaMapmode;
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Area);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Region);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Continent);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Superregion);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Area || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Region || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Superregion)
                UpdateMap();
        }
        private void AreaNameChangeSave_Click(object sender, EventArgs e)
        {
            AreaNameChange();
        }
        private void AddNewArea_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Areas.Any(x => x.Name == AddNewAreaBox.Text))
                AddNewAreaBox.Text = "Already taken!";
            else
            {
                Area a = new Area(AddNewAreaBox.Text);
                AreaBox.Items.Add(a.Name);

                if (GlobalVariables.ClickedProvinces.Any())
                {
                    int index = AreaBox.Items.Count - 2;
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        p.Area = GlobalVariables.Areas[index];
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Area);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Area)
                        UpdateMap();
                    //Saving.SaveThingsToUpdate();
                }
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Area))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Area));
                AddNewAreaBox.Text = "";
            }
        }
        private void RegionNameChangeSave_Click(object sender, EventArgs e)
        {
            RegionNameChange();
        }
        private void AddNewRegion_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Regions.Any(x => x.Name == AddNewRegionBox.Text))
                AddNewRegionBox.Text = "Already taken!";
            else
            {
                Region a = new Region(AddNewRegionBox.Text);
                RegionBox.Items.Add(a.Name);
                List<Province> provincestoupdate = new List<Province>();
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    int index = RegionBox.Items.Count - 2;
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        if (p.Area != null)
                        {
                            provincestoupdate.AddRange(p.Area.Provinces);
                            if (p.Area.Region != null)
                            {
                                p.Area.Region.Areas.Remove(p.Area);

                            }
                            p.Area.Region = GlobalVariables.Regions[index];
                            GlobalVariables.Regions[index].Areas.Add(p.Area);
                        }
                    }
                    provincestoupdate = provincestoupdate.Distinct().ToList();
                    MapManagement.UpdateMap(provincestoupdate, MapManagement.UpdateMapOptions.Region);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Region)
                        UpdateMap();
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Region))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Region));
                    //Saving.SaveThingsToUpdate();
                }

                AddNewRegionBox.Text = "";
            }
        }
        
        private void OpenWordCreator_Click(object sender, EventArgs e)
        {
            //TODO
            //Make this engine better
            LanguageWindow lw = new LanguageWindow();
            lw.Show();
        }
        private void AddNewTradeNodeButton_Click(object sender, EventArgs e)
        {
            if (TradeNodeNameBox.Text == "" || TradeNodeNameBox.Text == " ")
                return;
            if (TradeNodeColorButton.BackColor == Color.Transparent)
                return;
            if (GlobalVariables.TradeNodes.Any(x => x.Name == TradeNodeNameBox.Text))
                return;

            Tradenode tn = new Tradenode();
            tn.Name = TradeNodeNameBox.Text.ToLower().Replace(' ', '_');
            tn.Color = TradeNodeColorButton.BackColor;
            GlobalVariables.TradeNodes.Add(tn);
            TradeNodeBox.Items.Add(tn.Name);
            ProvinceTradeNodeBox.Items.Add(tn.Name);
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    if (p.TradeNode != null)
                        p.TradeNode.Provinces.Remove(p);
                    tn.Provinces.Add(p);
                    p.TradeNode = tn;
                }
                tn.Location = GlobalVariables.ClickedProvinces[0];
            }

            TradeNodeNameBox.Text = "";
            TradeNodeColorButton.BackColor = Color.Transparent;
            TradeNodeBox.SelectedIndex = TradeNodeBox.Items.Count - 1;

            MapManagement.UpdateMap(tn.Provinces, MapManagement.UpdateMapOptions.TradeNode);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                UpdateMap();
            if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
        }
        public void TradeNodeClick(object sender, MouseEventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            Label l = (Label)sender;
            Tradenode tn = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1];
            if (e.Button == MouseButtons.Left)
            {
                TradeNodeBox.SelectedIndex = int.Parse(l.Tag.ToString()) + 1;
            }
            else if (e.Button == MouseButtons.Right)
            {
                tn.Destination.RemoveAll(x => x.TradeNode == GlobalVariables.TradeNodes[int.Parse(l.Tag.ToString())]);
                GlobalVariables.TradeNodes[int.Parse(l.Tag.ToString())].Incoming.Remove(tn);
                TradeNodeDestinationsBox.Controls.Remove(l);
                AddTradeNodeDestinationBox.Items.Clear();
                foreach (Tradenode tr in GlobalVariables.TradeNodes)
                {
                    if (!tn.Destination.Any(x => x.TradeNode == tr) && tn != tr && !tn.Incoming.Contains(tr))
                        AddTradeNodeDestinationBox.Items.Add(tr.Name);
                }
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
            }
        }
        private void TradeNodeNameSaveButton_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            if (ChangeTradeNodeNameBox.Text == "" || ChangeTradeNodeNameBox.Text == " ")
            {
                ChangeTradeNodeNameBox.Text = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Name;
                return;
            }
            if (GlobalVariables.TradeNodes.Any(x => x.Name == ChangeTradeNodeNameBox.Text))
            {
                ChangeTradeNodeNameBox.Text = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Name;
                return;
            }
            GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Name = ChangeTradeNodeNameBox.Text.ToLower().Replace(' ', '_');
            TradeNodeBox.Items[TradeNodeBox.SelectedIndex] = ChangeTradeNodeNameBox.Text.ToLower().Replace(' ', '_');
            ProvinceTradeNodeBox.Items[TradeNodeBox.SelectedIndex] = ChangeTradeNodeNameBox.Text.ToLower().Replace(' ', '_');

            if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
        }
        private void AddTradeNodeDestinationButton_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            Tradenode tn = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1];
            Tradenode tr = GlobalVariables.TradeNodes.Find(x => x.Name.ToLower() == AddTradeNodeDestinationBox.SelectedItem.ToString().ToLower());
            AddTradeDestination(tn, tr);
        }
        private void ChangeTradeNodeRandomColorButton_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            ChangeTradeNodeColorButton.BackColor = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
            GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Color = ChangeTradeNodeColorButton.BackColor;
            MapManagement.UpdateMap(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeNode);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                UpdateMap();
            if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
        }
        private void ChangeTradeNodeColorButton_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Color = cd.Color;
                ChangeTradeNodeColorButton.BackColor = cd.Color;
                MapManagement.UpdateMap(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeNode);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                    UpdateMap();
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
            }
        }
        private void TradeNodeLocationSetAsCliecked_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                TradeNodeProvinceLocationBox.Text = GlobalVariables.ClickedProvinces[0].ID + "";
                int n = 0;
                if (!int.TryParse(TradeNodeProvinceLocationBox.Text, out n))
                {
                    if (GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location != null)
                        TradeNodeProvinceLocationBox.Text = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location.ID + "";
                    return;
                }
                GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location = GlobalVariables.Provinces.Find(x=>x.ID == n);
                MapManagement.UpdateMap(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeNode);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                    UpdateMap();
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
            }

        }
        private void TradeNodeLocationSave_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            int n = 0;
            if (!int.TryParse(TradeNodeProvinceLocationBox.Text, out n))
            {
                if (GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location != null)
                    TradeNodeProvinceLocationBox.Text = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location.ID + "";
                return;
            }
            GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location = GlobalVariables.Provinces.Find(x=>x.ID == n);
            MapManagement.UpdateMap(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeNode);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                UpdateMap();
            if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
        }
        private void TradeNodeSelectAllProvinces_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;

            AddToClickedProvinces(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Provinces);
        }
        private void RemoveTradeNodeButton_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            Tradenode tn = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1];
            GlobalVariables.TradeNodes.Remove(tn);
            List<Province> toup = new List<Province>();
            toup.AddRange(tn.Provinces);
            foreach (Province p in tn.Provinces)
                p.TradeNode = null;
            foreach (Destination ds in tn.Destination)
                ds.TradeNode.Incoming.Remove(tn);
            foreach (Tradenode tr in GlobalVariables.TradeNodes)
                tr.Destination.RemoveAll(x => x.TradeNode == tn);
            MapManagement.UpdateMap(toup, MapManagement.UpdateMapOptions.TradeNode);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                UpdateMap();
            TradeNodeBox.Items.Remove(tn.Name);
            ProvinceTradeNodeBox.Items.Remove(tn.Name);
            TradeNodeBox.SelectedIndex = 0;
            if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));

        }
        private void TradeNodeColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                TradeNodeColorButton.BackColor = cd.Color;
            }
        }
        private void TradeNodeRandomColorButton_Click(object sender, EventArgs e)
        {
            TradeNodeColorButton.BackColor = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
        }
        private void AddTradeNodeDestClickButton_Click(object sender, EventArgs e)
        {
            GlobalVariables.TradeDestClickingMode = true;
        }
        private void AddToHREButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.HRE, true);
        }
        private void RemoveFromHREButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.HRE, false);
        }
        private void AddFortButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.Fort, true);
        }
        private void RemoveFortButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.Fort, false);
        }
        private void ContinentNameChangeSave_Click(object sender, EventArgs e)
        {
            ContinentNameChange();
        }
        private void AddNewContinent_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Continents.Any(x => x.Name == AddNewContinentBox.Text))
                AddNewContinentBox.Text = "Already taken!";
            else
            {
                Continent c = new Continent(AddNewContinentBox.Text);
                ContinentBox.Items.Add(c.Name);

                if (GlobalVariables.ClickedProvinces.Any())
                {
                    int index = ContinentBox.Items.Count - 2;
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        p.Continent = GlobalVariables.Continents[index];
                        if (!GlobalVariables.ToUpdate.Contains(p))
                            GlobalVariables.ToUpdate.Add(p);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Continent);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent)
                        UpdateMap();
                    // Saving.SaveThingsToUpdate();
                }
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Continent))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Continent));
                AddNewContinentBox.Text = "";
            }
        }
        private void AddCoreButton_Click(object sender, EventArgs e)
        {
            Country c = (Country)AddCoreBox.SelectedItem;
            ChangeProvinceInfo(ChangeProvinceMode.Core, c.Tag);

            if (ProvinceTabControl.SelectedTab == MainPage)
            {
                UpdateCoresPanel();
            }
        }
        private void AddOwnerCoreButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.CoreOwner, null);
            UpdateCoresPanel();
        }
        public void CoreClick(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            if (e.Button == MouseButtons.Right)
            {
                ChangeProvinceInfo(ChangeProvinceMode.Core, l.Tag, true);
            }
            //if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
            //GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
            //Saving.SaveThingsToUpdate();

            if (ProvinceTabControl.SelectedTab == MainPage)
            {
                UpdateCoresPanel();
            }
        }
        public void ClaimClick(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            if (e.Button == MouseButtons.Right)
            {
                ChangeProvinceInfo(ChangeProvinceMode.Claim, l.Tag, true);
            }
            UpdateCoresPanel();
        }
        private void AddCenterOfTrade_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.CoT, 1);
        }
        private void RemoveCenterOfTrade_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.CoT, 0);
        }
        private void RandomIdeaBoxButton_Click(object sender, EventArgs e)
        {
            RandomIdeaBox rib = new RandomIdeaBox();
            rib.Show();
        }
        private void RefreshChanges_Click(object sender, EventArgs e)
        {
            UpdateChangesTab();
        }
        private void SaveAllChangesButton_Click(object sender, EventArgs e)
        {
            MergeChanges();
            foreach (VariableChange vc in GlobalVariables.Changes)
            {
                if (!GlobalVariables.Saves.Contains(vc.Object))
                    GlobalVariables.Saves.Add(vc.Object);
            }
            GlobalVariables.Changes.Clear();
            UpdateChangesTab();
            UpdateSavesTab();
        }
        private void RevertAllChangesButton_Click(object sender, EventArgs e)
        {
            MergeChanges();
            foreach (VariableChange vc in GlobalVariables.Changes)
            {
                if (vc.Object is Province)
                {
                    (vc.Object as Province).Variables[vc.VariableName] = vc.PreviousValue;
                }
                else if (vc.Object is Country)
                {
                    (vc.Object as Country).Variables[vc.VariableName] = vc.PreviousValue;
                }
                GlobalVariables.Changes.Remove(vc);
            }
            UpdateChangesTab();
            UpdateSavesTab();
        }
        private void SaveTradeCompanyFile_Click(object sender, EventArgs e)
        {
        }
        private void TradeCompanyRandomColor_Click(object sender, EventArgs e)
        {
            if (TradeCompanyBox.SelectedIndex == 0)
                return;
            TradeCompanyColorButton.BackColor = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
            GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Color = TradeCompanyColorButton.BackColor;
            GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].MadeChanges = true;

            if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeCompany))
                GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeCompany));

            MapManagement.UpdateMap(GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeCompany);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeCompany)
                UpdateMap();
        }
        private void SaveProvinceName_Click(object sender, EventArgs e)
        {
            if (!GlobalVariables.ClickedProvinces.Any())
                return;
            if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV" + GlobalVariables.ClickedProvinces[0].ID))
                GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceNameLocalisationBox.Text;
            else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV" + GlobalVariables.ClickedProvinces[0].ID))
            {
                if (GlobalVariables.LocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID] != ProvinceNameLocalisationBox.Text)
                    GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceNameLocalisationBox.Text;
            }
            else
                GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceNameLocalisationBox.Text;
            MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Localisation);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Localisation)
                UpdateMap();
        }
        private void SaveProvinceAdj_Click(object sender, EventArgs e)
        {
            if (!GlobalVariables.ClickedProvinces.Any())
                return;
            if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID))
                GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceAdjectiveLocalisationBox.Text;
            else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID))
            {
                if (GlobalVariables.LocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID] != ProvinceAdjectiveLocalisationBox.Text)
                    GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceAdjectiveLocalisationBox.Text;
            }
            else
                GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceAdjectiveLocalisationBox.Text;
            MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Localisation);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Localisation)
                UpdateMap();
        }
        private void SaveLocalisationButton_Click(object sender, EventArgs e)
        {
            string tosave = "";
            string filename = "";
            switch (GlobalVariables.LocalisationLanguage)
            {
                case GlobalVariables.Languages.English:
                    filename = "localisation\\mod_edt_loc_l_english.yml";
                    tosave = "l_english:\n";
                    break;
                case GlobalVariables.Languages.French:
                    filename = "localisation\\mod_edt_loc_l_french.yml";
                    tosave = "l_french:\n";
                    break;
                case GlobalVariables.Languages.German:
                    filename = "localisation\\mod_edt_loc_l_german.yml";
                    tosave = "l_german:\n";
                    break;
                case GlobalVariables.Languages.Spanish:
                    filename = "localisation\\mod_edt_loc_l_spanish.yml";
                    tosave = "l_spanish:\n";
                    break;
            }
            foreach (string key in GlobalVariables.ModLocalisationEntries.Keys)
            {
                tosave += " " + key + ": \"" + GlobalVariables.ModLocalisationEntries[key] + "\"\n";
            }
            if (!Directory.Exists(GlobalVariables.pathtomod + "localisation"))
                Directory.CreateDirectory(GlobalVariables.pathtomod + "localisation");
            File.WriteAllText(GlobalVariables.pathtomod + filename, tosave, Encoding.Default);
        }
        private void SuperregionNameChangeSave_Click(object sender, EventArgs e)
        {
            SuperregionNameChange();
        }
        private void AddNewSuperregion_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Superregions.Any(x => x.Name == AddNewSuperregionBox.Text))
                AddNewSuperregionBox.Text = "Already taken!";
            else
            {
                Superregion c = new Superregion(AddNewSuperregionBox.Text);
                SuperregionBox.Items.Add(c.Name);

                ChangeProvinceInfo(ChangeProvinceMode.Superregion, SuperregionBox.Items.Count - 2);
                AddNewSuperregionBox.Text = "";

                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Superregion))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Superregion));
            }
        }
       
        private void TradeComapnyNameChangeSave_Click(object sender, EventArgs e)
        {
            TradeCompanyNameChange();
        }
        private void AddNewTradeCompany_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.TradeCompanies.Any(x => x.Name == AddNewTradeCompanyBox.Text))
                AddNewTradeCompanyBox.Text = "Already taken!";
            else
            {
                TradeCompany c = new TradeCompany() { Name = AddNewTradeCompanyBox.Text };
                GlobalVariables.TradeCompanies.Add(c);
                TradeCompanyBox.Items.Add(c.Name);

                if (GlobalVariables.ClickedProvinces.Any())
                {
                    int index = TradeCompanyBox.Items.Count - 2;
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        if (p.TradeCompany != null)
                            p.TradeCompany.Provinces.Remove(p);
                        p.TradeCompany = GlobalVariables.TradeCompanies[index];
                        p.TradeCompany.MadeChanges = true;
                        GlobalVariables.TradeCompanies[index].Provinces.Add(p);
                        if (!GlobalVariables.ToUpdate.Contains(p))
                            GlobalVariables.ToUpdate.Add(p);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.TradeCompany);
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeCompany))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeCompany));
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeCompany)
                        UpdateMap();
                    // Saving.SaveThingsToUpdate();
                }

                AddNewTradeCompanyBox.Text = "";
            }
        }
        private void TradeCompanyColorButton_Click(object sender, EventArgs e)
        {
            if (TradeCompanyBox.SelectedIndex == 0)
                return;
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Color = cd.Color;
                GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].MadeChanges = true;
                TradeCompanyColorButton.BackColor = cd.Color;
                MapManagement.UpdateMap(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeNode);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                    UpdateMap();

            }
        }
        private void AddClaimButton_Click(object sender, EventArgs e)
        {
            int index = AddCoreBox.SelectedIndex;
            Country c = GlobalVariables.Countries[index];
            ChangeProvinceInfo(ChangeProvinceMode.Claim, c.Tag);

            if (ProvinceTabControl.SelectedTab == MainPage)
            {
                UpdateCoresPanel();
            }
        }
        private void HideSeaTiles3_Click(object sender, EventArgs e)
        {
            GlobalVariables.ShowSeaTilesAreaMapmode = !GlobalVariables.ShowSeaTilesAreaMapmode;
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Area);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Region);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Continent);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Superregion);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Area || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Region || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Superregion)
                UpdateMap();
        }
        private void HideSeaTiles_Click(object sender, EventArgs e)
        {
            GlobalVariables.ShowSeaTilesAreaMapmode = !GlobalVariables.ShowSeaTilesAreaMapmode;
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Area);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Region);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Continent);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Superregion);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Area || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Region || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Superregion)
                UpdateMap();
        }
        private void HideSeaTiles2_Click(object sender, EventArgs e)
        {
            GlobalVariables.ShowSeaTilesAreaMapmode = !GlobalVariables.ShowSeaTilesAreaMapmode;
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Area);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Region);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Continent);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Superregion);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Area || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Region || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent || GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Superregion)
                UpdateMap();
        }
        private void RefreshSavesButton_Click(object sender, EventArgs e)
        {
            UpdateSavesTab();
        }
        private void OpenProvinceFileButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ClickedProvinces[0] != null)
                Process.Start(GlobalVariables.ClickedProvinces[0].HistoryFile.Path);
        }
        private void ReloadProvinceFromFileButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ClickedProvinces.Any())
                foreach(Province p in GlobalVariables.ClickedProvinces)
                    Saving.LoadObject(p);
            if (Tabs.SelectedTab == ProvinceTab) {
                UpdateProvincePanel();
            }
        }
        private void ReloadProvinceAllMapmodesButton_Click(object sender, EventArgs e)
        {
            MapManagement.ReloadProvince(GlobalVariables.ClickedProvinces);
        }
        private void SaveAllFilesButton_Click(object sender, EventArgs e)
        {
            foreach (object obj in GlobalVariables.Saves)
            {
                Saving.SaveObject(obj);
            }
            GlobalVariables.Saves.Clear();
            UpdateSavesTab();
        }
        private void LoadAllFilesAgainButton_Click(object sender, EventArgs e)
        {
            foreach (object obj in GlobalVariables.Saves)
            {
                Saving.LoadObject(obj);
            }
            GlobalVariables.Saves.Clear();
            UpdateSavesTab();
        }
        private void OpenCountryHistoryFileButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry != null)
                Process.Start(GlobalVariables.SelectedCountry.HistoryFile.Path);
        }
        private void AddDiscoveredByButton_Click(object sender, EventArgs e)
        {
            if (DiscoveredByBox.SelectedItem.ToString() != "")
                ChangeProvinceInfo(ChangeProvinceMode.DiscoveredBy, DiscoveredByBox.SelectedItem, false);
            UpdateDiscoveredBy();
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);
        }
        private void MakeCityButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.City, true);
            if (Tabs.SelectedTab == ProvinceTab)
            {
                UpdateProvincePanel();
            }
        }
        private void RemoveCityButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.City, false);
            UpdateProvincePanel();
        }
        private void AddBuildingButton_Click(object sender, EventArgs e)
        {
            if (BuildingsBox.SelectedIndex != -1)
            {
                Building bl = GlobalVariables.Buildings.Find(x => x.Name.ToLower() == BuildingsBox.SelectedItem.ToString().ToLower());
                if (bl != null)
                    ChangeProvinceInfo(ChangeProvinceMode.Building, bl);
                UpdateBuildings();
            }
        }
        public void BuildingClick(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            if (e.Button == MouseButtons.Right)
            {
                ChangeProvinceInfo(ChangeProvinceMode.Building, (Building)l.Tag, true);
            }
            UpdateBuildings();
        }
        public void TechClick(object sender, MouseEventArgs e)
        {
            Label l = sender as Label;
            if (e.Button == MouseButtons.Right)
            {
                ChangeProvinceInfo(ChangeProvinceMode.DiscoveredBy, (string)l.Tag, true);
            }
            UpdateDiscoveredBy();
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);

        }
        private void AddOwnerDiscoveredByButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.DiscoveredByOwner, null);
            UpdateDiscoveredBy();
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);
        }
        private void SaveCountryLocalisation_Click(object sender, EventArgs e)
        {
            string tosave = "";
            string filename = "";
            switch (GlobalVariables.LocalisationLanguage)
            {
                case GlobalVariables.Languages.English:
                    filename = "localisation\\mod_edt_loc_l_english.yml";
                    tosave = "l_english:\n";
                    break;
                case GlobalVariables.Languages.French:
                    filename = "localisation\\mod_edt_loc_l_french.yml";
                    tosave = "l_french:\n";
                    break;
                case GlobalVariables.Languages.German:
                    filename = "localisation\\mod_edt_loc_l_german.yml";
                    tosave = "l_german:\n";
                    break;
                case GlobalVariables.Languages.Spanish:
                    filename = "localisation\\mod_edt_loc_l_spanish.yml";
                    tosave = "l_spanish:\n";
                    break;
            }
            foreach (string key in GlobalVariables.ModLocalisationEntries.Keys)
            {
                tosave += " " + key + ": \"" + GlobalVariables.ModLocalisationEntries[key] + "\"\n";
            }
            if (!Directory.Exists(GlobalVariables.pathtomod + "localisation"))
                Directory.CreateDirectory(GlobalVariables.pathtomod + "localisation");
            File.WriteAllText(GlobalVariables.pathtomod + filename, tosave, Encoding.Default);
        }
        private void SaveCountryName_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry == null)
                return;
            if (GlobalVariables.ModLocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag))
                GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag] = CountryNameLocalisationBox.Text;
            else if (GlobalVariables.LocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag))
            {
                if (GlobalVariables.LocalisationEntries[GlobalVariables.SelectedCountry.Tag] != CountryNameLocalisationBox.Text)
                    GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag] = CountryNameLocalisationBox.Text;
            }
            else
                GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag] = CountryNameLocalisationBox.Text;
        }
        private void SaveCountryAdj_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry == null)
                return;
            if (GlobalVariables.ModLocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag + "_ADJ"))
                GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"] = CountryAdjLocalisationBox.Text;
            else if (GlobalVariables.LocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag + "_ADJ"))
            {
                if (GlobalVariables.LocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"] != CountryAdjLocalisationBox.Text)
                    GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"] = CountryAdjLocalisationBox.Text;
            }
            else
                GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"] = CountryAdjLocalisationBox.Text;
        }
        private void StatisticsButton_Click(object sender, EventArgs e)
        {
            StatisticsForm sf = new StatisticsForm();
            sf.Show();
        }
        private void DarkmodeButton_Click(object sender, EventArgs e)
        {
            GlobalVariables.DarkMode = !GlobalVariables.DarkMode;
            ChangeDarkMode();
        }
        private void AddMonarchNameButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry != null)
            {
                if (GlobalVariables.SelectedCountry.MonarchNames.Any(x=>x.Name == MonarchNameBox.Text))
                {
                    MessageBox.Show("This name already is on the list!");
                    return;
                }
                if (MonarchNameChancesTextBox.Text.Any(x => !char.IsNumber(x) && x != '-'))
                {
                    MessageBox.Show("You can only have numbers in the chances box!");
                    return;
                }
                int n = 0;
                if (MonarchNameChancesTextBox.Text != "")
                    n = int.Parse(MonarchNameChancesTextBox.Text);
                GlobalVariables.SelectedCountry.MonarchNames.Add(new MonarchName(MonarchNameBox.Text, n));
                UpdateMonarchNames();
            }
        }
        public void RemoveMonarchNameClick(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry != null)
            {
                GlobalVariables.SelectedCountry.MonarchNames.Remove((MonarchName)(sender as Control).Tag);
            }
            UpdateMonarchNames();
        }
        #endregion

        #region Clicked Provinces
        void AddToClickedProvinces(Province p, bool Update = true)
        {
            AddToClickedProvinces(new List<Province> { p }, Update);
        }
        void AddToClickedProvinces(List<Province> p, bool Update = true)
        {
            if (p.Any())
            {
                bool onlyone = false;
                if (GlobalVariables.ClickedProvinces.Count == 0)
                {
                    if (p[0].OwnerCountry != null)
                    {
                        if (GlobalVariables.SelectedDiscoveredByTechGroup != p[0].OwnerCountry?.TechnologyGroup && GlobalVariables.mapmode == MapManagement.UpdateMapOptions.DiscoveredBy)
                        {
                            GlobalVariables.SelectedDiscoveredByTechGroup = p[0].OwnerCountry?.TechnologyGroup;
                            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);
                        }
                    }
                    onlyone = true;
                }

                List<Province> UpdateMapList = new List<Province>();
                foreach (Province pr in p)
                {
                    // if (!GlobalVariables.ToUpdate.Contains(pr))
                    //    GlobalVariables.ToUpdate.Add(pr);
                    if (!GlobalVariables.ClickedProvinces.Contains(pr))
                    {
                        GlobalVariables.ClickedProvinces.Add(pr);
                        UpdateMapList.Add(pr);
                    }
                }
                if (onlyone)
                {
                    if (Tabs.SelectedTab == ProvinceTab)
                    {
                        UpdateProvincePanel();
                    }
                }
                if (GlobalVariables.ClickedProvinces.Any())
                    if (GlobalVariables.ClickedProvinces[0].OwnerCountry != null && GlobalVariables.ClickedProvinces[0].OwnerCountry != Country.NoCountry)
                        CountryBox.SelectedItem = GlobalVariables.ClickedProvinces[0].OwnerCountry;
                MapManagement.UpdateClickedMap(UpdateMapList, Color.LightYellow);
                UpdateTotalSelectedLabel();
                UpdateMap();
            }
        }
        void RemoveFromClickedProvinces(Province p, bool Update = true)
        {
            RemoveFromClickedProvinces(new List<Province> { p }, Update);
        }
        void RemoveFromClickedProvinces(List<Province> p, bool Update = true)
        {
            List<Province> UpdateMapList = new List<Province>();
            foreach (Province pr in p)
            {
                if (GlobalVariables.ClickedProvinces.Contains(pr))
                {
                    GlobalVariables.ClickedProvinces.Remove(pr);
                    UpdateMapList.Add(pr);
                }
            }
            MapManagement.UpdateClickedMap(UpdateMapList, Color.LightYellow, false);
            UpdateTotalSelectedLabel();
            UpdateMap();
        }
        void AddAndRemoveFromClickedProvinces(List<Province> toremove, List<Province> toadd)
        {
            List<Province> UpdateMapList = new List<Province>();
            foreach (Province pr in toremove)
            {
                if (GlobalVariables.ClickedProvinces.Contains(pr))
                {
                    GlobalVariables.ClickedProvinces.Remove(pr);
                    UpdateMapList.Add(pr);
                }
            }
            MapManagement.UpdateClickedMap(UpdateMapList, Color.LightYellow, false);
            if (toadd.Any())
            {
                bool onlyone = false;
                if (GlobalVariables.ClickedProvinces.Count == 0)
                {

                    if (toadd[0].OwnerCountry != null)
                    {
                        if (GlobalVariables.SelectedDiscoveredByTechGroup != toadd[0].OwnerCountry?.TechnologyGroup && GlobalVariables.mapmode == MapManagement.UpdateMapOptions.DiscoveredBy)
                        {
                            GlobalVariables.SelectedDiscoveredByTechGroup = toadd[0].OwnerCountry?.TechnologyGroup;
                            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);
                        }
                    }
                    onlyone = true;
                }

               
                UpdateMapList = new List<Province>();
                foreach (Province pr in toadd)
                {
                    // if (!GlobalVariables.ToUpdate.Contains(pr))
                    //    GlobalVariables.ToUpdate.Add(pr);
                    if (!GlobalVariables.ClickedProvinces.Contains(pr))
                    {
                        GlobalVariables.ClickedProvinces.Add(pr);
                        UpdateMapList.Add(pr);
                    }
                }

                if (GlobalVariables.ClickedProvinces.Any())
                    if (GlobalVariables.ClickedProvinces[0].OwnerCountry != null && GlobalVariables.ClickedProvinces[0].OwnerCountry != Country.NoCountry)
                        CountryBox.SelectedItem = GlobalVariables.ClickedProvinces[0].OwnerCountry;

            }
            if (Tabs.SelectedTab == ProvinceTab)
            {
                UpdateProvincePanel();
            }
            MapManagement.UpdateClickedMap(UpdateMapList, Color.LightYellow);
            //TODO
            // Add averages and enable adding one for each

            UpdateTotalSelectedLabel();
            UpdateMap();
        }
        #endregion

        #region Important forms stuff
        public static ModEditor form;
        public void OnExitDo(object sender, EventArgs e)
        {
            GlobalVariables.UpdtGraphicsThread.Abort();
            GlobalVariables.Exited = true;
            //TODO
            //Save files to temp?
        }
        #endregion

        #region Value Changed Handlers
        private void ProvinceTaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    if(p.TradeGood != null)
                        p.TradeGood.TotalDev += (int)ProvinceTaxNumeric.Value - p.Tax;
                    p.Tax = (int)ProvinceTaxNumeric.Value;
                }
                
            }
            MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();

            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();
        }
        private void ProvinceProductionNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    if (p.TradeGood != null)
                        p.TradeGood.TotalDev += (int)ProvinceProductionNumeric.Value - p.Production;
                    p.Production = (int)ProvinceProductionNumeric.Value;
                }

            }
            MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();

            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();

        }
        private void ProvinceManpowerNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    if (p.TradeGood != null)
                        p.TradeGood.TotalDev += (int)ProvinceManpowerNumeric.Value - p.Manpower;
                    p.Manpower = (int)ProvinceManpowerNumeric.Value;
                }

            }
            MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();

            if (Tabs.SelectedTab == TradeGoodsTab)
                RefreshTradeGoodsTab();

        }
        private void TradeGoodBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                TradeGood tg = GlobalVariables.TradeGoods.Find(x => x.ReadableName == (string)TradeGoodBox.SelectedItem);
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    if (p.TradeGood != null)
                    {
                        p.TradeGood.TotalProvinces--;
                        p.TradeGood.TotalDev -= p.Tax + p.Production + p.Manpower;
                    }
                    p.TradeGood = tg;
                    if (tg != null)
                    {
                        tg.TotalProvinces++;
                        p.TradeGood.TotalDev += p.Tax + p.Production + p.Manpower;
                    }

                    //  if (!GlobalVariables.ToUpdate.Contains(p))
                    //      GlobalVariables.ToUpdate.Add(p);
                }
                MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.TradeGood);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeGood)
                    UpdateMap();
                //Saving.SaveThingsToUpdate();
                if (Tabs.SelectedTab == TradeGoodsTab)
                    RefreshTradeGoodsTab();
            }
        }
        private void CultureBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            ChangeProvinceInfo(ChangeProvinceMode.Culture, (Culture)CultureBox.SelectedItem);
        }
        private void ReligionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            ChangeProvinceInfo(ChangeProvinceMode.Religion, (Religion)ReligionBox.SelectedItem);
        }
        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country c = (Country)CountryBox.SelectedItem;
            if (c != null)
            {
                GlobalVariables.SelectedCountry = c;
            }
            if (Tabs.SelectedTab == CountryPage)
                UpdateCountryPage();
        }
        private void OwnerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            ChangeProvinceInfo(ChangeProvinceMode.Owner, OwnerBox.SelectedItem);
        }
        private void CountryReligionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.SelectedCountry != null)
            {
                if (CountryReligionBox.SelectedItem != Religion.NoReligion)
                {
                    GlobalVariables.SelectedCountry.Religion = (Religion)CountryReligionBox.SelectedItem;
                }
                else
                {
                    GlobalVariables.SelectedCountry.Religion = null;
                }
                //GlobalVariables.ToUpdate.Add(GlobalVariables.SelectedCountry);
                GlobalVariables.SelectedCountry.Provinces.ForEach(x => MapManagement.UpdateMap(x, MapManagement.UpdateMapOptions.Religion));
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Religion)
                    UpdateMap();
                //Saving.SaveThingsToUpdate();
            }
        }
        private void TechnologyGroupBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry != null)
            {
                if (!GlobalVariables.InternalChanges)
                    GlobalVariables.SelectedCountry.TechnologyGroup = TechnologyGroupBox.Items[TechnologyGroupBox.SelectedIndex].ToString();
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);
            }
        }
        private void CountryPrimaryCultureBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry != null)
            {
                if (!GlobalVariables.InternalChanges)
                {
                    GlobalVariables.SelectedCountry.PrimaryCulture = (Culture)CountryPrimaryCultureBox.SelectedItem;
                }
            }
        }
        private void LatentTradeGoodBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                TradeGood tg = GlobalVariables.TradeGoods.Find(x => x.ReadableName == (string)LatentTradeGoodBox.SelectedItem);
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    if (p.LatentTradeGood != null)
                    {
                        p.LatentTradeGood.TotalProvinces--;
                        p.LatentTradeGood.TotalDev -= p.Tax + p.Production + p.Manpower;
                    }
                    p.LatentTradeGood = tg;
                    if (p.LatentTradeGood != null)
                    {
                        tg.TotalProvinces++;
                        p.LatentTradeGood.TotalDev += p.Tax + p.Production + p.Manpower;
                    }
                    //if (!GlobalVariables.ToUpdate.Contains(p))
                    //GlobalVariables.ToUpdate.Add(p);
                }
                MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.TradeGood);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeGood)
                    UpdateMap();
                //Saving.SaveThingsToUpdate();
                if (Tabs.SelectedTab == TradeGoodsTab)
                    RefreshTradeGoodsTab();
            }
        }
        private void AreaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GlobalVariables.InternalChanges)
            {
                int index = AreaBox.SelectedIndex - 1;
                ChangeProvinceInfo(ChangeProvinceMode.Area, index);
            }
            AreaNameChangeBox.Text = AreaBox.Text;
        }
        private void RegionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GlobalVariables.InternalChanges)
            {
                int index = RegionBox.SelectedIndex - 1;
                ChangeProvinceInfo(ChangeProvinceMode.Region, index);
            }
            RegionNameChangeBox.Text = RegionBox.Text;
        }
        private void ProvinceTradeNodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if(ProvinceTradeNodeBox.SelectedItem is string)
                ChangeProvinceInfo(ChangeProvinceMode.TradeNode, null);
            else
                ChangeProvinceInfo(ChangeProvinceMode.TradeNode, ProvinceTradeNodeBox.SelectedItem);
        }
        private void TradeNodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Tabs.SelectedTab == TradeNodesTab)
                UpdateTradeNodesPage();
        }
        private void TradeNodeInlandCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Inland = TradeNodeInlandCheckbox.Checked;
        }
        private void ContinentBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
            {
                ContinentNameChangeBox.Text = ContinentBox.Text;
                return;
            }
            if (GlobalVariables.ClickedProvinces.Any())
            {
                int index = ContinentBox.SelectedIndex - 1;
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    if (index == -1)
                    {
                        p.Continent = null;
                    }
                    else
                    {
                        p.Continent = GlobalVariables.Continents[index];
                    }
                }
                MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Continent);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent)
                    UpdateMap();
                //Saving.SaveThingsToUpdate();
            }
            ContinentNameChangeBox.Text = ContinentBox.Text;
        }
        private void TradeCompanyBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!GlobalVariables.InternalChanges)
            {
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    int index = TradeCompanyBox.SelectedIndex - 1;

                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        if (p.TradeCompany != null)
                        {
                            p.TradeCompany.Provinces.Remove(p);
                            p.TradeCompany.MadeChanges = true;
                        }
                        if (index == -1)
                        {
                            p.TradeCompany = null;
                        }
                        else
                        {
                            p.TradeCompany = GlobalVariables.TradeCompanies[index];
                            p.TradeCompany.MadeChanges = true;
                            GlobalVariables.TradeCompanies[index].Provinces.Add(p);
                        }
                        //if (!GlobalVariables.ToUpdate.Contains(p))
                        //GlobalVariables.ToUpdate.Add(p);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.TradeCompany);
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeCompany))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeCompany));
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeCompany)
                        UpdateMap();
                    //Saving.SaveThingsToUpdate();
                }
            }          
            TradeCompanyNameChangeBox.Text = TradeCompanyBox.Text;
            if (TradeCompanyBox.SelectedIndex != 0)
                TradeCompanyColorButton.BackColor = GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Color;
        }
        private void ControllerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GlobalVariables.InternalChanges)
                ChangeProvinceInfo(ChangeProvinceMode.Controller, ControllerBox.SelectedItem);

        }
        private void GovernmentTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry != null)
            {
                if (!GlobalVariables.InternalChanges)
                {
                    GlobalVariables.SelectedCountry.Government = GlobalVariables.Governments.Find(x => x.Type == GovernmentTypeBox.Items[GovernmentTypeBox.SelectedIndex].ToString());
                    GlobalVariables.SelectedCountry.GovernmentReform = GlobalVariables.SelectedCountry.Government.reforms[0];
                }

                GovernmentReformBox.Items.Clear();
                foreach (string reform in GlobalVariables.SelectedCountry.Government.reforms)
                    GovernmentReformBox.Items.Add(reform);


            }
        }
        private void GovernmentReformBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GlobalVariables.InternalChanges)
                GlobalVariables.SelectedCountry.GovernmentReform = GovernmentReformBox.SelectedItem.ToString();
        }
        private void GovernmentRankNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.SelectedCountry != null)
            {
                if (GlobalVariables.SelectedCountry.GovernmentRank != GovernmentRankNumeric.Value)
                    GlobalVariables.SelectedCountry.GovernmentRank = (int)GovernmentRankNumeric.Value;
            }
        }
        private void CenterOfTradeNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            ChangeProvinceInfo(ChangeProvinceMode.CoT, (int)CenterOfTradeNumeric.Value);

        }
        private void SuperregionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GlobalVariables.InternalChanges)
            {
                int index = SuperregionBox.SelectedIndex - 1;
                ChangeProvinceInfo(ChangeProvinceMode.Superregion, index);
            }
            SuperregionNameChangeBox.Text = SuperregionBox.Text;
        }
        private void GraphicalCultureBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.SelectedCountry != null)
            {
                GlobalVariables.SelectedCountry.GraphicalCulture = (string)GraphicalCultureBox.SelectedItem;
            }
        }
        private void WinterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    p.Winter = WinterBox.SelectedIndex;
                }
                MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Winter);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Winter)
                    UpdateMap();
            }
        }
        private void MonsoonBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    p.Monsoon = MonsoonBox.SelectedIndex;
                }
                MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Winter);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Winter)
                    UpdateMap();
            }
        }
        private void ClimateBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    p.Climate = ClimateBox.SelectedIndex;
                }
                MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Climate);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Climate)
                    UpdateMap();
            }
        }
        private void ImpassableBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    p.Impassable = ImpassableBox.SelectedIndex;
                }
                MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Climate);
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Climate)
                    UpdateMap();
            }
        }

        


        #endregion

        #region Macros
        private void MacroSelectProvincesEqualDev_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            int mode = 0;
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                mode = 1;
            else if ((ModifierKeys & Keys.Control) == Keys.Control)
                mode = 2;
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Tax + p.Production + p.Manpower == MacroDevNumeric.Value)
                {
                    ToSelect.Add(p);
                }
            }

            switch (mode)
            {
                case 0:
                    RemoveFromClickedProvinces(GlobalVariables.ClickedProvinces.ToList());
                    AddToClickedProvinces(ToSelect);
                    break;
                case 1:
                    AddToClickedProvinces(ToSelect);
                    break;
                case 2:
                    RemoveFromClickedProvinces(ToSelect);
                    break;
            }
            UpdateDiscoveredBy();
        }
        public void PerformMacroFunc(List<Province> Select)
        {
            int mode = 0;
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                mode = 1;
            else if ((ModifierKeys & Keys.Control) == Keys.Control)
                mode = 2;
            switch (mode)
            {
                case 0:
                    RemoveFromClickedProvinces(GlobalVariables.ClickedProvinces.ToList());
                    AddToClickedProvinces(Select);
                    break;
                case 1:
                    AddToClickedProvinces(Select);
                    break;
                case 2:
                    RemoveFromClickedProvinces(Select);
                    break;
            }
            UpdateDiscoveredBy();
        }
        private void MacroSameReligion_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Religion == MacroReligionBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentReligion_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Religion != MacroReligionBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSameCountryReligion_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.OwnerCountry?.Religion == MacroReligionBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentCountryReligion_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.OwnerCountry?.Religion != MacroReligionBox.SelectedItem && p.OwnerCountry?.Religion != null)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSameCulture_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Culture == MacroCultureBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentCulture_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Culture != MacroCultureBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSameCountryCulture_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.OwnerCountry?.PrimaryCulture == MacroCultureBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }

            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentCountryCulture_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.OwnerCountry?.PrimaryCulture != MacroCultureBox.SelectedItem && p.OwnerCountry?.PrimaryCulture != null)
                {
                    ToSelect.Add(p);
                }
            }

            PerformMacroFunc(ToSelect);
        }
        private void MacroWithFort_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Fort)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroWithoutFort_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (!p.Fort)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroInsideFort_Click(object sender, EventArgs e)
        {
            /*List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if ()
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
            */
        }
        private void BorderingDebugButton_Click(object sender, EventArgs e)
        {
            GlobalVariables.BorderingMode = !GlobalVariables.BorderingMode;
        }
        private void MacroSameArea_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.Area == MacroAreaBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentArea_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.Area != MacroAreaBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSameRegion_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.Area?.Region == MacroRegionBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentRegion_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.Area?.Region != MacroRegionBox.SelectedItem && p.Area?.Region != null)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSameSuperregion_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.Area?.Region?.Superregion == MacroSuperregionBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentSuperregion_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.Area?.Region?.Superregion != MacroSuperregionBox.SelectedItem && p.Area?.Region?.Superregion != null)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSameContinent_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.Continent == MacroContinentBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentContinent_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.Continent != MacroContinentBox.SelectedItem && p.Continent != null)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroInsideHRE_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.HRE)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroHREProvOutCountry_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.HRE && !(p.OwnerCountry?.Capital?.HRE ?? false))
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroHRECountry_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.OwnerCountry?.Capital?.HRE ?? false)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroOutsideHRE_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (!p.HRE)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSameTradenode_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.TradeNode == MacroTradeNodeBox.SelectedItem)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentTradenode_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.TradeNode != MacroTradeNodeBox.SelectedItem && p.TradeNode != null)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }  
        private void MacroDiscoveredBy_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.GetDiscoveredBy().Contains(MacroTechGroupBox.SelectedItem))
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroNotDiscoveredBy_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (!p.GetDiscoveredBy().Contains(MacroTechGroupBox.SelectedItem))
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSameTechGroup_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.OwnerCountry?.TechnologyGroup == MacroTechGroupBox.SelectedItem.ToString())
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroDifferentTechGroup_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.OwnerCountry?.TechnologyGroup != MacroTechGroupBox.SelectedItem.ToString() && p.OwnerCountry?.TechnologyGroup != null)
                {
                    ToSelect.Add(p);
                }
            }
            PerformMacroFunc(ToSelect);
        }
        private void MacroSelectAllExceptSeas_Click(object sender, EventArgs e)
        {
            AddToClickedProvinces(GlobalVariables.Provinces.Where(x => !x.Lake && !x.Sea && !x.Wasteland).ToList());
        }
        private void MacroDeselectAllProvincesButton_Click(object sender, EventArgs e)
        {
            RemoveFromClickedProvinces(GlobalVariables.Provinces);
        }
        private void MacroSelectAllProvincesButton_Click(object sender, EventArgs e)
        {
            AddToClickedProvinces(GlobalVariables.Provinces.ToList());
        }
        private void MacroSelectProvincesAboveDev_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            int mode = 0;
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                mode = 1;
            else if ((ModifierKeys & Keys.Control) == Keys.Control)
                mode = 2;
            else if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                mode = 3;
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Tax + p.Production + p.Manpower > MacroDevNumeric.Value)
                {
                    ToSelect.Add(p);
                }
            }

            switch (mode)
            {
                case 0:
                    RemoveFromClickedProvinces(GlobalVariables.ClickedProvinces.ToList());
                    AddToClickedProvinces(ToSelect);
                    break;
                case 1:
                    AddToClickedProvinces(ToSelect);
                    break;
                case 2:
                    RemoveFromClickedProvinces(ToSelect);
                    break;
                case 3:
                    List<Province> filtered = new List<Province>(GlobalVariables.ClickedProvinces);
                    filtered.RemoveAll(x => ToSelect.Contains(x));
                    break;
            }
            UpdateDiscoveredBy();
        }
        private void MacroSelectProvincesBelowDev_Click(object sender, EventArgs e)
        {
            List<Province> ToSelect = new List<Province>();
            int mode = 0;
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                mode = 1;
            else if ((ModifierKeys & Keys.Control) == Keys.Control)
                mode = 2;
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Tax + p.Production + p.Manpower < MacroDevNumeric.Value)
                {
                    ToSelect.Add(p);
                }
            }

            switch (mode)
            {
                case 0:
                    RemoveFromClickedProvinces(GlobalVariables.ClickedProvinces.ToList());
                    AddToClickedProvinces(ToSelect);
                    break;
                case 1:
                    AddToClickedProvinces(ToSelect);
                    break;
                case 2:
                    RemoveFromClickedProvinces(ToSelect);
                    break;
            }
            UpdateDiscoveredBy();
        }
        private void MacroDeselectProvincesAboveDev_Click(object sender, EventArgs e)
        {
            List<Province> ToDeselect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Tax + p.Production + p.Manpower > MacroDevNumeric.Value)
                {
                    ToDeselect.Add(p);
                }
            }
            GlobalVariables.ClickedProvinces.RemoveAll(x => ToDeselect.Contains(x));
            MapManagement.UpdateClickedMap(ToDeselect, Color.White, false);
            UpdateDiscoveredBy();
        }
        private void MacroDeselectProvincesBelowDev_Click(object sender, EventArgs e)
        {
            List<Province> ToDeselect = new List<Province>();
            foreach (Province p in GlobalVariables.Provinces.Where(x => !x.Wasteland && !x.Lake && !x.Sea))
            {
                if (p.Tax + p.Production + p.Manpower < MacroDevNumeric.Value)
                {
                    ToDeselect.Add(p);
                }
            }
            GlobalVariables.ClickedProvinces.RemoveAll(x => ToDeselect.Contains(x));
            MapManagement.UpdateClickedMap(ToDeselect, Color.White, false);
            UpdateDiscoveredBy();
        }
        #endregion

        public enum ChangeProvinceMode { CoT, Fort, HRE, Religion, Culture, DiscoveredBy, DiscoveredByOwner, Area, Owner, Controller, City, Building, Core, Claim, CoreOwner, Superregion, Region, Continent, TradeNode };

        #region Functions
        public void ChangeProvinceInfo(ChangeProvinceMode mode, object change, object secondvalue = null)
        {
            List<Province> ApplyTo = new List<Province>();
            if (GlobalVariables.ClickedProvinces.Any())
                ApplyTo.AddRange(GlobalVariables.ClickedProvinces);
            switch (mode)
            {
                case ChangeProvinceMode.TradeNode:
                    {
                        List<Province> provincestoupdate = new List<Province>();
                        Tradenode tochange = (Tradenode)change;
                        foreach (Province p in ApplyTo)
                        {
                            if (p.TradeNode != null)
                            {
                                p.TradeNode.Provinces.Remove(p);
                            }                            
                            if (p.TradeNode != tochange)
                                provincestoupdate.Add(p);
                            p.TradeNode = tochange;
                            if(tochange != null)
                                tochange.Provinces.Add(p);
                        }
                        provincestoupdate = provincestoupdate.Distinct().ToList();
                        MapManagement.UpdateMap(provincestoupdate, MapManagement.UpdateMapOptions.TradeNode);
                        if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                            UpdateMap();
                    }
                    break;

                case ChangeProvinceMode.CoT:
                    foreach (Province p in ApplyTo)
                    {
                        p.CenterOfTrade = (int)change;
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.TradeNode);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                        UpdateMap();
                    break;

                case ChangeProvinceMode.Fort:
                    foreach (Province p in ApplyTo)
                    {
                        p.Fort = (bool)change;
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Fort);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Fort)
                        UpdateMap();
                    break;

                case ChangeProvinceMode.HRE:
                    foreach (Province p in ApplyTo)
                    {
                        p.HRE = (bool)change;
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.HRE);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.HRE)
                        UpdateMap();
                    break;


                case ChangeProvinceMode.Religion:
                    Religion r = (Religion)change;
                    foreach (Province p in ApplyTo)
                    {
                        p.Religion = r;
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Religion);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Religion)
                        UpdateMap();
                    break;

                case ChangeProvinceMode.Culture:
                    Culture c = (Culture)change;
                    foreach (Province p in ApplyTo)
                    {
                        p.Culture = c;
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Culture);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Culture)
                        UpdateMap();
                    break;
                case ChangeProvinceMode.DiscoveredBy:
                    string Tech = (string)change;
                    bool remove = (bool)secondvalue;
                    if (remove)
                    {
                        foreach (Province p in ApplyTo)
                        {
                            p.RemoveDiscoveredBy(Tech);
                        }
                    }
                    else
                    {
                        foreach (Province p in ApplyTo)
                        {
                            p.AddDiscoveredBy(Tech);
                        }
                    }

                    UpdateDiscoveredBy();
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.DiscoveredBy)
                        UpdateMap();
                    break;

                case ChangeProvinceMode.DiscoveredByOwner:
                    foreach (Province p in ApplyTo)
                    {
                        if (p.OwnerCountry != null)
                            p.AddDiscoveredBy(p.OwnerCountry.TechnologyGroup);
                    }
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.DiscoveredBy)
                        UpdateMap();
                    break;

                case ChangeProvinceMode.Area:

                    int index = (int)change;
                    foreach (Province p in ApplyTo)
                    {
                        if (index != -1)
                        {
                            p.Area = GlobalVariables.Areas[index];
                        }
                        else
                            p.Area = null;
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Area);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Area)
                        UpdateMap();
                    break;

                case ChangeProvinceMode.Owner:
                    foreach (Province p in ApplyTo)
                    {
                        if ((Country)change == Country.NoCountry || (Country)change == null)
                        {
                            if (p.OwnerCountry != null)
                            {
                                p.OwnerCountry.Provinces.Remove(p);
                                p.RemoveCore(p.OwnerCountry.Tag);
                            }
                            p.OwnerCountry = null;
                            p.Controller = null;
                        }
                        else
                        {
                            if (p.OwnerCountry != null)
                            {
                                p.OwnerCountry.Provinces.Remove(p);
                                p.RemoveCore(p.OwnerCountry.Tag);
                            }
                            p.OwnerCountry = (Country)change;
                            p.AddCore(p.OwnerCountry.Tag);
                            p.Controller = p.OwnerCountry;
                        }
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Political);
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Government);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Political)
                        UpdateMap();

                    break;

                case ChangeProvinceMode.Controller:
                    foreach (Province p in ApplyTo)
                    {
                        p.Controller = (Country)change;
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Political);
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Government);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Political)
                        UpdateMap();
                    break;
                case ChangeProvinceMode.City:
                    foreach (Province p in ApplyTo)
                    {
                        p.City = (bool)change;
                    }
                    break;
                case ChangeProvinceMode.Building:
                    if (secondvalue != null)
                    {
                        foreach (Province p in ApplyTo)
                        {
                            p.RemoveBuilding((Building)change);
                        }
                    }
                    else
                    {
                        foreach (Province p in ApplyTo)
                        {
                            p.AddBuilding((Building)change);
                        }
                    }
                    break;
                case ChangeProvinceMode.Core:
                    if (secondvalue != null)
                    {
                        foreach (Province p in ApplyTo)
                        {
                            p.RemoveCore((string)change);
                        }
                    }
                    else
                    {
                        foreach (Province p in ApplyTo)
                        {
                            p.AddCore((string)change);
                        }
                    }
                    break;
                case ChangeProvinceMode.Claim:
                    if (secondvalue != null)
                    {
                        foreach (Province p in ApplyTo)
                        {
                            p.RemoveClaim((string)change);
                        }
                    }
                    else
                    {
                        foreach (Province p in ApplyTo)
                        {
                            p.AddClaim((string)change);
                        }
                    }
                    break;
                case ChangeProvinceMode.CoreOwner:
                    foreach (Province p in ApplyTo)
                    {
                        if (p.OwnerCountry != null)
                            p.AddCore(p.OwnerCountry.Tag);
                    }
                    break;
                case ChangeProvinceMode.Superregion:
                    {
                        int indexsuperregion = (int)change;
                        List<Province> provincestoupdatesuperregion = new List<Province>();
                        foreach (Province p in ApplyTo)
                        {
                            if (p.Area != null)
                            {
                                if (p.Area.Region != null)
                                {
                                    foreach (Area a in p.Area.Region.Areas)
                                        provincestoupdatesuperregion.AddRange(a.Provinces);
                                    if (p.Area.Region.Superregion != null)
                                    {
                                        p.Area.Region.Superregion.Regions.Remove(p.Area.Region);
                                    }

                                    if (indexsuperregion == -1)
                                    {
                                        p.Area.Region.Superregion = null;
                                    }
                                    else
                                    {
                                        p.Area.Region.Superregion = GlobalVariables.Superregions[indexsuperregion];
                                        GlobalVariables.Superregions[indexsuperregion].Regions.Add(p.Area.Region);
                                    }
                                }
                            }
                        }
                        if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Superregion))
                            GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Superregion));
                        provincestoupdatesuperregion = provincestoupdatesuperregion.Distinct().ToList();
                        MapManagement.UpdateMap(provincestoupdatesuperregion, MapManagement.UpdateMapOptions.Superregion);
                        if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Superregion)
                            UpdateMap();
                    }
                    break;
                case ChangeProvinceMode.Region:
                    {
                        int indexregion = (int)change;
                        List<Province> provincestoupdate = new List<Province>();
                        foreach (Province p in ApplyTo)
                        {

                            if (p.Area != null)
                            {
                                provincestoupdate.AddRange(p.Area.Provinces);
                                if (p.Area.Region != null)
                                {
                                    p.Area.Region.Areas.Remove(p.Area);
                                }

                                if (indexregion == -1)
                                {
                                    p.Area.Region = null;
                                }

                                else
                                {
                                    p.Area.Region = GlobalVariables.Regions[indexregion];
                                    GlobalVariables.Regions[indexregion].Areas.Add(p.Area);
                                }
                            }
                        }

                        provincestoupdate = provincestoupdate.Distinct().ToList();

                        if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Region))
                            GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Region));

                        MapManagement.UpdateMap(provincestoupdate, MapManagement.UpdateMapOptions.Region);
                        if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Region)
                            UpdateMap();
                    }
                    break;
            }
        }
        void MoveCameraTo(Province p)
        {
            //TODO
            //when scaling the game don't forget about this!

            if (p != null)
            {
                GlobalVariables.CameraPosition = new Point(0, 0);
                if (p.Pixels.Any())
                {
                    Point dest = p.Pixels[0];
                    int x = 0;
                    do
                    {
                        x += 220;
                    } while (dest.X - 440 > x);
                    x -= 220;
                    if (x < 0)
                        x = 0;
                    int y = 0;
                    do
                    {
                        y += 160;
                    } while (dest.Y - 320 > y);
                    y -= 160;
                    if (y < 0)
                        y = 0;

                    if (x + GlobalVariables.MapDrawingWidth >= GlobalVariables.ProvincesMap.Width)
                        x = GlobalVariables.ProvincesMap.Width - GlobalVariables.MapDrawingWidth;
                    if (y + GlobalVariables.MapDrawingHeight >= GlobalVariables.ProvincesMap.Height)
                        y = GlobalVariables.ProvincesMap.Height - GlobalVariables.MapDrawingHeight;

                    GlobalVariables.CameraPosition = new Point(x, y);
                }
                UpdateMap();
            }
        }
        public void ChangeValueInternally(Control control, object value)
        {
            GlobalVariables.InternalChanges = true;
            if (control is NumericUpDown)
                ((NumericUpDown)control).Value = (int)value;
            else if (control is CheckBox)
                ((CheckBox)control).Checked = (bool)value;
            else if (control is ComboBox)
            {
                if (value is int)
                {
                    if ((int)value < 0)
                        ((ComboBox)control).SelectedIndex = 0;
                    else
                        ((ComboBox)control).SelectedIndex = (int)value;
                }
                else
                {
                    ((ComboBox)control).SelectedItem = value;
                }
            }
            else if (control is TextBox)
                ((TextBox)control).Text = (string)value;
            GlobalVariables.InternalChanges = false;
        }
        public double RoundUp(double value, int places)
        {
            value *= Math.Pow(10, places);
            value = (int)value;
            value /= Math.Pow(10, places);
            return value;
        }
        public void ShowMessageBox(string text, string title = "Info")
        {
            MessageBox.Show(text, title);
        }
        public void AddTradeDestination(Tradenode start, Tradenode end)
        {
            if (start.Incoming.Contains(end))
                return;
            if (end.Incoming.Contains(start))
                return;

            Label tnl = new Label();
            tnl.Text = end.Name;
            tnl.MouseClick += TradeNodeClick;
            TradeNodeDestinationsBox.Controls.Add(tnl);
            tnl.Tag = GlobalVariables.TradeNodes.IndexOf(end);
            tnl.BackColor = Color.DarkGray;
            tnl.ForeColor = Color.White;
            tnl.TextAlign = ContentAlignment.MiddleCenter;
            tnl.AutoSize = true;
            start.Destination.Add(new Destination() { TradeNode = end });
            end.Incoming.Add(start);
            AddTradeNodeDestinationBox.Items.Clear();
            foreach (Tradenode tnn in GlobalVariables.TradeNodes)
            {
                if (!start.Destination.Any(x => x.TradeNode == tnn) && start != tnn)
                    AddTradeNodeDestinationBox.Items.Add(tnn.Name);
            }
            if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
        }
        
        public void ChangeDarkMode()
        {

        }
        #endregion

        #region Saving
        public void MergeChanges()
        {
            List<VariableChange> newList = new List<VariableChange>();
            foreach (VariableChange change in GlobalVariables.Changes)
            {
                if (change.VariableName == "Core" || change.VariableName == "DiscoveredBy" || change.VariableName == "Buildings" || change.VariableName == "Claims")
                {
                    newList.Add(change);
                    continue;
                }
                VariableChange nv = newList.Find(x => x.Object == change.Object && x.VariableName == change.VariableName);
                if (nv != null)
                {
                    nv.CurrentValue = change.CurrentValue;
                }
                else
                    newList.Add(change);
            }
            newList = newList.Where(x => x.PreviousValue != x.CurrentValue).ToList();
            GlobalVariables.Changes.Clear();
            GlobalVariables.Changes.AddRange(newList);
        }
        
        public void KeepAndSave(object sender, EventArgs e)
        {
            int index = (int)(sender as Control).Tag;
            switch (GlobalVariables.Changes[index].VariableName)
            {
                case "TradeNode":
                    {
                        if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeNode))
                            GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeNode));
                    }
                    break;
                case "Area":
                    {
                        if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Area))
                            GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Area));
                    }
                    break;
                case "Continent":
                    {
                        if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Continent))
                            GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Continent));
                    }
                    break;
                default:
                    {
                        if (!GlobalVariables.Saves.Contains(GlobalVariables.Changes[index].Object))
                            GlobalVariables.Saves.Add(GlobalVariables.Changes[index].Object);
                    }
                    break;
            }          
            GlobalVariables.Changes.RemoveAt((int)(sender as Control).Tag);
            UpdateChangesTab();
            UpdateSavesTab();
        }
        public void Revert(object sender, EventArgs e)
        {
            VariableChange vc = GlobalVariables.Changes[(int)(sender as Control).Tag];
            if (vc.Object is Province)
            {
                if (vc.VariableName != "Core" && vc.VariableName != "DiscoveredBy" && vc.VariableName != "Buildings" && vc.VariableName != "Claims")
                    (vc.Object as Province).Variables[vc.VariableName] = vc.PreviousValue;
                else if (vc.VariableName == "Core")
                {
                    if (vc.PreviousValue == null)
                        ((vc.Object as Province).Variables["Cores"] as List<string>).Remove(vc.CurrentValue.ToString());
                    else if (vc.CurrentValue == null)
                        ((vc.Object as Province).Variables["Cores"] as List<string>).Add(vc.PreviousValue.ToString());
                }
                else if (vc.VariableName == "DiscoveredBy")
                {
                    if (vc.PreviousValue == null)
                        ((vc.Object as Province).Variables["DiscoveredBy"] as List<string>).Remove(vc.CurrentValue.ToString());
                    else if (vc.CurrentValue == null)
                        ((vc.Object as Province).Variables["DiscoveredBy"] as List<string>).Add(vc.PreviousValue.ToString());
                }
                else if (vc.VariableName == "Buildings")
                {
                    if (vc.PreviousValue == null)
                        ((vc.Object as Province).Variables["Buildings"] as List<Building>).Remove(vc.CurrentValue as Building);
                    else if (vc.CurrentValue == null)
                        ((vc.Object as Province).Variables["Buildings"] as List<Building>).Add(vc.PreviousValue as Building);
                }
                else if (vc.VariableName == "Claims")
                {
                    if (vc.PreviousValue == null)
                        ((vc.Object as Province).Variables["Claims"] as List<string>).Remove(vc.CurrentValue.ToString());
                    else if (vc.CurrentValue == null)
                        ((vc.Object as Province).Variables["Claims"] as List<string>).Add(vc.PreviousValue.ToString());
                }

            }
            else if (vc.Object is Country)
            {
                (vc.Object as Country).Variables[vc.VariableName] = vc.PreviousValue;
            }
            GlobalVariables.Changes.Remove(vc);
            UpdateChangesTab();
            UpdateSavesTab();
        }
        public void OpenFile(object sender, EventArgs e)
        {
            Process.Start((sender as Control).Tag.ToString());
        }
        public void SaveFile(object sender, EventArgs e)
        {
            Saving.SaveObject(GlobalVariables.Saves[(int)(sender as Control).Tag]);
            GlobalVariables.Saves.RemoveAt((int)(sender as Control).Tag);
            UpdateSavesTab();
        }
        public void LoadFileAgain(object sender, EventArgs e)
        {
            Saving.LoadObject(GlobalVariables.Saves[(int)(sender as Control).Tag]);
            UpdateSavesTab();
        }



        #endregion

        #region OnExitCrash
        
        public void ExitingFunction()
        {

        }

        #endregion

        #region Focus lost
        private void FocusLost (object sender, EventArgs e)
        {
            if(sender is TextBox)
            {
                TextBox stb = (TextBox)sender;
                switch (stb.Name)
                {
                    case "CountryNameLocalisationBox":
                        {
                            if (GlobalVariables.SelectedCountry == null)
                                return;
                            if (GlobalVariables.ModLocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag))
                                GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag] = CountryNameLocalisationBox.Text;
                            else if (GlobalVariables.LocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag))
                            {
                                if (GlobalVariables.LocalisationEntries[GlobalVariables.SelectedCountry.Tag] != CountryNameLocalisationBox.Text)
                                    GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag] = CountryNameLocalisationBox.Text;
                            }
                            else
                                GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag] = CountryNameLocalisationBox.Text;
                        }
                        break;
                    case "CountryAdjLocalisationBox":
                        {
                            if (GlobalVariables.SelectedCountry == null)
                                return;
                            if (GlobalVariables.ModLocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag + "_ADJ"))
                                GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"] = CountryAdjLocalisationBox.Text;
                            else if (GlobalVariables.LocalisationEntries.Keys.Contains(GlobalVariables.SelectedCountry.Tag + "_ADJ"))
                            {
                                if (GlobalVariables.LocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"] != CountryAdjLocalisationBox.Text)
                                    GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"] = CountryAdjLocalisationBox.Text;
                            }
                            else
                                GlobalVariables.ModLocalisationEntries[GlobalVariables.SelectedCountry.Tag + "_ADJ"] = CountryAdjLocalisationBox.Text;
                        }
                        break;
                    case "CountryCapitalIDBox":
                        {
                            if (CountryCapitalIDBox.Text.Any(x => !nums.Contains(x)))
                            {
                                CountryCapitalIDBox.Text = CountryCapitalIDBox.Text.Where(x => nums.Contains(x)).ToString();
                            }

                            if (GlobalVariables.SelectedCountry != null)
                            {
                                if (CountryCapitalIDBox.Text != "")
                                {
                                    int nn = int.Parse(CountryCapitalIDBox.Text);
                                    Province p = GlobalVariables.Provinces.Find(x => x.ID == nn);
                                    if (p != null)
                                    {
                                        GlobalVariables.SelectedCountry.CapitalID = int.Parse(CountryCapitalIDBox.Text);
                                        GlobalVariables.SelectedCountry.Capital = p;
                                        //GlobalVariables.ToUpdate.Add(GlobalVariables.SelectedCountry);

                                        MapManagement.UpdateMap(GlobalVariables.SelectedCountry.Provinces, MapManagement.UpdateMapOptions.Political);
                                        if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Political)
                                            UpdateMap();

                                        //Saving.SaveThingsToUpdate();

                                    }
                                }
                            }
                        }
                        break;
                    case "ProvinceNameLocalisationBox":
                        {
                            if (!GlobalVariables.ClickedProvinces.Any())
                                return;
                            if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV" + GlobalVariables.ClickedProvinces[0].ID))
                                GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceNameLocalisationBox.Text;
                            else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV" + GlobalVariables.ClickedProvinces[0].ID))
                            {
                                if (GlobalVariables.LocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID] != ProvinceNameLocalisationBox.Text)
                                    GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceNameLocalisationBox.Text;
                            }
                            else
                                GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceNameLocalisationBox.Text;
                            MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Localisation);
                            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Localisation)
                                UpdateMap();
                        }
                        break;
                    case "ProvinceAdjectiveLocalisationBox":
                        {
                            if (!GlobalVariables.ClickedProvinces.Any())
                                return;
                            if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID))
                                GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceAdjectiveLocalisationBox.Text;
                            else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID))
                            {
                                if (GlobalVariables.LocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID] != ProvinceAdjectiveLocalisationBox.Text)
                                    GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceAdjectiveLocalisationBox.Text;
                            }
                            else
                                GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvinces[0].ID] = ProvinceAdjectiveLocalisationBox.Text;
                            MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Localisation);
                            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Localisation)
                                UpdateMap();
                        }
                        break;
                    case "AreaNameChangeBox":
                        AreaNameChange();
                        break;
                    case "RegionNameChangeBox":
                        RegionNameChange();
                        break;
                    case "ContinentNameChangeBox":
                        ContinentNameChange();
                        break;
                    case "SuperregionNameChangeBox":
                        SuperregionNameChange();
                        break;
                    case "TradeCompanyNameChangeBox":
                        TradeCompanyNameChange();
                        break;
                    case "ChangeTradeNodeNameBox":
                        {
                            if (TradeNodeBox.SelectedIndex == 0)
                                return;
                            if (ChangeTradeNodeNameBox.Text == "" || ChangeTradeNodeNameBox.Text == " ")
                            {
                                ChangeTradeNodeNameBox.Text = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Name;
                                return;
                            }
                            if (GlobalVariables.TradeNodes.Any(x => x.Name == ChangeTradeNodeNameBox.Text))
                            {
                                ChangeTradeNodeNameBox.Text = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Name;
                                return;
                            }
                            GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Name = ChangeTradeNodeNameBox.Text.ToLower().Replace(' ', '_');
                            TradeNodeBox.Items[TradeNodeBox.SelectedIndex] = ChangeTradeNodeNameBox.Text.ToLower().Replace(' ', '_');
                            ProvinceTradeNodeBox.Items[TradeNodeBox.SelectedIndex] = ChangeTradeNodeNameBox.Text.ToLower().Replace(' ', '_');
                        }
                        break;
                    case "TradeNodeProvinceLocationBox":
                        {
                            if (TradeNodeBox.SelectedIndex == 0)
                                return;
                            int n = 0;
                            if (!int.TryParse(TradeNodeProvinceLocationBox.Text, out n))
                            {
                                if (GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location != null)
                                    TradeNodeProvinceLocationBox.Text = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location.ID + "";
                                return;
                            }
                            GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location = GlobalVariables.Provinces.Find(x=>x.ID == n);
                            MapManagement.UpdateMap(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeNode);
                            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                                UpdateMap();
                        }
                        break;
                    case "LeaderNamesBox":
                        {
                            if (GlobalVariables.SelectedCountry != null)
                            {
                                if (stb.Text == "" || string.IsNullOrWhiteSpace(stb.Text))
                                {
                                    GlobalVariables.SelectedCountry.LeaderNames.Clear();
                                }
                                else
                                {
                                    string[] split = LeaderNamesBox.Text.Split(',');
                                    List<string> newnames = new List<string>();
                                    foreach (string leadername in split)
                                    {
                                        if (!string.IsNullOrWhiteSpace(leadername))
                                            newnames.Add(leadername.Replace("\"", "").Trim());
                                    }
                                    newnames = newnames.Distinct().ToList();
                                    GlobalVariables.SelectedCountry.LeaderNames = new List<string>(newnames);
                                }
                            }
                        }
                        break;
                    case "ShipNamesBox":
                        {
                            if (GlobalVariables.SelectedCountry != null)
                            {
                                if (stb.Text == "" || string.IsNullOrWhiteSpace(stb.Text))
                                {
                                    GlobalVariables.SelectedCountry.ShipNames.Clear();
                                }
                                else
                                {
                                    string[] split = ShipNamesBox.Text.Split(',');
                                    List<string> newnames = new List<string>();
                                    foreach (string shipname in split)
                                    {
                                        if (!string.IsNullOrWhiteSpace(shipname))
                                            newnames.Add(shipname.Replace("\"", "").Trim());
                                    }
                                    newnames = newnames.Distinct().ToList();
                                    GlobalVariables.SelectedCountry.ShipNames = new List<string>(newnames);
                                }
                            }
                        }
                        break;
                    case "ArmyNamesBox":
                        {
                            if (GlobalVariables.SelectedCountry != null)
                            {
                                if (stb.Text == "" || string.IsNullOrWhiteSpace(stb.Text))
                                {
                                    GlobalVariables.SelectedCountry.ArmyNames.Clear();
                                }
                                else
                                {
                                    string[] split = ArmyNamesBox.Text.Split(',');
                                    List<string> newnames = new List<string>();
                                    foreach (string armyname in split)
                                    {
                                        if (!string.IsNullOrWhiteSpace(armyname))
                                            newnames.Add(armyname.Replace("\"", "").Trim());
                                    }
                                    newnames = newnames.Distinct().ToList();
                                    GlobalVariables.SelectedCountry.ArmyNames = new List<string>(newnames);
                                }
                            }
                        }
                        break;
                    case "FleetNamesBox":
                        {
                            if (GlobalVariables.SelectedCountry != null)
                            {
                                if (stb.Text == "" || string.IsNullOrWhiteSpace(stb.Text))
                                {
                                    GlobalVariables.SelectedCountry.FleetNames.Clear();
                                }
                                else
                                {
                                    string[] split = FleetNamesBox.Text.Split(',');
                                    List<string> newnames = new List<string>();
                                    foreach (string fleetname in split)
                                    {
                                        if (!string.IsNullOrWhiteSpace(fleetname))
                                            newnames.Add(fleetname.Replace("\"", "").Trim());
                                    }
                                    newnames = newnames.Distinct().ToList();
                                    GlobalVariables.SelectedCountry.FleetNames = new List<string>(newnames);
                                }
                            }
                        }
                        break;
                    case "CountryTagBox":
                        {
                            if (GlobalVariables.SelectedCountry != null)
                            {
                                if (CountryTagBox.Text != "" && CountryTagBox.Text.All(x=>char.IsLetter(x) || char.IsDigit(x)))
                                {
                                    string newtag = CountryTagBox.Text.ToUpper();
                                    string oldtag = "";
                                    if(GlobalVariables.Countries.Any(x=>x.Tag != CountryTagBox.Text))
                                    {
                                        oldtag = GlobalVariables.SelectedCountry.Tag;
                                        GlobalVariables.SelectedCountry.Tag = newtag;
                                        if(GlobalVariables.SelectedCountry.HistoryFile != null)
                                        {
                                            if(!GlobalVariables.SelectedCountry.HistoryFile.ReadOnly)
                                                File.Delete(GlobalVariables.SelectedCountry.HistoryFile.Path);
                                            GlobalVariables.SelectedCountry.HistoryFile.Path = GlobalVariables.pathtomod + $"history\\countries\\{GlobalVariables.SelectedCountry.Tag} - {GlobalVariables.SelectedCountry.FullName}.txt";
                                            GlobalVariables.SelectedCountry.HistoryFile.SaveFile();
                                        }
                                        if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TagFile))
                                            GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TagFile));

                                        foreach(Province p in GlobalVariables.Provinces)
                                        {
                                            if (p.OwnerCountry == GlobalVariables.SelectedCountry)
                                                GlobalVariables.Changes.Add(new VariableChange(p, "Owner tag", oldtag, newtag));
                                            else if (p.Controller == GlobalVariables.SelectedCountry)
                                                GlobalVariables.Changes.Add(new VariableChange(p, "Controller tag", oldtag, newtag));
                                            else if (p.GetDiscoveredBy().Contains(GlobalVariables.SelectedCountry.Tag))
                                            {
                                                p.RemoveDiscoveredBy(oldtag, true);
                                                p.AddDiscoveredBy(newtag, true);
                                                GlobalVariables.Changes.Add(new VariableChange(p, "Discovered by tag", oldtag, newtag));
                                            }
                                            else if(p.GetCores().Contains(GlobalVariables.SelectedCountry.Tag))
                                            {
                                                p.RemoveCore(oldtag, true);
                                                p.AddCore(newtag, true);
                                                GlobalVariables.Changes.Add(new VariableChange(p, "Core tag", oldtag, newtag));
                                            }
                                            else if (p.GetClaims().Contains(GlobalVariables.SelectedCountry.Tag))
                                            {
                                                p.RemoveClaim(oldtag, true);
                                                p.AddClaim(newtag, true);
                                                GlobalVariables.Changes.Add(new VariableChange(p, "Claim tag", oldtag, newtag));
                                            }
                                        }
                                        CountryTagBox.Text = GlobalVariables.SelectedCountry.Tag;
                                    }
                                    else
                                    {
                                        CountryTagBox.Text = GlobalVariables.SelectedCountry.Tag;
                                    }
                                }
                                else
                                {
                                    //TODO
                                    //REMOVE THE COUNTRY
                                    CountryTagBox.Text = GlobalVariables.SelectedCountry.Tag;
                                }
                            }
                        }
                        break;
                    case "CountryNameBox":
                        {
                            if (GlobalVariables.SelectedCountry != null)
                            {
                                if (CountryNameBox.Text != "" && CountryNameBox.Text.All(x => char.IsLetter(x) || char.IsDigit(x)))
                                {
                                    string newname = CountryNameBox.Text;
                                    string oldname = "";
                                    if (GlobalVariables.Countries.Any(x => x.FullName != CountryNameBox.Text))
                                    {
                                        oldname = GlobalVariables.SelectedCountry.FullName;
                                        GlobalVariables.SelectedCountry.FullName = newname;
                                        if (GlobalVariables.SelectedCountry.HistoryFile != null)
                                        {
                                            if (!GlobalVariables.SelectedCountry.HistoryFile.ReadOnly)
                                                File.Delete(GlobalVariables.SelectedCountry.HistoryFile.Path);
                                            GlobalVariables.SelectedCountry.HistoryFile.Path = GlobalVariables.pathtomod + $"history\\countries\\{GlobalVariables.SelectedCountry.Tag} - {GlobalVariables.SelectedCountry.FullName}.txt";
                                            GlobalVariables.SelectedCountry.HistoryFile.SaveFile();
                                        }

                                        if (GlobalVariables.SelectedCountry.CommonFile != null)
                                        {
                                            if (!GlobalVariables.SelectedCountry.CommonFile.ReadOnly)
                                                File.Delete(GlobalVariables.SelectedCountry.CommonFile.Path);
                                            GlobalVariables.SelectedCountry.CommonFile.Path = GlobalVariables.pathtomod + $"common\\countries\\{GlobalVariables.SelectedCountry.FullName}.txt";
                                            GlobalVariables.SelectedCountry.CommonFile.SaveFile();
                                        }

                                        if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TagFile))
                                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TagFile));
                                        UpdateCountryListUsingControls();
                                        CountryNameBox.Text = GlobalVariables.SelectedCountry.FullName;
                                    }
                                    else
                                    {
                                        CountryNameBox.Text = GlobalVariables.SelectedCountry.FullName;
                                    }
                                }
                                else
                                {
                                    //TODO
                                    //REMOVE THE COUNTRY
                                    CountryNameBox.Text = GlobalVariables.SelectedCountry.FullName;
                                }
                            }
                        }
                        break;
                    default:
                        switch ((string)stb.Tag)
                        {
                            case "name":
                                if (stb.Text == "" || string.IsNullOrWhiteSpace(stb.Text))
                                    GlobalVariables.SelectedCountry.MonarchNames.Remove((MonarchName)stb.Parent.Tag);
                                else
                                {
                                    if (GlobalVariables.SelectedCountry.MonarchNames.Any(x => x.Name == stb.Text && x != stb.Parent.Tag))
                                    {
                                        MessageBox.Show("This name already is on the list!");
                                        stb.Text = ((MonarchName)stb.Parent.Tag).Name;
                                    }
                                    else if (((MonarchName)stb.Parent.Tag).Name != stb.Text)
                                    {
                                        ((MonarchName)stb.Parent.Tag).Name = stb.Text;
                                        UpdateMonarchNames();
                                    }
                                }
                                break;
                            case "chance":
                                if (stb.Text == "" || string.IsNullOrWhiteSpace(stb.Text))
                                    ((MonarchName)stb.Parent.Tag).Chance = 0;
                                else
                                {
                                    if (stb.Text.Any(x => !char.IsNumber(x) && x != '-'))
                                    {
                                        MessageBox.Show("You can only have numbers in the chances box!");
                                        stb.Text = ((MonarchName)stb.Parent.Tag).Chance.ToString();
                                    }
                                    else if(((MonarchName)stb.Parent.Tag).Chance != int.Parse(stb.Text))
                                    {
                                        ((MonarchName)stb.Parent.Tag).Chance = int.Parse(stb.Text);
                                        UpdateMonarchNames();
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }

        }


        #endregion

        private void SaveNamesToFiles_Click(object sender, EventArgs e)
        {
            foreach(Country c in GlobalVariables.Countries)
            {
                if (!(c.CommonFile?.ReadOnly ?? true))
                {
                    NodeFile n = c.CommonFile;

                    Node monarchnames = n.MainNode.Nodes.Find(x => x.Name == "monarch_names");
                    if (monarchnames == null)
                        monarchnames = n.MainNode.AddNode("monarch_names");
                    monarchnames.ItemOrder.Clear();
                    monarchnames.Variables.Clear();
                    foreach(MonarchName mn in c.MonarchNames)
                    {
                        monarchnames.AddVariable($"\"{mn.Name}\"", mn.Chance.ToString());
                    }

                    Node leadernames = n.MainNode.Nodes.Find(x => x.Name == "leader_names");
                    if (leadernames == null)
                        leadernames = n.MainNode.AddNode("leader_names");
                    leadernames.RemoveAllPureValues();
                    foreach (string ln in c.LeaderNames)
                    {
                        if (ln.Contains(" "))
                            leadernames.AddPureValue($"\"{ln}\"");
                        else
                            leadernames.AddPureValue(ln);
                    }

                    Node shipnames = n.MainNode.Nodes.Find(x => x.Name == "ship_names");
                    if (shipnames == null)
                        shipnames = n.MainNode.AddNode("ship_names");
                    shipnames.RemoveAllPureValues();
                    foreach (string sn in c.ShipNames)
                    {
                        if (sn.Contains(" "))
                            shipnames.AddPureValue($"\"{sn}\"");
                        else
                            shipnames.AddPureValue(sn);
                    }

                    Node fleetnames = n.MainNode.Nodes.Find(x => x.Name == "fleet_names");
                    if (fleetnames == null)
                        fleetnames = n.MainNode.AddNode("fleet_names");
                    fleetnames.RemoveAllPureValues();
                    foreach (string fn in c.FleetNames)
                    {
                        if (fn.Contains(" "))
                            fleetnames.AddPureValue($"\"{fn}\"");
                        else
                            fleetnames.AddPureValue(fn);
                    }

                    Node armynames = n.MainNode.Nodes.Find(x => x.Name == "army_names");
                    if (armynames == null)
                        armynames = n.MainNode.AddNode("army_names");
                    armynames.RemoveAllPureValues();
                    foreach (string an in c.ArmyNames)
                    {
                        if (an.Contains(" "))
                            armynames.AddPureValue($"\"{an}\"");
                        else
                            armynames.AddPureValue(an);
                    }
                    n.SaveFile(c.CommonFile.Path);
                }               
            }
        }

        public void AreaNameChange()
        {
            
            if (AreaBox.SelectedIndex <= 0)
                return;
            if (GlobalVariables.Areas.Any(x => x.Name == AreaNameChangeBox.Text))
                AreaNameChangeBox.Text = AreaBox.Text;
            else 
            {
                if (AreaNameChangeBox.Text == "")
                {
                    int index = AreaBox.SelectedIndex;
                    GlobalVariables.Areas[index - 1].Provinces.ForEach(x => x.Area = null);
                    GlobalVariables.Regions.ForEach(x => x.Areas.Remove(GlobalVariables.Areas[index - 1]));
                    GlobalVariables.Areas.RemoveAt(index - 1);
                    AreaBox.Items.RemoveAt(index);
                    AreaBox.SelectedIndex = 0;
                }
                else
                {
                    GlobalVariables.Areas[AreaBox.SelectedIndex - 1].Name = AreaNameChangeBox.Text;
                    AreaBox.Items[AreaBox.SelectedIndex] = AreaNameChangeBox.Text;
                }
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Area))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Area));
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Region))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Region));
            }
        }

        public void RegionNameChange()
        {
            if (RegionBox.SelectedIndex <= 0)
                return;
            if (GlobalVariables.Regions.Any(x => x.Name == RegionNameChangeBox.Text))
                RegionNameChangeBox.Text = RegionBox.Text;
            else
            {
                if (RegionNameChangeBox.Text == "")
                {
                    int index = RegionBox.SelectedIndex;
                    GlobalVariables.Regions[index - 1].Areas.ForEach(x => x.Region = null);
                    GlobalVariables.Superregions.ForEach(x => x.Regions.Remove(GlobalVariables.Regions[index - 1]));
                    GlobalVariables.Regions.RemoveAt(index - 1);                    
                    RegionBox.Items.RemoveAt(index);
                    RegionBox.SelectedIndex = 0;
                }
                else if(RegionBox.SelectedIndex != 0)
                {
                    GlobalVariables.Regions[RegionBox.SelectedIndex - 1].Name = RegionNameChangeBox.Text;
                    RegionBox.Items[RegionBox.SelectedIndex] = RegionNameChangeBox.Text;
                }
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Region))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Region));
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Region))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Region));
            }
        }

        public void ContinentNameChange()
        {
            if (ContinentBox.SelectedIndex <= 0)
                return;
            if (GlobalVariables.Continents.Any(x => x.Name == ContinentNameChangeBox.Text))
                ContinentNameChangeBox.Text = ContinentBox.Text;
            else
            {
                if (ContinentNameChangeBox.Text == "")
                {
                    int index = ContinentBox.SelectedIndex;
                    GlobalVariables.Continents[index - 1].Provinces.ForEach(x => x.Continent = null);
                    GlobalVariables.Continents.RemoveAt(index - 1);
                    ContinentBox.Items.RemoveAt(index);
                    ContinentBox.SelectedIndex = 0;
                }
                else if(ContinentBox.SelectedIndex != 0)
                {
                    GlobalVariables.Continents[ContinentBox.SelectedIndex - 1].Name = ContinentNameChangeBox.Text;
                    ContinentBox.Items[ContinentBox.SelectedIndex] = ContinentNameChangeBox.Text;
                }
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Continent))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Continent));
            }
        }

        public void SuperregionNameChange()
        {
            if (SuperregionBox.SelectedIndex <= 0)
                return;
            if (GlobalVariables.Superregions.Any(x => x.Name == SuperregionNameChangeBox.Text))
                SuperregionNameChangeBox.Text = SuperregionBox.Text;
            else
            {
                if (SuperregionNameChangeBox.Text == "")
                {
                    int index = SuperregionBox.SelectedIndex;
                    GlobalVariables.Superregions[index - 1].Regions.ForEach(x => x.Superregion = null);
                    GlobalVariables.Superregions.RemoveAt(index - 1);
                    SuperregionBox.Items.RemoveAt(index);
                    SuperregionBox.SelectedIndex = 0;
                }
                else if(SuperregionBox.SelectedIndex != 0)
                {
                    GlobalVariables.Superregions[SuperregionBox.SelectedIndex - 1].Name = SuperregionNameChangeBox.Text;
                    SuperregionBox.Items[SuperregionBox.SelectedIndex] = SuperregionNameChangeBox.Text;
                }
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Superregion))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Superregion));
            }
        }

        public void TradeCompanyNameChange()
        {
            if (TradeCompanyBox.SelectedIndex <= 0)
                return;
            if (GlobalVariables.TradeCompanies.Any(x => x.Name == TradeCompanyNameChangeBox.Text))
                TradeCompanyNameChangeBox.Text = TradeCompanyBox.Text;
            else
            {
                if (TradeCompanyNameChangeBox.Text == "")
                {
                    int index = TradeCompanyBox.SelectedIndex;
                    GlobalVariables.TradeCompanies[index - 1].Provinces.ForEach(x => x.TradeCompany = null);
                    GlobalVariables.TradeCompanies.RemoveAt(index - 1);
                    TradeCompanyBox.Items.RemoveAt(index);
                    TradeCompanyBox.SelectedIndex = 0;
                }
                else if (TradeCompanyBox.SelectedIndex != 0)
                {
                    GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Name = TradeCompanyNameChangeBox.Text;
                    TradeCompanyBox.Items[TradeCompanyBox.SelectedIndex] = TradeCompanyNameChangeBox.Text;
                }
                if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.TradeCompany))
                    GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.TradeCompany));
            }
        }

        public void DisplayOnConsole(string text)
        {
            ConsoleBox.AppendText(Environment.NewLine + text);
        }

        public void SendToConsole(string message)
        {
            DisplayOnConsole(message);
            switch(message)
            {
                case "help":
                    DisplayOnConsole("savein <type> <filename>");
                    DisplayOnConsole("\tTypes: area region superregion continent climate");
                    DisplayOnConsole("\tThe file will be saved in a 'ModEditor' directory in the location of the tool with a specified name (.txt will be added automatically)");
                    DisplayOnConsole("");
                    DisplayOnConsole("savehistory <type> <object> <filename>");
                    DisplayOnConsole("\tTypes: province country");
                    DisplayOnConsole("\tObject: ID or TAG");
                    DisplayOnConsole("\tThe file will be saved in a 'ModEditor' directory in the location of the tool with a specified name (.txt will be added automatically)");
                    DisplayOnConsole("");
                    break;
                default:
                    string[] arguments = message.Split(' ');
                    switch (arguments[0])
                    {

                        case "savehistory":
                            if (arguments.Count() != 4)
                                DisplayOnConsole("Not enough arguments!");
                            switch (arguments[1])
                            {
                                case "province":
                                    {
                                        if (!Directory.Exists("ModEditor\\"))
                                            Directory.CreateDirectory("ModEditor\\");
                                        NodeFile nf = new NodeFile
                                        {
                                            Path = $"ModEditor\\{arguments[3]}.txt"
                                        };
                                        Province p = GlobalVariables.Provinces.Find(x => x.ID.ToString() == arguments[2]);
                                        if (p == null)
                                            SendToConsole("Province not found!");
                                        else
                                            Saving.WriteToNodeFile(nf, p);
                                        nf.SaveFile($"ModEditor\\{arguments[3]}.txt");
                                    }
                                    break;
                                case "country":
                                    {
                                        if (!Directory.Exists("ModEditor\\"))
                                            Directory.CreateDirectory("ModEditor\\");
                                        NodeFile nf = new NodeFile
                                        {
                                            Path = $"ModEditor\\{arguments[3]}.txt"
                                        };
                                        Country c = GlobalVariables.Countries.Find(x => x.Tag.ToLower() == arguments[2].ToLower());
                                        if (c == null)
                                            SendToConsole("Country not found!");
                                        else
                                            Saving.WriteToNodeFile(nf, c);
                                        nf.SaveFile($"ModEditor\\{arguments[3]}.txt");
                                    }
                                    break;
                            }
                            break;

                        case "savein":
                            if (arguments.Count() != 3)
                                DisplayOnConsole("Not enough arguments!");
                            else
                            {
                                switch (arguments[1])
                                {
                                    case "area":
                                        {
                                            if (!Directory.Exists("ModEditor\\"))
                                                Directory.CreateDirectory("ModEditor\\");
                                            NodeFile nf = new NodeFile
                                            {
                                                Path = $"ModEditor\\{arguments[2]}.txt"
                                            };
                                            Saving.WriteToNodeFile(nf, new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Area));                                     
                                            nf.SaveFile($"ModEditor\\{arguments[2]}.txt");
                                        }
                                        break;
                                    case "region":
                                        {
                                            if (!Directory.Exists("ModEditor\\"))
                                                Directory.CreateDirectory("ModEditor\\");
                                            NodeFile nf = new NodeFile
                                            {
                                                Path = $"ModEditor\\{arguments[2]}.txt"
                                            };
                                            Saving.WriteToNodeFile(nf, new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Region));
                                            nf.SaveFile($"ModEditor\\{arguments[2]}.txt");
                                        }
                                        break;
                                    case "superregion":
                                        {
                                            if (!Directory.Exists("ModEditor\\"))
                                                Directory.CreateDirectory("ModEditor\\");
                                            NodeFile nf = new NodeFile
                                            {
                                                Path = $"ModEditor\\{arguments[2]}.txt"
                                            };
                                            Saving.WriteToNodeFile(nf, new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Superregion));
                                            nf.SaveFile($"ModEditor\\{arguments[2]}.txt");
                                        }
                                        break;
                                    case "continent":
                                        {
                                            if (!Directory.Exists("ModEditor\\"))
                                                Directory.CreateDirectory("ModEditor\\");
                                            NodeFile nf = new NodeFile
                                            {
                                                Path = $"ModEditor\\{arguments[2]}.txt"
                                            };
                                            Saving.WriteToNodeFile(nf, new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Continent));
                                            nf.SaveFile($"ModEditor\\{arguments[2]}.txt");
                                        }
                                        break;
                                    case "climate":
                                        {
                                            if (!Directory.Exists("ModEditor\\"))
                                                Directory.CreateDirectory("ModEditor\\");
                                            NodeFile nf = new NodeFile
                                            {
                                                Path = $"ModEditor\\{arguments[2]}.txt"
                                            };
                                            Saving.WriteToNodeFile(nf, new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
                                            nf.SaveFile($"ModEditor\\{arguments[2]}.txt");
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                    break;
            }
        }

        private void SendConsoleButton_Click(object sender, EventArgs e)
        {
            if (ConsoleInputBox.Text != "")
            {
                SendToConsole(ConsoleInputBox.Text);
                ConsoleInputBox.Text = "";
            }
        }
    }
}