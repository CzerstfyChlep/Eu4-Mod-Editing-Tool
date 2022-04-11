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
    public partial class LanguageWindow : Form
    {
        public LanguageWindow()
        {
            InitializeComponent();
            PrimaryLanguage.SelectedIndex = 0;
            SecondaryLanguage.SelectedIndex = 0;
            this.TopMost = true;
        }

        private void GenerateWord_Click(object sender, EventArgs e)
        {

            int length = GlobalVariables.GlobalRandom.Next(4 + (3 * LengthBar.Value), 7 + (3 * LengthBar.Value));
            LanguageEngine.Language primary = (LanguageEngine.Language)PrimaryLanguage.SelectedIndex;
            LanguageEngine.Language secondary = (LanguageEngine.Language)(SecondaryLanguage.SelectedIndex - 1);
            
            if (SecondaryLanguage.SelectedIndex != 0)
                OutputBox.Text = LanguageEngine.GenerateAWord(GlobalVariables.GlobalRandom, new LanguageEngine.Language[] {
                    primary, secondary}, length);
            else
                OutputBox.Text = LanguageEngine.GenerateAWord(GlobalVariables.GlobalRandom, new LanguageEngine.Language[] {
                    primary}, length);

            if (AreaOption.Checked)            
                OutputBox.Text = OutputBox.Text.ToLower() + "_area";
            else if(RegionOption.Checked)
                OutputBox.Text = OutputBox.Text.ToLower() + "_region";
        }
    }
}
