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

namespace ConnectorPadding
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

            NodeViewModel sourcenode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = 300,
                OffsetY = 200,
                Shape = this.Resources["Rectangle"],
            };

            NodeViewModel targetenode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = 500,
                OffsetY = 200,
                Shape = this.Resources["Rectangle"],
            };

            (Diagram.Nodes as NodeCollection).Add(sourcenode);
            (Diagram.Nodes as NodeCollection).Add(targetenode);

            ConnectorViewModel nodeToNodeConnection = new ConnectorViewModel()
            {
                SourceNode = sourcenode,
                TargetNode = targetenode,
                //Update the padding for connector
                SourcePadding = 5,
                TargetPadding = 5,
            };

            (Diagram.Connectors as ConnectorCollection).Add(nodeToNodeConnection);
        }
    }
}
