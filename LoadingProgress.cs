using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    public partial class LoadingProgress : Form
    {
        public LoadingProgress()
        {
            InitializeComponent();
        }
        public void UpdateProgressLabel(object senrder, ProgressChangedEventArgs e)
        {
            GlobalVariables.CurrentLoadingProgress = e.ProgressPercentage;
            if (e.ProgressPercentage == -1)
            {
                MessageBox.Show(e.UserState.ToString());
            }
            else
            {
                progressBar2.Value = e.ProgressPercentage;

                switch (e.ProgressPercentage)
                {
                    case 0:
                        LoadingProgressLabel.Text = "Loading definition...";
                        break;
                    case 5:
                        LoadingProgressLabel.Text = "Loading province map...";
                        break;
                    case 10:
                        LoadingProgressLabel.Text = "Setting provinces' centres...";
                        break;
                    case 15:
                        LoadingProgressLabel.Text = "Loading trade goods files...";
                        break;
                    case 20:
                        LoadingProgressLabel.Text = "Adding trade goods...";
                        break;
                    case 24:
                        LoadingProgressLabel.Text = "Loading trade goods prices files...";
                        break;
                    case 28:
                        LoadingProgressLabel.Text = "Adding trade goods prices...";
                        break;

                    case 32:
                        LoadingProgressLabel.Text = "Loading culture files...";
                        break;
                    case 36:
                        LoadingProgressLabel.Text = "Adding cultures...";
                        break;
                    case 40:
                        LoadingProgressLabel.Text = "Loading religion files...";
                        break;
                    case 44:
                        LoadingProgressLabel.Text = "Adding religions...";
                        break;
                    case 48:
                        LoadingProgressLabel.Text = "Loading government files...";
                        break;
                    case 52:
                        LoadingProgressLabel.Text = "Adding governments...";
                        break;
                    case 56:
                        LoadingProgressLabel.Text = "Loading technology group file...";
                        break;
                    case 60:
                        LoadingProgressLabel.Text = "Adding technology groups...";
                        break;
                    case 63:
                        LoadingProgressLabel.Text = "Loading country tags files...";
                        break;
                    case 66:
                        LoadingProgressLabel.Text = "Adding country tags...";
                        break;
                    case 69:
                        LoadingProgressLabel.Text = "Loading country history files...";
                        break;
                    case 72:
                        LoadingProgressLabel.Text = "Adding country histories...";
                        break;
                    case 75:
                        LoadingProgressLabel.Text = "Loading building files...";
                        break;
                    case 78:
                        LoadingProgressLabel.Text = "Adding buildings...";
                        break;
                    case 81:
                        LoadingProgressLabel.Text = "Loading country common files...";
                        break;
                    case 84:
                        LoadingProgressLabel.Text = "Adding country common info...";
                        break;
                    case 86:
                        LoadingProgressLabel.Text = "Loading province files...";
                        break;
                    case 88:
                        LoadingProgressLabel.Text = "Adding provinces...";
                        break;
                    case 90:
                        LoadingProgressLabel.Text = "Loading area files...";
                        break;
                    case 92:
                        LoadingProgressLabel.Text = "Adding areas...";
                        break;
                    case 94:
                        LoadingProgressLabel.Text = "Loading region files...";
                        break;
                    case 96:
                        LoadingProgressLabel.Text = "Adding regions...";
                        break;
                    case 98:
                        LoadingProgressLabel.Text = "Loading continent files...";
                        break;
                    case 100:
                        LoadingProgressLabel.Text = "Adding continents...";
                        break;
                    case 102:
                        LoadingProgressLabel.Text = "Loading tradenode files...";
                        break;
                    case 105:
                        LoadingProgressLabel.Text = "Adding tradenodes...";
                        break;
                    case 106:
                        LoadingProgressLabel.Text = "Loading superregion file...";
                        break;
                    case 107:
                        LoadingProgressLabel.Text = "Adding superregions...";
                        break;
                    case 108:
                        LoadingProgressLabel.Text = "Loading map variables...";
                        break;
                    case 110:
                        this.Close();
                        break;
                }
            }
        }
    }
}
