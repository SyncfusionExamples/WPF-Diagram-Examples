using Syncfusion.UI.Xaml.Diagram;
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

namespace Selector_Tooltip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Diagram.Nodes = new NodeCollection();

            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 100,
                OffsetY = 100,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
            };

            (Diagram.Nodes as NodeCollection).Add(node);

            (Diagram.SelectedItems as SelectorViewModel).SelectorConstraints = (Diagram.SelectedItems as SelectorViewModel).SelectorConstraints.Remove(SelectorConstraints.Rotator,SelectorConstraints.Resizer);
        }
    }
}
