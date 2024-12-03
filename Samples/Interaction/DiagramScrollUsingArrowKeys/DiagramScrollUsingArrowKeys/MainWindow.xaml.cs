using Syncfusion.UI.Xaml.Diagram.Theming;
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

namespace DiagramScrollUsingArrowKeys_462
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Diagram.Theme = new OfficeTheme();

            Diagram.ScrollSettings = new ScrollSettings()
            {
                ScrollLimit = Syncfusion.UI.Xaml.Diagram.ScrollLimit.Infinity
            };

            Diagram.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Horizontal };
            Diagram.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation= Orientation.Vertical };

            Diagram.PageSettings = new PageSettings()
            {
                ShowPageBreaks = true,
                MultiplePage = true,
            };

            Diagram.Nodes = new NodeCollection();

            NodeViewModel node = new NodeViewModel();
            node.OffsetX = 100;
            node.OffsetY = 100;
            node.UnitHeight = 100;
            node.UnitWidth = 100;
            node.Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) };

            NodeViewModel node1 = new NodeViewModel();
            node1.OffsetX = 10000;
            node1.OffsetY = 10000;
            node1.UnitHeight = 100;
            node1.UnitWidth = 100;
            node1.Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) };


            (Diagram.Nodes as NodeCollection).Add(node);
            (Diagram.Nodes as NodeCollection).Add(node1);

        }

        private void ScrollLimit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if(combobox.SelectedIndex == 0)
            {
                (Diagram.ScrollSettings as ScrollSettings).ScrollLimit = Syncfusion.UI.Xaml.Diagram.ScrollLimit.Infinity;
            }
            else if (combobox.SelectedIndex == 1)
            {
                (Diagram.ScrollSettings as ScrollSettings).ScrollLimit = Syncfusion.UI.Xaml.Diagram.ScrollLimit.Diagram;
            }
            else if (combobox.SelectedIndex == 2)
            {
                (Diagram.ScrollSettings as ScrollSettings).ScrollableArea = new Rect(0, 0, 5000, 5000);
                (Diagram.ScrollSettings as ScrollSettings).ScrollLimit = Syncfusion.UI.Xaml.Diagram.ScrollLimit.Limited;
            }
        }
    }
}
