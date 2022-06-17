using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
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

namespace HierarchicalTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Initialize the source and connectors collection
            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();
            //adding vertical and horizontal ruler.
            diagram.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Vertical };
            diagram.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Horizontal };

            //Creating nodes.
            NodeViewModel nodeA = new NodeViewModel()
            {
                ID = "A",
                UnitHeight = 100,
                UnitWidth = 100,
            };
            NodeViewModel nodeB = new NodeViewModel()
            {
                ID = "B",
                UnitHeight = 100,
                UnitWidth = 100,
            };
            NodeViewModel nodeC = new NodeViewModel()
            {
                ID = "C",
                UnitHeight = 100,
                UnitWidth = 100,
            };
            NodeViewModel nodeD = new NodeViewModel()
            {
                ID = "D",
                UnitHeight = 100,
                UnitWidth = 100,
            };
            NodeViewModel nodeE = new NodeViewModel()
            {
                ID = "E",
                UnitHeight = 100,
                UnitWidth = 100,
            };
            NodeViewModel nodeF = new NodeViewModel()
            {
                ID = "F",
                UnitHeight = 100,
                UnitWidth = 100,
            };
            NodeViewModel nodeG = new NodeViewModel()
            {
                ID = "G",
                UnitHeight = 100,
                UnitWidth = 100,
            };

            //Adding nodes into nodes collection.
            (diagram.Nodes as NodeCollection).Add(nodeA);
            (diagram.Nodes as NodeCollection).Add(nodeB);
            (diagram.Nodes as NodeCollection).Add(nodeC);
            (diagram.Nodes as NodeCollection).Add(nodeD);
            (diagram.Nodes as NodeCollection).Add(nodeE);
            (diagram.Nodes as NodeCollection).Add(nodeF);
            (diagram.Nodes as NodeCollection).Add(nodeG);

            //Creating connectors.
            ConnectorViewModel AB = new ConnectorViewModel()
            {
                SourceNodeID = "A",
                TargetNodeID = "B"
            };
            ConnectorViewModel AC = new ConnectorViewModel()
            {
                SourceNodeID = "A",
                TargetNodeID = "C"
            };
            ConnectorViewModel BD = new ConnectorViewModel()
            {
                SourceNodeID = "B",
                TargetNodeID = "D"
            };
            ConnectorViewModel BE = new ConnectorViewModel()
            {
                SourceNodeID = "B",
                TargetNodeID = "E"
            };
            ConnectorViewModel CF = new ConnectorViewModel()
            {
                SourceNodeID = "C",
                TargetNodeID = "F"
            };
            ConnectorViewModel CG = new ConnectorViewModel()
            {
                SourceNodeID = "C",
                TargetNodeID = "G"
            };

            //adding connectors into connector collection.
            (diagram.Connectors as ConnectorCollection).Add(AB);
            (diagram.Connectors as ConnectorCollection).Add(AC);
            (diagram.Connectors as ConnectorCollection).Add(BE);
            (diagram.Connectors as ConnectorCollection).Add(BD);
            (diagram.Connectors as ConnectorCollection).Add(CF);
            (diagram.Connectors as ConnectorCollection).Add(CG);

            //Initialize layout Manager to arrnage and position the nodes automatically.
            diagram.LayoutManager = new LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    HorizontalSpacing = 70,
                    VerticalSpacing = 50,
                    Orientation = TreeOrientation.TopToBottom,
                    Type = LayoutType.Hierarchical,
                    AvoidSegmentOverlapping = false,
                },
            };
        }
    }
}
