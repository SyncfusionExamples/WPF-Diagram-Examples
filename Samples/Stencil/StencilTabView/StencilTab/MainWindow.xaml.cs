using StencilCategory.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace StencilCategory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //stencil.HeaderMenu = new DiagramMenu();
            //stencil.HeaderMenu.MenuItems = new ObservableCollection<DiagramMenuItem>();
            //DiagramMenuItem mi = new DiagramMenuItem()
            //{
            //    Content = "Delete",
            //};
            //(stencil.HeaderMenu.MenuItems as ICollection<DiagramMenuItem>).Add(mi);

            stencil.MenuItemClicked += Stencil_MenuItemClicked;
            //stencil.HeaderVisibility = Visibility.Collapsed;
        }

        private void Stencil_MenuItemClicked(object sender, MenuItemClickedEventArgs args)
        {
            if (args.Item.Content.Equals("Delete"))
            {
                if (args.Source is SymbolGroup)
                {
                    var group = args.Source as SymbolGroup;
                    foreach (var item in group.ItemsSource)
                    {
                        (stencil.SymbolSource as ObservableCollection<object>).Remove(item);
                    }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (stencil.DataContext is StencilViewModel)
            {
                var vm = stencil.DataContext as StencilViewModel;
                stencil.SymbolFilters = vm.Symbolfilters;
                stencil.SelectedFilter = vm.Symbolfilters[0];
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (stencil.DataContext is StencilViewModel)
            {
                var vm = stencil.DataContext as StencilViewModel;
                stencil.SymbolFilters = null;
                stencil.SelectedFilter = null;
            }
        }
    }

    public class StringToTemplateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            if (str != null)
            {
                if (str == "Basic Shapes") return App.Current.MainWindow.Resources["basicheader"] as DataTemplate;
                else if (str == "Flow Shapes") return App.Current.MainWindow.Resources["flowheader"] as DataTemplate;
                else if (str == "Arrow Shapes") return App.Current.MainWindow.Resources["arrowheader"] as DataTemplate;
                else if (str == "UMLUseCase Shapes") return App.Current.MainWindow.Resources["umlheader"] as DataTemplate;
                else if (str == "Swimlane Shapes") return App.Current.MainWindow.Resources["swimlaneheader"] as DataTemplate;
                else return App.Current.MainWindow.Resources["flowheader"] as DataTemplate;
            }
            
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            if (str != null)
            {
                if (str == "Basic Shapes") return Visibility.Visible;
                else if (str == "Flow Shapes") return Visibility.Visible;
                else if (str == "Arrow Shapes") return Visibility.Visible;
                else if (str == "UMLActivity Shapes") return Visibility.Visible;
                else if (str == "UMLUseCase Shapes") return Visibility.Visible;
                else if (str == "UMLRelationship Shapes") return Visibility.Visible;

                return Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
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
