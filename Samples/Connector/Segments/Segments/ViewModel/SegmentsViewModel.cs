using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Segments
{
    public class SegmentsViewModel : DiagramViewModel
    {
        public SegmentsViewModel()
        {
            //Initialize the Nodes Collection 
            this.Nodes = new ObservableCollection<NodeViewModel>();
            //Initialize the connectors Collection
            this.Connectors = new ObservableCollection<ConnectorViewModel>();

            //Creating source node
            NodeViewModel sourceNode = new NodeViewModel()
            {
                UnitHeight = 80,
                UnitWidth = 150,
                OffsetX = 800,
                OffsetY = 250,
                Shape = App.Current.Resources["Rectangle"],
            };

            //Creating target node
            NodeViewModel targetNode = new NodeViewModel()
            {
                UnitHeight = 80,
                UnitWidth = 150,
                OffsetX = 700,
                OffsetY = 200,
                Shape = App.Current.Resources["Rectangle"],
            };

            //Adding the nodes into Collection
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(sourceNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(targetNode);

            //create the connector with orthogonal segments
            ConnectorViewModel orthogonalConnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(100, 100),
                TargetPoint = new Point(200, 200),
                //Initialize the connector segment collection
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    //Specify the segment as orthogonal segment
                    new OrthogonalSegment()
                },
                //Initialize the AnnotationCollection
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    //Initialize the Annotation with content
                    new AnnotationEditorViewModel()
                    {
                        Content ="Orthogonal Connector"
                    }
                }
            };

            //create the connector with custom orthogonal segments.
            ConnectorViewModel customOrthogonalConnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(300, 100),
                TargetPoint = new Point(600, 300),
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    new OrthogonalSegment()
                    {
                        //Specify the direction of the segment
                        Direction = OrthogonalDirection.Right,
                        //Specify the length of the segment
                        Length = 100,
                    },
                    new OrthogonalSegment()
                    {
                        Direction = OrthogonalDirection.Bottom,
                        Length = 100,
                    },
                    new OrthogonalSegment()
                    {
                        Direction = OrthogonalDirection.Right,
                        Length = 100,
                    },
                    new OrthogonalSegment()
                    {
                        Direction = OrthogonalDirection.Bottom,
                        Length = 100,
                    },
                    new OrthogonalSegment()
                    {
                        Direction = OrthogonalDirection.Right,
                        Length = 100,
                    }
                },
                //Initialize the AnnotationCollection
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    //Initialize the Annotation with content
                    new AnnotationEditorViewModel()
                    {
                        Content ="Custom Orthogonal Connector"
                    }
                }
            };

            //Connector with avoid overlapping
            ConnectorViewModel routingConnector = new ConnectorViewModel()
            {
                SourceNode = sourceNode,
                TargetNode = targetNode,
            };

            //create the connector with cubic bezier segments
            ConnectorViewModel cubicBezierConnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(100, 300),
                TargetPoint = new Point(200, 400),
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    //Specify the segment as cubic curve segment
                    new CubicCurveSegment()
                },
                //Initialize the AnnotationCollection
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    //Initialize the Annotation with content
                    new AnnotationEditorViewModel()
                    {
                        Content ="Cubic segment"
                    }
                }
            };

            //create the connector with custom cubic bezier segments
            ConnectorViewModel customCubicBezier = new ConnectorViewModel()
            {
                SourcePoint = new Point(300, 300),
                TargetPoint = new Point(500, 500),
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    //Specify the segment as cubic curve segment
                    new CubicCurveSegment()
                    {
                        //Specifies the left side control point
                        Point1 = new Point(250,350),
                        //Specifies the right side control point
                        Point2 = new Point(460,350),
                        //Specifies the ending point of the segment
                        Point3 = new Point(400,400),
                    },
                    new CubicCurveSegment()
                    {
                        Point1 = new Point(300,450),
                        Point2 = new Point(550,460),
                    }
                },
                //Initialize the AnnotationCollection
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    //Initialize the Annotation with content
                    new AnnotationEditorViewModel()
                    {
                        Content ="Custom cubic segment"
                    }
                }
            };

            //create the connector with qudratic bezier segments
            ConnectorViewModel qudraticBezierConnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(100, 450),
                TargetPoint = new Point(200, 550),
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    //Specify the segment as qudratic curve segment
                    new QuadraticCurveSegment()
                },
                //Initialize the AnnotationCollection
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    //Initialize the Annotation with content
                    new AnnotationEditorViewModel()
                    {
                        Content ="Qudratic segment"
                    }
                }
            };

            //create the connector with multiple segments
            ConnectorViewModel multipleSegments = new ConnectorViewModel()
            {
                SourcePoint = new Point(600,350),
                TargetPoint = new Point(950,650),
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    //Specify the segment as Quadratic curve segment
                    new QuadraticCurveSegment()
                    {
                        Point1 = new Point(550, 550),
                        Point2 = new Point(700, 430),
                    },
                    //Specify the segment as straight segment
                    new StraightSegment()
                    {
                        Point = new Point(800,550),
                    },
                    //Specify the segment as orthogonal segment
                    new OrthogonalSegment()
                    {
                        Length = 100,
                    },
                },
                //Initialize the AnnotationCollection
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    //Initialize the Annotation with content
                    new AnnotationEditorViewModel()
                    {
                        Content ="Multiple segments"
                    }
                }
            };

            //Adding connector into Collection
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(orthogonalConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(customOrthogonalConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(routingConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(cubicBezierConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(customCubicBezier);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(qudraticBezierConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(multipleSegments);
        }
    }
}
