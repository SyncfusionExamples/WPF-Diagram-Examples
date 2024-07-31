using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace LoadSymbol
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
    }

    public class FloorPlanSymbolItem : ISymbol
    {
        public object Symbol
        {
            get;
            set;
        }

        public object Key { get; set; }

        public DataTemplate SymbolTemplate
        {
            get;
            set;
        }

        public string GroupName { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> SearchTags { get; set; }
        public DiagramMenu Menu { get; set; }

        public ISymbol Clone()
        {
            return new FloorPlanSymbolItem()
            {
                Symbol = this.Symbol,
                SymbolTemplate = this.SymbolTemplate
            };
        }
    }

    public class SymbolCollection : ObservableCollection<ISymbol>
    {

    }
}
