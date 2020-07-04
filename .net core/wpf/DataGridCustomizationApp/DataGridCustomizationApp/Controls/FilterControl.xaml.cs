using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DataGridCustomizationApp.Controls
{
    /// <summary>
    /// Interaction logic for FilterControl.xaml
    /// </summary>
    public partial class FilterControl
    {
        public static readonly DependencyProperty DataGridProperty = DependencyProperty.Register("DataGrid",
            typeof(DataGrid),
            typeof(FilterControl),
            new PropertyMetadata(default(DataGrid), DataGridPropertyChangedCallback));

        private static void DataGridPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        public static readonly DependencyProperty SelectedColumnProperty = DependencyProperty.Register(
            "SelectedColumn", typeof(string), typeof(FilterControl), new PropertyMetadata(default(string), SelectedColumnPropertyChangedCallback));

        public static readonly DependencyProperty SelectedOrderDirectionProperty = DependencyProperty.Register("SelectedOrderDirection", typeof(OrderDirection), typeof(FilterControl), new PropertyMetadata(default(OrderDirection), SelectedOrderDirectionPropertyChangedCallback));

        private static void SelectedOrderDirectionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var filterControl = d as FilterControl;
            filterControl?.Filter(filterControl.SelectedColumn, e.NewValue);
        }

        private static void SelectedColumnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var filterControl = d as FilterControl;
            filterControl?.Filter(e.NewValue, filterControl.SelectedOrderDirection);
        }

        public string SelectedColumn
        {
            get { return (string) GetValue(SelectedColumnProperty); }
            set { SetValue(SelectedColumnProperty, value); }
        }

        public DataGrid DataGrid
        {
            get { return (DataGrid) GetValue(DataGridProperty); }
            set { SetValue(DataGridProperty, value); }
        }

       OrderDirection SelectedOrderDirection
        {
            get { return (OrderDirection) GetValue(SelectedOrderDirectionProperty); }
            set { SetValue(SelectedOrderDirectionProperty, value); }
        }

        public FilterControl()
        {
            InitializeComponent();
        }

        private void Filter(object columnMemberPath, object order)
        {
            DataGrid.Items.SortDescriptions.Clear();

            try
            {
                var propertyName = columnMemberPath?.ToString();
                if (string.IsNullOrEmpty(propertyName)) return;

                var orderDirection = order as OrderDirection?;
                if (!orderDirection.HasValue) return;

                if (orderDirection == OrderDirection.None) return;

                var listSortDirection = orderDirection == OrderDirection.Asc ? ListSortDirection.Ascending : ListSortDirection.Descending;

                DataGrid.Items.SortDescriptions.Add(new SortDescription(propertyName, listSortDirection));

                foreach (DataGridColumn c in DataGrid.Columns)
                {
                    c.SortDirection = null;

                    if (c.SortMemberPath == propertyName)
                        c.SortDirection = listSortDirection;
                }
            }
            finally
            {
                DataGrid.Items.Refresh();
            }
        }
    }

    public enum OrderDirection
    {
        None,
        Asc,
        Desc
    }
}
