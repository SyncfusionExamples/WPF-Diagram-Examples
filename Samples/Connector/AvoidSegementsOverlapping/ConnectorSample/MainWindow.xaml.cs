using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using ArcSegment = System.Windows.Media.ArcSegment;
using OrthogonalSegment = Syncfusion.UI.Xaml.Diagram.OrthogonalSegment;
using ScrollViewer = Syncfusion.UI.Xaml.Diagram.Controls.ScrollViewer;

namespace Simple_SfDiagram_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Diagram.Nodes = new NodeCollection();
            Diagram.Connectors = new ConnectorCollection();

            NodePortViewModel port0 = new NodePortViewModel()
            {
                UnitHeight = 10,
                UnitWidth = 10,
                NodeOffsetX = 0,
                NodeOffsetY = 0.5,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
            };

            NodePortViewModel port1 = new NodePortViewModel()
            {
                UnitHeight = 10,
                UnitWidth = 10,
                NodeOffsetX = 0,
                NodeOffsetY = 0.5,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
            };

            NodePortViewModel port2 = new NodePortViewModel()
            {
                UnitHeight = 10,
                UnitWidth = 10,
                NodeOffsetX = 0,
                NodeOffsetY = 0.5,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
            };


            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 40,
                UnitWidth = 150,
                OffsetX = 500,
                OffsetY = 200,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Content = "Test",
            };

            NodeViewModel node1 = new NodeViewModel()
            {
                UnitHeight = 40,
                UnitWidth = 150,
                OffsetX = 550,
                OffsetY = 275,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Content = "Node 0",
                Ports = new PortCollection() { port0 },
            };

            NodeViewModel node2 = new NodeViewModel()
            {
                UnitHeight = 40,
                UnitWidth = 150,
                OffsetX = 550,
                OffsetY = 325,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Content= "Node 1",
                Ports = new PortCollection() { port1 },
            };

            NodeViewModel node3 = new NodeViewModel()
            {
                UnitHeight = 40,
                UnitWidth = 150,
                OffsetX = 550,
                OffsetY = 375,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Content = "Node 2",
                Ports = new PortCollection() { port2 },
            };

            (Diagram.Nodes as NodeCollection).Add(node);
            (Diagram.Nodes as NodeCollection).Add(node1);
            (Diagram.Nodes as NodeCollection).Add(node2);
            (Diagram.Nodes as NodeCollection).Add(node3);

            ConnectorViewModel cp = new ConnectorViewModel() 
            {
                SourceNode = node,
                TargetNode = node1,
            };

            ConnectorViewModel con1 = new ConnectorViewModel()
            {
                SourcePort = port0,
                TargetPort = port2,
                Segments = new ConnectorSegments()
                {
                    new OrthogonalSegment()
                    {
                        Length = 40,
                        Direction = OrthogonalDirection.Left,
                    },
                },
            };

            ConnectorViewModel con2 = new ConnectorViewModel()
            {
                SourcePort = port1,
                TargetPort = port2,
            };

            (Diagram.Connectors as ConnectorCollection).Add(cp);
            (Diagram.Connectors as ConnectorCollection).Add(con1);
            (Diagram.Connectors as ConnectorCollection).Add(con2);
        }
    }
}