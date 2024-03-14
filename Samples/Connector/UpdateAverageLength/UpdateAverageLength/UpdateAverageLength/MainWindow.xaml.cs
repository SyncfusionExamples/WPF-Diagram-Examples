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

namespace UpdateAverageLength
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (diagram.Info as IGraphInfo).ConnectorSourceChangedEvent += MainWindow_ConnectorSourceChangedEvent;
            (diagram.Info as IGraphInfo).ConnectorTargetChangedEvent += MainWindow_ConnectorTargetChangedEvent;
            (diagram.Info as IGraphInfo).SelectorChangedEvent += MainWindow_SelectorChangedEvent;

            diagram.SnapSettings = new SnapSettings() { SnapConstraints = SnapConstraints.All };
            diagram.PortVisibility = PortVisibility.Visible;
            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();
            //Creating nodes
            CreateNodes(600, 200);
            CreateNodes(500, 400);
            CreateNodes(300, 400);
            CreateNodes(100, 400);
        }
        //To update the average segment length for existing connectors.
        private void MainWindow_SelectorChangedEvent(object sender, SelectorChangedEventArgs args)
        {
            if (args.NewValue.InteractionState == NodeChangedInteractionState.Dragged || args.NewValue.InteractionState == NodeChangedInteractionState.Resized
                || args.NewValue.InteractionState == NodeChangedInteractionState.Rotated)
            {
                var nodes = (diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>;
                foreach (var item in nodes)
                {
                    var node = item as NodeViewModel;
                    if ((node.Info as INodeInfo).InConnectors != null && (node.Info as INodeInfo).InConnectors.Any())
                    {
                        foreach (var connector in (node.Info as INodeInfo).InConnectors as IEnumerable<object>)
                        {
                            //updating the segment length for existing connector
                            UpdateSegmentLength(connector as ConnectorViewModel);
                        }
                    }

                    if ((node.Info as INodeInfo).OutConnectors != null && (node.Info as INodeInfo).OutConnectors.Any())
                    {
                        foreach (var connector in (node.Info as INodeInfo).OutConnectors as IEnumerable<object>)
                        {
                            //updating the segment length for existing connector
                            UpdateSegmentLength(connector as ConnectorViewModel);
                        }
                    }
                }
            }
        }

        private void MainWindow_ConnectorTargetChangedEvent(object sender, ChangeEventArgs<object, ConnectorChangedEventArgs> args)
        {
            //updating the segment length at connector drawn completed.
            if (args.NewValue.DragState == DragState.Completed)
                UpdateSegmentLength(args.Item as ConnectorViewModel);
        }

        private void MainWindow_ConnectorSourceChangedEvent(object sender, ChangeEventArgs<object, ConnectorChangedEventArgs> args)
        {
            //updating the segment length at connector drawn completed
            if (args.NewValue.DragState == DragState.Completed)
                UpdateSegmentLength(args.Item as ConnectorViewModel);
        }

        private void CreateNodes(double offsetX, double offsetY)
        {
            NodeViewModel node = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = offsetX,
                OffsetY = offsetY,
                Shape = App.Current.MainWindow.Resources["Rectangle"],
                //creating ports.
                Ports = new PortCollection()
                {
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0.5,
                        NodeOffsetY = 0,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0.5,
                        NodeOffsetY = 1,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0,
                        NodeOffsetY = 0.5,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 0.5,
                    }
                }
            };
            //adding the node diagram nodes collection 
            (diagram.Nodes as NodeCollection).Add(node);
        }

        //Method to update average segment length for connector
        private void UpdateSegmentLength(ConnectorViewModel connector)
        {
            //getting segments points of the connector drawn.
            List<Point> segmentsPoints = (connector.Info as IConnectorInfo).ToPoints(true).ToList();
            //total segment count of the connector
            int segmentsCount = segmentsPoints.Count;
            //for non-edited segments
            if (segmentsCount == 4)
            {
                List<Point> newPoints = new List<Point>();
                //for top and bottom direction connectors
                if (segmentsPoints[0].X == segmentsPoints[1].X)
                {
                    //average y distance need to update for segment
                    double average = (segmentsPoints[segmentsCount - 1].Y - segmentsPoints[0].Y) / 2;
                    //new y position for intermediate segments
                    double newy = segmentsPoints[segmentsCount - 1].Y - average;
                    //Updating new y point to the intermediate segments
                    for (int i = 1; i <= segmentsCount - 2; i++)
                    {
                        Point pnt = new Point(segmentsPoints[i].X, newy);
                        newPoints.Add(pnt);
                    }
                }
                //for left and right direction connectors.
                else if (segmentsPoints[0].Y == segmentsPoints[1].Y)
                {
                    //average x distance need to update for segment
                    double average = (segmentsPoints[segmentsCount - 1].X - segmentsPoints[0].X) / 2;
                    //new x position for intermediate segments
                    double newx = segmentsPoints[segmentsCount - 1].X - average;
                    //Updating new x point to the intermediate segments
                    for (int i = 1; i <= segmentsCount - 2; i++)
                    {
                        Point pnt = new Point(newx, segmentsPoints[i].Y);
                        newPoints.Add(pnt);
                    }
                }

                //passing new segments points to the connector using LoadSegment method.
                (connector.Info as IConnectorInfo).LoadSegments(newPoints);
            }
        }
    }
}
