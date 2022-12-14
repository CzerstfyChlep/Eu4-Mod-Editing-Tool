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
    public partial class DateWindow : Form
    {
        public DateWindow()
        {
            InitializeComponent();

            DayInput.Value = GlobalVariables.CurrentDate.Day;
            MonthInput.Value = GlobalVariables.CurrentDate.Month;
            YearInput.Value = GlobalVariables.CurrentDate.Year;

            PrevDayValue = (int)DayInput.Value;
            PrevMonthValue = (int)MonthInput.Value;
            PrevYearValue = (int)YearInput.Value;
            this.TopMost = true;
        }

        int PrevDayValue = 0;
        int PrevMonthValue = 0;
        int PrevYearValue = 0;

        private void DayInput_ValueChanged(object sender, EventArgs e)
        {
            decimal Diff = PrevDayValue - DayInput.Value;
            GlobalVariables.CurrentDate = GlobalVariables.CurrentDate.AddDays((int)-Diff);
           
            PrevDayValue = GlobalVariables.CurrentDate.Day;
            PrevMonthValue = GlobalVariables.CurrentDate.Month;
            PrevYearValue = GlobalVariables.CurrentDate.Year;

            DayInput.ValueChanged -= DayInput_ValueChanged;
            MonthInput.ValueChanged -= MonthInput_ValueChanged;
            YearInput.ValueChanged -= YearInput_ValueChanged;

            DayInput.Value = GlobalVariables.CurrentDate.Day;
            MonthInput.Value = GlobalVariables.CurrentDate.Month;
            YearInput.Value = GlobalVariables.CurrentDate.Year;

            DayInput.ValueChanged += DayInput_ValueChanged;
            MonthInput.ValueChanged += MonthInput_ValueChanged;
            YearInput.ValueChanged += YearInput_ValueChanged;
        }

        private void MonthInput_ValueChanged(object sender, EventArgs e)
        {
            decimal Diff = PrevMonthValue - MonthInput.Value;
            GlobalVariables.CurrentDate = GlobalVariables.CurrentDate.AddMonths((int)-Diff);

            PrevDayValue = GlobalVariables.CurrentDate.Day;
            PrevMonthValue = GlobalVariables.CurrentDate.Month;
            PrevYearValue = GlobalVariables.CurrentDate.Year;

            DayInput.ValueChanged -= DayInput_ValueChanged;
            MonthInput.ValueChanged -= MonthInput_ValueChanged;
            YearInput.ValueChanged -= YearInput_ValueChanged;

            DayInput.Value = GlobalVariables.CurrentDate.Day;
            MonthInput.Value = GlobalVariables.CurrentDate.Month;
            YearInput.Value = GlobalVariables.CurrentDate.Year;

            DayInput.ValueChanged += DayInput_ValueChanged;
            MonthInput.ValueChanged += MonthInput_ValueChanged;
            YearInput.ValueChanged += YearInput_ValueChanged;
        }

        private void YearInput_ValueChanged(object sender, EventArgs e)
        {
            decimal Diff = PrevYearValue - YearInput.Value;
            GlobalVariables.CurrentDate = GlobalVariables.CurrentDate.AddYears((int)-Diff);

            PrevDayValue = GlobalVariables.CurrentDate.Day;
            PrevMonthValue = GlobalVariables.CurrentDate.Month;
            PrevYearValue = GlobalVariables.CurrentDate.Year;

            DayInput.ValueChanged -= DayInput_ValueChanged;
            MonthInput.ValueChanged -= MonthInput_ValueChanged;
            YearInput.ValueChanged -= YearInput_ValueChanged;

            DayInput.Value = GlobalVariables.CurrentDate.Day;
            MonthInput.Value = GlobalVariables.CurrentDate.Month;
            YearInput.Value = GlobalVariables.CurrentDate.Year;

            DayInput.ValueChanged += DayInput_ValueChanged;
            MonthInput.ValueChanged += MonthInput_ValueChanged;
            YearInput.ValueChanged += YearInput_ValueChanged;
        }

        
    }
}
