using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    public partial class IdeaEditor : Form
    {
        public IdeaEditor()
        {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(IdeaSetTypes)))
            {
                IdeaTypeBox.Items.Add(item);
            }
            foreach (var item in Enum.GetValues(typeof(IdeaSetCategories)))
            {
                CategoryBox.Items.Add(item);
            }
            IdeaTypeBox.SelectedIndex = 0;

            Button b = new Button();
            b.Text = "Add";
            b.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            b.AutoSize = true;
            ModifiersFlowPanel.Controls.Add(b);
            b.Click += AddNewModifier;

            AddNewModifierButton = b;
            UpdateIdeaButtons();
            UpdateTriggerBox();
        }

        public IdeaSet selectedIdeas;

        public int SelectedIdeaSlot = -2;

        public Task CurrentTriggerUpdate;

        public Button AddNewModifierButton;

        enum IdeaSetTypes { National_Ideas = 0, Idea_Group }
        enum IdeaSetCategories { National = 0, Administrative, Diplomatic, Military }

        private void IdeaTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((IdeaSetTypes)IdeaTypeBox.SelectedItem == IdeaSetTypes.National_Ideas)
            {
                TraditionLabel.Enabled = true;
                TraditionLabel.BackColor = Color.FromArgb(255,255,192);
                IdeaSetBox.SelectedItem = null;
                IdeaSetBox.Items.Clear();
                IdeaSetBox.Items.AddRange(GlobalVariables.NationalIdeas.ToArray());

            }
            else
            {
                TraditionLabel.Enabled = false;
                TraditionLabel.BackColor = Color.Gray;
                IdeaSetBox.SelectedItem = null;
                IdeaSetBox.Items.Clear();
                IdeaSetBox.Items.AddRange(GlobalVariables.IdeaGroups.ToArray());
            }
            ModifiersFlowPanel.Controls.Clear();
            selectedIdeas = null;
            SelectedIdeaSlot = -2;
            UpdateIdeaButtons();
            UpdateTriggerBox();
        }

        public GroupBox CreateModifierBox(Modifier m)
        {
            GroupBox gb = new GroupBox();
            gb.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gb.AutoSize = true;
            gb.MaximumSize = new Size(100000, 35);
            gb.Tag = m;

            Button remove = new Button();
            gb.Tag = m;
            remove.AutoSize = true;
            remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            remove.Text = "x";
            remove.Click += RemoveModifierClick;
            remove.Location = new Point(205, 9);
            gb.Controls.Add(remove);

            ComboBox cb = new ComboBox();
            cb.Size = new Size(160, 21);
            foreach (var item in Enum.GetValues(typeof(CountryModifierType)))
            {
                cb.Items.Add(item);
            }
            cb.SelectedItem = Modifier.ConvertToCountry(m.Type);
            cb.Location = new Point(6,10);
            cb.SelectedIndexChanged += ChangeModifierType;
            gb.Controls.Add(cb);

            TextBox tb = new TextBox();
            tb.Size = new Size(30, 21);
            tb.Location = new Point(170, 10);
            tb.Text = m.Value.ToString();
            tb.TextChanged += ChangeModifierValue;
            gb.Controls.Add(tb);

            return gb;
        }

        public void ChangeModifierValue(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            Modifier m = (Modifier)t.Parent.Tag;
            object val;
            if (int.TryParse(t.Text, out int res))
            {
                //m.Value = Modifier.CheckValueOrReturnDefault(m.Type, res);
                val = res;
                m.Value = res;
            }
            else if (float.TryParse(t.Text, NumberStyles.Float,CultureInfo.InvariantCulture, out float f))
            {
                //m.Value = Modifier.CheckValueOrReturnDefault(m.Type, f);
                val = f;
                m.Value = f;
            }
            else if (t.Text.ToLower() == "yes")
            {
                //m.Value = Modifier.CheckValueOrReturnDefault(m.Type, true);
                val = true;
                m.Value = true;

            }
            else if (t.Text.ToLower() == "no")
            {
                //m.Value = Modifier.CheckValueOrReturnDefault(m.Type, false);
                val = false;
                m.Value = false;
            }
            else
            {
                //m.Value = Modifier.CheckValueOrReturnDefault(m.Type, res);
                val = t.Text;
                m.Value = t.Text;
            }

            int result = Modifier.IsCorrectType(m.Type, val);
            if (result == 2)
                t.BackColor = Color.White;
            else if (result == 1)
                t.BackColor = Color.Yellow;
            else
                t.BackColor = Color.Red;
        }

        public void AddNewModifier(object sender, EventArgs e)
        {
            if (selectedIdeas == null)
                return;
            ModifiersFlowPanel.Controls.Remove(AddNewModifierButton);
            Modifier m = new Modifier("", 0);
            m.Type = ModifierType.NoType;
            ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
            switch (SelectedIdeaSlot)
            {
                case -1:
                    (selectedIdeas as NationalIdeas).traditionModifiers.Add(m);
                    break;
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    selectedIdeas.ideaModifiers[SelectedIdeaSlot].Add(m);
                    break;
                case 7:
                    selectedIdeas.ambitionModifiers.Add(m);
                    break;
            }

            ModifiersFlowPanel.Controls.Add(AddNewModifierButton);
        }

        public void ChangeModifierType(object sender, EventArgs e)
        {
            Modifier m = (Modifier)(sender as Control).Parent.Tag;
            m.Type = Modifier.ConvertFromCountry((CountryModifierType)(sender as ComboBox).SelectedItem);           
        }

        public void RemoveModifierClick(object sender, EventArgs e)
        {
            Control parent = (sender as Control).Parent;
            parent.Parent.Controls.Remove(parent);
            Modifier m = (Modifier)parent.Tag;
            switch (SelectedIdeaSlot)
            {
                case -1:
                    selectedIdeas.ambitionModifiers.Remove(m);
                    break;
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    selectedIdeas.ideaModifiers[SelectedIdeaSlot].Remove(m);
                    break;
                case 7:
                    selectedIdeas.ambitionModifiers.Remove(m);
                    break;
            }
            
        }


        public void UpdateTriggerBox()
        {
            if(selectedIdeas == null)
            {
                TriggerBox.Text = "";
                TriggerBox.Enabled = false;
            }
            else
            {
                TriggerBox.Text = selectedIdeas.Trigger.ToString().Replace("\n", Environment.NewLine);
                TriggerBox.Enabled = true;
            }
        }

        public void UpdateIdeaSlotModifiers()
        {
            ModifiersFlowPanel.Controls.Clear();
            if (selectedIdeas == null)
                return;       
            switch (SelectedIdeaSlot)
            {
                case -1:
                    ModifiersBox.Text = "Modifiers - Tradition";
                    foreach(Modifier m in (selectedIdeas as NationalIdeas).traditionModifiers)
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
                case 0:
                    ModifiersBox.Text = "Modifiers - Idea 1";                 
                    foreach (Modifier m in selectedIdeas.ideaModifiers[0])
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
                case 1:
                    ModifiersBox.Text = "Modifiers - Idea 2";
                    foreach (Modifier m in selectedIdeas.ideaModifiers[1])
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
                case 2:
                    ModifiersBox.Text = "Modifiers - Idea 3";
                    foreach (Modifier m in selectedIdeas.ideaModifiers[2])
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
                case 3:
                    ModifiersBox.Text = "Modifiers - Idea 4";
                    foreach (Modifier m in selectedIdeas.ideaModifiers[3])
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
                case 4:
                    ModifiersBox.Text = "Modifiers - Idea 5";
                    foreach (Modifier m in selectedIdeas.ideaModifiers[4])
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
                case 5:
                    ModifiersBox.Text = "Modifiers - Idea 6";
                    foreach (Modifier m in selectedIdeas.ideaModifiers[5])
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
                case 6:
                    ModifiersBox.Text = "Modifiers - Idea 7";
                    foreach (Modifier m in selectedIdeas.ideaModifiers[6])
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
                case 7:
                    ModifiersBox.Text = "Modifiers - Ambition";
                    foreach (Modifier m in selectedIdeas.ambitionModifiers)
                    {
                        ModifiersFlowPanel.Controls.Add(CreateModifierBox(m));
                    }
                    break;
            }
            ModifiersFlowPanel.Controls.Add(AddNewModifierButton);
        }

        public void UpdateIdeaButtons()
        {
            if (selectedIdeas != null) {
                if (!selectedIdeas.ideaModifiers[0].Any())
                    Idea1Label.BackColor = Color.Red;
                else
                    Idea1Label.BackColor = Color.FromArgb(255,255,192);
                if (!selectedIdeas.ideaModifiers[1].Any())
                    Idea2Label.BackColor = Color.Red;
                else
                    Idea2Label.BackColor = Color.FromArgb(255, 255, 192);
                if (!selectedIdeas.ideaModifiers[2].Any())
                    Idea3Label.BackColor = Color.Red;
                else
                    Idea3Label.BackColor = Color.FromArgb(255, 255, 192);
                if (!selectedIdeas.ideaModifiers[3].Any())
                    Idea4Label.BackColor = Color.Red;
                else
                    Idea4Label.BackColor = Color.FromArgb(255, 255, 192);
                if (!selectedIdeas.ideaModifiers[4].Any())
                    Idea5Label.BackColor = Color.Red;
                else
                    Idea5Label.BackColor = Color.FromArgb(255, 255, 192);
                if (!selectedIdeas.ideaModifiers[5].Any())
                    Idea6Label.BackColor = Color.Red;
                else
                    Idea6Label.BackColor = Color.FromArgb(255, 255, 192);
                if (!selectedIdeas.ideaModifiers[6].Any())
                    Idea7Label.BackColor = Color.Red;
                else
                    Idea7Label.BackColor = Color.FromArgb(255, 255, 192);

                if (!selectedIdeas.ambitionModifiers.Any())
                    AmbitionLabel.BackColor = Color.Red;
                else
                    AmbitionLabel.BackColor = Color.FromArgb(255, 255, 192);

                if(selectedIdeas is NationalIdeas)
                {
                    if (!(selectedIdeas as NationalIdeas).traditionModifiers.Any())
                        TraditionLabel.BackColor = Color.Red;
                    else
                        TraditionLabel.BackColor = Color.FromArgb(255, 255, 192);
                }
                switch (SelectedIdeaSlot)
                {
                    case -1:
                        TraditionLabel.BackColor = Color.Azure;
                        break;
                    case 0:
                        Idea1Label.BackColor = Color.Azure;
                        break;
                    case 1:
                        Idea2Label.BackColor = Color.Azure;
                        break;
                    case 2:
                        Idea3Label.BackColor = Color.Azure;
                        break;
                    case 3:
                        Idea4Label.BackColor = Color.Azure;
                        break;
                    case 4:
                        Idea5Label.BackColor = Color.Azure;
                        break;
                    case 5:
                        Idea6Label.BackColor = Color.Azure;
                        break;
                    case 6:
                        Idea7Label.BackColor = Color.Azure;
                        break;
                    case 7:
                        AmbitionLabel.BackColor = Color.Azure;
                        break;
                }
            }
            else
            {
                TraditionLabel.BackColor = Color.Gray; Idea1Label.BackColor = Color.Gray;
                Idea2Label.BackColor = Color.Gray; Idea3Label.BackColor = Color.Gray;
                Idea4Label.BackColor = Color.Gray; Idea5Label.BackColor = Color.Gray;
                Idea6Label.BackColor = Color.Gray; Idea7Label.BackColor = Color.Gray;
                AmbitionLabel.BackColor = Color.Gray;
            }
        }



        private void IdeaSetBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIdeas = (IdeaSet)IdeaSetBox.SelectedItem;
            SelectedIdeaSlot = -2;
            UpdateIdeaButtons();
            UpdateTriggerBox();
        }

        private void AddNewIdeaSet_Click(object sender, EventArgs e)
        {
            if(GlobalVariables.IdeaGroups.Any(x=>x.setName.ToLower() == NewIdeaSetBox.Text.ToLower()) || GlobalVariables.NationalIdeas.Any(x => x.setName.ToLower() == NewIdeaSetBox.Text.ToLower()))
            {
                MessageBox.Show("Name already taken!");
                return;
            }
            IdeaSet set;

            if((IdeaSetCategories)CategoryBox.SelectedItem == IdeaSetCategories.National)
            {
                set = new NationalIdeas();
                GlobalVariables.NationalIdeas.Add(set);
            }
            else
            {
                set = new BasicIdeas();
                switch ((IdeaSetCategories)CategoryBox.SelectedItem)
                {
                    case IdeaSetCategories.Administrative:
                        (set as BasicIdeas).Type = "ADM";
                        break;
                    case IdeaSetCategories.Diplomatic:
                        (set as BasicIdeas).Type = "DIP";
                        break;
                    case IdeaSetCategories.Military:
                        (set as BasicIdeas).Type = "MIL";
                        break;
                }
                
            }
            set.setName = NewIdeaSetBox.Text;
            NewIdeaSetBox.Text = "";

        }

        private void TraditionLabel_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = -1;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
            
        }

        private void Idea1Label_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = 0;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
        }

        private void Idea2Label_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = 1;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
        }

        private void Idea3Label_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = 2;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
        }

        private void Idea4Label_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = 3;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
        }

        private void Idea5Label_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = 4;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
        }

        private void Idea6Label_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = 5;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
        }

        private void Idea7Label_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = 6;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
        }

        private void AmbitionLabel_Click(object sender, EventArgs e)
        {
            SelectedIdeaSlot = 7;
            UpdateIdeaSlotModifiers();
            UpdateIdeaButtons();
        }

        private void TriggerTextChanged(object sender, EventArgs e)
        {

        }

        private void AddCountryButton_Click(object sender, EventArgs e)
        {

        }

        private void ShowMatchingCountriesButton_Click(object sender, EventArgs e)
        {

        }
    }
}
