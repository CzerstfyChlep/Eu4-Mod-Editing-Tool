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
        public LoadingScreen()
        {
            InitializeComponent();
            if(File.Exists("directories.txt"))
            {
                string txt = File.ReadAllText("directories.txt");
                string[] sp = txt.Split('\n');
                if (sp.Count() > 0)
                    GameDirectoryBox.Text = sp[0];
                if (sp.Count() > 1)
                    ModDirectoryBox.Text = sp[1];
            }
            if (File.Exists("modeditor_darkmode.txt"))
            {
                string txt = File.ReadAllText("modeditor_darkmode.txt");
                if(bool.TryParse(txt, out bool dm))
                    if(dm)
                        GlobalVariables.DarkMode = true;
                //ApplyDarkMode();
            }


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

        private void CheckFilesButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText("directories.txt", GameDirectoryBox.Text + "\n" + ModDirectoryBox.Text);

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
                        mod.Checked = true;
                        both.Enabled = true;
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
                File.WriteAllText("modeditor_darkmode.txt", GlobalVariables.DarkMode.ToString());

                try
                {
                    string[] datesplit = StartDateBox.Text.Split('.');
                    GlobalVariables.StartDate = new DateTime(int.Parse(datesplit[0]), int.Parse(datesplit[1]), int.Parse(datesplit[2]));
                }
                catch
                {
                    GlobalVariables.StartDate = new DateTime(1444, 11, 11);
                }
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
    }
}
