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
    public partial class CompareOriginalEdited : Form
    {
        public TabsSeparate.CompareResult Result;
        public CompareOriginalEdited(string Original, string Edited, TabsSeparate.CompareResult result)
        {
            
            InitializeComponent();
            this.TopMost = true;
            OriginalText.Text = Original;

            EditedText.Text = Edited;
            Result = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result.SaveEdited = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result.SaveEdited = true;
            Result.EditedText = EditedText.Text;
            this.Close();
        }
    }
}
