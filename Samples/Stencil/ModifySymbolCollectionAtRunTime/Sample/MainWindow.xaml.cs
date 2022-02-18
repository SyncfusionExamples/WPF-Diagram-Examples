using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace StencilDiagramElements
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
            if ((args as ItemAddedEventArgs).Item is NodeViewModel)
            {
                if (((args as ItemAddedEventArgs).Item as NodeViewModel).Name == "DiamondNode")
                {
                    ((args as ItemAddedEventArgs).Item as NodeViewModel).Shape = this.Resources["Diamond"];
                }

                if (((args as ItemAddedEventArgs).Item as NodeViewModel).Name == "TriangleNode")
                {
                    ((args as ItemAddedEventArgs).Item as NodeViewModel).Shape = this.Resources["Triangle"];
                }
            }
        }

        private void TextBelowImgae_Click(object sender, RoutedEventArgs e)
        {
            if (stencil.SymbolSource != null)
            {
                foreach (var item in stencil.SymbolSource as IEnumerable<object>)
                {
                    if (item is NodeViewModel && (item as NodeViewModel).Name == "DiamondNode")
                    {
                        (item as NodeViewModel).UnitWidth = 100;
                        (item as NodeViewModel).ContentTemplate = this.Resources["DiamondTextBelowImage"] as DataTemplate;
                    }

                    if (item is NodeViewModel && (item as NodeViewModel).Name == "TriangleNode")
                    {
                        (item as NodeViewModel).UnitWidth = 100;
                        (item as NodeViewModel).ContentTemplate = this.Resources["TriangleTextBelowImage"] as DataTemplate;
                    }
                }

                stencil.SymbolSource = new ObservableCollection<object>(stencil.SymbolSource as IEnumerable<object>);
            }
        }

        private void TextBesideImgae_Click(object sender, RoutedEventArgs e)
        {
            if (stencil.SymbolSource != null)
            {
                foreach (var item in stencil.SymbolSource as IEnumerable<object>)
                {
                    if (item is NodeViewModel && (item as NodeViewModel).Name == "DiamondNode")
                    {
                        (item as NodeViewModel).UnitWidth = 130;
                        (item as NodeViewModel).ContentTemplate = this.Resources["DiamondTextBesideImage"] as DataTemplate;
                    }

                    if (item is NodeViewModel && (item as NodeViewModel).Name == "TriangleNode")
                    {
                        (item as NodeViewModel).UnitWidth = 130;
                        (item as NodeViewModel).ContentTemplate = this.Resources["TriangleTextBesideImage"] as DataTemplate;
                    }
                }

                stencil.SymbolSource = new ObservableCollection<object>(stencil.SymbolSource as IEnumerable<object>);
            }
        }
    }

    //Collection of Symbols
    public class SymbolCollection : ObservableCollection<object>
    {

    }
}
