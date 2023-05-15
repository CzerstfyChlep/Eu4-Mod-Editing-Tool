using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Eu4ModEditor
{
    partial class ModEditor
    {
        public ModEditor()
        {
            LanguageEngine.InitialiseLanguagePack();
            GlobalVariables.MainForm = this;

            NodeFile actions = new NodeFile();
            actions.ReadFile("00_on_actions.txt", true);
            string s = "";
            foreach(Node n in actions.MainNode.Nodes)
            {
                string[] nameSplit = n.Name.Split('_');
                string newName = "_";
                foreach(string name in nameSplit)
                {
                    newName += name[0].ToString().ToUpper() + name.Substring(1);
                }
                s += "CacheItem( dbName, this, " + newName + ", \"" + n.Name + "\" );\n";
            }
            File.WriteAllText("outputwork.txt", s);
            return;

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
            GlobalVariables.Threads.UpdtGraphicsThread = new Thread(UpdateGraphics);
            GlobalVariables.Threads.AutoSaveThread = new Thread(Autosaving);

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
            GlobalVariables.Threads.UpdtGraphicsThread.Start();
            GlobalVariables.Threads.AutoSaveThread.Start();
            

            this.ResizeEnd += Resizing;
          
            this.FormBorderStyle = FormBorderStyle.Sizable;

            GlobalVariables.CurrentDate = GlobalVariables.StartDate;
        }

    }
}
