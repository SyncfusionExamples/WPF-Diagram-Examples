using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConnectorAppearance
{
    public class AppearanceViewModel : DiagramViewModel
    {
        public AppearanceViewModel()
        {
            //Initialize the Nodes Collection 
            this.Nodes = new ObservableCollection<NodeViewModel>();
            //Initialize the connectors Collection
            this.Connectors = new ObservableCollection<ConnectorViewModel>();
            //Specify the default connector type
            this.DefaultConnectorType = ConnectorType.Line;
            //Enable the bridging
            this.Constraints |= GraphConstraints.Bridging;
            //Specify the bridge direction
            this.BridgeDirection = BridgeDirection.Bottom;
            //Specify the port visibility
            this.PortVisibility = PortVisibility.Visible;

            //creating source node
            NodeViewModel sourceNode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = 150,
                OffsetY = 300,
                Shape = App.Current.Resources["Rectangle"],
            };

            //creating target node
            NodeViewModel targetNode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = 350,
                OffsetY = 300,
                Shape = App.Current.Resources["Rectangle"],
            };

            //creating node with ports
            NodeViewModel PortsNode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = 150,
                OffsetY = 500,
                Shape = App.Current.Resources["Rectangle"],
                Ports = new PortCollection()
                {
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0.7,
                        NodeOffsetY = 0.3,
                    }
                }
            };

            //Adding the nodes into Collection
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(sourceNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(targetNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(PortsNode);

            //Define the Connector with ellipse decorator
            ConnectorViewModel EllipseDecorator = new ConnectorViewModel()
            {
                SourcePoint = new Point(100, 100),
                TargetPoint = new Point(200, 200),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                //Specifies ellipse shape source decorator
                SourceDecorator = App.Current.Resources["Ellipse"],
                //Specifies the style for source decorator
                SourceDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style,
                //Specifies arrow shape target decorator
                TargetDecorator = App.Current.Resources["ClosedSharp"],
                //Specifies the style for target decorator
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style
            };

            //Define the Connector with rectange decorator
            ConnectorViewModel RectangleDecorator = new ConnectorViewModel()
            {
                SourcePoint = new Point(300, 100),
                TargetPoint = new Point(400, 200),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                //Specifies diamond shape source decorator
                SourceDecorator = App.Current.Resources["Rectangle"],
                //Specifies style for source decorator
                SourceDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style,
                //Specifies Open arrow shape decorator
                TargetDecorator = App.Current.Resources["OpenSharp"],
                //Specifies style for target decorator
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style
            };

            //Define the Connector with hollow ellipse Decorator
            ConnectorViewModel HollowEllipseDecorator = new ConnectorViewModel()
            {
                SourcePoint = new Point(500, 100),
                TargetPoint = new Point(600, 200),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                SourceDecorator = App.Current.Resources["Ellipse"],
                //Specify the hollow style for source decorator
                SourceDecoratorStyle = App.Current.Resources["DecoratorHollowStyle"] as Style,
                TargetDecorator = App.Current.Resources["ClosedSharp"],
                //Specify the hollow style for target decorator
                TargetDecoratorStyle = App.Current.Resources["DecoratorHollowStyle"] as Style
            };

            //Define the Connector with hollow rectangle decorator
            ConnectorViewModel HollowRectangleDecorator = new ConnectorViewModel()
            {
                SourcePoint = new Point(700, 100),
                TargetPoint = new Point(800, 200),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                SourceDecorator = App.Current.Resources["Rectangle"],
                //Specify the hollow style for source decorator
                SourceDecoratorStyle = App.Current.Resources["DecoratorHollowStyle"] as Style,
                TargetDecorator = App.Current.Resources["OpenSharp"],
                //Specify the hollow style for target decorator
                TargetDecoratorStyle = App.Current.Resources["DecoratorHollowStyle"] as Style
            };

            //Define the Connector with pivot value for decorator
            ConnectorViewModel PivotDecorator = new ConnectorViewModel()
            {
                SourcePoint = new Point(900, 100),
                TargetPoint = new Point(1000, 200),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                //Defines source pivot position
                SourceDecoratorPivot = new Point(0, 0),
                SourceDecorator = App.Current.Resources["Ellipse"],
                SourceDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style,
                //Defines target pivot position
                TargetDecoratorPivot = new Point(0, 0),
                TargetDecorator = App.Current.Resources["ClosedSharp"],
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style
            };

            //Creating the connector with padding
            ConnectorViewModel paddingConnector = new ConnectorViewModel()
            {
                SourceNode = sourceNode,
                TargetNode = targetNode,
                //Defines the souce point padding value of connector
                SourcePadding = 5,
                //Defines the target point padding value of connector
                TargetPadding = 5,
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                TargetDecorator = App.Current.Resources["ClosedSharp"] as object,
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style
            };

            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourcePoint = new Point(450, 300),
                TargetPoint = new Point(550, 300),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                TargetDecorator = App.Current.Resources["ClosedSharp"] as object,
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style
            };

            //connector for bridging
            ConnectorViewModel bridgeConnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(500, 250),
                TargetPoint = new Point(500, 350),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                TargetDecorator = App.Current.Resources["ClosedSharp"] as object,
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style
            };

            //connector for bridging simpleConnector
            ConnectorViewModel customBridgeConnector = new ConnectorViewModel()
            {
                //Specify the size of the bridging
                BridgeSpace = 40,
                SourcePoint = new Point(620, 300),
                TargetPoint = new Point(780, 300),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                TargetDecorator = App.Current.Resources["ClosedSharp"] as object,
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style
            };

            ConnectorViewModel simpleConnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(700, 250),
                TargetPoint = new Point(700, 350),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                TargetDecorator = App.Current.Resources["ClosedSharp"] as object,
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style
            };

            //Create connector with customized geometry style and target decorator
            ConnectorViewModel customConnector = new ConnectorViewModel()
            {
                SourcePoint = new Point(820, 250),
                TargetPoint = new Point(920, 350),
                ConnectorGeometryStyle = App.Current.Resources["CustomConnectorStyle"] as Style,
                TargetDecoratorStyle = App.Current.Resources["CustomDecoratorStyle"] as Style,
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    //Specify the segment as orthogonal segment
                    new OrthogonalSegment()
                }
            };

            //Create connector with hit padding value
            ConnectorViewModel connectionPadding = new ConnectorViewModel()
            {
                //Defines the hit padding value
                HitPadding = 25,
                SourcePoint = new Point(970, 250),
                TargetPoint = new Point(1070, 350),
                ConnectorGeometryStyle = App.Current.Resources["connectorLineStyle"] as Style,
                TargetDecorator = App.Current.Resources["ClosedSharp"] as object,
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style,
                Segments = new ObservableCollection<IConnectorSegment>()
                {
                    //Specify the segment as orthogonal segment
                    new OrthogonalSegment()
                }
            };


            //Adding connector into Collection
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(EllipseDecorator);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(RectangleDecorator);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(HollowEllipseDecorator);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(HollowRectangleDecorator);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(PivotDecorator);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(paddingConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(bridgeConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(simpleConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(customBridgeConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(customConnector);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connectionPadding);
        }
    }
}
