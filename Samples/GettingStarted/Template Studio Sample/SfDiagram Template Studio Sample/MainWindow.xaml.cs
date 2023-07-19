using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
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

namespace SfDiagram_Template_Studio_Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool first = true;
        public MainWindow()
        {
            InitializeComponent();

            (Diagram.Info as IGraphInfo).ViewPortChangedEvent += MainWindow_ViewPortChangedEvent;

            stencil.SymbolFilters = new SymbolFilters();

            SymbolFilterProvider basicshapes = new SymbolFilterProvider { Content = "Basic Shapes", SymbolFilter = Filter ,IsChecked = true };
            SymbolFilterProvider flowshapes = new SymbolFilterProvider { Content = "Flow Shapes", SymbolFilter = Filter, IsChecked = true };
            SymbolFilterProvider dataflowshapes = new SymbolFilterProvider { Content = "DataFlow Shapes", SymbolFilter = Filter, IsChecked = true };
            SymbolFilterProvider arrowshapes = new SymbolFilterProvider { Content = "Arrow Shapes", SymbolFilter = Filter , IsChecked = true };
            
            stencil.SymbolFilters.Add(basicshapes);
            stencil.SymbolFilters.Add(flowshapes);
            stencil.SymbolFilters.Add(dataflowshapes);
            stencil.SymbolFilters.Add(arrowshapes);
            stencil.SelectedFilter = stencil.SymbolFilters[0];
        }

        private void MainWindow_ViewPortChangedEvent(object sender, ChangeEventArgs<object, ScrollChanged> args)
        {
            if (Diagram.Info != null && (args.Item as SfDiagram).IsLoaded == true && first && args.NewValue.ContentBounds != args.OldValue.ContentBounds)
            {
                (Diagram.Info as IGraphInfo).BringIntoCenter(args.NewValue.ContentBounds);
                first = false;
            }
        }

        private bool Filter(SymbolFilterProvider sender, object symbol)
        {
            if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
            {
                return true;
            }
            if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
            {
                return true;
            }
            if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
            {
                return true;
            }
            return false;
        }
    }
}
