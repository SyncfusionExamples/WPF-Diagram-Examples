using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using Syncfusion.UI.Xaml.Diagram.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Xml.Linq;
using Node = Syncfusion.UI.Xaml.Diagram.Node;

namespace PortsSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.MouseMove += Diagram_MouseMove;
            diagram.Constraints = GraphConstraints.Default | GraphConstraints.Bridging;
           
            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 300,
                OffsetY = 200,
                UnitHeight = 100,
                UnitWidth = 150,
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    //Initialize the Annotation with content
                    new AnnotationEditorViewModel()
                    {
                        Content="Source node"
                    }
                },
               // node.Constraints &= ~NodeConstraints.Connectable;
            };
            node.Constraints = NodeConstraints.Default & ~ (NodeConstraints.InheritHitPadding | NodeConstraints.Connectable);
            node.HitPadding = 20;

            NodePortViewModel nodePort = new NodePortViewModel()
            {
                ID= "nodePort",
                NodeOffsetX = 0,
                NodeOffsetY = 0.5,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["RedStyle"] as Style,
                Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable,
            };
          
            NodePortViewModel nodePort1 = new NodePortViewModel()
            {
                ID = "nodePort1",
                NodeOffsetX = 0.5,
                NodeOffsetY = 0,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["GreenStyle"] as Style,
                Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable,
            };
          
            NodePortViewModel nodePort2 = new NodePortViewModel()
            {
                ID = "nodePort2",
                NodeOffsetX = 0.5,
                NodeOffsetY = 1,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["BlackStyle"] as Style,
                Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable,
            };
         
            NodePortViewModel nodePort3 = new NodePortViewModel()
            {
                ID = "nodePort3",
                NodeOffsetX = 1,
                NodeOffsetY = 0.5,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["BlueStyle"] as Style,
                Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable,
            };
           
            
            (node.Ports as PortCollection).Add(nodePort);
            (node.Ports as PortCollection).Add(nodePort1);
            (node.Ports as PortCollection).Add(nodePort2);
            (node.Ports as PortCollection).Add(nodePort3);
           
            (diagram.Nodes as NodeCollection).Add(node);



            NodeViewModel node2 = new NodeViewModel()
            {
                ID = "Node2",
                OffsetX = 700,
                OffsetY = 300,
                UnitHeight = 100,
                UnitWidth = 150,
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    //Initialize the Annotation with content
                    new AnnotationEditorViewModel()
                    {
                        Content="Target Node"
                    }
                }
            };
            node2.Constraints = NodeConstraints.Default & ~(NodeConstraints.InheritHitPadding | NodeConstraints.Connectable);
            node2.HitPadding = 20;
            NodePortViewModel Port = new NodePortViewModel()
            {
                ID= "Port",
                NodeOffsetX = 0,
                NodeOffsetY = 0.5,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["RedStyle"] as Style,
                Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable,
            };
            NodePortViewModel Port1 = new NodePortViewModel()
            {
                ID="Port1",
                NodeOffsetX = 0.5,
                NodeOffsetY = 0,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["GreenStyle"] as Style,
                Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable,
            };
           
            NodePortViewModel Port2 = new NodePortViewModel()
            {
                ID="Port2",
                NodeOffsetX = 0.5,
                NodeOffsetY = 1,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["BlackStyle"] as Style,
                Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable,
            };
            NodePortViewModel Port3 = new NodePortViewModel()
            {
                ID="Port3",
                NodeOffsetX = 1,
                NodeOffsetY = 0.5,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["BlueStyle"] as Style,
                Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable,
            };
           
            (node2.Ports as PortCollection).Add(Port);
            (node2.Ports as PortCollection).Add(Port1);
            (node2.Ports as PortCollection).Add(Port2);
            (node2.Ports as PortCollection).Add(Port3);
            (diagram.Nodes as NodeCollection).Add(node2);
        }

        private void Diagram_MouseMove(object sender, MouseEventArgs e)
        {
            DependencyObject source = e.OriginalSource as DependencyObject;

            // Find the Visual Parent for the node.
            var node = source.FindVisualParent<Node>();

           
            if ((node != null))
            {
                //Get the NodeViewModel from the node.
                NodeViewModel nodeVM = node.DataContext as NodeViewModel;

                foreach(var port in nodeVM.Ports as IEnumerable<object>)
                {
                    if((port as NodePortViewModel).PortVisibility == PortVisibility.Collapse)
                    {
                        (port as NodePortViewModel).PortVisibility = PortVisibility.MouseOver;
                    }
                }
            }
        }
    }
    public class CustomDiagram : SfDiagram
    {
        // Validate the connection when connector endpoints are dragged
        protected override void ValidateConnection(ConnectionParameter args)
        {
            if (args.TargetNode is NodeViewModel && (args.Connector as ConnectorViewModel).SourcePort != null)
            {
               foreach (var port1 in (args.TargetNode as NodeViewModel).Ports as IEnumerable<object>)
                {
                    if ((port1 as NodePortViewModel).ShapeStyle == (args.Connector as ConnectorViewModel).SourcePort.ShapeStyle)
                    {
                        (port1 as NodePortViewModel).PortVisibility = PortVisibility.ValidConnection;

                        (port1 as NodePortViewModel).Constraints = (port1 as NodePortViewModel).Constraints.Remove(PortConstraints.InheritPortVisibility);
                    }
                    else
                    {
                        (port1 as NodePortViewModel).PortVisibility = PortVisibility.Collapse;

                        (port1 as NodePortViewModel).Constraints = (port1 as NodePortViewModel).Constraints.Remove(PortConstraints.InheritPortVisibility);
                    }
                }
            }
            base.ValidateConnection(args);
        }
    }
}
