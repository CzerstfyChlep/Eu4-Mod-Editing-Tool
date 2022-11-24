using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    partial class ModEditor
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
            if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.provincesBMP] == 1 || GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.provincesBMP] == 2)
                GlobalVariables.ProvincesMap = Image.FromFile(GlobalVariables.pathtomod + "map/provinces.bmp");
            else if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.provincesBMP] == 0)
                GlobalVariables.ProvincesMap = Image.FromFile(GlobalVariables.pathtogame + "map/provinces.bmp");
            GlobalVariables.ProvincesMapBitmap = new Bitmap(GlobalVariables.ProvincesMap);
            GlobalVariables.UpdtGraphicsThread = new Thread(UpdateGraphics);


            this.FormClosing += OnExitDo;

            GlobalVariables.ClickedMask = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));
            GlobalVariables.DrawingMain = new LockBitmap(new Bitmap(GlobalVariables.ProvincesMapBitmap, GlobalVariables.ProvincesMapBitmap.Width, GlobalVariables.ProvincesMapBitmap.Height));


            //TerrainMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;

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

            TabsSeparateWindow = new TabsSeparate();

            TabsSeparateWindow.Show();

            MapmodesSeparateWindow = new MapmodesWindow();
            MapmodesSeparateWindow.Show();

            MoveCameraTo(GlobalVariables.Provinces[0]);
            GlobalVariables.UpdtGraphicsThread.Start();

            this.ResizeEnd += Resizing;
          
            //SIZING
            if (GlobalVariables.AppSizeOption == 1)
            {
                this.Width = 1290;
                GlobalVariables.MapDrawingWidth = 620;
                UpButton.Width = 620;
                DownButton.Width = 620;
                RightButton.Location = new Point(665, RightButton.Location.Y);
            }
            else if (GlobalVariables.AppSizeOption == 2)
            {
                this.Width = 1290;
                this.Height = 870;
                GlobalVariables.MapDrawingWidth = 620;
                GlobalVariables.MapDrawingHeight = 660;
                UpButton.Width = 620;
                DownButton.Width = 620;
                RightButton.Location = new Point(665, RightButton.Location.Y);
                LeftButton.Height = 664;
                RightButton.Height = 664;
                //ButtonsPanel.Location = new Point(ButtonsPanel.Location.X,738);
                DownButton.Location = new Point(DownButton.Location.X, 710);
            }

            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

    }
}
