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
    public partial class MapmodesWindow : Form
    {
        public MapmodesWindow()
        {
            InitializeComponent();

            ProvincesMapmodeButton.Click += ChangeMapmode.ChangeMapmodeVoid;
            DevelopmentMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            TradeGoodsMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            ReligionMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            CultureMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            PoliticalMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            AreaMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            RegionMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            TradeNodeMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            HREMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            FortMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            ContinentMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            SuperregionMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            DiscoveredByMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            TradeCompanyMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            GovernmentMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            ClimateMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;
            WinterMapmode.Click += ChangeMapmode.ChangeMapmodeVoid;

        }
    }
}
