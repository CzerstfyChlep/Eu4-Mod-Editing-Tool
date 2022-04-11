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
                case 50:
                    LoadingProgressLabel.Text = "Loading trade goods...";
                    break;
                case 55:
                    LoadingProgressLabel.Text = "Loading trade good prices...";
                    break;
                case 60:
                    LoadingProgressLabel.Text = "Loading cultures...";
                    break;
                case 65:
                    LoadingProgressLabel.Text = "Loading religions...";
                    break;
                case 70:
                    LoadingProgressLabel.Text = "Loading countries...";
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
