using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Eu4ModEditor
{
    public partial class LoadingScreen : Form
    {

        NodeFile options;

        public void ReadOptions()
        {
            if (!File.Exists("options.txt"))
                File.Create("options.txt");

            options = new NodeFile();
            options.ReadFile("options.txt", true);


            if (options.MainNode.TryGetVariableValue("gamepath", out string s))
                GameDirectoryBox.Text = s;
            if (options.MainNode.TryGetVariableValue("modpath", out string m))
                ModDirectoryBox.Text = m;
            if (options.MainNode.TryGetNode("lastOptions", out Node N))
            {
                for (int a = 0; a < N.PureValues.Count; a++)
                {
                    lastOptions[a] = int.Parse(N.PureValues[a].Name);
                }
            }
            if (options.MainNode.TryGetVariableValue("autosaving", out string at))
                GlobalVariables.Options.Autosaving = at.ToLower() == "true" ? true : false;
            if (options.MainNode.TryGetVariableValue("autosaving_interval", out string ai))
                GlobalVariables.Options.AutosavingInterval = int.TryParse(ai, out int inter) ? inter : 5;
            if (options.MainNode.TryGetVariableValue("autosaving_path", out string atp))
                GlobalVariables.Options.AutosavingPath = atp;
            if (options.MainNode.TryGetVariableValue("autosaving_on_exit", out string atoe))
                GlobalVariables.Options.AutosavingOnExit = atoe.ToLower() == "true" ? true : false;
            if (options.MainNode.TryGetVariableValue("save_on_crash", out string soc))
                GlobalVariables.Options.SaveCrash = soc.ToLower() == "true" ? true : false;
            if (options.MainNode.TryGetVariableValue("save_on_crash_path", out string socp))
                GlobalVariables.Options.SaveCrashPath = socp;
            if (options.MainNode.TryGetVariableValue("custom_province_random", out string cspr))
                GlobalVariables.Options.RandomProvinceCustom = cspr.ToLower() == "true" ? true : false;
            if (options.MainNode.TryGetNode("province_low", out Node plow))
            {
                if (plow.PureValues.Count > 2)
                {
                    GlobalVariables.Options.RandomProvinceLowMinimum = int.TryParse(plow.PureValues[0].Name, out int plowmin) ? plowmin : 1;
                    GlobalVariables.Options.RandomProvinceLowAverage = int.TryParse(plow.PureValues[1].Name, out int plowavg) ? plowavg : 1;
                    GlobalVariables.Options.RandomProvinceLowMaximum = int.TryParse(plow.PureValues[2].Name, out int plowmax) ? plowmax : 1;
                }
            }
            if (options.MainNode.TryGetNode("province_medium", out Node pmed))
            {
                if (pmed.PureValues.Count > 2)
                {
                    GlobalVariables.Options.RandomProvinceMediumMinimum = int.TryParse(plow.PureValues[0].Name, out int pmin) ? pmin : 1;
                    GlobalVariables.Options.RandomProvinceMediumAverage = int.TryParse(plow.PureValues[1].Name, out int pavg) ? pavg : 1;
                    GlobalVariables.Options.RandomProvinceMediumMaximum = int.TryParse(plow.PureValues[2].Name, out int pmax) ? pmax : 1;
                }
            }
            if (options.MainNode.TryGetNode("province_high", out Node phigh))
            {
                if (phigh.PureValues.Count > 2)
                {
                    GlobalVariables.Options.RandomProvinceHighMinimum = int.TryParse(plow.PureValues[0].Name, out int pmin) ? pmin : 1;
                    GlobalVariables.Options.RandomProvinceHighAverage = int.TryParse(plow.PureValues[1].Name, out int pavg) ? pavg : 1;
                    GlobalVariables.Options.RandomProvinceHighMaximum = int.TryParse(plow.PureValues[2].Name, out int pmax) ? pmax : 1;
                }
            }
            if (options.MainNode.TryGetVariableValue("same_value_provinces", out string svp))
                GlobalVariables.Options.SameValueForAllProvinces = svp.ToLower() == "true" ? true : false;
            if (options.MainNode.TryGetVariableValue("last_used_date", out string lsdt))
                StartDateBox.Text = lsdt;
        }


        public LoadingScreen()
        {
            InitializeComponent();
            Text = "Loading screen - " + GlobalVariables.Version;
            if (File.Exists("directories.txt"))
            {
                string[] read = File.ReadAllLines("directories.txt");
                string towrite = "";

                if (read.Length > 0)
                    towrite += "gamepath=\"" + read[0]+"\"\n";
                if (read.Length > 1)
                    towrite += "modpath=\"" + read[1] +"\"";
                File.WriteAllText("options.txt", towrite);
                File.Delete("directories.txt");
                /*
                string txt = File.ReadAllText("directories.txt");
                string[] sp = txt.Split('\n');
                if (sp.Count() > 0)
                    GameDirectoryBox.Text = sp[0];
                if (sp.Count() > 1)
                    ModDirectoryBox.Text = sp[1];
                if (sp.Count() > 2) {
                    int a = 0;
                    foreach (string s in sp[2].Split(' '))
                    {
                        if (s != "")
                        {
                            lastOptions[a] = int.Parse(s);
                            a++;
                        }
                    }
                }
                */                    
            }
            ReadOptions();
           


            /*if (File.Exists("modeditor_darkmode.txt"))
            {
                string txt = File.ReadAllText("modeditor_darkmode.txt");
                if(bool.TryParse(txt, out bool dm))
                    if(dm)
                        GlobalVariables.DarkMode = true;
                //ApplyDarkMode();
            }*/


            int n = 0;
            foreach(string tl in ToLoad)
            {
                GroupBox gb = new GroupBox();
                gb.Text = tl;
                gb.Tag = n;
                gb.Size = new Size(365, 50);
                n++;

                Label StatusLabelCosmetic = new Label();
                StatusLabelCosmetic.Text = "Status:";
                StatusLabelCosmetic.Location = new Point(15, 25);
                StatusLabelCosmetic.AutoSize = true;
                gb.Controls.Add(StatusLabelCosmetic);

                Label StatusLabel = new Label();
                StatusLabel.Text = "";
                StatusLabel.Location = new Point(60, 25);
                StatusLabel.AutoSize = true;
                gb.Controls.Add(StatusLabel);

                StatusLabels.Add(StatusLabel);

                RadioButton both = new RadioButton();
                both.Tag = "both";
                both.Text = "Both";
                both.AutoSize = true;
                both.Location = new Point(130, 25);
                gb.Controls.Add(both);

                RadioButton game = new RadioButton();
                game.Tag = "game";
                game.Text = "Game";
                game.AutoSize = true;
                game.Location = new Point(178, 25);
                gb.Controls.Add(game);
                game.CheckedChanged += RadioButtonChange;

                RadioButton mod = new RadioButton();
                mod.Tag = "mod";
                mod.Text = "Mod";
                mod.AutoSize = true;
                mod.Location = new Point(233, 25);
                gb.Controls.Add(mod);

                CheckBox ReadOnly = new CheckBox();
                ReadOnly.Text = "Read Only";
                ReadOnly.AutoSize = true;
                ReadOnly.Location = new Point(285, 25);
                gb.Controls.Add(ReadOnly);

                LoadingPanel.Controls.Add(gb);
            }

        }

        List<string> ToLoad = new List<string>() { /*0*/"map\\definition.csv",
            /*1*/"map\\provinces.bmp", /*2*/"common\\tradegoods\\", /*3*/"common\\prices\\",
            /*4*/"common\\cultures\\", /*5*/"common\\religions\\", /*6*/"map\\region.txt",
            /*7*/"common\\countries\\", /*8*/"history\\provinces\\", /*9*/"map\\area.txt",
            /*10*/"map\\default.map", /*11*/"history\\countries\\", /*12*/"common\\tradenodes\\",
            /*13*/"map\\continent.txt", /*14*/"common\\country_tags\\", /*15*/"common\\technology.txt",
            /*16*/"common\\governments\\", /*17*/"common\\buildings\\", /*18*/"map\\superregion.txt",
            /*19*/"common\\trade_companies\\", /*20*/"localisation\\", /*21*/"map\\climate.txt"
        };

        int[] lastOptions = new int[22];

        public void RadioButtonChange(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                foreach (Control c in rb.Parent.Controls) {
                    if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = true;
                        (c as CheckBox).Enabled = false;
                    }
                }
            }
            else
            {
                foreach (Control c in rb.Parent.Controls)
                {
                    if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = false;
                        (c as CheckBox).Enabled = true;
                    }
                }
            }
        }

        public enum FileStatus { FileNotFound = 0, GameFoundModNot = 1, GameNotModFound = 2, BothFound = 3 };

        public List<Label> StatusLabels = new List<Label>();
        public List<FileStatus> FileStatuses = new List<FileStatus>();

        public List<Bookmark> bookmarks = new List<Bookmark>();

        private void CheckFilesButton_Click(object sender, EventArgs e)
        {
            //File.WriteAllText("directories.txt", GameDirectoryBox.Text + "\n" + ModDirectoryBox.Text);
            options.MainNode.ChangeVariable("gamepath", GameDirectoryBox.Text, true);
            options.MainNode.ChangeVariable("modpath", ModDirectoryBox.Text, true);


            if (GameDirectoryBox.Text != "") 
                {
                    groupBox2.Enabled = true;
                    if (GameDirectoryBox.Text.Last() == '\\')
                        GlobalVariables.pathtogame = GameDirectoryBox.Text;
                    else
                        GlobalVariables.pathtogame = GameDirectoryBox.Text + "\\";
                }

            if (ModDirectoryBox.Text != "")
            {
                if (ModDirectoryBox.Text.Last() == '\\')
                    GlobalVariables.pathtomod = ModDirectoryBox.Text;
                else
                    GlobalVariables.pathtomod = ModDirectoryBox.Text + "\\";
            }


            //BOOKMARKS READING

            bookmarks.Clear();

            List<string> FilesDone = new List<string>();

            if (Directory.Exists(GlobalVariables.pathtomod + "common\\bookmarks\\"))
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\bookmarks\\"))
                {
                    FilesDone.Add(Path.GetFileName(file));
                    NodeFile nf = new NodeFile(file);
                    Node bookmark = nf.MainNode.Nodes.Find(x => x.Name.ToLower() == "bookmark");
                    if (bookmark != null)
                    {
                        Variable date = bookmark.Variables.Find(x => x.Name.ToLower() == "date");
                        if (date != null)
                        {

                            string[] spl = date.Value.Split('.');
                            if (spl.Count() >= 3)
                            {
                                int year = 1444, month = 11, day = 11;
                                int.TryParse(spl[0], out year);
                                int.TryParse(spl[1], out month);
                                int.TryParse(spl[2], out day);

                                if (year > 0 && month > 0 && day > 0 && month < 12 && day < 32)
                                {
                                    Bookmark bm = new Bookmark(year, month, day);
                                    if (!bookmarks.Any(x => x.Date.CompareTo(bm.Date) == 0))
                                        bookmarks.Add(bm);
                                }
                            }

                        }
                    }
                }
            }

            if (Directory.Exists(GlobalVariables.pathtogame + "common\\bookmarks\\"))
            {
                foreach(string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\bookmarks\\"))
                {
                   // MessageBox.Show("DAte found: " + file);
                    if (FilesDone.Contains(Path.GetFileName(file)))
                        continue;
                    NodeFile nf = new NodeFile(file);
                   // MessageBox.Show(nf.MainNode.Nodes[0].Name);
                    Node bookmark = nf.MainNode.Nodes.Find(x => x.Name.ToLower() == "bookmark");
                    if (bookmark!= null)
                    {
                        Variable date = bookmark.Variables.Find(x => x.Name.ToLower() == "date");
                        
                        if (date != null)
                        {
                           
                            string[] spl = date.Value.Split('.');
                            if(spl.Count() >= 3)
                            {
                                int year = 1444, month = 11, day = 11;
                                int.TryParse(spl[0], out year);
                                int.TryParse(spl[1], out month);
                                int.TryParse(spl[2], out day);
                                
                                if(year > 0 && month > 0 && day > 0 && month < 12 && day < 32)
                                {
                                    Bookmark bm = new Bookmark(year, month, day);
                                    if (!bookmarks.Any(x=>x.Date.CompareTo(bm.Date) == 0))
                                        bookmarks.Add(bm);
                                }                                  
                            }
                            
                        }
                    }
                }
            }

            bookmarks.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            BookmarksComboBox.Items.Clear();
            BookmarksComboBox.Items.AddRange(bookmarks.ToArray());

            if (bookmarks.Any())
            {
                StartDateBox.Text = $"{bookmarks[0].Date.Year}.{bookmarks[0].Date.Month}.{bookmarks[0].Date.Day}";
            }
            FileStatuses.Clear();

            int n = 0;
            foreach(string s in ToLoad)
            {
                FileStatuses.Add(new FileStatus());
                if (File.Exists(GlobalVariables.pathtogame + s) || Directory.Exists(GlobalVariables.pathtogame + s))
                {
                    FileStatuses[n]++;
                }
                if (File.Exists(GlobalVariables.pathtomod + s) || Directory.Exists(GlobalVariables.pathtomod + s))
                {
                    FileStatuses[n] += 2;
                }
                n++;
            }
            foreach (Label l in StatusLabels)
            {
                int index = StatusLabels.IndexOf(l);
                l.ForeColor = Color.White;
                RadioButton game = null;
                RadioButton mod = null;
                RadioButton both = null;
                CheckBox check = null;
                foreach(Control c in l.Parent.Controls)
                {
                    if(c is RadioButton)
                    {
                        if (c.Tag == "mod")
                            mod = c as RadioButton;
                        else if (c.Tag == "game")
                            game = c as RadioButton;
                        else if (c.Tag == "both")
                            both = c as RadioButton;
                    }
                    if (c is CheckBox)
                        check = c as CheckBox;
                }
                switch (FileStatuses[index])
                {
                    case FileStatus.FileNotFound:
                        l.BackColor = Color.Red;
                        l.Text = "Not found!";
                        mod.Enabled = false;
                        game.Enabled = false;
                        check.Enabled = false;
                        both.Enabled = false;
                        break;
                    case FileStatus.GameFoundModNot:
                        l.BackColor = Color.Green;
                        l.Text = "Only game";
                        mod.Enabled = false;
                        game.Enabled = true;
                        check.Enabled = false;
                        game.Checked = true;
                        both.Enabled = false;
                        break;
                    case FileStatus.GameNotModFound:
                        l.BackColor = Color.Green;
                        l.Text = "Only mod";
                        mod.Enabled = true;
                        game.Enabled = false;
                        check.Enabled = true;
                        mod.Checked = true;
                        both.Enabled = false;
                        break;
                    case FileStatus.BothFound:
                        l.BackColor = Color.Green;
                        l.Text = "Both found";
                        mod.Enabled = true;
                        game.Enabled = true;
                        check.Enabled = true;
                        //mod.Checked = true;
                        both.Enabled = true;
                        if (lastOptions[index] == 0)
                        {
                            game.Checked = true;
                            check.Checked = true;
                            check.Enabled = false;
                        }
                        else if (lastOptions[index] == 1)
                            mod.Checked = true;
                        else
                            both.Checked = true;
                       
                        break;
                }
            }
        }

        private void GameDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                GameDirectoryBox.Text = ofd.SelectedPath;
            }
        }

        private void ModDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ModDirectoryBox.Text = ofd.SelectedPath;
            }
        }

        private void ConfirmFileUsage_Click(object sender, EventArgs e)
        {
            if (!FileStatuses.Any(x => x == FileStatus.FileNotFound))
            {
                foreach (GroupBox gb in LoadingPanel.Controls)
                {
                    int index = int.Parse(gb.Tag.ToString());
                    RadioButton game = null;
                    RadioButton mod = null;
                    RadioButton both = null;
                    CheckBox check = null;
                    foreach (Control c in gb.Controls)
                    {
                        if (c is RadioButton)
                        {
                            if (c.Tag == "mod")
                                mod = c as RadioButton;
                            else if (c.Tag == "game")
                                game = c as RadioButton;
                            else if (c.Tag == "both")
                                both = c as RadioButton;
                        }
                        if (c is CheckBox)
                            check = c as CheckBox;
                    }


                    GlobalVariables.UseMod[index] = (mod.Checked ? 1 : (both.Checked ? 2 : 0));
                    GlobalVariables.ReadOnly[index] = check.Checked;
                    lastOptions[index] = (mod.Checked ? 1 : (both.Checked ? 2 : 0));
                }
                GlobalVariables.LoadedProperly = true;
                GlobalVariables.CreateNewFilesReadOnly = ReadOnlyNewFiles.Checked;
                GlobalVariables.NewObjectsNewFiles = NewFilesForNewObjects.Checked;
                switch (LanguageBox.SelectedIndex)
                {
                   
                    case 0:
                        GlobalVariables.LocalisationLanguage = GlobalVariables.Languages.English;
                        break;
                    case 1:
                        GlobalVariables.LocalisationLanguage = GlobalVariables.Languages.French;
                        break;
                    case 2:
                        GlobalVariables.LocalisationLanguage = GlobalVariables.Languages.German;
                        break;
                    case 3:
                        GlobalVariables.LocalisationLanguage = GlobalVariables.Languages.Spanish;
                        break;
                    default:
                        GlobalVariables.LocalisationLanguage = GlobalVariables.Languages.English;
                        break;
                }
                GlobalVariables.AppSizeOption = AppSizeBox.SelectedIndex;
                //File.WriteAllText("modeditor_darkmode.txt", GlobalVariables.DarkMode.ToString());

                try
                {
                    string[] datesplit = StartDateBox.Text.Split('.');
                    GlobalVariables.StartDate = new DateTime(int.Parse(datesplit[0]), int.Parse(datesplit[1]), int.Parse(datesplit[2]));
                }
                catch
                {
                    GlobalVariables.StartDate = new DateTime(1444, 11, 11);
                }

                options.MainNode.ChangeVariable("last_used_date", GlobalVariables.StartDate.Year + "." + GlobalVariables.StartDate.Month + "." + GlobalVariables.StartDate.Day, true);

                Node lastOP = new Node("lastOptions");

                foreach (int n in lastOptions)
                    lastOP.AddPureValue(n.ToString());

                if (options.MainNode.TryGetNode("lastOptions", out Node nd))
                    options.MainNode.ReplaceNode(nd, lastOP);
                else
                    options.MainNode.AddNode(lastOP);
                this.Close();
            }
        }

        private void AllMod_Click(object sender, EventArgs e)
        {
            foreach (Label l in StatusLabels)
            {
                int index = StatusLabels.IndexOf(l);
                RadioButton game = null;
                RadioButton mod = null;
                RadioButton both = null;
                CheckBox check = null;
                foreach (Control c in l.Parent.Controls)
                {
                    if (c is RadioButton)
                    {
                        if (c.Tag == "mod")
                            mod = c as RadioButton;
                        else if (c.Tag == "game")
                            game = c as RadioButton;
                        else if (c.Tag == "both")
                            both = c as RadioButton;
                    }
                    if (c is CheckBox)
                        check = c as CheckBox;
                }
                switch (FileStatuses[index])
                {
                    case FileStatus.FileNotFound:
                        break;
                    case FileStatus.GameFoundModNot:                       
                        break;
                    case FileStatus.GameNotModFound:
                        mod.Checked = true;
                        break;
                    case FileStatus.BothFound:
                        mod.Checked = true;
                        break;
                }
            }
        }

        private void AllGame_Click(object sender, EventArgs e)
        {
            foreach (Label l in StatusLabels)
            {
                int index = StatusLabels.IndexOf(l);
                RadioButton game = null;
                RadioButton mod = null;
                RadioButton both = null;
                CheckBox check = null;
                foreach (Control c in l.Parent.Controls)
                {
                    if (c is RadioButton)
                    {
                        if (c.Tag == "mod")
                            mod = c as RadioButton;
                        else if (c.Tag == "game")
                            game = c as RadioButton;
                        else if (c.Tag == "both")
                            both = c as RadioButton;
                    }
                    if (c is CheckBox)
                        check = c as CheckBox;
                }
                switch (FileStatuses[index])
                {
                    case FileStatus.FileNotFound:
                        break;
                    case FileStatus.GameFoundModNot:
                        game.Checked = true;
                        break;
                    case FileStatus.GameNotModFound:
                        break;
                    case FileStatus.BothFound:
                        game.Checked = true;
                        break;
                }
            }
        }

        private void AllBoth_Click(object sender, EventArgs e)
        {
            foreach (Label l in StatusLabels)
            {
                int index = StatusLabels.IndexOf(l);
                RadioButton game = null;
                RadioButton mod = null;
                RadioButton both = null;
                CheckBox check = null;
                foreach (Control c in l.Parent.Controls)
                {
                    if (c is RadioButton)
                    {
                        if (c.Tag == "mod")
                            mod = c as RadioButton;
                        else if (c.Tag == "game")
                            game = c as RadioButton;
                        else if (c.Tag == "both")
                            both = c as RadioButton;
                    }
                    if (c is CheckBox)
                        check = c as CheckBox;
                }
                switch (FileStatuses[index])
                {
                    case FileStatus.FileNotFound:
                        break;
                    case FileStatus.GameFoundModNot:
                        break;
                    case FileStatus.GameNotModFound:
                        break;
                    case FileStatus.BothFound:
                        both.Checked = true;
                        break;
                }
            }
        }

        private void AllRead_Click(object sender, EventArgs e)
        {
            foreach (Label l in StatusLabels)
            {
                int index = StatusLabels.IndexOf(l);
                RadioButton game = null;
                RadioButton mod = null;
                RadioButton both = null;
                CheckBox check = null;
                foreach (Control c in l.Parent.Controls)
                {
                    if (c is RadioButton)
                    {
                        if (c.Tag == "mod")
                            mod = c as RadioButton;
                        else if (c.Tag == "game")
                            game = c as RadioButton;
                        else if (c.Tag == "both")
                            both = c as RadioButton;
                    }
                    if (c is CheckBox)
                        check = c as CheckBox;
                }
                switch (FileStatuses[index])
                {
                    case FileStatus.FileNotFound:
                        break;
                    case FileStatus.GameFoundModNot:
                        check.Checked = true;
                        break;
                    case FileStatus.GameNotModFound:
                        check.Checked = true;
                        break;
                    case FileStatus.BothFound:
                        check.Checked = true;
                        break;
                }
            }
        }

        private void AllNotRead_Click(object sender, EventArgs e)
        {
            foreach (Label l in StatusLabels)
            {
                int index = StatusLabels.IndexOf(l);
                RadioButton game = null;
                RadioButton mod = null;
                RadioButton both = null;
                CheckBox check = null;
                foreach (Control c in l.Parent.Controls)
                {
                    if (c is RadioButton)
                    {
                        if (c.Tag == "mod")
                            mod = c as RadioButton;
                        else if (c.Tag == "game")
                            game = c as RadioButton;
                        else if (c.Tag == "both")
                            both = c as RadioButton;
                    }
                    if (c is CheckBox)
                        check = c as CheckBox;
                }
                if(both.Checked || mod.Checked)
                {
                    check.Checked = false;
                }
            }
        }

        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            LanguageBox.SelectedIndex = 0;
            AppSizeBox.SelectedIndex = 0;
        }

        private void DarkmodeOption_Click(object sender, EventArgs e)
        {
            GlobalVariables.DarkMode = !GlobalVariables.DarkMode;
            ApplyDarkMode();

        }

        public void ApplyDarkMode()
        {
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;
            foreach (Control b in Controls)
            {
                if(b is GroupBox){
                    b.ForeColor = Color.White;
                    b.BackColor = Color.Black;
                    foreach (Control c in (b as GroupBox).Controls)
                        if (c is Button)
                            (c as Button).FlatStyle = FlatStyle.Flat;
                }
                if(b is Button)
                    (b as Button).FlatStyle = FlatStyle.Flat;
            }
        }

        public class Bookmark
        {
            public bool MOD = false;
            public DateTime Date = new DateTime(1444,11,11);           
            public Bookmark(int year, int month, int day, bool mod = false)
            {
                Date = new DateTime(year, month, day);
                MOD = mod;
            }
            public override string ToString()
            {
                return Date.ToString("yyyy.MM.dd");
            }

        }

        private void BookmarksComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartDateBox.Text = $"{bookmarks[BookmarksComboBox.SelectedIndex].Date.Year}.{bookmarks[BookmarksComboBox.SelectedIndex].Date.Month}.{bookmarks[BookmarksComboBox.SelectedIndex].Date.Day}";
        }
    }
}
