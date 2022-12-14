using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class DevelopmentManagement
    {


        private static void RandomDev(int Min, int Avg, int Max)
        {
            foreach (Province p in GlobalVariables.ClickedProvinces)
            {
                int average = GlobalVariables.GlobalRandom.Next(0, 10);
                if (average > 7)
                    p.Tax = Avg;
                else if (average == 6 && Avg > 1 && Avg > Min)
                    p.Tax = Avg - 1;
                else if (average == 5 && Max > Avg)
                    p.Tax = Avg + 1;
                else
                    p.Tax = GlobalVariables.GlobalRandom.Next(Min, Max + 1);

                average = GlobalVariables.GlobalRandom.Next(0, 10);
                if (average > 7)
                    p.Production = Avg;
                else if (average == 6 && Avg > 1 && Avg > Min)
                    p.Production = Avg - 1;
                else if (average == 5 && Max > Avg)
                    p.Production = Avg + 1;
                else
                    p.Production = GlobalVariables.GlobalRandom.Next(Min, Max + 1);

                average = GlobalVariables.GlobalRandom.Next(0, 10);
                if (average > 7)
                    p.Manpower = Avg;
                else if (average == 6 && Avg > 1 && Avg > Min)
                    p.Manpower = Avg - 1;
                else if (average == 5 && Max > Avg)
                    p.Manpower = Avg + 1;
                else
                    p.Manpower = GlobalVariables.GlobalRandom.Next(Min, Max + 1);
            }
        }


        /// <summary>
        /// Removes one development of each type if it is higher than 1.
        /// </summary>
        public static void RemoveAll()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.ClickedProvinces.Any())
            {
                if (TabsSeparate.form.ProvinceTaxNumeric.Value > 1)
                    TabsSeparate.form.ProvinceTaxNumeric.DownButton();
                if (TabsSeparate.form.ProvinceProductionNumeric.Value > 1)
                    TabsSeparate.form.ProvinceProductionNumeric.DownButton();
                if (TabsSeparate.form.ProvinceManpowerNumeric.Value > 1)
                    TabsSeparate.form.ProvinceManpowerNumeric.DownButton();
            }
        }
        /// <summary>
        /// Sets development to 1-6
        /// </summary>
        public static void RandomHighDev()
        {
            //TODO
            //Replace by average values
            
            if(GlobalVariables.ClickedProvinces.Any())
            {

                if (!GlobalVariables.Options.RandomProvinceCustom)
                {
                    foreach (Province p in GlobalVariables.Provinces)
                    {
                        int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (TaxRandom < 1)
                           p.Tax = 1;
                        else if (TaxRandom < 2)
                            p.Tax = 2;
                        else if (TaxRandom < 4)
                            p.Tax = 3;
                        else if (TaxRandom < 7)
                            p.Tax = 4;
                        else if (TaxRandom < 9)
                            p.Tax = 5;
                        else
                            p.Tax = 6;


                        int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (ProductionRandom < 1)
                            p.Production = 1;
                        else if (ProductionRandom < 2)
                            p.Production = 2;
                        else if (ProductionRandom < 4)
                            p.Production = 3;
                        else if (ProductionRandom < 7)
                            p.Production = 4;
                        else if (ProductionRandom < 9)
                            p.Production = 5;
                        else
                            p.Production = 6;


                        int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (ManpowerRandom < 1)
                            p.Manpower = 1;
                        else if (ManpowerRandom < 2)
                            p.Manpower = 2;
                        else if (ManpowerRandom < 4)
                            p.Manpower = 3;
                        else if (ManpowerRandom < 7)
                            p.Manpower = 4;
                        else if (ManpowerRandom < 9)
                            p.Manpower = 5;
                        else
                            p.Manpower = 6;
                    }
                }
                else
                {
                    RandomDev(GlobalVariables.Options.RandomProvinceHighMinimum, GlobalVariables.Options.RandomProvinceHighAverage, GlobalVariables.Options.RandomProvinceHighMaximum);

                }
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceTaxNumeric, GlobalVariables.ClickedProvinces.First().Tax);
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceProductionNumeric, GlobalVariables.ClickedProvinces.First().Production);
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceManpowerNumeric, GlobalVariables.ClickedProvinces.First().Manpower);
            }
        }
        /// <summary>
        /// Sets development to 1-4
        /// </summary>
        public static void RandomMedDev()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.ClickedProvinces.Any())
            {
                if (!GlobalVariables.Options.RandomProvinceCustom)
                {
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (TaxRandom < 2)
                            p.Tax = 1;
                        else if (TaxRandom < 6)
                            p.Tax = 2;
                        else if (TaxRandom < 9)
                            p.Tax = 3;
                        else
                            p.Tax = 4;


                        int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (ProductionRandom < 2)
                            p.Production = 1;
                        else if (ProductionRandom < 6)
                            p.Production = 2;
                        else if (ProductionRandom < 9)
                            p.Production = 3;
                        else
                            p.Production = 4;


                        int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (ManpowerRandom < 2)
                            p.Manpower = 1;
                        else if (ManpowerRandom < 6)
                            p.Manpower = 2;
                        else if (ManpowerRandom < 9)
                            p.Manpower = 3;
                        else
                            p.Manpower = 4;
                    }

                }
                else
                {
                    RandomDev(GlobalVariables.Options.RandomProvinceMediumMinimum, GlobalVariables.Options.RandomProvinceMediumAverage, GlobalVariables.Options.RandomProvinceMediumMaximum);
                }
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceTaxNumeric, GlobalVariables.ClickedProvinces.First().Tax);
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceProductionNumeric, GlobalVariables.ClickedProvinces.First().Production);
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceManpowerNumeric, GlobalVariables.ClickedProvinces.First().Manpower);
            }
        }
        /// <summary>
        /// Sets development to 1-3
        /// </summary>
        public static void RandomLowDev()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.ClickedProvinces.Any())
            {
                if (!GlobalVariables.Options.RandomProvinceCustom)
                {
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (TaxRandom < 5)
                            p.Tax = 1;
                        else if (TaxRandom < 9)
                            p.Tax = 2;
                        else
                            p.Tax = 3;


                        int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (ProductionRandom < 6)
                            p.Production = 1;
                        else if (ProductionRandom < 9)
                            p.Production = 2;
                        else
                            p.Production = 3;


                        int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (ManpowerRandom < 5)
                            p.Manpower = 1;
                        else if (ManpowerRandom < 9)
                            p.Manpower = 2;
                        else
                            p.Manpower = 3;
                    }
                }
                else
                {
                    RandomDev(GlobalVariables.Options.RandomProvinceLowMinimum, GlobalVariables.Options.RandomProvinceLowAverage, GlobalVariables.Options.RandomProvinceLowMaximum);
                }
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceTaxNumeric, GlobalVariables.ClickedProvinces.First().Tax);
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceProductionNumeric, GlobalVariables.ClickedProvinces.First().Production);
                ModEditor.TabsSeparateWindow.ChangeValueInternally(ModEditor.TabsSeparateWindow.ProvinceManpowerNumeric, GlobalVariables.ClickedProvinces.First().Manpower);
            }
        }
        /// <summary>
        /// Adds one development of each type
        /// </summary>
        public static void DevIncreaseAll()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.ClickedProvinces.Any())
            {
                TabsSeparate.form.ProvinceTaxNumeric.UpButton();
                TabsSeparate.form.ProvinceProductionNumeric.UpButton();
                TabsSeparate.form.ProvinceManpowerNumeric.UpButton();
            }
        }
        /// <summary>
        /// Sets all development to 0. Updates tradegooddev
        /// </summary>
        public static void ClearDev()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.ClickedProvinces.Any())
            {
                foreach (Province p in GlobalVariables.ClickedProvinces)
                {

                    if (p.TradeGood != null)
                    {
                        p.TradeGood.TotalDev -= p.Tax + p.Production + p.Manpower;
                    }
                    p.Tax = 0;
                    p.Production = 0;
                    p.Manpower = 0;
                    if (!GlobalVariables.ToUpdate.Contains(p))
                        GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvinces);
                }
                MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
            }
        }
    }
}
