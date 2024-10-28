using Syncfusion.UI.Xaml.HeatMap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Getting_Started.ViewModel
{
    public class SfHeatMapViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProductInfo> _itemSource;
        private ItemsMapping _itemsMapping;
        string[] productName = new string[]
        {
            "Vegie-spread",
            "Tofuaa",
            "Alice Mutton",
            "Konbu",
            "Fløtemysost",
            "Perth",
            "Boston",
            "Raclette",
            "Gorgonzola",
            "Chartreuse",
            "Tigers",
            "Tarte",
            "Queso",
            "Valkoinen",
            "Thüringer",
        };
        private object _selectedItem;
        private HeatMapCell _selectedValue;

        public SfHeatMapViewModel()
        {
            ItemSource = AddCellData();
            ItemsMapping = GetCellModel();
            
        }

        public ItemsMapping ItemsMapping
        {
            get
            {
                return _itemsMapping;
            }
            set
            {
                if (value != _itemsMapping)
                {
                    _itemsMapping = value;
                    OnPropertyChanged("ItemsMapping");
                }
            }
        }

        public ObservableCollection<ProductInfo> ItemSource
        {
            get
            {
                return _itemSource;
            }
            set
            {
                if (value != _itemSource)
                {
                    _itemSource = value;
                    OnPropertyChanged("ItemSource");
                }
            }
        }

        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public HeatMapCell SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                if (value != _selectedValue)
                {
                    _selectedValue = value;
                    OnPropertyChanged("SelectedValue");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<ProductInfo> AddCellData()
        {
            Random r = new Random();
            var dataFlat = new ObservableCollection<ProductInfo>();
            var datas = new Dictionary<string, List<int>>();
            for (int j = 0; j <= 5; j++)
            {
                var list = new List<int>();
                if (productName[j] == "Vegie-spread")
                {
                    list = new List<int>() { 37, 98, 11, 46, 95, 13 };
                }
                if (productName[j] == "Tofuaa")
                {
                    list = new List<int>() { 35, 76, 71, 3, 75, 6 };
                }
                if (productName[j] == "Alice Mutton")
                {
                    list = new List<int>() { 82, 83, 22, 54, 34, 63 };
                }
                if (productName[j] == "Konbu")
                {
                    list = new List<int>() { 59, 80, 62, 86, 80, 36 };
                }
                if (productName[j] == "Fløtemysost")
                {
                    list = new List<int>() { 15, 21, 71, 61, 19, 98 };
                }
                if (productName[j] == "Perth")
                {
                    list = new List<int>() { 14, 57, 25, 5, 17, 57 };
                }
                datas.Add(productName[j], list);
            }
            for (int j = 0; j <= 5; j++)
            {
                dataFlat.Add(new ProductInfo(productName[j], 2011, datas[productName[j]][0]));
                dataFlat.Add(new ProductInfo(productName[j], 2012, datas[productName[j]][1]));
                dataFlat.Add(new ProductInfo(productName[j], 2013, datas[productName[j]][2]));
                dataFlat.Add(new ProductInfo(productName[j], 2014, datas[productName[j]][3]));
                dataFlat.Add(new ProductInfo(productName[j], 2015, datas[productName[j]][4]));
                dataFlat.Add(new ProductInfo(productName[j], 2016, datas[productName[j]][5]));
            }

            return dataFlat;
        }

        private ItemsMapping GetCellModel()
        {
            CellMapping cell = new CellMapping();
            cell.Column = new ColumnMapping() { PropertyName = "ProductName", DisplayName = "Product Name" };
            cell.Row = new ColumnMapping() { PropertyName = "Year", DisplayName = "Year", };
            cell.Value = new ColumnMapping() { PropertyName = "Value" };
            return cell;
        }
    }

    public class ProductInfo: INotifyPropertyChanged
    {
        private string productName;
        private int year;
        private double _value;
        private bool isSelected;

        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                if (value != productName)
                {
                    productName = value;
                    OnPropertyChanged("ProductName");
                }
            }
        }

        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (value != year)
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public ProductInfo(string productname, int year, double value)
        {
            this.ProductName = productname;
            this.Year = year;
            this.Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
