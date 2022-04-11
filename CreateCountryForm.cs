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
    public partial class CreateCountryForm : Form
    {
        public string Name = "";
        public string Tag = "";
        public bool Canceled = false;
        public Color CountryColor = Color.White;
        public CreateCountryForm()
        {
            InitializeComponent();    
        }

        public void ChooseColor(object sender, EventArgs e)
        {
           
                    
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Canceled = true;
            this.Close();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            if(NameBox.Text == "" || TagBox.Text == "" || CountryColor == Color.White)
            {
                MessageBox.Show("You need to provide a name, tag and color!");
                return;
            }

            if (GlobalVariables.Countries.Any(x => x.FullName == NameBox.Text))
            {
                MessageBox.Show("Name already taken!");
                return;
            }

            if (GlobalVariables.Countries.Any(x => x.Tag == TagBox.Text))
            {
                MessageBox.Show("Tag already taken!");
                return;
            }

            if (GlobalVariables.Countries.Any(x => x.Color == CountryColor))
            {
                MessageBox.Show("Color already taken!");
                return;
            }

            Name = NameBox.Text;
            Tag = TagBox.Text;
            this.Close();
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                CountryColor = colorDialog1.Color;
                ColorButton.BackColor = CountryColor;
            }
        }
    }
}
