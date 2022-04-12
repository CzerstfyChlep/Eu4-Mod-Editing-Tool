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
            progressBar2.Value = e.ProgressPercentage;
            switch (e.ProgressPercentage)
            {
                case 0:
                    LoadingProgressLabel.Text = "Loading definition...";
                    break;
                case 10:
                    LoadingProgressLabel.Text = "Loading province map...";
                    break;
                case 20:
                    LoadingProgressLabel.Text = "Setting provinces' centres...";
                    break;
                case 25:
                    LoadingProgressLabel.Text = "Loading trade goods files...";
                    break;
                case 30:
                    LoadingProgressLabel.Text = "Adding trade goods...";
                    break;
                case 35:
                    LoadingProgressLabel.Text = "Loading trade goods prices files...";
                    break;
                case 40:
                    LoadingProgressLabel.Text = "Adding trade goods prices...";
                    break;

                case 45:
                    LoadingProgressLabel.Text = "Loading culture files...";
                    break;
                case 50:
                    LoadingProgressLabel.Text = "Adding cultures...";
                    break;
                case 55:
                    LoadingProgressLabel.Text = "Loading religion files...";
                    break;
                case 60:
                    LoadingProgressLabel.Text = "Adding religions...";
                    break;
                case 65:
                    LoadingProgressLabel.Text = "Loading government files...";
                    break;
                case 70:
                    LoadingProgressLabel.Text = "Adding governments...";
                    break;
                case 73:
                    LoadingProgressLabel.Text = "Loading technology group file...";
                    break;
                case 74:
                    LoadingProgressLabel.Text = "Adding technology groups...";
                    break;
                case 75:
                    LoadingProgressLabel.Text = "Adding technology groups...";
                    break;
                case 80:
                    LoadingProgressLabel.Text = "Loading provinces...";
                    break;
                case 90:
                    LoadingProgressLabel.Text = "Loading areas...";
                    break;
                case 95:
                    LoadingProgressLabel.Text = "Loading regions...";
                    break;
                case 100:
                    LoadingProgressLabel.Text = "Loading tradenodes...";
                    break;
                case 105:                    
                    LoadingProgressLabel.Text = "Loading map variables...";
                    break;
                case 110:
                    this.Close();
                    break;
            }
        }
    }
}
