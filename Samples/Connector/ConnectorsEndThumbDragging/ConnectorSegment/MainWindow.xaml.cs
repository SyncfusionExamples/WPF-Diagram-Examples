using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
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

namespace ConnectorSegment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            diagram.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.All,
            };
            diagram.HorizontalRuler = new Ruler();
            diagram.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            diagram.Constraints |= GraphConstraints.Undoable | GraphConstraints.Zoomable;

            (diagram.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;

            diagram.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = Orientation.Horizontal,
            };

            diagram.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = Orientation.Vertical,
            };

            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();


            ////Create nodeviewmodel
            NodeViewModel node1 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 500,
                OffsetY = 300,
                Shape = this.Resources["Rectangle"],
            };

           // node1.Constraints = NodeConstraints.Default & ~NodeConstraints.InheritDraggable & ~NodeConstraints.Draggable;
         

            ////Add Node to Nodes property of the Diagram
            (diagram.Nodes as NodeCollection).Add(node1);
            //(diagram.Nodes as NodeCollection).Add(node2);

            ConnectorViewModel c1 = new ConnectorViewModel()
            {
                SourcePoint = new Point(100,100),
                TargetPoint = new Point(200,200),
                //SourceNode = node1,
               // TargetNode = node2,
                //SourceConnector = dockport1,
                //TargetConnector = dockport2,
                //SourcePort = connectorport1,
                //TargetPort = connectorport2,
            };

            c1.Constraints = ConnectorConstraints.Default | ConnectorConstraints.Draggable;

            ConnectorViewModel lineCOnnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(300, 300),
                TargetPoint = new Point(400, 300),
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    //Specify the segment as straight segment
                    new StraightSegment()
                }
            };
            lineCOnnector.Constraints = ConnectorConstraints.Default | ConnectorConstraints.Draggable;

            c1.Constraints = ConnectorConstraints.Default | ConnectorConstraints.Draggable;
            ////Adding the connector into Collection
            (diagram.Connectors as ConnectorCollection).Add(c1);
            (diagram.Connectors as ConnectorCollection).Add(lineCOnnector);
        }

        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.Item is IConnector)
            {

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            diagram.PreviewSettings = new PreviewSettings()
            {
                PreviewMode = PreviewMode.Preview,
            };

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            diagram.PreviewSettings = new PreviewSettings()
            {
                PreviewMode = PreviewMode.Preview,
            };

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            diagram.Tool = Tool.ContinuesDraw;
            diagram.DefaultConnectorType = ConnectorType.Line;
            diagram.DrawingTool = DrawingTool.Connector;
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            diagram.Tool = Tool.MultipleSelect;
            diagram.DefaultConnectorType = ConnectorType.Orthogonal;
            diagram.DrawingTool = DrawingTool.None;
        }
    }
}

