using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataGridCustomizationApp.Common;

namespace DataGridCustomizationApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DataGridViewModel _mainContent;

        public DataGridViewModel MainContent
        {
            get => _mainContent;
            set => Set(ref _mainContent, value);
        }

        public MainViewModel(DataGridViewModel content)
        {
            MainContent = content;
        }
    }

    public class DataGridViewModel : ViewModelBase
    {
        private ObservableCollection<ItemViewModel> _items;
        private string _searchText;
        private object _selectedColumn;

        public ObservableCollection<ItemViewModel> Items
        {
            get => _items;
            set => Set(ref _items, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => Set(ref _searchText, value);
        }

        public object SelectedColumn
        {
            get => _selectedColumn;
            set => Set(ref _selectedColumn, value);
        }

        public DataGridViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            var random = new Random();
            var count = 100;
            var items = new List<ItemViewModel>(count);
            for (int i = 0; i < count; i++)
            {
                var itemViewModel = new ItemViewModel
                {
                    Id = i + 1,
                    Name = $"item {i + 1}",
                    Value = random.Next(i + 23),
                };

                if (itemViewModel.Value % 3 == 0)
                    itemViewModel.ItemDecorationType = ItemDecorationType.C;
                else if (itemViewModel.Value % 2 == 0)
                    itemViewModel.ItemDecorationType = ItemDecorationType.B;
                else if (itemViewModel.Value == 0)
                    itemViewModel.ItemDecorationType = ItemDecorationType.A;

                items.Add(itemViewModel);
            }

            Items = new ObservableCollection<ItemViewModel>(items);
        }
    }

    public class ItemViewModel : ViewModelBase, IDecorationItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public ItemDecorationType ItemDecorationType { get; set; }
    }

    public interface IDecorationItem
    {
        ItemDecorationType ItemDecorationType { get; }
    }

    public enum ItemDecorationType
    {
        None = 0,
        A = 1,
        B = 2,
        C = 4
    }
}
