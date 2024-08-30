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
                }
            };
            

            NodePortViewModel nodePort = new NodePortViewModel()
            {
                ID= "nodePort",
                NodeOffsetX = 0,
                NodeOffsetY = 0.5,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["RedStyle"] as Style,
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
            
            NodePortViewModel Port = new NodePortViewModel()
            {
                ID= "Port",
                NodeOffsetX = 0,
                NodeOffsetY = 0.5,
                UnitHeight = 10,
                UnitWidth = 10,
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["RedStyle"] as Style,
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
            };
            

            (node2.Ports as PortCollection).Add(Port);
            (node2.Ports as PortCollection).Add(Port1);
            (node2.Ports as PortCollection).Add(Port2);
            (node2.Ports as PortCollection).Add(Port3);
            (diagram.Nodes as NodeCollection).Add(node2);

            

        }

       
    }
}
