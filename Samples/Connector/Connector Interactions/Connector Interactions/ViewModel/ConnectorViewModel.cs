using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConnectorInteractions
{
    public class ConnectorsViewModel : DiagramViewModel
    {
        public ConnectorsViewModel()
        {
            //Initialize the Nodes Collection 
            this.Nodes = new ObservableCollection<NodeViewModel>();
            //Initialize the connectors Collection
            this.Connectors = new ObservableCollection<ConnectorViewModel>();

            //Specify the port visibility
            this.PortVisibility = PortVisibility.Visible;
            //Specify the connector type
            this.DefaultConnectorType = ConnectorType.Orthogonal;
            //Enable the routing constraint
            this.Constraints |= GraphConstraints.Routing;

            //Creating source node
            NodeViewModel sourceNode = new NodeViewModel()
            {
                UnitHeight = 50,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 150,
                Shape = App.Current.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Source",
                    }
                },
            };

            //Creating target node
            NodeViewModel targetNode = new NodeViewModel()
            {
                UnitHeight = 50,
                UnitWidth = 100,
                OffsetX = 850,
                OffsetY = 150,
                Shape = App.Current.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Target",
                    }
                },
            };

            //Routing enabled node
            NodeViewModel routingEnabledNode = new NodeViewModel()
            {
                UnitHeight = 50,
                UnitWidth = 100,
                OffsetX = 550,
                OffsetY = 150,
                Shape = App.Current.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Routing obstacle",
                    }
                },
            };

            //Routing disable node
            NodeViewModel routingDisabledNode = new NodeViewModel()
            {
                UnitHeight = 50,
                UnitWidth = 100,
                OffsetX = 700,
                OffsetY = 180,
                Shape = App.Current.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Non-Routing obstacle",
                    }
                },
            };

            //Disable the routing obstacle property
            routingDisabledNode.Constraints = NodeConstraints.Default & ~NodeConstraints.RoutingObstacle;

            //Create source node for non routing connector
            NodeViewModel routingSourceNode = new NodeViewModel()
            {
                UnitHeight = 50,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 300,
                Shape = App.Current.Resources["Rectangle"],
            };

            //Create target node for non routing connector
            NodeViewModel routingTargetNode = new NodeViewModel()
            {
                UnitHeight = 50,
                UnitWidth = 100,
                OffsetX = 700,
                OffsetY = 300,
                Shape = App.Current.Resources["Rectangle"],
            };

            //Routing obstacle for non routing connector
            NodeViewModel routingObstacle = new NodeViewModel()
            {
                UnitHeight = 50,
                UnitWidth = 100,
                OffsetX = 550,
                OffsetY = 300,
                Shape = App.Current.Resources["Rectangle"],
            };

            //Adding the nodes into Collection
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(sourceNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(targetNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(routingEnabledNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(routingDisabledNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(routingSourceNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(routingTargetNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(routingObstacle);

            //creating simple connector through collection using source and target points.
            ConnectorViewModel simpleConnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(50, 100),
                TargetPoint = new Point(150, 200),
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Select and edit",
                    }
                },
            };

            //creating simple connector with draggable constraint.
            ConnectorViewModel simpleConnectorDrag = new ConnectorViewModel()
            {
                SourcePoint = new Point(200, 100),
                TargetPoint = new Point(300, 200),
                //Define the constraint as draggable
                Constraints = ConnectorConstraints.Default | ConnectorConstraints.Draggable,
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Select and drag",
                    }
                },
            };

            //Connector with routing behaviour
            ConnectorViewModel routingConnector = new ConnectorViewModel()
            {
                SourceNode = sourceNode,
                TargetNode = targetNode,
            };

            //Connector with disabled routing behaviour
            ConnectorViewModel routingDisabledConnector = new ConnectorViewModel()
            {
                SourceNode = routingSourceNode,
                TargetNode = routingTargetNode,
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Non-Routing connector",
                    }
                },
            };

            //Disable the routing from diagram constraint and enable the routing constraint of a connector
            routingDisabledConnector.Constraints &= ~ConnectorConstraints.InheritRouting | ConnectorConstraints.Routing;

            //Adding the connectors into Collection
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(simpleConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(simpleConnectorDrag);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(routingConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(routingDisabledConnector);
        }
    }
}
