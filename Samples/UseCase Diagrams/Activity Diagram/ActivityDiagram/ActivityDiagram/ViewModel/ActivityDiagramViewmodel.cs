using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using Syncfusion.UI.Xaml.Diagram.Theming;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ActivityDiagram.ViewModel
{
    public class ActivityDiagramViewmodel : DiagramViewModel
    {
        private bool first = true;
        ResourceDictionary resourceDictionary = new ResourceDictionary()
        {
            Source = new Uri(@"/Syncfusion.SfDiagram.Wpf;component/Resources/BasicShapes.xaml", UriKind.RelativeOrAbsolute)
        };

        public ActivityDiagramViewmodel()
        {
           this.Nodes = new ObservableCollection<NodeViewModel>();
           this.Connectors = new ObservableCollection<ConnectorViewModel>();
           this.Swimlanes = new ObservableCollection<SwimlaneViewModel>(); 
           InitializeDiagram(); 
        }
        private void InitializeDiagram()
        {
            NodeViewModel n1 = CreateNode("InitialNode",300, 70, 35, 35, "InitialNode", "");
            NodeViewModel n2 = CreateNode("Receivecall",300, 130, 40, 120, "Activity", "Receive Customer Call");
            NodeViewModel n3 = CreateNode("HorizontalFork", 300, 180, 20, 80, "HorizontalFork", "");
            NodeViewModel n4 = CreateNode("DetermineCall", 150, 270, 40, 110, "Activity", "Determine Type Of Call");
            NodeViewModel n5 = CreateNode("CustomerCall", 440, 270, 40, 120, "Activity", "Customer Logging a call");
            NodeViewModel n6 = CreateNode("MergeNode", 150, 348, 50, 50, "MergeNode", "");
            NodeViewModel n7 = CreateNode("salesCall", 40, 420, 40, 120, "Activity", "Tranfer the call to sales");
            NodeViewModel n8 = CreateNode("helpdeskCall", 260, 420, 40, 120, "Activity", "Transfer the call to help desk");
            NodeViewModel n9 = CreateNode("MergeNode2", 150, 490, 50, 50, "MergeNode", "");
            NodeViewModel n10 = CreateNode("Join", 420, 600, 20, 80, "JoinNode", "");
            NodeViewModel n11 = CreateNode("MergeCall", 420, 660, 30, 70, "Activity", "Close Call");
            NodeViewModel n12 = CreateNode("Final", 420, 720, 35, 35, "Final", "");
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n1);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n2);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n3);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n4);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n5);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n6);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n7);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n8);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n9);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n10);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n11);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n12);

            CreatePort(n3);
            CreatePort(n4);
            CreatePort(n5);
            CreatePort(n6);
            CreatePort(n7);
            CreatePort(n8);
            CreatePort(n9);
            CreatePort(n10);
            CreateConnectors("c1", "InitialNode", "Receivecall");
            CreateConnectors("c1", "Receivecall", "HorizontalFork");
            CreateConnectors("c1", "DetermineCall", "MergeNode");
            CreateConnectors("c1", "Join", "MergeCall");
            CreateConnectors("c1", "MergeCall", "Final");
            ConnectorViewModel connector2 = ConnectPort("Forkport3", "port3","");
            ConnectorViewModel connector3 = ConnectPort("port2", "Salesport","[type = new Customer]");
            ConnectorViewModel connector4 = ConnectPort("port", "helpdeskCall", "[type = Existing Customer]");
            ConnectorViewModel connector5 = ConnectPort("Salesport2", "mergeport", "");
            ConnectorViewModel connector6 = ConnectPort("helpdeskCall2", "mergeport2", "");
            ConnectorViewModel connector7 = ConnectPort("port4", "Jointopright", "");
            ConnectorViewModel connector8 = ConnectPort("Forkport4", "DetermineCallport", "");
            ConnectorViewModel connector9 = ConnectPort("mergeport3", "Jointopleft", "");


            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector2);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector3);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector4);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector5);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector6);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector7);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector8);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector9);
            ViewPortChangedCommand = new DelegateCommand(OnViewPortChanged);
        }
        private NodeViewModel CreateNode(string name, double offsetX, double offsetY, double height, double width, string pathData, string annotation)
        {
            NodeViewModel node = new NodeViewModel()
            {
                ID= name,
               Name=name,
                OffsetX = offsetX + 70,
                OffsetY = offsetY,
                UnitHeight = height,
                UnitWidth = width,
                Shape = resourceDictionary[pathData],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                   new TextAnnotationViewModel()
                   {
                       Text = annotation,
                         FontSize = 14,
                       Foreground = new SolidColorBrush(Colors.Black),
                   },
                }
                
                
            }; 
            return node;
        }
        private void OnViewPortChanged(object obj)
        {
            var args = obj as ChangeEventArgs<object, ScrollChanged>;
            if (Info != null && (args.Item as SfDiagram).IsLoaded == true && args.NewValue.ContentBounds != args.OldValue.ContentBounds && first)
            {
                var bounds = args.NewValue.ContentBounds;
                bounds.Inflate(new Size(50, 50));
                if (bounds.Height > args.NewValue.ViewPort.Height)
                {
                    (Info as IGraphInfo).Commands.FitToPage.Execute(new FitToPageParameter() { FitToPage = FitToPage.FitToPage, Margin = new Thickness(20) });
                }
                else
                {
                    (Info as IGraphInfo).BringIntoCenter(bounds);
                }
                first = false;
            }
        }
        private void CreatePort(NodeViewModel node)
        {
            if(node.Name.ToString() == "HorizontalFork")
            {
               
                NodePortViewModel Forkport3 = new NodePortViewModel()
                {
                    ID= "Forkport3",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.75,
                    NodeOffsetY = 1,
                };
                NodePortViewModel Forkport4 = new NodePortViewModel()
                {
                    ID = "Forkport4",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.25,
                    NodeOffsetY = 1,
                };
                (node.Ports as PortCollection).Add(Forkport3);
                (node.Ports as PortCollection).Add(Forkport4);
            }
           if( node.Name.ToString() == "CustomerCall" )
            {
             
                NodePortViewModel port3 = new NodePortViewModel()
                {
                    ID = "port3",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.5,
                    NodeOffsetY = 0,
                };
                NodePortViewModel port4 = new NodePortViewModel()
                {
                    ID = "port4",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.5,
                    NodeOffsetY = 1,
                };

                (node.Ports as PortCollection).Add(port3);
                (node.Ports as PortCollection).Add(port4);
            }
            if (node.Name.ToString() == "DetermineCall")
            {

                NodePortViewModel DetermineCallport = new NodePortViewModel()
                {
                    ID = "DetermineCallport",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.5,
                    NodeOffsetY = 0,
                };
               
                (node.Ports as PortCollection).Add(DetermineCallport);
               
            }
            if (node.Name.ToString() == "salesCall")
            {

                NodePortViewModel Salesport = new NodePortViewModel()
                {
                    ID = "Salesport",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.5,
                    NodeOffsetY = 0,
                };
                NodePortViewModel Salesport2 = new NodePortViewModel()
                {
                    ID = "Salesport2",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.5,
                    NodeOffsetY = 1,
                };

                (node.Ports as PortCollection).Add(Salesport);
                (node.Ports as PortCollection).Add(Salesport2);
            }
            if (node.Name.ToString() == "helpdeskCall")
            {

                NodePortViewModel helpdeskCall = new NodePortViewModel()
                {
                    ID = "helpdeskCall",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.5,
                    NodeOffsetY = 0,
                };
                NodePortViewModel helpdeskCall2 = new NodePortViewModel()
                {
                    ID = "helpdeskCall2",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.5,
                    NodeOffsetY = 1,
                };

                (node.Ports as PortCollection).Add(helpdeskCall);
                (node.Ports as PortCollection).Add(helpdeskCall2);
            }
            if (node.Name.ToString() == "MergeNode")
            {
                NodePortViewModel port = new NodePortViewModel()
                {
                    ID= "port",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 1,
                    NodeOffsetY = 0.5,
                };
               
                NodePortViewModel port2 = new NodePortViewModel()
                {
                    ID= "port2",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0,
                    NodeOffsetY = 0.5,
                };
                
                (node.Ports as PortCollection).Add(port);
                (node.Ports as PortCollection).Add(port2);
            }
            if (node.Name.ToString() == "MergeNode2")
            {
                NodePortViewModel mergeport = new NodePortViewModel()
                {
                    ID = "mergeport",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0,
                    NodeOffsetY = 0.5,
                };

                NodePortViewModel mergeport2 = new NodePortViewModel()
                {
                    ID = "mergeport2",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 1,
                    NodeOffsetY = 0.5,
                };
                NodePortViewModel mergeport3 = new NodePortViewModel()
                {
                    ID = "mergeport3",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.5,
                    NodeOffsetY = 1,
                };

                (node.Ports as PortCollection).Add(mergeport);
                (node.Ports as PortCollection).Add(mergeport2);
                (node.Ports as PortCollection).Add(mergeport3);
            }
            if (node.Name.ToString() == "Join")
            {
                NodePortViewModel Jointopleft = new NodePortViewModel()
                {
                    ID= "Jointopleft",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.25,
                    NodeOffsetY = 0,
                };
                NodePortViewModel Jointopright = new NodePortViewModel()
                {
                    ID= "Jointopright",
                    UnitHeight = 7,
                    UnitWidth = 7,
                    NodeOffsetX = 0.75,
                    NodeOffsetY = 0,
                };
              
                (node.Ports as PortCollection).Add(Jointopleft);
                (node.Ports as PortCollection).Add(Jointopright);
               

            }
        }
        private ConnectorViewModel ConnectPort(string sourceportid, string targetportid, string content)
        {
            ConnectorViewModel con = new ConnectorViewModel()
            {
                SourcePortID = sourceportid,
                TargetPortID = targetportid,
                Annotations = new ObservableCollection<IAnnotation>()
            {
            new AnnotationEditorViewModel()
            {
                Content=content,
                Alignment = ConnectorAnnotationAlignment.Center,
                Foreground = new SolidColorBrush(Colors.Black),
                Background = new SolidColorBrush(Colors.White),
                RotateAngle = -90,
                Displacement = 30,
            }
            }
            };
            return con;
        }
        private void CreateConnectors(string id, string sourceID, string targetID )
        {
            ConnectorViewModel connector = new ConnectorViewModel()
            {
                ID = id,
                SourceNodeID = sourceID,
                TargetNodeID = targetID,
            };
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector);
        }
    }
}
