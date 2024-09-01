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

namespace StraightSegmentSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //adding rulers
            Diagram.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            Diagram.HorizontalRuler = new Ruler() { Orientation = Orientation.Horizontal };
            //adding snap lines
            Diagram.SnapSettings = new SnapSettings() { SnapConstraints = SnapConstraints.All };
            //adding selector changed event.
            (Diagram.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;
            (Diagram.Info as IGraphInfo).SelectorChangedEvent += MainWindow_SelectorChangedEvent;
            (Diagram.Info as IGraphInfo).ObjectDrawn += MainWindow_ObjectDrawn;
            //creating node and connector collection
            Diagram.Nodes = new NodeCollection();
            Diagram.Connectors = new ConnectorCollection();

            //Creating and adding nodes 
            NodeViewModel node1 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 200,
                OffsetY = 200,
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "node1"
                    }
                },
            };

            (Diagram.Nodes as NodeCollection).Add(node1);

            NodeViewModel node2 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 600,
                OffsetY = 400,
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "node2"
                    }
                },
            };

            (Diagram.Nodes as NodeCollection).Add(node2);

            //Create and add connectors
            ConnectorViewModel conn1 = new ConnectorViewModel()
            {
                SourceNode = node1,
                TargetNode = node2,
                SegmentDecorators = new ObservableCollection<ISegmentDecorator>()
                {
                    //Define the SegmentDecorator
                    new SegmentDecorator()
                    {
                        //Define the Shape of segment decorator
                        Shape = Application.Current.MainWindow.Resources["Triangle"] as object,
                        //Defines the positon of the decorator on connector
                        Length = 0.4
                    }
                },
            };

            (Diagram.Connectors as ConnectorCollection).Add(conn1);
        }

        //Object drawn event.
        private void MainWindow_ObjectDrawn(object sender, ObjectDrawnEventArgs args)
        {
            if (args.State == DragState.Completed && args.Item is IConnector)
            {
                var connector = args.Item as ConnectorViewModel;
                //finding connector length
                double connectorLength = FindConnectorLength(connector);
                if (connectorLength < 51)
                {
                    //Deleing the newly drawn connector when it is not reached minimum length.
                    (Diagram.Info as IGraphInfo).Commands.Delete.Execute(null);
                }
            }
        }

        //Selector changed event.
        private void MainWindow_SelectorChangedEvent(object sender, SelectorChangedEventArgs args)
        {
            if (args.NewValue.InteractionState == NodeChangedInteractionState.Dragging)
            {
                if (args.NewValue.OffsetX != args.OldValue.OffsetX || args.NewValue.OffsetY != args.OldValue.OffsetY)
                {
                    var draggingNode = ((args.Item as SelectorViewModel).Nodes as IEnumerable<object>).FirstOrDefault() as NodeViewModel;
                    if (draggingNode != null)
                    {
                        //dependent connector of dragged node
                        var connector = (draggingNode.Info as INodeInfo).InOutConnectors.FirstOrDefault();
                        if (connector != null)
                        {
                            //finding connector length
                            double connectorLength = FindConnectorLength(connector as ConnectorViewModel);
                            var diffX = draggingNode.OffsetX - args.OldValue.OffsetX;
                            var diffY = draggingNode.OffsetY - args.OldValue.OffsetY;
                            diffX = Math.Abs(diffX);
                            diffY = Math.Abs(diffY);
                            //resettig node position to initial value when its dependent connector legnth is less then 50.
                            if (connectorLength + diffX <= 51 || connectorLength + diffY <= 51)
                            {
                                draggingNode.OffsetX = args.OldValue.OffsetX;
                                draggingNode.OffsetY = args.OldValue.OffsetY;
                            }
                        }
                    }
                }
            }
        }

        //Item added event.
        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.Item is ConnectorViewModel && args.ItemSource == ItemSource.DrawingTool)
            {
                var connector = args.Item as ConnectorViewModel;
                if (connector.SegmentDecorators is null)
                {
                    //Creating segment decorator to the newly added connector.
                    connector.SegmentDecorators = new ObservableCollection<ISegmentDecorator>()
                    {
                        //Define the SegmentDecorator
                        new SegmentDecorator()
                        {
                            //Define the Shape of segment decorator
                            Shape = Application.Current.MainWindow.Resources["Triangle"] as object,
                            //Defines the positon of the decorator on connector
                            Length = 0.5
                        }
                    };
                }
            }
        }

        //To find and return connector length
        private double FindConnectorLength(ConnectorViewModel connector)
        {
            List<Point> points = new List<Point>();
            //Getting segments as points of collection
            IEnumerable<Point> segmentpoints = (connector.Info as IConnectorInfo).ToPoints(true);
            foreach (var point in segmentpoints)
            {
                points.Add(point);
            }
            double connectorLength = 0.0;
            Point start = points[0];
            foreach (Point i in points)
            {
                connectorLength += start.FindLength(i);
                start = i;
            }
            return connectorLength;
        }
    }

    /// <summary>
    /// To find lenght of the for provided list of points
    /// </summary>
    internal static class PointExtensions
    {
        public static double FindLength(this Point s, Point e)
        {
            double length = Math.Sqrt(Math.Pow((s.X - e.X), 2) + Math.Pow((s.Y - e.Y), 2));
            return Math.Round(length, 4);
        }
    }
}
