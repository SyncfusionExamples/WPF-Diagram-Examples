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

namespace SnappingForNode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();

            NodeViewModel node1 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 300,
                OffsetY = 60,
                Shape = this.Resources["Rectangle"],

            };
            ConnectorViewModel Connector1 = new ConnectorViewModel()
            {
                SourcePoint = new Point(100, 100),
                TargetPoint = new Point(200, 200),
            };

           
            (diagram.Connectors as ConnectorCollection).Add(Connector1);
            (diagram.Nodes as NodeCollection).Add(node1);

            diagram.SnapSettings = new SnapSettings()
            {
                SnapToObject = SnapToObject.All & ~SnapToObject.Segment,
            };
        }
    }
}
