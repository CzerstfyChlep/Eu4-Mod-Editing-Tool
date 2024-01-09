using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    public partial class LoadingProgress : Form
    {
        public LoadingProgress()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        int amount = 0;
        public void UpdateProgress(int id, int status)
        {
            Label l = null;
            switch (id)
            {
                case 0:
                    l = DefinitionProgress;
                    break;
                case 1:
                    l = TradeGoodsProgress;
                    break;
                case 2:
                    l = MapProgress;
                    break;
                case 3:
                    l = CentresProgress;
                    break;
                case 4:
                    l = CultureProgress;
                    break;
                case 5:
                    l = ReligionProgress;
                    break;
                case 6:
                    l = GovernmentsProgress;
                    break;
                case 7:
                    l = TechnologyProgress;
                    break;
                case 8:
                    l = TagsProgress;
                    break;
                case 9:
                    l = CountryHistoryProgress;
                    break;
                case 10:
                    l = CountryCommonProgress;
                    break;
                case 11:
                    l = ProvinceHistoryProgress;
                    break;
                case 12:
                    l = AreaProgress;
                    break;
                case 13:
                    l = RegionProgress;
                    break;
                case 14:
                    l = ContinentsProgress;
                    break;
                case 15:
                    l = TradeNodesProgress;
                    break;
                case 16:
                    l = SuperregionProgress;
                    break;
                case 17:
                    l = DefaultMapProgress;
                    break;
                case 18:
                    l = UpdateMapProgress;
                    break;
                case 19:
                    l = UpdateControlsProgress;
                    break;
                case 20:
                    l = BuildingProgress;
                    break;
                case 21:
                    l = TradeCompaniesProgress;
                    break;
                case 22:
                    l = LocalisationProgress;
                    break;
                case 23:
                    l = BordersProgress;
                    break;
                case 24:
                    l = ClimateProgress;
                    break;
            }
            if (status == 0)
                l.ForeColor = Color.DarkGoldenrod;
            else if (status == 1)
            {
                l.ForeColor = Color.DarkRed;
                amount++;
            }
            else if (status == 2)
            {
                l.ForeColor = Color.DarkGreen;
                amount++;
            }
            
            if(amount == 25)
            {
                ContinueButton.Enabled = true;
            }
        }

        public void ReportError(string message)
        {
            ErrorList.AppendText(Environment.NewLine + $"{message}");
        }

        public void MakeContinueAvailable()
        {
            ContinueButton.Enabled = true;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
