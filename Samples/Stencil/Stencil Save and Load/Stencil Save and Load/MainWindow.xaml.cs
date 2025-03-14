using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace UserInteraction_Stencil
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

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
            if (selectedItem.Content.ToString() == "Icons Only")
            {
                stencil.SymbolsDisplayMode = Syncfusion.UI.Xaml.Diagram.SymbolsDisplayMode.IconsOnly;
            }
            else
            {
                stencil.SymbolsDisplayMode = Syncfusion.UI.Xaml.Diagram.SymbolsDisplayMode.NamesUnderIcons;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save XAML";

            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream s = File.Open(dialog.FileName, FileMode.OpenOrCreate))
                {
                    stencil.Save(s);
                }
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            stencil.SymbolGroups = new SymbolGroups();

            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                using (Stream myStream = dialog.OpenFile())
                {
                    stencil.Load(myStream, LoadSettings.IsChecked.Value);
                }
            }
        }
    }

    public class EnumDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString() == "")
                return string.Empty;

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo != null)
            {
                var attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributes.Length > 0)
                {
                    var attribute = attributes[0] as DisplayAttribute;
                    if (!String.IsNullOrEmpty(attribute.Name))
                        return attribute.Name;
                    else if (!String.IsNullOrEmpty(attribute.ShortName))
                        return attribute.ShortName;
                }
            }
            return Enum.GetName(value.GetType(), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
