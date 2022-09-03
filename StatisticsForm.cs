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
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();

            UpdateStats();

        }


        public void UpdateStats()
        {
            int tax = 0;
            int production = 0;
            int manpower = 0;
            int totalprov = 0;
            foreach(Province p in GlobalVariables.Provinces)
            {
                if (!p.Wasteland && !p.Sea && !p.Lake)
                {
                    tax += p.Tax;
                    production += p.Production;
                    manpower += p.Manpower;
                    totalprov++;
                }
            }
            int totaldev = tax + production + manpower;
            StatsProvTotalDev.Text = totaldev.ToString();
            StatsProvTotalTax.Text = tax.ToString();
            StatsProvTotalProduction.Text = production.ToString();
            StatsProvTotalManpower.Text = manpower.ToString();

            StatsProvAvgDev.Text = ((double)totaldev / totalprov).ToString("f2");
            StatsProvAvgTax.Text = ((double)tax / totalprov).ToString("f2");
            StatsProvAvgProduction.Text = ((double)production / totalprov).ToString("f2");
            StatsProvAvgManpower.Text = ((double)manpower / totalprov).ToString("f2");
        }
    }
}
