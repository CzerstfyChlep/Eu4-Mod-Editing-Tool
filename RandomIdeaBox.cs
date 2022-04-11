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
    public partial class RandomIdeaBox : Form
    {
        public RandomIdeaBox()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void RandomIdeaButton_Click(object sender, EventArgs e)
        {
            OutputBox.Text = GlobalVariables.CountryModifiers[GlobalVariables.GlobalRandom.Next(0, GlobalVariables.CountryModifiers.Count)];
        }
    }
}
