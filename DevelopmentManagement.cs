using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class DevelopmentManagement
    {
        public static void RemoveAll()
        {
            //TODO
            //Replace by average values

            if (GlobalVariables.MultiProvinceMode)
            {
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        int change = 0;
                        if (p.Tax > 1)
                        {
                            change++;
                            p.Tax--;
                        }

                        if (p.Production > 1)
                        {
                            p.Production--;
                            change++;
                        }
                        if (p.Manpower > 1)
                        {
                            p.Manpower--;
                            change++;
                        }
                        if (p.TradeGood != null)
                        {
                            p.TradeGood.TotalDev -= change;
                        }
                        if (!GlobalVariables.ToUpdate.Contains(p))
                            GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
                    ModEditor.form.UpdateDevCount();
                    //Saving.SaveThingsToUpdate();
                }
            }
            else
            {
                if (ModEditor.form.ProvinceTaxNumeric.Value > 1)
                    ModEditor.form.ProvinceTaxNumeric.DownButton();
                if (ModEditor.form.ProvinceProductionNumeric.Value > 1)
                    ModEditor.form.ProvinceProductionNumeric.DownButton();
                if (ModEditor.form.ProvinceManpowerNumeric.Value > 1)
                    ModEditor.form.ProvinceManpowerNumeric.DownButton();
            }
        }
        public static void RandomHighDev()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.MultiProvinceMode)
            {
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                        if (p.TradeGood != null)
                        {
                            p.TradeGood.TotalDev -= p.Tax + p.Production + p.Manpower;
                        }
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
                        if (p.TradeGood != null)
                        {
                            p.TradeGood.TotalDev += p.Tax + p.Production + p.Manpower;
                        }
                        if (!GlobalVariables.ToUpdate.Contains(p))
                            GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
                    ModEditor.form.UpdateDevCount();
                    //Saving.SaveThingsToUpdate();
                }
            }
            else
            {               
                int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (TaxRandom < 1)
                    ModEditor.form.ProvinceTaxNumeric.Value = 1;
                else if (TaxRandom < 2)
                    ModEditor.form.ProvinceTaxNumeric.Value = 2;
                else if (TaxRandom < 4)
                    ModEditor.form.ProvinceTaxNumeric.Value = 3;
                else if (TaxRandom < 7)
                    ModEditor.form.ProvinceTaxNumeric.Value = 4;
                else if (TaxRandom < 9)
                    ModEditor.form.ProvinceTaxNumeric.Value = 5;
                else
                    ModEditor.form.ProvinceTaxNumeric.Value = 6;


                int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ProductionRandom < 1)
                    ModEditor.form.ProvinceProductionNumeric.Value = 1;
                else if (ProductionRandom < 2)
                    ModEditor.form.ProvinceProductionNumeric.Value = 2;
                else if (ProductionRandom < 4)
                    ModEditor.form.ProvinceProductionNumeric.Value = 3;
                else if (ProductionRandom < 7)
                    ModEditor.form.ProvinceProductionNumeric.Value = 4;
                else if (ProductionRandom < 9)
                    ModEditor.form.ProvinceProductionNumeric.Value = 5;
                else
                    ModEditor.form.ProvinceProductionNumeric.Value = 6;


                int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ManpowerRandom < 1)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 1;
                else if (ManpowerRandom < 2)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 2;
                else if (ManpowerRandom < 4)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 3;
                else if (ManpowerRandom < 7)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 4;
                else if (ManpowerRandom < 9)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 5;
                else
                    ModEditor.form.ProvinceManpowerNumeric.Value = 6;
            }
        }
        public static void RandomMedDev()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.MultiProvinceMode)
            {
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        if (p.TradeGood != null)
                        {
                            p.TradeGood.TotalDev -= p.Tax + p.Production + p.Manpower;
                        }
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
                        if (p.TradeGood != null)
                        {
                            p.TradeGood.TotalDev += p.Tax + p.Production + p.Manpower;
                        }
                        if (!GlobalVariables.ToUpdate.Contains(p))
                            GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
                    ModEditor.form.UpdateDevCount();
                    //Saving.SaveThingsToUpdate();
                }
            }
            else
            {
                int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (TaxRandom < 2)
                    ModEditor.form.ProvinceTaxNumeric.Value = 1;
                else if (TaxRandom < 6)
                    ModEditor.form.ProvinceTaxNumeric.Value = 2;
                else if (TaxRandom < 9)
                    ModEditor.form.ProvinceTaxNumeric.Value = 3;
                else
                    ModEditor.form.ProvinceTaxNumeric.Value = 4;


                int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ProductionRandom < 2)
                    ModEditor.form.ProvinceProductionNumeric.Value = 1;
                else if (ProductionRandom < 6)
                    ModEditor.form.ProvinceProductionNumeric.Value = 2;
                else if (ProductionRandom < 9)
                    ModEditor.form.ProvinceProductionNumeric.Value = 3;
                else
                    ModEditor.form.ProvinceProductionNumeric.Value = 4;


                int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ManpowerRandom < 2)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 1;
                else if (ManpowerRandom < 6)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 2;
                else if (ManpowerRandom < 9)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 3;
                else
                    ModEditor.form.ProvinceManpowerNumeric.Value = 4;
            }
        }
        public static void RandomLowDev()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.MultiProvinceMode)
            {
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        if (p.TradeGood != null)
                        {
                            p.TradeGood.TotalDev -= p.Tax + p.Production + p.Manpower;
                        }
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
                        if (p.TradeGood != null)
                        {
                            p.TradeGood.TotalDev += p.Tax + p.Production + p.Manpower;
                        }
                        if (!GlobalVariables.ToUpdate.Contains(p))
                            GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
                    ModEditor.form.UpdateDevCount();
                    //Saving.SaveThingsToUpdate();
                }
            }
            else
            {
                int TaxRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (TaxRandom < 5)
                    ModEditor.form.ProvinceTaxNumeric.Value = 1;
                else if (TaxRandom < 9)
                    ModEditor.form.ProvinceTaxNumeric.Value = 2;
                else
                    ModEditor.form.ProvinceTaxNumeric.Value = 3;


                int ProductionRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ProductionRandom < 6)
                    ModEditor.form.ProvinceProductionNumeric.Value = 1;
                else if (ProductionRandom < 9)
                    ModEditor.form.ProvinceProductionNumeric.Value = 2;
                else
                    ModEditor.form.ProvinceProductionNumeric.Value = 3;


                int ManpowerRandom = GlobalVariables.GlobalRandom.Next(0, 10);
                if (ManpowerRandom < 5)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 1;
                else if (ManpowerRandom < 9)
                    ModEditor.form.ProvinceManpowerNumeric.Value = 2;
                else
                    ModEditor.form.ProvinceManpowerNumeric.Value = 3;
            }
        }
        public static void DevIncreaseAll()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.MultiProvinceMode)
            {
                if (GlobalVariables.ClickedProvinces.Any())
                {
                    foreach (Province p in GlobalVariables.ClickedProvinces)
                    {
                        if (p.TradeGood != null)
                        {
                            p.TradeGood.TotalDev += 3;
                        }
                        p.Tax++;
                        p.Production++;
                        p.Manpower++;
                        if (!GlobalVariables.ToUpdate.Contains(p))
                            GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
                    ModEditor.form.UpdateDevCount();
                    //Saving.SaveThingsToUpdate();
                }
            }
            else
            {
                ModEditor.form.ProvinceTaxNumeric.UpButton();
                ModEditor.form.ProvinceProductionNumeric.UpButton();
                ModEditor.form.ProvinceManpowerNumeric.UpButton();
            }
        }
        public static void ClearDev()
        {
            //TODO
            //Replace by average values
            if (GlobalVariables.MultiProvinceMode)
            {
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
                            GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    }
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvinces, MapManagement.UpdateMapOptions.Development);
                    //Saving.SaveThingsToUpdate();
                }
            }
            else
            {
                if (GlobalVariables.ClickedProvince != null)
                {
                    GlobalVariables.ClickedProvince.Tax = 0;
                    GlobalVariables.ClickedProvince.Production = 0;
                    GlobalVariables.ClickedProvince.Manpower = 0;
                    GlobalVariables.ToUpdate.Add(GlobalVariables.ClickedProvince);
                    //Saving.SaveThingsToUpdate();
                    MapManagement.UpdateMap(GlobalVariables.ClickedProvince, MapManagement.UpdateMapOptions.Development);
                }
            }
        }
    }
}
