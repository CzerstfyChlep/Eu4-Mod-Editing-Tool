using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Eu4ModEditor
{
    public partial class ModEditor : Form
    {
        public static int ZoomIn = 1;

        public static TabsSeparate TabsSeparateWindow;
        public static MapmodesWindow MapmodesSeparateWindow;
        public static DateWindow DateSeparateWindow;

        #region Graphical
        public static Graphics graphics;

        public static Stopwatch stopwatch = new Stopwatch();

        public static void UpdateMap()
        {
            if (GlobalVariables.OldMapUpdatingStyle)
            {

            }
            else
            {
                form.Invalidate();
            }
        }
        public void UpdateGraphics()
        {
            
        }
        #endregion


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(GlobalVariables.DrawingMain.source, new Rectangle(GlobalVariables.MapDrawingPosX, GlobalVariables.MapDrawingPosY, GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight), new Rectangle(GlobalVariables.CameraPosition, new Size(GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight)), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(GlobalVariables.ClickedMask.source, new Rectangle(GlobalVariables.MapDrawingPosX, GlobalVariables.MapDrawingPosY, GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight), new Rectangle(GlobalVariables.CameraPosition, new Size(GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight)), GraphicsUnit.Pixel);
            //base.OnPaint(e);
        }


        #region Click Handlers
        void MouseClickHandler(object sender, MouseEventArgs e)
        {
            //TODO
            //when scaling the game don't forget about this
            if (sender == this)
            {
                if (e.Location.X > GlobalVariables.MapDrawingPosX && e.Location.X < GlobalVariables.MapDrawingWidth + GlobalVariables.MapDrawingPosX && e.Location.Y > GlobalVariables.MapDrawingPosY && e.Location.Y < GlobalVariables.MapDrawingHeight + GlobalVariables.MapDrawingPosY)
                {
                    Point truePosition = new Point(e.Location.X - GlobalVariables.MapDrawingPosX + GlobalVariables.CameraPosition.X, e.Location.Y - GlobalVariables.MapDrawingPosY + GlobalVariables.CameraPosition.Y);
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
                                if (TabsSeparateWindow.TradeNodeBox.SelectedIndex == 0)
                                    return;
                                TabsSeparateWindow.AddTradeDestination(GlobalVariables.TradeNodes[TabsSeparateWindow.TradeNodeBox.SelectedIndex - 1], p.TradeNode);
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
            if (TabsSeparateWindow.Tabs.SelectedTab == TabsSeparateWindow.TradeGoodsTab)
                TabsSeparateWindow.RefreshTradeGoodsTab();
        }
        #endregion

        #region Clicked Provinces
        public void AddToClickedProvinces(Province p, bool Update = true)
        {
            AddToClickedProvinces(new List<Province> { p }, Update);
        }
        public void AddToClickedProvinces(List<Province> p, bool Update = true)
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
                    if (TabsSeparateWindow.Tabs.SelectedTab == TabsSeparateWindow.ProvinceTab)
                    {
                        TabsSeparateWindow.UpdateProvincePanel();
                    }
                }
                if (GlobalVariables.ClickedProvinces.Any())
                    if (GlobalVariables.ClickedProvinces[0].OwnerCountry != null && GlobalVariables.ClickedProvinces[0].OwnerCountry != Country.NoCountry)
                        TabsSeparateWindow.CountryBox.SelectedItem = GlobalVariables.ClickedProvinces[0].OwnerCountry;
                MapManagement.UpdateClickedMap(UpdateMapList, Color.LightYellow);
                TabsSeparateWindow.UpdateTotalSelectedLabel();
                UpdateMap();
            }
        }
        public void RemoveFromClickedProvinces(Province p, bool Update = true)
        {
            RemoveFromClickedProvinces(new List<Province> { p }, Update);
        }
        public void RemoveFromClickedProvinces(List<Province> p, bool Update = true)
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
            TabsSeparateWindow.UpdateTotalSelectedLabel();
            UpdateMap();
        }
        public void AddAndRemoveFromClickedProvinces(List<Province> toremove, List<Province> toadd)
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
                {
                    if (GlobalVariables.ClickedProvinces[0].OwnerCountry != null && GlobalVariables.ClickedProvinces[0].OwnerCountry != Country.NoCountry)
                        TabsSeparateWindow.CountryBox.SelectedItem = GlobalVariables.ClickedProvinces[0].OwnerCountry;
                    if (GlobalVariables.ClickedProvinces[0].TradeNode != null)
                        TabsSeparateWindow.TradeNodeBox.SelectedIndex = GlobalVariables.TradeNodes.IndexOf(GlobalVariables.ClickedProvinces[0].TradeNode) + 1;
                }


            }
            if (TabsSeparateWindow.Tabs.SelectedTab == TabsSeparateWindow.ProvinceTab)
            {
                TabsSeparateWindow.UpdateProvincePanel();
            }
            MapManagement.UpdateClickedMap(UpdateMapList, Color.LightYellow);
            //TODO
            // Add averages and enable adding one for each

            TabsSeparateWindow.UpdateTotalSelectedLabel();
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

        #region Functions
        
        public void MoveCameraTo(Province p)
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
       
        public void ShowMessageBox(string text, string title = "Info")
        {
            MessageBox.Show(text, title);
        }
       
        

        #endregion
        
        #region OnExitCrash
        
        public void ExitingFunction()
        {

        }

        #endregion

        private void ModEditor_Load(object sender, EventArgs e)
        {

        }

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            /*
            ZoomIn *= 2;
            if (ZoomIn == 16)
                ZoomIn = 1;*/
            UpButton.Text = form.Width + "";
            GlobalVariables.MapDrawingHeight = form.Height - 133;
            GlobalVariables.MapDrawingWidth = form.Width - 95;
            graphics = form.CreateGraphics();
            UpdateMap();
        }

        private void showTabsMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabsSeparateWindow != null)
                TabsSeparateWindow.Close();
            TabsSeparateWindow = new TabsSeparate();
            TabsSeparateWindow.Show();
        }

        private void showMapmodesMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MapmodesSeparateWindow != null)
                MapmodesSeparateWindow.Close();
            MapmodesSeparateWindow = new MapmodesWindow();
            MapmodesSeparateWindow.Show();
        }

        private void showDateMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DateSeparateWindow != null)
                DateSeparateWindow.Close();
            DateSeparateWindow = new DateWindow();
            DateSeparateWindow.Show();
        }

        private void Resizing(object sender, EventArgs e)
        {
            GlobalVariables.MapDrawingHeight = this.Height - 133 + (NavHidden?64:0);
            GlobalVariables.MapDrawingWidth = this.Width - 95 + (NavHidden ? 72 : 0);
        }

        private void hideNavigationButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hideNavigationButtonsToolStripMenuItem.Checked)
            {
                LeftButton.Visible = false;
                UpButton.Visible = false;
                RightButton.Visible = false;
                DownButton.Visible = false;
                ZoomInButton.Visible = false;
                NavHidden = true;
                GlobalVariables.MapDrawingPosX -= 32;
                GlobalVariables.MapDrawingPosY -= 32;
                GlobalVariables.MapDrawingWidth += 72;
                GlobalVariables.MapDrawingHeight += 64;
                UpdateMap();
            }
            else
            {
                LeftButton.Visible = true;
                UpButton.Visible = true;
                RightButton.Visible = true;
                DownButton.Visible = true;
                ZoomInButton.Visible = true;
                NavHidden = false;
                GlobalVariables.MapDrawingPosX += 32;
                GlobalVariables.MapDrawingPosY += 32;
                GlobalVariables.MapDrawingWidth -= 72;
                GlobalVariables.MapDrawingHeight -= 64;
                UpdateMap();
            }
        }

        bool NavHidden = false;

        private void politicalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Political);
        }

        private void hREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.HRE);
        }

        private void religionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Religion);
        }

        private void cultureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Culture);
        }

        private void governmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Government);
        }

        private void areaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Area);
        }

        private void regionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Region);
        }

        private void superregionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Superregion);
        }
        private void continentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Continent);
        }

        private void tradeGoodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.TradeGood);
        }

        private void tradeNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.TradeNode);
        }

        private void tradeCompaniesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.TradeCompany);
        }

        private void developmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Development);
        }

        private void provincesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Provinces);
        }

        private void fortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Fort);
        }

        private void discoveredByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.DiscoveredBy);
        }

        private void winterMonsoonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Winter);
        }

        private void climateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMapmode.ChangeMapmodeDirectly(MapManagement.UpdateMapOptions.Climate);
        }
    }
}