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
            //(Diagram.Info as IGraphInfo).SelectorChangedEvent += MainWindow_SelectorChangedEvent;
            (Diagram.Info as IGraphInfo).NodeChangedEvent += MainWindow_NodeChangedEvent;
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
                        Length = 0.5
                    }
                },
            };


            (Diagram.Connectors as ConnectorCollection).Add(conn1);
        }

        //Node changed event to restrict segment decorator hiding and resetting the rotate angle to nodes based on segment decorator angle.
        private void MainWindow_NodeChangedEvent(object sender, ChangeEventArgs<object, NodeChangedEventArgs> args)
        {
            //at node drag completed state.
            if (args.NewValue.InteractionState == NodeChangedInteractionState.Dragged)
            {
                var draggedNode = args.Item as NodeViewModel;
                //dependent connector of dragged node
                var connector = (draggedNode.Info as INodeInfo).InOutConnectors.FirstOrDefault() as ConnectorViewModel;
                //finding connector length
                double connectorLength = FindConnectorLength(connector as ConnectorViewModel);
                if (connector != null)
                {
                    //resettig node position to initial value when its dependent connector legnth is less then 50.
                    if (connectorLength < 51)
                    {
                        draggedNode.OffsetX = args.InitialValue.OffsetX;
                        draggedNode.OffsetY = args.InitialValue.OffsetY;
                    }
                }

                //Getting opposite node of dragged node
                NodeViewModel oppositeNode = null;
                if (connector.SourceNode == draggedNode)
                    oppositeNode = connector.TargetNode as NodeViewModel;
                else
                    oppositeNode = connector.SourceNode as NodeViewModel;
                //segment decorator position length
                double segmentDecoratorPosition = ((connector.SegmentDecorators as IEnumerable<object>).FirstOrDefault() as SegmentDecorator).Length;
                //setting rotate angle to nodes based on segment decorator angle.
                this.SetRotateAngleForNodes(draggedNode, oppositeNode, connector, connectorLength, segmentDecoratorPosition);
            }
        }

        //Method to find rotated angle of segment and reset the same to nodes.
        private void SetRotateAngleForNodes(NodeViewModel draggedNode, NodeViewModel oppositeNode, ConnectorViewModel connector, double conlength, double segmentDecoratorPosition)
        {
            //Getting segments as points of collection
            List<Point> segmentpoints = (connector.Info as IConnectorInfo).ToPoints(true).ToList();
            //Getting decorator length on connector.
            var decoratorLength = conlength * segmentDecoratorPosition;
            double segmentLength = 0;
            for(int i = 0; i < segmentpoints.Count - 1; i++)
            {
                //Finding length between teo segment points
                segmentLength += segmentpoints[i].FindLength(segmentpoints[i + 1]);
                //finding the segment where decorator is placed
                if (decoratorLength <= segmentLength)
                {
                    //Finding angle of the decorator on the 2 segment points.
                    var angle = segmentpoints[i].FindAngle(segmentpoints[i + 1]);
                    //resetting segment decorator angle to source and target nodes.
                    draggedNode.RotateAngle = angle;
                    oppositeNode.RotateAngle = angle;
                    break;
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

        internal Point GetPointAtLength(double length, List<Point> pts)
        {
            double run = 0;
            Point? pre = null;
            Point found = new Point(0, 0);
            foreach (Point pt in pts)
            {
                if (!pre.HasValue)
                {
                    pre = pt;
                    continue;
                }
                else
                {
                    double l = pre.Value.FindLength(pt);
                    if (run + l >= length)
                    {
                        double r = length - run;
                        double deg = pre.Value.FindAngle(pt);
                        double x = r * Math.Cos(deg * Math.PI / 180);
                        double y = r * Math.Sin(deg * Math.PI / 180);
                        found = new Point(pre.Value.X + x, pre.Value.Y + y);
                        break;
                    }
                    else
                    {
                        run += l;
                    }
                }

                pre = pt;
            }

            return found;
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

        public static double FindAngle(this Point s, Point e)
        {
            if (s.Equals(e))
            {
                return 0d;
            }

            Point r = new Point(e.X, s.Y);
            double sr = s.FindLength(r);
            double re = r.FindLength(e);
            double es = e.FindLength(s);
            double ang = Math.Asin(re / es);
            if (double.IsNaN(ang))
            {
                ang = 0;
            }

            ang = ang * 180 / Math.PI;
            if (s.X < e.X)
            {
                if (s.Y < e.Y)
                {
                }
                else
                {
                    ang = 360 - ang;
                }
            }
            else
            {
                if (s.Y < e.Y)
                {
                    ang = 180 - ang;
                }
                else
                {
                    ang = 180 + ang;
                }
            }

            return ang;
        }
    }
}
