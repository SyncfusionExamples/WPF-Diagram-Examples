using Getting_Started.ViewModel;
using System.Globalization;
using System;
using System.Windows;
using System.Windows.Data;
using Syncfusion.UI.Xaml.HeatMap;
using System.Xml.Linq;
using System.Reflection;

namespace Getting_Started
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            heatmap.ColorMappingCollection = App.Current.MainWindow.Resources["ColorMapping2"];
            
            Legend.ColorMappingCollection = App.Current.MainWindow.Resources["ColorMapping2"];
           Legend.InvalidateMeasure();
        }
    }

    public class ObjectToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HeatMapCell)
            {
                HeatMapCell cell = value as HeatMapCell;
                Type type = cell.GetType();
                FieldInfo fieldInfo = type.GetField("info", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    CellInfo info = (CellInfo)fieldInfo.GetValue(cell);
                    if (info != null && info.Source is ProductInfo)
                    {
                        return (info.Source as ProductInfo).Value;
                    }
                }
            }

            return "-----";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
