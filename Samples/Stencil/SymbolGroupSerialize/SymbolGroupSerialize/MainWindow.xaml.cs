using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.UI.Xaml.Diagram.Stencil.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StencilCategorySample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (diagram.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;
        }

        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.Item is NodeViewModel)
            {
                NodeViewModel symbol = args.Item as NodeViewModel;
                if (symbol.Name == "single Sofa")
                {
                    symbol.ContentTemplate = (DataTemplate)App.Current.Resources["singleSofa"];
                }
                if (symbol.Name == "mat")
                {
                    symbol.ContentTemplate = (DataTemplate)App.Current.Resources["mat"];
                }
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save XAML";

            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream s = File.Open(dialog.FileName, FileMode.OpenOrCreate))
                {
                    stencil1.ExportGroup(s, Textbox.Text);
                }
            }

        }

        private void Load(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                using (Stream myStream = dialog.OpenFile())
                {
                    stencil2.ImportGroup(myStream);
                }
            }

            // Add data template for custom shapes
            var symbolCollection = (stencil2.SymbolSource as System.Collections.ObjectModel.ObservableCollection<object>);
            foreach (NodeViewModel symbol in symbolCollection.Where(symbol => symbol is NodeViewModel))
            {
                if (symbol.Name == "single Sofa")
                {
                    symbol.ContentTemplate = (DataTemplate)App.Current.Resources["singleSofa"];
                }
                if (symbol.Name == "mat")
                {
                    symbol.ContentTemplate = (DataTemplate)App.Current.Resources["mat"];
                }
                if (symbol.Key == "Image")
                {

                }
            }
            foreach (SymbolViewModel symbol in symbolCollection.Where(symbol => symbol is SymbolViewModel))
            {
                if (symbol.Key.ToString() == "Image" && symbol.Symbol.ToString() == "User")
                {
                    symbol.SymbolTemplate = (DataTemplate)App.Current.Resources["symboltemplate"];
                }
            }
        }

        private void SymbolGroupDisplayMode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (diagram == null) return;
            switch (SymbolGroupDisplayMode.SelectedIndex)
            {
                case 0:
                    stencil1.SymbolGroupDisplayMode = Syncfusion.UI.Xaml.Diagram.SymbolGroupDisplayMode.Accordion;
                    break;
                case 1:
                    stencil1.SymbolGroupDisplayMode = Syncfusion.UI.Xaml.Diagram.SymbolGroupDisplayMode.Tab;
                    break;
                case 2:
                    stencil1.SymbolGroupDisplayMode = Syncfusion.UI.Xaml.Diagram.SymbolGroupDisplayMode.List;
                    break;

            }
        }
        private void SymbolGroupDisplayMode2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (diagram == null) return;
            switch (SymbolGroupDisplayMode2.SelectedIndex)
            {
                case 0:
                    stencil2.SymbolGroupDisplayMode = Syncfusion.UI.Xaml.Diagram.SymbolGroupDisplayMode.Accordion;
                    break;
                case 1:
                    stencil2.SymbolGroupDisplayMode = Syncfusion.UI.Xaml.Diagram.SymbolGroupDisplayMode.Tab;
                    break;
                case 2:
                    stencil2.SymbolGroupDisplayMode = Syncfusion.UI.Xaml.Diagram.SymbolGroupDisplayMode.List;
                    break;

            }
        }

        private void ExpandMode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (diagram == null) return;
            switch (ExpandMode.SelectedIndex)
            {
                case 0:
                    stencil1.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.All;
                    break;
                case 1:
                    stencil1.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.One;
                    break;
                case 2:
                    stencil1.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.OneOrMore;
                    break;
                case 3:
                    stencil1.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.ZeroOrMore;
                    break;
                case 4:
                    stencil1.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.ZeroOrOne;
                    break;
            }
        }

        private void ExpandMode2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (diagram == null) return;
            switch (ExpandMode2.SelectedIndex)
            {
                case 0:
                    stencil2.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.All;
                    break;
                case 1:
                    stencil2.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.One;
                    break;
                case 2:
                    stencil2.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.OneOrMore;
                    break;
                case 3:
                    stencil2.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.ZeroOrMore;
                    break;
                case 4:
                    stencil2.ExpandMode = Syncfusion.UI.Xaml.Diagram.ExpandMode.ZeroOrOne;
                    break;
            }
        }
    }
}
