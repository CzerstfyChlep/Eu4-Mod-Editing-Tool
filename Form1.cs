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
            GlobalVariables.DevelopmentBitmap = new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height);
            GlobalVariables.DevelopmentBitmapLocked = new LockBitmap(GlobalVariables.DevelopmentBitmap);
            GlobalVariables.TradeGoodBitmapLocked = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.ReligionBitmapLocked = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.CultureBitmapLocked = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.PoliticalBitmapLocked = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.ClickedMask = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.AreaBitmapLocked = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.RegionBitmapLocked = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.TradeNodeBitmap = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.HREBitmap = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.FortBitmap = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.ContinentBitmap = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.SuperregionBitmap = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.BaseWhiteProvincesBitmap = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.DiscoveredByBitmap = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.TradeCompanyLocked = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.GovernmentLocked = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));

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

            LoadFilesClass.LoadFiles();

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

            HideSeaTiles.Click += ShowHideSeaTilesAreaMapmode_Click;

            boxes.AddRange(new ComboBox[] { OwnerBox, ReligionBox, CultureBox, TradeGoodBox, CountryBox, CountryReligionBox, AreaBox, RegionBox, ProvinceTradeNodeBox, TradeNodeBox, AddTradeNodeDestinationBox, ContinentBox, AddCoreBox, TechnologyGroupBox, CountryPrimaryCultureBox, DiscoveredByBox, BuildingsBox, SuperregionBox, TradeCompanyBox });
            textboxes.AddRange(new TextBox[] { AreaNameChangeBox, AddNewAreaBox, AddNewRegionBox, RegionNameChangeBox, ChangeTradeNodeNameBox, TradeNodeNameBox, TradeNodeProvinceLocationBox, ContinentNameChangeBox, AddNewContinentBox, SuperregionNameChangeBox, AddNewSuperregionBox, TradeCompanyNameChangeBox, AddNewTradeCompanyBox });
            foreach (TradeGood tg in GlobalVariables.TradeGoods)
            {
                CreateTradeGoodsInfoBox(tg);
            }

            foreach (string s in GlobalVariables.TechGroups)
            {
                TechnologyGroupBox.Items.Add(s);
                DiscoveredByBox.Items.Add(s);
            }

            MoveCameraTo(GlobalVariables.Provinces[0]);
            GlobalVariables.UpdtGraphicsThread.Start();
            GotFocus += GainedFocus;

            MapManagement.CreateBaseWhiteMap();

            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.OwnerCountry == null)
                    continue;
                if (p.GetCores().Contains(p.OwnerCountry.Tag))
                    continue;
                p.AddCore(p.OwnerCountry.Tag, true);
                //GlobalVariables.ToUpdate.Add(p);
            }
            //Saving.SaveThingsToUpdate();

            MacroReligionBox.DataSource = Religion.Religions;
            MacroCultureBox.DataSource = Culture.Cultures;
            MacroAreaBox.DataSource = GlobalVariables.Areas;
            MacroRegionBox.DataSource = GlobalVariables.Regions;
            MacroSuperregionBox.DataSource = GlobalVariables.Superregions;
            MacroContinentBox.DataSource = GlobalVariables.Continents;
            MacroTradeNodeBox.DataSource = GlobalVariables.TradeNodes;
            MacroTechGroupBox.DataSource = GlobalVariables.TechGroups;
            //TODO
            //Replace all InternalValueChanges with this
            //To do this I need a special class for binding all provinces
            //ToTest.DataBindings.Add("Value", GlobalVariables.ClickedProvince, "Tax", true, DataSourceUpdateMode.OnPropertyChanged, 0);
        }

        private void ModEditor_Load(object sender, EventArgs e)
        {
            UpMp();
        }

        public void GainedFocus(object sender, EventArgs e)
        {
            UpdateMap();
        }

        public void UpMp()
        {
            UpdateMap();
        }

        public static Graphics graphics;

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

        public static ModEditor form;
        public static List<ComboBox> boxes = new List<ComboBox>();
        public static List<GroupBox> TradeGoodInfoBoxes = new List<GroupBox>();
        public static List<TextBox> textboxes = new List<TextBox>();



