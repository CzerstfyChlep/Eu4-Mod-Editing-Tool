using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        List<string> ToLoad = new List<string>() { "map\\definition.csv", "map\\provinces.bmp", "common\\tradegoods\\", "common\\prices\\", "common\\cultures\\", "common\\religions\\", "map\\region.txt", "common\\countries\\", "history\\provinces\\", "map\\area.txt", "map\\default.map", "history\\countries\\", "common\\tradenodes\\", "map\\continent.txt", "common\\country_tags\\", "common\\technology.txt", "common\\governments\\", "common\\buildings\\" };

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

            groupBox2.Enabled = true;
            if(GameDirectoryBox.Text.Last() == '\\')
                GlobalVariables.pathtogame = GameDirectoryBox.Text;
            else
                GlobalVariables.pathtogame = GameDirectoryBox.Text + "\\";

            if (ModDirectoryBox.Text.Last() == '\\')
                GlobalVariables.pathtomod = ModDirectoryBox.Text;
            else
                GlobalVariables.pathtomod = ModDirectoryBox.Text + "\\";

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
                        check.Enabled = true;
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
                this.Close();
            }
        }
    }
}
