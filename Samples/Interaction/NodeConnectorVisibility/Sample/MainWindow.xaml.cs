using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NodeConnectorVisibility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Initialize the node and connector collection 
            Diagram.Nodes = new NodeCollection();
            Diagram.Connectors = new ConnectorCollection();
            //Create nodes
            NodeVM node = new NodeVM()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 200,
                OffsetY = 200,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Constraints = NodeConstraints.Default | NodeConstraints.AllowDrop,
            };
            NodeVM node1 = new NodeVM()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 400,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Constraints = NodeConstraints.Default | NodeConstraints.AllowDrop,
            };
            NodeVM node2 = new NodeVM()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 600,
                OffsetY = 600,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Constraints = NodeConstraints.Default | NodeConstraints.AllowDrop,
            };
            //Add nodes into collection
            (Diagram.Nodes as NodeCollection).Add(node);
            (Diagram.Nodes as NodeCollection).Add(node1);
            (Diagram.Nodes as NodeCollection).Add(node2);

            //Create connector
            ConnectorVM con = new ConnectorVM()
            {
                SourceNode = node,
                TargetNode = node1,
                Constraints = ConnectorConstraints.Default | ConnectorConstraints.AllowDrop,
            };
            //Add connector into collection
            (Diagram.Connectors as ConnectorCollection).Add(con);
        }

        //Method to set visibility for node and connector.
        private void ChangeVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (((Diagram.Nodes as NodeCollection)[0] as NodeVM).NodeVisibility == Visibility.Visible)
            {
                ((Diagram.Nodes as NodeCollection)[0] as NodeVM).NodeVisibility = Visibility.Collapsed;
                ((Diagram.Connectors as ConnectorCollection)[0] as ConnectorVM).ConnectorVisibility = Visibility.Collapsed;
            }
            else
            {
                ((Diagram.Nodes as NodeCollection)[0] as NodeVM).NodeVisibility = Visibility.Visible;
                ((Diagram.Connectors as ConnectorCollection)[0] as ConnectorVM).ConnectorVisibility = Visibility.Visible;
            }
        }
    }

    /// <summary>
    /// Custom class to NodeViewModel for adding custom property for visibility handle of Node.
    /// </summary>
    public class NodeVM : NodeViewModel
    {
        private Visibility _NodeVisibility = Visibility.Visible;
        private NodeConstraints _nodeConstraints = NodeConstraints.Default;

        /// <summary>
        /// Gets or sets the visibility of node
        /// </summary>
        public Visibility NodeVisibility
        {
            get
            {
                return _NodeVisibility;
            }
            set
            {
                if(value == Visibility.Collapsed)
                {
                    _nodeConstraints = this.Constraints;
                    this.Constraints = NodeConstraints.None;
                }
                else
                {
                    this.Constraints = _nodeConstraints;
                }
                _NodeVisibility = value;
                OnPropertyChanged("NodeVisibility");
            }
        }
    }

    /// <summary>
    /// Custom class to ConnectorViewModel for adding custom property for visibility handle of Connector.
    /// </summary>
    public class ConnectorVM : ConnectorViewModel
    {
        private Visibility _ConnectorVisibility = Visibility.Visible;
        private ConnectorConstraints _conConstraints = ConnectorConstraints.Default;

        /// <summary>
        /// Gets or sets the visibility of node
        /// </summary>
        public Visibility ConnectorVisibility
        {
            get
            {
                return _ConnectorVisibility;
            }
            set
            {
                if (value == Visibility.Collapsed)
                {
                    _conConstraints = this.Constraints;
                    this.Constraints = ConnectorConstraints.None;
                }
                else
                {
                    this.Constraints = _conConstraints;
                }
                _ConnectorVisibility = value;
                OnPropertyChanged("ConnectorVisibility");
            }
        }
    }
}
