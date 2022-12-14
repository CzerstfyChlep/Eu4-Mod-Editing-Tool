using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Eu4ModEditor
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }


        public void UpdateValues()
        {
            AutoSavingEnabled.Checked = GlobalVariables.Options.Autosaving;

            AutosavingInterval.Value = GlobalVariables.Options.AutosavingInterval;
            AutosavingPath.Text = GlobalVariables.Options.AutosavingPath;

            SavingOnCrashEnabled.Checked = GlobalVariables.Options.SaveCrash;
            CrashSavingPath.Text = GlobalVariables.Options.SaveCrashPath;

            CustomRandomValues.Checked = GlobalVariables.Options.RandomProvinceCustom;

            LowMinimum.Value = GlobalVariables.Options.RandomProvinceLowMinimum;
            LowAverage.Value = GlobalVariables.Options.RandomProvinceLowAverage;
            LowMaximum.Value = GlobalVariables.Options.RandomProvinceLowMaximum;

            MediumMinimum.Value = GlobalVariables.Options.RandomProvinceMediumMinimum;
            MediumAverage.Value = GlobalVariables.Options.RandomProvinceMediumAverage;
            MediumMaximum.Value = GlobalVariables.Options.RandomProvinceMediumMaximum;

            HighMinimum.Value = GlobalVariables.Options.RandomProvinceHighMinimum;
            HighAverage.Value = GlobalVariables.Options.RandomProvinceHighAverage;
            HighMaximum.Value = GlobalVariables.Options.RandomProvinceHighMaximum;

            SetTheSameForAll.Checked = GlobalVariables.Options.SameValueForAllProvinces;
        }

        private void AutoSavingEnabled_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.Autosaving = AutoSavingEnabled.Checked;
        }

        private void AutosavingInterval_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.AutosavingInterval = (int)AutosavingInterval.Value;
        }

        private void AutosavingPath_TextChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.AutosavingPath = AutosavingPath.Text;
        }

        private void SetPathAutosaving_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                AutosavingPath.Text = ofd.SelectedPath;
            }
        }

        private void GotoPathAutosaving_Click(object sender, EventArgs e)
        {

            //TODO DOESN'T WORK
            Process.Start(AutosavingPath.Text);
        }

        private void SavingOnCrashEnabled_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.SaveCrash = SavingOnCrashEnabled.Checked;
        }

        private void CrashSavingPath_TextChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.SaveCrashPath = CrashSavingPath.Text;
        }

        private void CrashSavingPathSet_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CrashSavingPath.Text = ofd.SelectedPath;
            }
        }

        private void CrashSavingPathGoto_Click(object sender, EventArgs e)
        {
            Process.Start(CrashSavingPath.Text);
        }

        private void CustomRandomValues_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceCustom = CustomRandomValues.Checked;
        }

        private void LowMinimum_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceLowMinimum = (int)LowMinimum.Value;
            if (LowMaximum.Value < LowMinimum.Value)
                LowMaximum.Value = LowMinimum.Value;
        }

        private void LowAverage_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceLowAverage = (int)LowAverage.Value;
        }

        private void LowMaximum_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceLowMaximum = (int)LowMaximum.Value;
            if (LowMinimum.Value > LowMaximum.Value)
                LowMinimum.Value = LowMaximum.Value;
        }

        private void MediumMinimum_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceMediumMinimum = (int)MediumMinimum.Value;
            if (MediumMaximum.Value < MediumMinimum.Value)
                MediumMaximum.Value = MediumMinimum.Value;
        }

        private void MediumAverage_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceMediumAverage = (int)MediumAverage.Value;
        }

        private void MediumMaximum_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceMediumMaximum = (int)MediumMaximum.Value;
            if (MediumMinimum.Value > MediumMaximum.Value)
                MediumMinimum.Value = MediumMaximum.Value;
        }

        private void HighMinimum_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceHighMinimum = (int)HighMinimum.Value;
            if (HighMaximum.Value < HighMinimum.Value)
                HighMaximum.Value = HighMinimum.Value;
        }

        private void HighAverage_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceHighAverage = (int)HighAverage.Value;
        }

        private void HighMaximum_ValueChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.RandomProvinceHighMaximum = (int)HighMaximum.Value;
            if (HighMinimum.Value > HighMaximum.Value)
                HighMinimum.Value = HighMaximum.Value;
        }

        private void OnExitAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables.Options.AutosavingOnExit = OnExitAutoSave.Checked;
        }
    }
}
