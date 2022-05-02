using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
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

namespace Sfdiagram_Stencil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.PageSettings.PageWidth = 1000;
            diagram.PageSettings.PageHeight = 1000;
            diagram.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { };
            diagram.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Vertical };
            diagram.SnapSettings.SnapToObject = SnapToObject.All;
            diagram.SnapSettings.SnapConstraints = SnapConstraints.All;
        }

    }
    public class StencilVM : INotifyPropertyChanged
    {
        public StencilVM()
        {
            Symbolfilters = new SymbolFilters();

            SymbolFilterProvider node1 = new SymbolFilterProvider { Content = "Basic Shapes", IsChecked = true, SymbolFilter = Filter };
            SymbolFilterProvider node2 = new SymbolFilterProvider { Content = "Flow Shapes", IsChecked = true, SymbolFilter = Filter };
            SymbolFilterProvider node3 = new SymbolFilterProvider { Content = "Arrow Shapes", IsChecked = true, SymbolFilter = Filter };
            SymbolFilterProvider node4 = new SymbolFilterProvider { Content = "DataFlow Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node5 = new SymbolFilterProvider { Content = "UMLActivity Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node6 = new SymbolFilterProvider { Content = "UMLUseCase Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node7 = new SymbolFilterProvider { Content = "UMLRelationship Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node8 = new SymbolFilterProvider { Content = "Electrical Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node9 = new SymbolFilterProvider { Content = "Swimlane Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node10 = new SymbolFilterProvider { Content = "BPMNEditor Shapes", SymbolFilter = Filter };

            this.Symbolfilters.Add(node1);
            this.Symbolfilters.Add(node2);
            this.Symbolfilters.Add(node3);
            this.Symbolfilters.Add(node4);
            this.Symbolfilters.Add(node5);
            this.Symbolfilters.Add(node6);
            this.Symbolfilters.Add(node7);
            this.Symbolfilters.Add(node8);
            this.Symbolfilters.Add(node9);
            this.Symbolfilters.Add(node10);
            this.Selectedfilter = Symbolfilters[0];
        }

        // Define filtering of Symbols
        private bool Filter(SymbolFilterProvider sender, object symbol)
        {
            if (symbol is NodeViewModel && (symbol as NodeViewModel).ParentGroup==null)
            {
                if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
                    return true;
            }
            if (symbol is LaneViewModel)
            {
                if (sender.Content.ToString() == (symbol as LaneViewModel).Key.ToString())
                    return true;
            }
            if (symbol is PhaseViewModel)
            {
                if (sender.Content.ToString() == (symbol as PhaseViewModel).Key.ToString())
                    return true;
            }
            if (symbol is ConnectorViewModel)
            {
                if (sender.Content.ToString() == (symbol as ConnectorViewModel).Key.ToString())
                    return true;
            }
            if (sender.Content.ToString() == "All")
            {
                return true;
            }
            return false;
        }
        public ObservableCollection<SymbolFilterProvider> Symbolfilters { get; set; }

        public SymbolFilterProvider Selectedfilter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
