using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
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

namespace StencilDiagramElements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Define the SymbolSource with SymbolCollection.
            stencil.SymbolSource = new SymbolCollection();

            //Initialize the node  diagram element.
            NodeViewModel node = new NodeViewModel()
            {
                Key = "Nodes",
                UnitHeight = 70,
                UnitWidth = 100,
                OffsetX = 100,
                OffsetY = 100,
                Shape = this.Resources["Rectangle"],
            };

            //Initialize the connector diagram element.
            ConnectorViewModel connector = new ConnectorViewModel()
            {
                Key = "Connectors",
                SourcePoint = new Point(100, 100),
                TargetPoint = new Point(200, 200),
            };

            //Initialize the group element.
            GroupViewModel group = new GroupViewModel()
            {
                Key = "Groups",
                //Adding group nodes
                Nodes = new NodeCollection()
                {
                    new NodeViewModel()
                    {
                        ID="srcnode",
                        UnitHeight=70,
                        UnitWidth=100,
                        OffsetX=0,
                        OffsetY=300,
                        Shape=this.Resources["Rectangle"]
                    },
       
                    new NodeViewModel()
                    {
                        ID="tarnode",
                        UnitHeight=70,
                        UnitWidth=100,
                        OffsetX=100,
                        OffsetY=500,
                        Shape=this.Resources["Rectangle"]
                    }
                },
                //Adding group connector.
                Connectors = new ConnectorCollection()
                {
                    new ConnectorViewModel()
                    {
                        SourceNodeID="srcnode",
                        TargetNodeID="tarnode"
                    }
                }
            };

            //Creating container element.
            ContainerViewModel container = new ContainerViewModel()
            {
                Key = "Container",
                UnitHeight = 200,
                UnitWidth = 250,
                OffsetX = 300,
                OffsetY = 300,
                //Creating container nodes
                Nodes = new NodeCollection()
                {
                    new NodeViewModel()
                    {
                        UnitHeight = 70,
                        UnitWidth = 100,
                        OffsetX = 250,
                        OffsetY = 250,
                        Shape = this.Resources["Rectangle"],
                    },
                    new NodeViewModel()
                    {
                        UnitHeight = 70,
                        UnitWidth = 100,
                        OffsetX = 300,
                        OffsetY = 350,
                        Shape = this.Resources["Rectangle"],
                    },
                },
            };

            //Adding diagram elements to the stencil SymbolSource collection.
            (stencil.SymbolSource as SymbolCollection).Add(node);
            (stencil.SymbolSource as SymbolCollection).Add(connector);
            (stencil.SymbolSource as SymbolCollection).Add(group);
            (stencil.SymbolSource as SymbolCollection).Add(container);
        }
    }
}
