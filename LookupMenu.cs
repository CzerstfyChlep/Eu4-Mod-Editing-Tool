using System;
using System.Collections;
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
    public partial class LookupMenu : Form
    {
        ListViewColumnSorter sorter;
        public LookupMenu()
        {
            InitializeComponent();
            Shown += ShownHandler;
            TopMost = true;
            MainListView.ItemActivate += ItemClick;
            MainListView.ColumnClick += ColumnClick;
            sorter = new ListViewColumnSorter();
            MainListView.ListViewItemSorter = sorter;
            MainListView.Sort();
        }

        public void ColumnClick(object Sender, EventArgs e)
        {
            if (sorter.OrderOfSort == SortOrder.Ascending)
                sorter.OrderOfSort = SortOrder.Descending;
            else
                sorter.OrderOfSort = SortOrder.Ascending;
            MainListView.Sort();
        }

        public void ItemClick(object Sender, EventArgs e)
        {
            if(MainListView.SelectedItems.Count > 0)
            {
                ChosenObject = MainListView.SelectedItems[0].Text;
                Close();
            }
        }

        public void ShownHandler(object Sender, EventArgs e)
        {
            DisplayedList = new List<string>(ObjectsList);
            UpdateList();
        }

        public void InitializeArray<T>(List<T> list, string Title, string ColumnTitle)
        {
            ObjectsList = new List<string>();
            DisplayedList = new List<string>();
            int index = 0;
            foreach (object obj in list)
            {
                ObjectsList.Add(obj.ToString());
                index++;
            }
            this.Text = Title;
            MainListView.Columns[0].Text = ColumnTitle;
            MainListView.Sort();
        }

        string ChosenObject;

        List<string> ObjectsList;
        List<string> DisplayedList;

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FilterTextbox_TextChanged(object sender, EventArgs e)
        {
            List<string> prevDisplay = new List<string>(DisplayedList);
            DisplayedList.Clear();
            if(FilterTextbox.Text.Length > 0 )
            {
                for (int a = 0; a < ObjectsList.Count; a++)
                {
                    if (AdditionalElements.StringSimilarity(ObjectsList[a].ToLower(), FilterTextbox.Text.ToLower()) > 75)
                    {
                        DisplayedList.Add(ObjectsList[a]);
                    }
                }
            }
            else
            {
                DisplayedList = new List<string>(ObjectsList);
            }

            if (!DisplayedList.SequenceEqual(prevDisplay))
            {
                UpdateList();
            }
        }

        public void UpdateList()
        {
            MainListView.Items.Clear();
            foreach(string obj in DisplayedList)
            {
                if(obj.Length > 0)
                    MainListView.Items.Add(obj);
            }
            MainListView.Sort();
        }

        public string GetChosenObject() 
        {
            return ChosenObject;
        }
    }

    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        public int ColumnToSort;

        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        public SortOrder OrderOfSort = SortOrder.Ascending;

        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        public CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// Class constructor. Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.Ascending;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }
    }

}
