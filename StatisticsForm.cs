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
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
            UpdateStats();
            CheckForIllegalCrossThreadCalls = false;
            FilterTypeBox.SelectedIndex = 0;
            FilteredProvinces = new List<Province>(GlobalVariables.Provinces);
            Thread t = new Thread(UpdateEvery5Seconds);
            t.Start();
        }


        public List<Country> CountriesFilterList = new List<Country>();
        public List<Religion> ReligionFilterList = new List<Religion>();
        public List<Continent> ContinentFilterList = new List<Continent>();
        public List<Superregion> SuperregionFilterList = new List<Superregion>();
        public List<Region> RegionFilterList = new List<Region>();
        public List<string> TechGroupFilterList = new List<string>();
        public List<Tradenode> TradenodesFilterList = new List<Tradenode>();
        public List<TradeGood> TradeGoodFilterList = new List<TradeGood>();
        public List<Building> BuildingFilterList = new List<Building>();

        public void UpdateEvery5Seconds()
        {
            while (!GlobalVariables.Exited)
            {
                Thread.Sleep(5000);
                if(!StatsRecentlyUpdated)
                    UpdateStats();
                StatsRecentlyUpdated = false;
            }
        }

        public List<Province> FilteredProvinces = new List<Province>();

        bool StatsRecentlyUpdated = false;

        public void UpdateStats()
        {
            StatsRecentlyUpdated = true;

            int tax = 0;
            int production = 0;
            int manpower = 0;
            int totalprov = 0;

            List<Country> countries = new List<Country>();

            List<Religion> religions = new List<Religion>();
            List<int> provinces = new List<int>();
            List<int> relavgdev = new List<int>();


            foreach(Province p in FilteredProvinces)
            {
                if (!p.Wasteland && !p.Sea && !p.Lake)
                {
                    tax += p.Tax;
                    production += p.Production;
                    manpower += p.Manpower;
                    totalprov++;
                    if (p.OwnerCountry != null)
                        countries.Add(p.OwnerCountry);
                }

                if (p.Religion != Religion.NoReligion && p.Religion != null)
                {
                    if (religions.Contains(p.Religion))
                    {
                        provinces[religions.IndexOf(p.Religion)]++;
                        relavgdev[religions.IndexOf(p.Religion)] += p.Tax + p.Manpower + p.Production;
                    }
                    else
                    {
                        religions.Add(p.Religion);
                        provinces.Add(1);
                        relavgdev.Add(p.Tax + p.Manpower + p.Production);
                    }
                }
            }
            countries = countries.Distinct().ToList();
            int totaldev = tax + production + manpower;
            StatsProvTotalDev.Text = totaldev.ToString();
            StatsProvTotalTax.Text = tax.ToString();
            StatsProvTotalProduction.Text = production.ToString();
            StatsProvTotalManpower.Text = manpower.ToString();

            StatsProvAvgDev.Text = ((double)totaldev / totalprov).ToString("f2");
            StatsProvAvgTax.Text = ((double)tax / totalprov).ToString("f2");
            StatsProvAvgProduction.Text = ((double)production / totalprov).ToString("f2");
            StatsProvAvgManpower.Text = ((double)manpower / totalprov).ToString("f2");

            StatsProvTotalNum.Text = totalprov.ToString();

            string toset = "";
            foreach(Country c in countries)
            {
                toset += c.ToString() + Environment.NewLine;
            }


            if (ChangesMadeReligion) {
                ChangesMadeReligion = false;
                ReligionsView.Rows.Clear();
                for (int a = 0; a < religions.Count; a++)
                {
                    ReligionsView.Rows.Add(religions[a].ReadableName, provinces[a], Math.Round((double)provinces[a] / totalprov, 2), Math.Round((double)relavgdev[a] / provinces[a], 2));
                }
            }

            if (ChangesMadeCountry)
            {
                ChangesMadeCountry = false;
                CountryView.Rows.Clear();
                int totalDEV = 0;
                int prov = 0;

                countries.ForEach(x => { totalDEV += x.TotalDevelopment; prov += x.Provinces.Count; });
                CountryView.Rows.Add("AVERAGE", Math.Round((double)prov/countries.Count, 2), Math.Round((double)totalDEV / countries.Count, 2), Math.Round((double)totalDEV / prov, 2));
                for (int a = 0; a < countries.Count; a++)
                {
                    CountryView.Rows.Add(countries[a].ToString(), countries[a].Provinces.Count, countries[a].TotalDevelopment, Math.Round((double)countries[a].TotalDevelopment / countries[a].Provinces.Count, 2));
                }

            }

            if (ChangesMadeProvinces)
            {
                ChangesMadeProvinces = false;
                ProvinceView.Rows.Clear();

                //ProvinceView.Rows.Add("", "Average", Math.Round((double)totaldev / totalprov, 2), Math.Round((double)totalDEV / prov, 2));
                foreach(Province p in FilteredProvinces)
                {
                    ProvinceView.Rows.Add(p.ID, p.DefinitionName, p.Tax + p.Production + p.Manpower, p.Tax, p.Production, p.Manpower, p.Religion ?? Religion.NoReligion, p.OwnerCountry);
                }

            }

        }

        bool ChangesMadeReligion = false;
        bool ChangesMadeCountry = false;
        bool ChangesMadeProvinces = false;

        private void FilterTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (FilterTypeBox.SelectedIndex)
            {
                case 0:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(GlobalVariables.Countries.Where(x=>x!=Country.NoCountry).ToArray());
                    break;
                case 1:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(Religion.Religions.ToArray());
                    break;
                case 2:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(GlobalVariables.Continents.ToArray());
                    break;
                case 3:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(GlobalVariables.Superregions.ToArray());
                    break;
                case 4:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(GlobalVariables.Regions.ToArray());
                    break;
                case 5:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(GlobalVariables.TechGroups.ToArray());
                    break;
                case 6:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(GlobalVariables.TradeNodes.ToArray());
                    break;
                case 7:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(GlobalVariables.TradeGoods.ToArray());
                    break;
                case 8:
                    FilterValueBox.Items.Clear();
                    FilterValueBox.Items.AddRange(GlobalVariables.Buildings.ToArray());
                    break;
            }

            FilterValueBox.SelectedIndex = 0;
        }

        private void AddRemoveFilter_Click(object sender, EventArgs e)
        {
            switch (FilterTypeBox.SelectedIndex)
            {
                case 0:
                    Country c = (Country)FilterValueBox.SelectedItem;
                    if (CountriesFilterList.Contains(c))
                        CountriesFilterList.Remove(c);
                    else
                        CountriesFilterList.Add(c);

                    if (CountriesFilterList.Any())
                    {
                        string toAdd = "";
                        CountriesFilterList.ForEach(x => toAdd += x.FullName + ", ");
                        TagFilter.Text = "Countries: " + toAdd;                           
                    }
                    else
                        TagFilter.Text = "Countries: All";


                    break;
                case 1:
                    Religion r = (Religion)FilterValueBox.SelectedItem;
                    if (ReligionFilterList.Contains(r))
                        ReligionFilterList.Remove(r);
                    else
                        ReligionFilterList.Add(r);

                    if (ReligionFilterList.Any())
                    {
                        string toAdd = "";
                        ReligionFilterList.ForEach(x => toAdd += x.ReadableName + ", ");
                        ReligionFilter.Text = "Religions: " + toAdd;
                    }
                    else
                        ReligionFilter.Text = "Religions: All";

                    break;
                case 2:
                    Continent con = (Continent)FilterValueBox.SelectedItem;
                    if (ContinentFilterList.Contains(con))
                        ContinentFilterList.Remove(con);
                    else
                        ContinentFilterList.Add(con);
                    if (ContinentFilterList.Any())
                    {
                        string toAdd = "";
                        ContinentFilterList.ForEach(x => toAdd += x.Name + ", ");
                        ContinentFilter.Text = "Continents: " + toAdd;
                    }
                    else
                        ContinentFilter.Text = "Continents: All";


                    break;
                case 3:
                    Superregion sup = (Superregion)FilterValueBox.SelectedItem;
                    if (SuperregionFilterList.Contains(sup))
                        SuperregionFilterList.Remove(sup);
                    else
                        SuperregionFilterList.Add(sup);

                    if (SuperregionFilterList.Any())
                    {
                        string toAdd = "";
                        SuperregionFilterList.ForEach(x => toAdd += x.Name + ", ");
                        SuperregionFilter.Text = "Superregions: " + toAdd;
                    }
                    else
                        SuperregionFilter.Text = "Superregions: All";

                    break;
                case 4:
                    Region reg = (Region)FilterValueBox.SelectedItem;
                    if (RegionFilterList.Contains(reg))
                        RegionFilterList.Remove(reg);
                    else
                        RegionFilterList.Add(reg);

                    if (RegionFilterList.Any())
                    {
                        string toAdd = "";
                        RegionFilterList.ForEach(x => toAdd += x.Name + ", ");
                        RegionsFilter.Text = "Regions: " + toAdd;
                    }
                    else
                        RegionsFilter.Text = "Regions: All";

                    break;
                case 5:
                    string Tech = (string)FilterValueBox.SelectedItem;
                    if (TechGroupFilterList.Contains(Tech))
                        TechGroupFilterList.Remove(Tech);
                    else
                        TechGroupFilterList.Add(Tech);
                    if (TechGroupFilterList.Any())
                    {
                        string toAdd = "";
                        TechGroupFilterList.ForEach(x => toAdd += x + ", ");
                        TechgroupsFilter.Text = "Tech groups: " + toAdd;
                    }
                    else
                        TechgroupsFilter.Text = "Tech groups: All";

                    break;
                case 6:
                    Tradenode nodes = (Tradenode)FilterValueBox.SelectedItem;
                    if (TradenodesFilterList.Contains(nodes))
                        TradenodesFilterList.Remove(nodes);
                    else
                        TradenodesFilterList.Add(nodes);
                    if (TradenodesFilterList.Any())
                    {
                        string toAdd = "";
                        TradenodesFilterList.ForEach(x => toAdd += x.Name + ", ");
                        TradenodesFilter.Text = "Tradenodes: " + toAdd;
                    }
                    else
                        TradenodesFilter.Text = "Tradenodes: All";

                    break;
                case 7:
                    TradeGood goods = (TradeGood)FilterValueBox.SelectedItem;
                    if (TradeGoodFilterList.Contains(goods))
                        TradeGoodFilterList.Remove(goods);
                    else
                        TradeGoodFilterList.Add(goods);
                    if (TradeGoodFilterList.Any())
                    {
                        string toAdd = "";
                        TradeGoodFilterList.ForEach(x => toAdd += x.ReadableName + ", ");
                        TradegoodsFilter.Text = "Trade goods: " + toAdd;
                    }
                    else
                        TradegoodsFilter.Text = "Trade goods: All";
                    break;
                case 8:
                    Building building = (Building)FilterValueBox.SelectedItem;
                    if (BuildingFilterList.Contains(building))
                        BuildingFilterList.Remove(building);
                    else
                        BuildingFilterList.Add(building);
                    if (BuildingFilterList.Any())
                    {
                        string toAdd = "";
                        BuildingFilterList.ForEach(x => toAdd += x.Name + ", ");
                        BuildingsFilter.Text = "With buildings: " + toAdd;
                    }
                    else
                        BuildingsFilter.Text = "With buildings: All";
                    break;
            }

            ApplyFilters();
        }

        public void ApplyFilters()
        {
            List<Province> oldFiltered = new List<Province>(FilteredProvinces);
            FilteredProvinces = new List<Province>(GlobalVariables.Provinces);
            if (CountriesFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => CountriesFilterList.Contains(x.OwnerCountry)).ToList();
            if (ReligionFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => ReligionFilterList.Contains(x.Religion)).ToList();
            if (ContinentFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => ContinentFilterList.Contains(x.Continent)).ToList();
            if (SuperregionFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => SuperregionFilterList.Contains(x.Area?.Region?.Superregion)).ToList();
            if (RegionFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => RegionFilterList.Contains(x.Area?.Region)).ToList();
            if (TechGroupFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => TechGroupFilterList.Contains(x.OwnerCountry?.TechnologyGroup)).ToList();
            if (TradenodesFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => TradenodesFilterList.Contains(x.TradeNode)).ToList();
            if (TradeGoodFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => TradeGoodFilterList.Contains(x.TradeGood)).ToList();
            if (BuildingFilterList.Any())
                FilteredProvinces = FilteredProvinces.Where(x => x.GetBuildings().Intersect(BuildingFilterList).Any()).ToList();

            if (!Enumerable.SequenceEqual(oldFiltered, FilteredProvinces))
            {
                ChangesMadeReligion = true;
                ChangesMadeCountry = true;
                ChangesMadeProvinces = true;
            }
            UpdateStats();
        }

        private void CountriesAll_Click(object sender, EventArgs e)
        {
            CountriesFilterList.Clear();
            TagFilter.Text = "Countries: All";
            ApplyFilters();
        }

        private void ReligionsAll_Click(object sender, EventArgs e)
        {
            ReligionFilterList.Clear();
            ReligionFilter.Text = "Religions: All";
            ApplyFilters();
        }

        private void ContinentsAll_Click(object sender, EventArgs e)
        {
            ContinentFilterList.Clear();
            ContinentFilter.Text = "Continents: All";
            ApplyFilters();
        }

        private void SuperregionsAll_Click(object sender, EventArgs e)
        {
            SuperregionFilterList.Clear();
            SuperregionFilter.Text = "Superregions: All";
            ApplyFilters();
        }

        private void RegionsAll_Click(object sender, EventArgs e)
        {
            RegionFilterList.Clear();
            RegionsFilter.Text = "Regions: All";
            ApplyFilters();
        }

        private void TechgroupsAll_Click(object sender, EventArgs e)
        {
            TechGroupFilterList.Clear();
            TechgroupsFilter.Text = "Tech groups: All";
            ApplyFilters();
        }

        private void TradenodesAll_Click(object sender, EventArgs e)
        {
            TradenodesFilterList.Clear();
            TradenodesFilter.Text = "Tradenodes: All";
            ApplyFilters();
        }

        private void TradegoodsAll_Click(object sender, EventArgs e)
        {
            TradeGoodFilterList.Clear();
            TradegoodsFilter.Text = "Trade goods: All";
            ApplyFilters();
        }

        private void BuildingsAll_Click(object sender, EventArgs e)
        {
            BuildingFilterList.Clear();
            BuildingsFilter.Text = "With buildings: All";
            ApplyFilters();
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            ChangesMadeReligion = true;
            ChangesMadeCountry = true;
            ChangesMadeProvinces = true;
            UpdateStats();

        }

    }
}