#pragma warning disable CS1998
        async public void UpdateDevCount()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int Total = 0;
            int ProvTotal = 0;
            foreach (Province p in GlobalVariables.Provinces)
            {
                int total = p.Tax + p.Production + p.Manpower;
                if (total > 2)
                {
                    Total += total;
                    ProvTotal++;
                }
            }
            double Average = Total / (double)ProvTotal;
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

                    if (x + 1090 >= GlobalVariables.ProvincesMap.Width)
                        x = GlobalVariables.ProvincesMap.Width - 1090;
                    if (y + 770 >= GlobalVariables.ProvincesMap.Height)
                        y = GlobalVariables.ProvincesMap.Height - 770;

                    GlobalVariables.CameraPosition = new Point(x, y);
                }
                UpdateMap();
            }
        }

        void MouseClickHandler(object sender, MouseEventArgs e)
        {

            //TODO
            //when scaling the game don't forget about this
            if (sender == this)
            {
                if (e.Location.X > 40 && e.Location.X < 1130 && e.Location.Y > 40 && e.Location.Y < 810)
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
                            if (p.HistoryFile != null)
                            {
                                //TODO
                                //stop previous ChangeClickedProvince calls
                                ChangeClickedProvince(p);

                            }
                        }
                    }
                    UpdateMap();
                }
                this.Focus();
            }
        }

        public void UpdateProvincePanel(Province p = null)
        {

            //TODO
            //make this whole thing work with ClickedProvinces

            //TODO 
            //Actually removed Clicked province entirely with a list with one element
            if (p == null)
            {
                if (GlobalVariables.ClickedProvince == null)
                    return;
                else
                    p = GlobalVariables.ClickedProvince;
            }

            //TODO
            //remove when there are averages here
            ProvinceTaxNumeric.Enabled = true;
            ProvinceManpowerNumeric.Enabled = true;
            ProvinceProductionNumeric.Enabled = true;



            GlobalVariables.MultiProvinceMode = false;

            //TODO
            //why is this here again?
            if (GlobalVariables.ClickedProvinces.Any())
            {
                MapManagement.UpdateClickedMap(GlobalVariables.ClickedProvinces, Color.White, false);
            }

            GlobalVariables.ClickedProvinces.Clear();


            GlobalVariables.ChangedSomething = false;

            if (GlobalVariables.BorderingMode)
            {
                MapManagement.UpdateClickedMap(GlobalVariables.Provinces, Color.Green, false);
            }
            GlobalVariables.ClickedProvince = p;
            MapManagement.UpdateClickedMap(new List<Province>() { p }, Color.White, true);
            if (GlobalVariables.BorderingMode)
            {
                MapManagement.UpdateClickedMap(p.BorderingProvinces, Color.Blue, true);
            }
            ProvinceLabelID.Text = "ID: " + p.ID;
            ProvinceColorLabelR.Text = "R: " + p.R;
            ProvinceColorLabelG.Text = "G: " + p.G;
            ProvinceColorLabelB.Text = "B: " + p.B;
            if (p.Sea || p.Lake)
                ProvinceSeaLakeLabel.Text = "S/L: Yes";
            else
                ProvinceSeaLakeLabel.Text = "S/L: No";

            ChangeValueInternally(ProvinceTaxNumeric, p.Tax);
            ChangeValueInternally(ProvinceProductionNumeric, p.Production);
            ChangeValueInternally(ProvinceManpowerNumeric, p.Manpower);
            ChangeValueInternally(TradeGoodBox, TradeGoodBox.Items.IndexOf(p.TradeGood?.ReadableName ?? ""));
            ChangeValueInternally(LatentTradeGoodBox, LatentTradeGoodBox.Items.IndexOf(p.LatentTradeGood?.ReadableName ?? ""));
            ChangeValueInternally(ReligionBox, ReligionBox.Items.IndexOf(p.Religion?.ReadableName ?? ""));
            ChangeValueInternally(CultureBox, CultureBox.Items.IndexOf(p.Culture?.Name ?? ""));
            ChangeValueInternally(HRECheckbox, p.HRE);
            ChangeValueInternally(FortCheckbox, p.Fort);
            ChangeValueInternally(CenterOfTradeNumeric, p.CenterOfTrade);
            int OwnerIndex = OwnerBox.Items.IndexOf((p.OwnerCountry?.FullName ?? "") + ", " + (p.OwnerCountry?.Tag ?? ""));
            ChangeValueInternally(OwnerBox, OwnerIndex);
            ChangeValueInternally(CountryBox, OwnerIndex);
            

            if (p.Controller != "")
            {
                int index = ControllerBox.Items.IndexOf(p.Controller);
                if (ControllerBox.SelectedIndex != index)
                {
                    ChangeValueInternally(ControllerBox, index);
                }
                if (ControllerBox.SelectedIndex != index)
                {
                    ControllerBox.SelectedIndex = index;
                }
            }
            else
            {
                if (ControllerBox.SelectedIndex != 0)
                {
                    ChangeValueInternally(ControllerBox, 0);
                }

                //TODO
                //Figure out why is this here

                /*
                if (ControllerBox.SelectedIndex != 0)
                {
                    ControllerBox.SelectedIndex = 0;
                }
                */
            }


            if (p.Area != null)
            {
                if (AreaBox.SelectedIndex != GlobalVariables.Areas.IndexOf(p.Area) + 1)
                {
                    ChangeValueInternally(AreaBox, GlobalVariables.Areas.IndexOf(p.Area) + 1);
                }

                if (p.Area.Region != null)
                {
                    if (RegionBox.SelectedIndex != GlobalVariables.Regions.IndexOf(p.Area.Region) + 1)
                    {
                        ChangeValueInternally(RegionBox, GlobalVariables.Regions.IndexOf(p.Area.Region) + 1);
                    }
                    if (p.Area.Region.Superregion != null)
                    {
                        if (SuperregionBox.SelectedIndex != GlobalVariables.Superregions.IndexOf(p.Area.Region.Superregion) + 1)
                        {
                            ChangeValueInternally(SuperregionBox, GlobalVariables.Superregions.IndexOf(p.Area.Region.Superregion) + 1);
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
            if (p.Continent != null)
            {
                if (GlobalVariables.Continents.IndexOf(p.Continent) + 1 != ContinentBox.SelectedIndex)                
                    ChangeValueInternally(ContinentBox, GlobalVariables.Continents.IndexOf(p.Continent) + 1);
                
                //TODO
                // Why was this here?
                //ContinentBox.SelectedIndex = GlobalVariables.Continents.IndexOf(p.Continent) + 1;
            }
            else
            {
                if (ContinentBox.SelectedIndex != 0)                
                    ChangeValueInternally(ContinentBox, 0);
                
            }



            if (p.TradeNode != null)
            {
                if (GlobalVariables.TradeNodes.IndexOf(p.TradeNode) + 1 != ProvinceTradeNodeBox.SelectedIndex)
                {
                    ChangeValueInternally(ProvinceTradeNodeBox, GlobalVariables.TradeNodes.IndexOf(p.TradeNode) + 1);
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

            if (p.TradeCompany != null)
            {
                if (GlobalVariables.TradeCompanies.IndexOf(p.TradeCompany) + 1 != TradeCompanyBox.SelectedIndex)
                {
                    ChangeValueInternally(TradeCompanyBox, GlobalVariables.TradeCompanies.IndexOf(p.TradeCompany) + 1);
                    TradeCompanyColorButton.BackColor = p.TradeCompany.Color;
                }
            }
            else
            {
                if (TradeCompanyBox.SelectedIndex != 0)                
                    ChangeValueInternally(TradeCompanyBox, 0);
                
            }

            if(GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV" + p.ID))           
                ProvinceNameLocalisationBox.Text = GlobalVariables.ModLocalisationEntries["PROV" + p.ID];
            else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV" + p.ID))
                ProvinceNameLocalisationBox.Text = GlobalVariables.LocalisationEntries["PROV" + p.ID];
            else
                ProvinceNameLocalisationBox.Text = "";

            if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV_ADJ" + p.ID))
                ProvinceAdjectiveLocalisationBox.Text = GlobalVariables.ModLocalisationEntries["PROV_ADJ" + p.ID];
            else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV_ADJ" + p.ID))
                ProvinceAdjectiveLocalisationBox.Text = GlobalVariables.LocalisationEntries["PROV_ADJ" + p.ID];
            else
                ProvinceAdjectiveLocalisationBox.Text = "";


            IsCityCheckbox.Checked = p.City;


            UpdateCoresPanel();
            UpdateDiscoveredBy();
            UpdateBuildings();
        }


        void ChangeClickedProvince(Province p)
        {
            //if (GlobalVariables.ClickedProvince != null && GlobalVariables.ChangedSomething)
            //if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
            //   GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);

            if (GlobalVariables.ClickedProvince != null)
                MapManagement.UpdateClickedMap(new List<Province>() { GlobalVariables.ClickedProvince }, Color.White, false);

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

                return;
            }

            UpdateProvincePanel(p);

            //label5.Text = p.TradeGood.Price + " " + p.TradeGood.GoldLike;

            if (p.OwnerCountry != null)
            {
                if (GlobalVariables.SelectedDiscoveredByTechGroup != p.OwnerCountry.TechnologyGroup && GlobalVariables.mapmode == MapManagement.UpdateMapOptions.DiscoveredBy)
                {
                    GlobalVariables.SelectedDiscoveredByTechGroup = p.OwnerCountry.TechnologyGroup;
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);
                }
            }
        }

        void AddToClickedProvinces(Province p, bool Update = true)
        {
            AddToClickedProvinces(new List<Province> { p }, Update);
        }

        void UpdateTotalSelectedLabel()
        {
            MacroTotalSelected.Text = $"Selected: {GlobalVariables.ClickedProvinces.Count}/{GlobalVariables.Provinces.Count} ({((double)GlobalVariables.ClickedProvinces.Count / GlobalVariables.Provinces.Count).ToString("p")})";
        }

        void AddToClickedProvinces(List<Province> p, bool Update = true)
        {
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

            MapManagement.UpdateClickedMap(UpdateMapList, Color.LightYellow);

            if (GlobalVariables.ClickedProvince != null)
                MapManagement.UpdateClickedMap(new List<Province>() { GlobalVariables.ClickedProvince }, Color.White, false);
            GlobalVariables.ClickedProvince = null;
            GlobalVariables.MultiProvinceMode = true;
            ProvinceLabelID.Text = "ID: N/A";
            ProvinceColorLabelR.Text = "R: N/A";
            ProvinceColorLabelG.Text = "G: N/A";
            ProvinceColorLabelB.Text = "B: N/A";
            ProvinceSeaLakeLabel.Text = "S/L: N/A";


            //TODO
            // Add averages and enable adding one for each

            ProvinceTaxNumeric.Enabled = false;
            ProvinceManpowerNumeric.Enabled = false;
            ProvinceProductionNumeric.Enabled = false;

            UpdateTotalSelectedLabel();

            UpdateMap();

        }

        void RemoveFromClickedProvinces(Province p, bool Update = true){
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

        public void OnExitDo(object sender, EventArgs e)
        {
            GlobalVariables.UpdtGraphicsThread.Abort();

            //TODO
            //Save files to temp?
        }

        public void UpdateGraphics()
        {

            //TODO
            //Figure out why this doesn't work
            int a = 0;
            while (true)
            {
                Thread.Sleep(100);
                UpdateMap();
                a++;
                if (a == 8)
                    break;
            }
        }

        public static void UpdateMap()
        {
            //TODO
            //make this update only a part of the screen saving resources

            switch (GlobalVariables.mapmode)
            {
                case MapManagement.UpdateMapOptions.Provinces:
                    graphics.DrawImage(GlobalVariables.ProvincesMap, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Development:
                    graphics.DrawImage(GlobalVariables.DevelopmentBitmap, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.TradeGood:
                    graphics.DrawImage(GlobalVariables.TradeGoodBitmapLocked.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Religion:
                    graphics.DrawImage(GlobalVariables.ReligionBitmapLocked.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Culture:
                    graphics.DrawImage(GlobalVariables.CultureBitmapLocked.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Political:
                    graphics.DrawImage(GlobalVariables.PoliticalBitmapLocked.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Area:
                    graphics.DrawImage(GlobalVariables.AreaBitmapLocked.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Region:
                    graphics.DrawImage(GlobalVariables.RegionBitmapLocked.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.TradeNode:
                    graphics.DrawImage(GlobalVariables.TradeNodeBitmap.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.HRE:
                    graphics.DrawImage(GlobalVariables.HREBitmap.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Fort:
                    graphics.DrawImage(GlobalVariables.FortBitmap.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Continent:
                    graphics.DrawImage(GlobalVariables.ContinentBitmap.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Superregion:
                    graphics.DrawImage(GlobalVariables.SuperregionBitmap.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.DiscoveredBy:
                    graphics.DrawImage(GlobalVariables.DiscoveredByBitmap.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.TradeCompany:
                    graphics.DrawImage(GlobalVariables.TradeCompanyLocked.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
                case MapManagement.UpdateMapOptions.Government:
                    graphics.DrawImage(GlobalVariables.GovernmentLocked.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
                    break;
            }

            //TODO
            // make this not flicker

            graphics.DrawImage(GlobalVariables.ClickedMask.source, new Rectangle(40, 40, 1090, 770), new Rectangle(GlobalVariables.CameraPosition, new Size(1090, 770)), GraphicsUnit.Pixel);
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
                if((int)value == -1)
                    ((ComboBox)control).SelectedIndex = 0;
                else
                    ((ComboBox)control).SelectedIndex = (int)value;
            }
            else if (control is TextBox)
                ((TextBox)control).Text = (string)value;
            GlobalVariables.InternalChanges = false; 
        }

        private void ProvinceTaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)            
                return;           
            if (GlobalVariables.ClickedProvince != null)
            {
                if (GlobalVariables.ClickedProvince.TradeGood != null)
                {
                    GlobalVariables.ClickedProvince.TradeGood.TotalDev += (int)ProvinceTaxNumeric.Value - GlobalVariables.ClickedProvince.Tax;
                }
                GlobalVariables.ClickedProvince.Tax = (int)ProvinceTaxNumeric.Value;
            }
            MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.Development);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();

            //if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
            //    GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
            GlobalVariables.UpdateDevInfo = new Task(UpdateDevCount);
            GlobalVariables.UpdateDevInfo.Start();
            RefreshTradeGoodsTab();
        }

        private void ProvinceProductionNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvince != null)
            {
                if (GlobalVariables.ClickedProvince.TradeGood != null)
                {
                    GlobalVariables.ClickedProvince.TradeGood.TotalDev += (int)ProvinceProductionNumeric.Value - GlobalVariables.ClickedProvince.Production;
                }
                GlobalVariables.ClickedProvince.Production = (int)ProvinceProductionNumeric.Value;

            }
            MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.Development);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
            // if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
            //    GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
            GlobalVariables.UpdateDevInfo = new Task(UpdateDevCount);
            GlobalVariables.UpdateDevInfo.Start();
            RefreshTradeGoodsTab();

        }

        private void ProvinceManpowerNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.ClickedProvince != null)
            {
                GlobalVariables.ClickedProvince.Manpower = (int)ProvinceManpowerNumeric.Value;
                if (GlobalVariables.ClickedProvince.TradeGood != null)
                {
                    GlobalVariables.ClickedProvince.TradeGood.TotalDev += (int)ProvinceManpowerNumeric.Value - GlobalVariables.ClickedProvince.Manpower;
                }
            }
            MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.Development);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
            //  if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
            //      GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
            GlobalVariables.UpdateDevInfo = new Task(UpdateDevCount);
            GlobalVariables.UpdateDevInfo.Start();
            RefreshTradeGoodsTab();

        }

        private void TradeGoodBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.MultiProvinceMode)
            {
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
                    RefreshTradeGoodsTab();
                }
            }
            else
            {
                if (GlobalVariables.InternalChanges)
                    return;
                if (GlobalVariables.ClickedProvince != null)
                    {
                        if (GlobalVariables.ClickedProvince.TradeGood != null)
                        {
                            GlobalVariables.ClickedProvince.TradeGood.TotalProvinces--;
                            GlobalVariables.ClickedProvince.TradeGood.TotalDev -= GlobalVariables.ClickedProvince.Tax + GlobalVariables.ClickedProvince.Production + GlobalVariables.ClickedProvince.Manpower;
                        }
                        GlobalVariables.ClickedProvince.TradeGood = GlobalVariables.TradeGoods.Find(x => x.ReadableName == (string)TradeGoodBox.SelectedItem);
                        if (GlobalVariables.ClickedProvince.TradeGood != null)
                        {
                            GlobalVariables.ClickedProvince.TradeGood.TotalProvinces++;
                            GlobalVariables.ClickedProvince.TradeGood.TotalDev += GlobalVariables.ClickedProvince.Tax + GlobalVariables.ClickedProvince.Production + GlobalVariables.ClickedProvince.Manpower;
                        }
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.TradeGood);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeGood)
                        UpdateMap();
                    GlobalVariables.ChangedSomething = true;
                    //if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
                    //    GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    RefreshTradeGoodsTab();
            }
        }

        private void CultureBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (GlobalVariables.InternalChanges)
                return;
            ChangeProvinceInfo(ChangeProvinceMode.Culture, Culture.Cultures.Find(x => x.Name == (string)CultureBox.SelectedItem));

        }

        private void ReligionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            ChangeProvinceInfo(ChangeProvinceMode.Religion, Religion.Religions.Find(x => x.ReadableName == (string)ReligionBox.SelectedItem));
        }

        private void RemoveDevButton_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.ClearDev();
            RefreshTradeGoodsTab();
        }
        private void DevRemoveAll_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.RemoveAll();
            RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void DevIncreaseAll_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.DevIncreaseAll();
            RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void RandomDevLow_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.RandomLowDev();
            RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void RandomDevMed_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.RandomMedDev();
            RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }
        private void RandomDevHigh_Click(object sender, EventArgs e)
        {
            DevelopmentManagement.RandomHighDev();
            RefreshTradeGoodsTab();
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Development)
                UpdateMap();
        }

        private void OwnerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            ChangeProvinceInfo(ChangeProvinceMode.Owner, OwnerBox.SelectedIndex);
        }

        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
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
                Country c = GlobalVariables.Countries.Find(x => CountryBox.SelectedItem.ToString().Split(',')[1].Trim() == x.Tag);
                if (c != null)
                {
                    GlobalVariables.SelectedCountry = c;
                    CountryNameBox.Text = GlobalVariables.SelectedCountry.FullName;
                    CountryTagBox.Text = GlobalVariables.SelectedCountry.Tag;
                    TotalDevelopmentBox.Text = GlobalVariables.SelectedCountry.TotalDevelopment.ToString();
                    CountryProvinceCountBox.Text = GlobalVariables.SelectedCountry.Provinces.Count().ToString();
                    if (GlobalVariables.SelectedCountry.Capital != null)
                        CountryCapitalIDBox.Text = GlobalVariables.SelectedCountry.Capital.ID.ToString();
                    else
                        CountryCapitalIDBox.Text = "";



                    int index = 0;
                    if (GlobalVariables.SelectedCountry.Religion != null)
                        index = CountryReligionBox.Items.IndexOf(GlobalVariables.SelectedCountry.Religion.ReadableName);
                    if (CountryReligionBox.SelectedIndex != index)
                    {
                        if (index == 0)
                            ChangeValueInternally(CountryReligionBox, 0);
                        else
                            ChangeValueInternally(CountryReligionBox, CountryReligionBox.Items.IndexOf(GlobalVariables.SelectedCountry.Religion.ReadableName));
                    }

                    index = GlobalVariables.TechGroups.IndexOf(GlobalVariables.SelectedCountry.TechnologyGroup);
                    if (TechnologyGroupBox.SelectedIndex != index)
                    {
                        ChangeValueInternally(TechnologyGroupBox, index);
                    }

                    index = Culture.Cultures.IndexOf(GlobalVariables.SelectedCountry.PrimaryCulture);
                    if (index == -1)
                        index = 0;
                    if (CountryPrimaryCultureBox.SelectedIndex != index)
                    {
                        if (index != -1)
                            ChangeValueInternally(CountryPrimaryCultureBox, index);

                    }

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
                   
                    ChangeValueInternally(GovernmentRankNumeric, GlobalVariables.SelectedCountry.GovernmentRank);
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
            }
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
                    ChangeClickedProvince(GlobalVariables.SelectedCountry.Capital);
                }
                else
                {
                    MoveCameraTo(GlobalVariables.SelectedCountry.Provinces[0]);
                    ChangeClickedProvince(GlobalVariables.SelectedCountry.Provinces[0]);
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
                Country c = new Country();
                c.HistoryFile = GlobalVariables.pathtomod + "history\\countries\\" + countryForm.Tag + " - " + countryForm.Name + ".txt";
                c.CommonFile = GlobalVariables.pathtomod + "common\\countries\\" + countryForm.Name + ".txt";
                File.WriteAllText(c.HistoryFile, "government = monarchy\nadd_government_reform = feudalism_reform\ngovernment_rank = 1\ntechnology_group = western");
                File.WriteAllText(c.CommonFile, "graphical_culture = westerngfx\ncolor = { " + countryForm.CountryColor.R + " " + countryForm.CountryColor.G + " " + countryForm.CountryColor.B + " }");
                File.AppendAllText(GlobalVariables.pathtomod + "common\\country_tags\\00_countries.txt", "\n" + countryForm.Tag + " = \"countries/" + countryForm.Name + ".txt\"");
                c.Color = countryForm.CountryColor;
                c.FullName = countryForm.Name;
                c.Government = GlobalVariables.Governments[0];
                c.Tag = countryForm.Tag;
                c.TechnologyGroup = "western";
                OwnerBox.Items.Add(c.FullName + ", " + c.Tag);
                CountryBox.Items.Add(c.FullName + ", " + c.Tag);
                GlobalVariables.Countries.Add(c);
            }
        }

        private void CountryReligionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.InternalChanges)
                return;
            if (GlobalVariables.SelectedCountry != null)
            {
                if ((string)CountryReligionBox.SelectedItem != "")
                {
                    GlobalVariables.SelectedCountry.Religion = Religion.Religions.Find(x => x.ReadableName == (string)CountryReligionBox.SelectedItem);
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
            GlobalVariables.ChangedSomething = true;
        }

        private char[] nums = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };


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

        private void TechnologyGroupBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.SelectedCountry != null)
            {
                if(!GlobalVariables.InternalChanges)
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
                    GlobalVariables.SelectedCountry.PrimaryCulture = Culture.Cultures.Find(x=>x.Name == CountryPrimaryCultureBox.Items[CountryPrimaryCultureBox.SelectedIndex].ToString());
                }
            }
        }

        private void CapitalSetClickedButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ClickedProvince != null)
            {
                CountryCapitalIDBox.Text = GlobalVariables.ClickedProvince.ID.ToString();
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
            RefreshTradeGoodsTab();
        }

        private void LatentTradeGoodBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.MultiProvinceMode)
            {
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
                    RefreshTradeGoodsTab();
                }
            }
            else
            {
                if (GlobalVariables.InternalChanges)
                    return;
                if (GlobalVariables.ClickedProvince != null)
                    {
                        if (GlobalVariables.ClickedProvince.LatentTradeGood != null)
                        {
                            GlobalVariables.ClickedProvince.LatentTradeGood.TotalProvinces--;
                            GlobalVariables.ClickedProvince.LatentTradeGood.TotalDev -= GlobalVariables.ClickedProvince.Tax + GlobalVariables.ClickedProvince.Production + GlobalVariables.ClickedProvince.Manpower;
                        }
                        GlobalVariables.ClickedProvince.LatentTradeGood = GlobalVariables.TradeGoods.Find(x => x.ReadableName == (string)LatentTradeGoodBox.SelectedItem);
                        if (GlobalVariables.ClickedProvince.LatentTradeGood != null)
                        {
                            GlobalVariables.ClickedProvince.LatentTradeGood.TotalProvinces++;
                            GlobalVariables.ClickedProvince.LatentTradeGood.TotalDev += GlobalVariables.ClickedProvince.Tax + GlobalVariables.ClickedProvince.Production + GlobalVariables.ClickedProvince.Manpower;
                        }
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.TradeGood);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeGood)
                        UpdateMap();
                    GlobalVariables.ChangedSomething = true;
                    //if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
                    //GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    RefreshTradeGoodsTab();
                
            }
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

        private void AreaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GlobalVariables.InternalChanges)
            {
                int index = AreaBox.SelectedIndex - 1;
                ChangeProvinceInfo(ChangeProvinceMode.Area, index);
            }
            AreaNameChangeBox.Text = AreaBox.Text;
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
            if (GlobalVariables.Areas.Any(x => x.Name == AreaNameChangeBox.Text))
                AreaNameChangeBox.Text = AreaBox.Text;
            else
            {
                GlobalVariables.Areas[AreaBox.SelectedIndex - 1].Name = AreaNameChangeBox.Text;
                AreaBox.Items[AreaBox.SelectedIndex] = AreaNameChangeBox.Text;
            }
        }

        private void SaveAreaFile_Click(object sender, EventArgs e)
        {
            //TODO
            //Save on ReadOnly
            if (GlobalVariables.ReadOnly[9])
                return;
            NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\area.txt");
            List<Node> newNodes = new List<Node>();
            foreach (Area a in GlobalVariables.Areas)
            {
                Node n = nf.MainNode.Nodes.Find(x => x.Name == a.Name);
                if (n != null)
                {
                    n.PureValues.Clear();
                    foreach (Province p in a.Provinces)
                        n.AddPureValue(p.ID.ToString());
                }
                else
                {
                    n = new Node(a.Name);
                    foreach (Province p in a.Provinces)
                        n.AddPureValue(p.ID.ToString());
                }
                newNodes.Add(n);
            }
            nf.MainNode.Nodes.Clear();
            nf.MainNode.Nodes.AddRange(newNodes);
            nf.SaveFile(GlobalVariables.pathtomod + "map\\area.txt");
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
                        if (p.Area != null)
                            p.Area.Provinces.Remove(p);
                        p.Area = GlobalVariables.Areas[index];
                        GlobalVariables.Areas[index].Provinces.Add(p);
                        //if (!GlobalVariables.ToUpdate.Contains(p))
                        // GlobalVariables.ToUpdate.Add(p);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Area);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Area)
                        UpdateMap();
                    //Saving.SaveThingsToUpdate();
                }

                AddNewAreaBox.Text = "";
            }
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

        private void RegionNameChangeSave_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Regions.Any(x => x.Name == RegionNameChangeBox.Text))
                RegionNameChangeBox.Text = RegionBox.Text;
            else
            {
                GlobalVariables.Regions[RegionBox.SelectedIndex - 1].Name = RegionNameChangeBox.Text;
                RegionBox.Items[RegionBox.SelectedIndex] = RegionNameChangeBox.Text;
            }
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
                    //Saving.SaveThingsToUpdate();
                }

                AddNewRegionBox.Text = "";
            }
        }

        private void SaveRegionFile_Click(object sender, EventArgs e)
        {

            //TODO 
            //Save on Read Only
            if (GlobalVariables.ReadOnly[11])
                return;
            NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\region.txt");
            List<Node> newNodes = new List<Node>();
            foreach (Region r in GlobalVariables.Regions)
            {
                Node n = nf.MainNode.Nodes.Find(x => x.Name == r.Name);

                if (n != null)
                {
                    Node purevaluesnode = n.Nodes.Find(x => x.Name == "areas");
                    if (purevaluesnode != null)
                    {
                        purevaluesnode.PureValues.Clear();
                        foreach (Area a in r.Areas)
                            purevaluesnode.AddPureValue(a.Name);
                    }
                    else
                    {
                        purevaluesnode = new Node("areas", n);
                        n.Nodes.Add(purevaluesnode);
                        foreach (Area a in r.Areas)
                            purevaluesnode.AddPureValue(a.Name);
                    }
                }
                else
                {
                    n = new Node(r.Name);
                    Node purevaluesnode = new Node("areas", n);
                    n.Nodes.Add(purevaluesnode);
                    foreach (Area a in r.Areas)
                        purevaluesnode.AddPureValue(a.Name);
                }
                newNodes.Add(n);
            }
            nf.MainNode.Nodes.Clear();
            nf.MainNode.Nodes.AddRange(newNodes);
            nf.SaveFile(GlobalVariables.pathtomod + "map\\region.txt");
        }

        private void OpenWordCreator_Click(object sender, EventArgs e)
        {
            //TODO
            //Make this engine better
            LanguageWindow lw = new LanguageWindow();
            lw.Show();
        }

        private void ProvinceTradeNodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.MultiProvinceMode)
            {
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    int index = ProvinceTradeNodeBox.SelectedIndex - 1;
                    List<Province> provincestoupdate = new List<Province>();
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        if (p.TradeNode != null)
                        {
                            p.TradeNode.Provinces.Remove(p);
                        }
                        if (index == -1)
                            p.TradeNode = null;
                        else
                        {
                            p.TradeNode = GlobalVariables.TradeNodes[index];
                            GlobalVariables.TradeNodes[index].Provinces.Add(p);
                        }

                    }

                    provincestoupdate = provincestoupdate.Distinct().ToList();
                    //Saving.SaveThingsToUpdate();
                    MapManagement.UpdateMap(provincestoupdate, MapManagement.UpdateMapOptions.TradeNode);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                        UpdateMap();

                }
            }
            else
            {
                if (GlobalVariables.InternalChanges)
                    return;
                if (GlobalVariables.ClickedProvince != null)
                {
                    if (GlobalVariables.ClickedProvince.TradeNode != null)
                        GlobalVariables.ClickedProvince.TradeNode.Provinces.Remove(GlobalVariables.ClickedProvince);
                    if (ProvinceTradeNodeBox.SelectedIndex == 0)
                    {
                        GlobalVariables.ClickedProvince.TradeNode = null;
                    }
                    else
                    {
                        GlobalVariables.ClickedProvince.TradeNode = GlobalVariables.TradeNodes[ProvinceTradeNodeBox.SelectedIndex - 1];
                        GlobalVariables.TradeNodes[ProvinceTradeNodeBox.SelectedIndex - 1].Provinces.Add(GlobalVariables.ClickedProvince);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.TradeNode);
                }
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                    UpdateMap();
            }
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

            if (GlobalVariables.ClickedProvince != null)
            {
                if (GlobalVariables.ClickedProvince.TradeNode != null)
                    GlobalVariables.ClickedProvince.TradeNode.Provinces.Remove(GlobalVariables.ClickedProvince);


                tn.Provinces.Add(GlobalVariables.ClickedProvince);
                GlobalVariables.ClickedProvince.TradeNode = tn;
                tn.Location = GlobalVariables.ClickedProvince;
            }
            else if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {
                    if (p.TradeNode != null)
                        p.TradeNode.Provinces.Remove(p);
                    tn.Provinces.Add(p);
                    p.TradeNode = tn;
                }
            }

            TradeNodeNameBox.Text = "";
            TradeNodeColorButton.BackColor = Color.Transparent;
            TradeNodeBox.SelectedIndex = TradeNodeBox.Items.Count - 1;

            MapManagement.UpdateMap(tn.Provinces, MapManagement.UpdateMapOptions.TradeNode);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                UpdateMap();
        }

        private void TradeNodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex != 0)
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
        }

        private void AddTradeNodeDestinationButton_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            Tradenode tn = GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1];
            Tradenode tr = GlobalVariables.TradeNodes.Find(x => x.Name == AddTradeNodeDestinationBox.SelectedItem.ToString());
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
            }
        }

        private void TradeNodeInlandCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Inland = TradeNodeInlandCheckbox.Checked;
        }

        private void TradeNodeLocationSetAsCliecked_Click(object sender, EventArgs e)
        {
            if (TradeNodeBox.SelectedIndex == 0)
                return;
            if (GlobalVariables.ClickedProvince != null)
                TradeNodeProvinceLocationBox.Text = GlobalVariables.ClickedProvince.ID + "";
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
            GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Location = GlobalVariables.Provinces[n - 1];
            MapManagement.UpdateMap(GlobalVariables.TradeNodes[TradeNodeBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeNode);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeNode)
                UpdateMap();
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

        private void SaveTradeNodeFile_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ReadOnly[12])
                return;
            NodeFile nf = new NodeFile();
            List<Tradenode> left = new List<Tradenode>();
            left.AddRange(GlobalVariables.TradeNodes);
            List<Tradenode> done = new List<Tradenode>();
            do
            {
                foreach (Tradenode tn in left)
                {
                    if (tn.Incoming.Any(x => !done.Contains(x)))
                        continue;
                    Node n = new Node(tn.Name);
                    if (tn.Location != null)
                        n.Variables.Add(new Variable("location", tn.Location.ID + ""));
                    else if (tn.Provinces.Any())
                        n.Variables.Add(new Variable("location", tn.Provinces[0].ID + ""));
                    if (tn.Inland)
                        n.Variables.Add(new Variable("inland", "yes"));
                    if (!tn.Destination.Any())
                        n.Variables.Add(new Variable("end", "yes"));
                    Node cl = new Node("color");
                    cl.PureValues = new List<PureValue>() { new PureValue(tn.Color.R + "" ), new PureValue(tn.Color.G + ""), new PureValue(tn.Color.B + "") };
                    n.Nodes.Add(cl);
                    foreach (Destination ds in tn.Destination)
                    {
                        Node des = new Node("outgoing");
                        des.Variables.Add(new Variable("name", "\"" + ds.TradeNode.Name + "\""));
                        Node path = new Node("path");
                        foreach (string s in ds.Path)
                            path.AddPureValue(s);                   
                        des.Nodes.Add(path);
                        Node control = new Node("control");
                        foreach (string s in ds.Control)
                            control.AddPureValue(s);
                        des.Nodes.Add(control);
                        n.Nodes.Add(des);
                    }
                    Node members = new Node("members");
                    tn.Provinces.ForEach(x => members.AddPureValue(x.ID + ""));
                    n.Nodes.Add(members);
                    nf.MainNode.Nodes.Add(n);
                    done.Add(tn);
                }
                left.RemoveAll(x => done.Contains(x));
            } while (left.Any());
            nf.SaveFile(GlobalVariables.pathtomod + "common\\tradenodes\\00_tradenodes.txt");
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

        private void ContinentBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.MultiProvinceMode)
            {
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    int index = ContinentBox.SelectedIndex - 1;

                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        if (p.Continent != null)
                            p.Continent.Provinces.Remove(p);
                        if (index == -1)
                        {
                            p.Continent = null;
                        }
                        else
                        {
                            p.Continent = GlobalVariables.Continents[index];
                            GlobalVariables.Continents[index].Provinces.Add(p);
                        }
                        //if (!GlobalVariables.ToUpdate.Contains(p))
                        //GlobalVariables.ToUpdate.Add(p);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Continent);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent)
                        UpdateMap();
                    //Saving.SaveThingsToUpdate();
                }
            }
            else
            {
                if (GlobalVariables.InternalChanges)
                {
                    ContinentNameChangeBox.Text = ContinentBox.Text;
                    return;
                }

                if (GlobalVariables.ClickedProvince != null)
                {
                    if (GlobalVariables.ClickedProvince.Continent != null)
                        GlobalVariables.ClickedProvince.Continent.Provinces.Remove(GlobalVariables.ClickedProvince);
                    if (ContinentBox.SelectedIndex == 0)
                    {
                        GlobalVariables.ClickedProvince.Continent = null;
                    }
                    else
                    {
                        GlobalVariables.ClickedProvince.Continent = GlobalVariables.Continents[ContinentBox.SelectedIndex - 1];
                        GlobalVariables.Continents[ContinentBox.SelectedIndex - 1].Provinces.Add(GlobalVariables.ClickedProvince);
                    }

                    MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.Continent);
                }
                if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent)
                    UpdateMap();
                //if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
                //GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
            }
            ContinentNameChangeBox.Text = ContinentBox.Text;
        }

        private void ContinentNameChangeSave_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Continents.Any(x => x.Name == ContinentNameChangeBox.Text))
                ContinentNameChangeBox.Text = ContinentBox.Text;
            else
            {
                GlobalVariables.Continents[ContinentBox.SelectedIndex - 1].Name = ContinentNameChangeBox.Text;
                ContinentBox.Items[ContinentBox.SelectedIndex] = ContinentNameChangeBox.Text;
            }
        }

        private void SaveContinentFile_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ReadOnly[13])
                return;
            NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\continent.txt");
            List<Node> newNodes = new List<Node>();
            foreach (Continent c in GlobalVariables.Continents)
            {
                Node n = nf.MainNode.Nodes.Find(x => x.Name == c.Name);
                if (n != null)
                {
                    n.PureValues.Clear();
                    foreach (Province p in c.Provinces)
                        n.AddPureValue(p.ID.ToString());
                }
                else
                {
                    n = new Node(c.Name);
                    foreach (Province p in c.Provinces)
                        n.AddPureValue(p.ID.ToString());
                }
                newNodes.Add(n);
            }
            nf.MainNode.Nodes.Clear();
            nf.MainNode.Nodes.AddRange(newNodes);
            nf.SaveFile(GlobalVariables.pathtomod + "map\\continent.txt");
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
                        if (p.Continent != null)
                            p.Continent.Provinces.Remove(p);
                        p.Continent = GlobalVariables.Continents[index];
                        GlobalVariables.Continents[index].Provinces.Add(p);
                        if (!GlobalVariables.ToUpdate.Contains(p))
                            GlobalVariables.ToUpdate.Add(p);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Continent);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Continent)
                        UpdateMap();
                    // Saving.SaveThingsToUpdate();
                }

                AddNewContinentBox.Text = "";
            }
        }

        private void AddCoreButton_Click(object sender, EventArgs e)
        {
            int index = AddCoreBox.SelectedIndex;
            Country c = GlobalVariables.Countries[index];
            ChangeProvinceInfo(ChangeProvinceMode.Core, c.Tag);
            UpdateCoresPanel();
        }

        private void AddOwnerCoreButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.CoreOwner, null);
            UpdateCoresPanel();
        }

        public void UpdateCoresPanel()
        {
            CoresPanel.Controls.Clear();
            if (GlobalVariables.ClickedProvince != null)
            {
                foreach (string core in GlobalVariables.ClickedProvince.GetCores())
                {
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
                foreach (string claim in GlobalVariables.ClickedProvince.GetClaims())
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
            else if (GlobalVariables.ClickedProvinces.Any())
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
            UpdateCoresPanel();
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
            GlobalVariables.Changes.Clear();
            GlobalVariables.Changes.AddRange(newList);
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
                gb.Size = new Size(507, 37);
                ChangesLayoutPanel.Controls.Add(gb);

                Label varlab = new Label();
                varlab.AutoSize = true;
                varlab.Location = new Point(6, 16);
                varlab.Text = "Variable: " + change.VariableName;
                gb.Controls.Add(varlab);

                Label oldlab = new Label();
                oldlab.Location = new Point(114, 16);
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
                newlab.Location = new Point(230, 16);
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
                keep.Size = new Size(75, 21);
                keep.Tag = GlobalVariables.Changes.IndexOf(change);
                keep.Click += KeepAndSave;
                gb.Controls.Add(keep);

                Button revert = new Button();
                revert.Location = new Point(428, 10);
                revert.Text = "Revert";
                revert.Size = new Size(75, 21);
                revert.Tag = GlobalVariables.Changes.IndexOf(change);
                revert.Click += Revert;
                gb.Controls.Add(revert);

            }
        }

        public void KeepAndSave(object sender, EventArgs e)
        {
            int index = (int)(sender as Control).Tag;
            if (!GlobalVariables.Saves.Contains(GlobalVariables.Changes[index].Object))
                GlobalVariables.Saves.Add(GlobalVariables.Changes[index].Object);
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
                    path = p.HistoryFile;
                }
                else if (obj is Country)
                {
                    Country c = obj as Country;
                    name = "Country " + c.Tag;
                    path = c.HistoryFile;
                    path2 = c.CommonFile;
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

                if (path2 != "" && false)
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

        private void RefreshSavesButton_Click(object sender, EventArgs e)
        {
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

        private void OpenProvinceFileButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ClickedProvince != null)
                Process.Start(GlobalVariables.ClickedProvince.HistoryFile);
        }

        private void ReloadProvinceFromFileButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ClickedProvince != null)
                Saving.LoadObject(GlobalVariables.ClickedProvince);
            UpdateProvincePanel(GlobalVariables.ClickedProvince);
        }

        private void ReloadProvinceAllMapmodesButton_Click(object sender, EventArgs e)
        {
            MapManagement.ReloadProvince(GlobalVariables.ClickedProvince);
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
                Process.Start(GlobalVariables.SelectedCountry.HistoryFile);
        }

        private void AddDiscoveredByButton_Click(object sender, EventArgs e)
        {
            if (DiscoveredByBox.SelectedItem.ToString() != "")
                ChangeProvinceInfo(ChangeProvinceMode.DiscoveredBy, DiscoveredByBox.SelectedItem, false);
            UpdateDiscoveredBy();
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);
        }

        public enum ChangeProvinceMode { CoT, Fort, HRE, Religion, Culture, DiscoveredBy, DiscoveredByOwner, Area, Owner, Controller, City, Building, Core, Claim, CoreOwner, Superregion, Region, Continent };

        public void ChangeProvinceInfo(ChangeProvinceMode mode, object change, object secondvalue = null)
        {
            List<Province> ApplyTo = new List<Province>();
            if (GlobalVariables.ClickedProvince != null)
                ApplyTo.Add(GlobalVariables.ClickedProvince);
            if (GlobalVariables.ClickedProvinces.Any())
                ApplyTo.AddRange(GlobalVariables.ClickedProvinces);
            switch (mode)
            {
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
                        if (p.Area != null)
                            p.Area.Provinces.Remove(p);
                        if (index != -1)
                        {
                            p.Area = GlobalVariables.Areas[index];
                            GlobalVariables.Areas[index].Provinces.Add(p);
                        }
                        else
                            p.Area = null;
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Area);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Area)
                        UpdateMap();
                    break;

                case ChangeProvinceMode.Owner:

                    int SelectedCountryIndex = (int)change;

                    foreach (Province p in ApplyTo)
                    {
                        if (SelectedCountryIndex == 0)
                        {
                            if (p.OwnerCountry != null)
                            {
                                p.OwnerCountry.Provinces.Remove(p);
                                p.RemoveCore(p.OwnerCountry.Tag);
                            }
                            p.OwnerCountry = null;
                            p.Controller = "";
                        }
                        else
                        {
                            if (p.OwnerCountry != null)
                            {
                                p.OwnerCountry.Provinces.Remove(p);
                                p.RemoveCore(p.OwnerCountry.Tag);
                            }
                            p.OwnerCountry = GlobalVariables.Countries.Find(x => x.FullName == (string)OwnerBox.SelectedItem.ToString().Split(',')[0]);
                            p.AddCore(p.OwnerCountry.Tag);
                            p.Controller = p.OwnerCountry.Tag;
                        }
                    }
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Political);
                    MapManagement.UpdateMap(ApplyTo, MapManagement.UpdateMapOptions.Government);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Political)
                        UpdateMap();

                    break;

                case ChangeProvinceMode.Controller:
                    int SelectedControllerIndex = (int)change;

                    foreach (Province p in ApplyTo)
                    {
                        if (SelectedControllerIndex == 0)
                        {
                            p.Controller = "";
                        }
                        else
                        {
                            p.Controller = ControllerBox.Items[SelectedControllerIndex].ToString();
                        }
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
                    foreach (Province p in ApplyTo)
                    {
                        int indexsuperregion = (int)change;
                        List<Province> provincestoupdatesuperregion = new List<Province>();
                        foreach (Province pr in ApplyTo)
                        {

                            if (p.Area != null)
                            {
                                if (p.Area.Region != null)
                                {
                                    provincestoupdatesuperregion.AddRange(p.Area.Provinces);
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

                        provincestoupdatesuperregion = provincestoupdatesuperregion.Distinct().ToList();

                        MapManagement.UpdateMap(provincestoupdatesuperregion, MapManagement.UpdateMapOptions.Superregion);
                        if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Superregion)
                            UpdateMap();
                    }
                    break;
                case ChangeProvinceMode.Region:
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

                    MapManagement.UpdateMap(provincestoupdate, MapManagement.UpdateMapOptions.Region);
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.Region)
                        UpdateMap();
                    break;
            }
        }

        private void AddOwnerDiscoveredByButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.DiscoveredByOwner, null);
            UpdateDiscoveredBy();
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.DiscoveredBy);
        }

        public void UpdateDiscoveredBy()
        {
            DiscoveredByPanel.Controls.Clear();
            if (GlobalVariables.ClickedProvince != null)
            {
                foreach (string tech in GlobalVariables.ClickedProvince.GetDiscoveredBy())
                {
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
            else if (GlobalVariables.ClickedProvinces.Any())
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

        private void ControllerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GlobalVariables.InternalChanges)            
                ChangeProvinceInfo(ChangeProvinceMode.Controller, ControllerBox.SelectedIndex);

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

        private void MakeCityButton_Click(object sender, EventArgs e)
        {
            ChangeProvinceInfo(ChangeProvinceMode.City, true);
            UpdateProvincePanel();
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
                Building bl = GlobalVariables.Buildings.Find(x => x.Name == BuildingsBox.SelectedItem.ToString());
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



        public void UpdateBuildings()
        {
            BuildingsPanel.Controls.Clear();
            if (GlobalVariables.ClickedProvince != null)
            {
                foreach (Building bl in GlobalVariables.ClickedProvince.GetBuildings())
                {
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
            else if (GlobalVariables.ClickedProvinces.Any())
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

        private void AddClaimButton_Click(object sender, EventArgs e)
        {
            int index = AddCoreBox.SelectedIndex;
            Country c = GlobalVariables.Countries[index];
            ChangeProvinceInfo(ChangeProvinceMode.Claim, c.Tag);
            UpdateCoresPanel();
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

        private void SuperregionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GlobalVariables.InternalChanges)
            {
                int index = SuperregionBox.SelectedIndex - 1;
                ChangeProvinceInfo(ChangeProvinceMode.Superregion, index);
            }
            SuperregionNameChangeBox.Text = SuperregionBox.Text;
        }

        private void SuperregionNameChangeSave_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.Superregions.Any(x => x.Name == SuperregionNameChangeBox.Text))
                SuperregionNameChangeBox.Text = SuperregionBox.Text;
            else
            {
                GlobalVariables.Superregions[SuperregionBox.SelectedIndex - 1].Name = SuperregionNameChangeBox.Text;
                SuperregionBox.Items[SuperregionBox.SelectedIndex] = SuperregionNameChangeBox.Text;
            }
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
            }
        }

        private void SaveSuperregionFile_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ReadOnly[18])
                return;
            NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\superregion.txt");
            List<Node> newNodes = new List<Node>();
            foreach (Superregion sr in GlobalVariables.Superregions)
            {
                Node n = nf.MainNode.Nodes.Find(x => x.Name == sr.Name);

                if (n != null)
                {
                    n.PureValues.Clear();
                    foreach (Region r in sr.Regions)
                        n.AddPureValue(r.Name);
                }
                else
                {
                    n = new Node(sr.Name);
                    foreach (Region r in sr.Regions)
                        n.AddPureValue(r.Name);
                }
                newNodes.Add(n);
            }
            nf.MainNode.Nodes.Clear();
            nf.MainNode.Nodes.AddRange(newNodes);
            nf.SaveFile(GlobalVariables.pathtomod + "map\\superregion.txt");
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

            switch(mode)
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

        private void TradeCompanyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.MultiProvinceMode)
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
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeCompany)
                        UpdateMap();
                    //Saving.SaveThingsToUpdate();
                }
            }
            else
            {
                if (!GlobalVariables.InternalChanges)
                {
                    if (GlobalVariables.ClickedProvince != null)
                    {
                        if (GlobalVariables.ClickedProvince.TradeCompany != null)
                        {
                            GlobalVariables.ClickedProvince.TradeCompany.Provinces.Remove(GlobalVariables.ClickedProvince);
                            GlobalVariables.ClickedProvince.TradeCompany.MadeChanges = true;
                        }

                        if (TradeCompanyBox.SelectedIndex == 0)
                        {
                            GlobalVariables.ClickedProvince.TradeCompany = null;
                        }
                        else
                        {
                            GlobalVariables.ClickedProvince.TradeCompany = GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1];
                            GlobalVariables.ClickedProvince.TradeCompany.MadeChanges = true;
                            GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Provinces.Add(GlobalVariables.ClickedProvince);
                        }

                        MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.TradeCompany);
                    }
                    if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeCompany)
                        UpdateMap();
                    //if (!GlobalVariables.ToUpdate.Contains(GlobalVariables.ClickedProvince))
                    //GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                }
            }
            TradeCompanyNameChangeBox.Text = TradeCompanyBox.Text;
            if (TradeCompanyBox.SelectedIndex != 0)
                TradeCompanyColorButton.BackColor = GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Color;
        }

        private void TradeComapnyNameChangeSave_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.TradeCompanies.Any(x => x.Name == TradeCompanyNameChangeBox.Text))
                TradeCompanyNameChangeBox.Text = TradeCompanyBox.Text;
            else
            {
                GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Name = TradeCompanyNameChangeBox.Text;
                TradeCompanyBox.Items[TradeCompanyBox.SelectedIndex] = TradeCompanyNameChangeBox.Text;
            }
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

        public List<NodeFile> GetModNodeFile(GlobalVariables.ModNodeFileTypes type)
        {
            switch ((int)type)
            {
                case 0:
                    return GlobalVariables.ModTradeGoodsFiles;
                case 1:
                    return GlobalVariables.ModPricesFiles;
                case 2:
                    return GlobalVariables.ModCulturesFiles;
                case 3:
                    return GlobalVariables.ModReligionsFiles;
                case 4:
                    return GlobalVariables.ModTradeNodesFiles;
                case 5:
                    return GlobalVariables.ModTradeCompanyFiles;
                case 6:
                    return GlobalVariables.ModCountryTagsFiles;
                case 7:
                    return GlobalVariables.ModGovernmentsFiles;
                default:
                    return null;
            }
        }
        public string GetModNodeFileName(GlobalVariables.ModNodeFileTypes type)
        {
            switch ((int)type)
            {
                case 0:
                    return "";
                case 1:
                    return "";
                case 2:
                    return "";
                case 3:
                    return "";
                case 4:
                    return "";
                case 5:
                    for(int a = 0; a < 99; a++)
                    {
                        if (File.Exists(GlobalVariables.pathtomod + $"\\common\\trade_companies\\{a.ToString("00")}_modeditor_trade_companies.txt"))
                            continue;
                        else
                            return $"\\common\\trade_companies\\{a.ToString("00")}_modeditor_trade_companies.txt";
                    }
                    return "\\common\\trade_companies\\99_modeditor_trade_companies.txt";
                case 6:
                    return "";
                case 7:
                    return "";
                default:
                    return "";
            }
        }

        public NodeFile DetermineSaveLocation(GlobalVariables.ModNodeFileTypes type, NodeFile Parent)
        {
            NodeFile toSaveTo = null;
            if (Parent != null) //file found
            {
                //file is readonly but there is permission to save over
                if (Parent.ReadOnly && GlobalVariables.CreateNewFilesReadOnly)
                {
                    toSaveTo = GetModNodeFile(type).Find(x => x.CreatedByEditor);
                    if (toSaveTo == null)
                    {
                        toSaveTo = new NodeFile(GlobalVariables.pathtomod + GetModNodeFileName(type));
                        toSaveTo.CreatedByEditor = true;
                        GetModNodeFile(type).Add(toSaveTo);
                    }
                }
                //file is readonly but no permission to save over
                else if (Parent.ReadOnly && !GlobalVariables.CreateNewFilesReadOnly)
                    return null;
                //file isn't readonly
                else
                    toSaveTo = Parent;

            }
            else //file wasn't found (new object)
            {
                //file will be saved to a new file
                if (GlobalVariables.NewObjectsNewFiles)
                {
                    toSaveTo = GetModNodeFile(type).Find(x => x.CreatedByEditor);
                    if (toSaveTo == null)
                    {
                        toSaveTo = new NodeFile(GlobalVariables.pathtomod + GetModNodeFileName(type));
                        toSaveTo.CreatedByEditor = true;
                        GetModNodeFile(type).Add(toSaveTo);
                    }
                   
                   
                }
                //editor will look for some already exisitng file (if it fails it won't save)
                else
                {
                    toSaveTo = GetModNodeFile(type).First(x => !x.ReadOnly);       
                }
            }
            return toSaveTo;
        }

        private void SaveTradeCompanyFile_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ReadOnly[19] && (!GlobalVariables.CreateNewFilesReadOnly && !GlobalVariables.NewObjectsNewFiles))            
                return;
            

            //NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\continent.txt");
            //List<Node> newNodes = new List<Node>();



            foreach (TradeCompany tc in GlobalVariables.TradeCompanies)
            {
                if (!tc.MadeChanges)
                    continue;
                tc.MadeChanges = false;
                NodeFile toSaveTo = DetermineSaveLocation(GlobalVariables.ModNodeFileTypes.TradeCompanies, tc.ParentFile);
                //MessageBox.Show(GetModNodeFileName(GlobalVariables.ModNodeFileTypes.TradeCompanies));
               
                if (toSaveTo == null)
                    continue;
                tc.ParentFile = toSaveTo;
                Node n = tc.ParentFile.MainNode.Nodes.Find(x => x.Name == tc.Name);
                if (n == null)
                {
                    n = new Node(tc.Name, toSaveTo.MainNode);
                    tc.ParentFile.MainNode.Nodes.Add(n);
                    n.Parent = tc.ParentFile.MainNode;

                    Node color = new Node("color", n)
                    {
                        PureValues = new List<PureValue>() { new PureValue(tc.Color.R.ToString()), new PureValue( tc.Color.G.ToString()), new PureValue(tc.Color.B.ToString()) }
                    };
                    n.Nodes.Add(color);
                    Node provinces = new Node("provinces", n);
                    n.Nodes.Add(provinces);
                    foreach (Province p in tc.Provinces)
                        provinces.AddPureValue(p.ID.ToString());
                    foreach(string name in tc.Names)
                    {
                        Node nm = new Node("names", n);
                        n.Nodes.Add(nm);
                        nm.ChangeVariable("name", name, true);
                    }

                }
                else
                {
                    n.Nodes.Find(x => x.Name == "color").PureValues = new List<PureValue>() { new PureValue(tc.Color.R.ToString()), new PureValue(tc.Color.G.ToString()), new PureValue(tc.Color.B.ToString()) };
                    Node pnode = n.Nodes.Find(x => x.Name == "provinces");
                    pnode.PureValues.Clear();
                    foreach (Province p in tc.Provinces)
                        pnode.AddPureValue(p.ID.ToString());
                    int N = 0;
                    List<Node> ToRemove = new List<Node>();
                    foreach (Node namenode in n.Nodes.FindAll(x => x.Name == "names"))
                    {
                        if (N >= tc.Names.Count)
                        {
                            ToRemove.Add(namenode);
                            continue;
                        }
                        namenode.ChangeVariable("name", tc.Names[N]);
                        N++;
                    }
                    if (N < tc.Names.Count)
                    {
                        for (; N < tc.Names.Count; N++)
                        {
                            Node nm = new Node("names", n);
                            n.Nodes.Add(nm);
                            nm.ChangeVariable("name", tc.Names[N], true);
                        }
                    }
                    n.Nodes.RemoveAll(x => ToRemove.Contains(x));

                }
                toSaveTo.SaveFile(toSaveTo.Path);
            }
        }

        private void TradeCompanyRandomColor_Click(object sender, EventArgs e)
        {
            if (TradeCompanyBox.SelectedIndex == 0)
                return;
            TradeCompanyColorButton.BackColor = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
            GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Color = TradeCompanyColorButton.BackColor;
            GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].MadeChanges = true;
            MapManagement.UpdateMap(GlobalVariables.TradeCompanies[TradeCompanyBox.SelectedIndex - 1].Provinces, MapManagement.UpdateMapOptions.TradeCompany);
            if (GlobalVariables.mapmode == MapManagement.UpdateMapOptions.TradeCompany)
                UpdateMap();
        }

        private void SaveProvinceName_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ClickedProvince == null)
                return;
            if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV" + GlobalVariables.ClickedProvince.ID))
                GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvince.ID] = ProvinceNameLocalisationBox.Text;
            else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV" + GlobalVariables.ClickedProvince.ID)) {
                if (GlobalVariables.LocalisationEntries["PROV" + GlobalVariables.ClickedProvince.ID] != ProvinceNameLocalisationBox.Text)
                    GlobalVariables.ModLocalisationEntries["PROV" + GlobalVariables.ClickedProvince.ID] = ProvinceNameLocalisationBox.Text;
            }
        }

        private void SaveProvinceAdj_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.ClickedProvince == null)
                return;
            if (GlobalVariables.ModLocalisationEntries.Keys.Contains("PROV_ADJ" + GlobalVariables.ClickedProvince.ID))
                GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvince.ID] = ProvinceAdjectiveLocalisationBox.Text;
            else if (GlobalVariables.LocalisationEntries.Keys.Contains("PROV_ADJ" + GlobalVariables.ClickedProvince.ID))
            {
                if (GlobalVariables.LocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvince.ID] != ProvinceAdjectiveLocalisationBox.Text)
                    GlobalVariables.ModLocalisationEntries["PROV_ADJ" + GlobalVariables.ClickedProvince.ID] = ProvinceAdjectiveLocalisationBox.Text;
            }
        }

        private void SaveLocalisationButton_Click(object sender, EventArgs e)
        {
            string tosave = "";
            string filename = "";
            switch(GlobalVariables.LocalisationLanguage)
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
            foreach(string key in GlobalVariables.ModLocalisationEntries.Keys)
            {
                tosave += " " + key + ": \"" + GlobalVariables.ModLocalisationEntries[key] + "\"\n"; 
            }
            if(!Directory.Exists(GlobalVariables.pathtomod + "localisation"))
                Directory.CreateDirectory(GlobalVariables.pathtomod + "localisation");
            File.WriteAllText(GlobalVariables.pathtomod + filename, tosave);
        }

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
    }
}