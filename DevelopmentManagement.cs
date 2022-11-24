using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class DevelopmentManagement
    {
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
                int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (TaxRandom < 1)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 1;
                else if (TaxRandom < 2)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 2;
                else if (TaxRandom < 4)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 3;
                else if (TaxRandom < 7)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 4;
                else if (TaxRandom < 9)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 5;
                else
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 6;


                int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ProductionRandom < 1)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 1;
                else if (ProductionRandom < 2)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 2;
                else if (ProductionRandom < 4)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 3;
                else if (ProductionRandom < 7)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 4;
                else if (ProductionRandom < 9)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 5;
                else
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 6;


                int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ManpowerRandom < 1)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 1;
                else if (ManpowerRandom < 2)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 2;
                else if (ManpowerRandom < 4)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 3;
                else if (ManpowerRandom < 7)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 4;
                else if (ManpowerRandom < 9)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 5;
                else
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 6;
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
                int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (TaxRandom < 2)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 1;
                else if (TaxRandom < 6)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 2;
                else if (TaxRandom < 9)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 3;
                else
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 4;


                int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ProductionRandom < 2)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 1;
                else if (ProductionRandom < 6)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 2;
                else if (ProductionRandom < 9)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 3;
                else
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 4;


                int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ManpowerRandom < 2)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 1;
                else if (ManpowerRandom < 6)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 2;
                else if (ManpowerRandom < 9)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 3;
                else
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 4;
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
                int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (TaxRandom < 5)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 1;
                else if (TaxRandom < 9)
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 2;
                else
                    TabsSeparate.form.ProvinceTaxNumeric.Value = 3;


                int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ProductionRandom < 6)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 1;
                else if (ProductionRandom < 9)
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 2;
                else
                    TabsSeparate.form.ProvinceProductionNumeric.Value = 3;


                int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ManpowerRandom < 5)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 1;
                else if (ManpowerRandom < 9)
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 2;
                else
                    TabsSeparate.form.ProvinceManpowerNumeric.Value = 3;
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
