using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;

using Syncfusion.UI.Xaml.Diagram.Utility;
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
using System.Xml.Linq;
//using Node = Syncfusion.UI.Xaml.Diagram.Node;

namespace NodeConnectorSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Here diagram is the instance of the SfDiagram.
            //Adding MouseMove Event
            diagram.MouseMove += Diagram_MouseMove;
        }


        /// <summary>
        /// Method for executing MouseMoveEvent
        /// </summary>
       
        private void Diagram_MouseMove(object sender, MouseEventArgs e)
        {
            //Caste the e.OriginalSource as Dependency Object.
            DependencyObject source = e.OriginalSource as DependencyObject;
            
            // Find the Visual Parent for Node
            var node = source.FindVisualParent<Node>();

            //Find the Visual Parnet for connector
            var connector = source.FindVisualParent<Connector>();


            if ((node != null))
            {
                //Get the NodeViewModel from the node.
                NodeViewModel nodeVM = node.DataContext as NodeViewModel;
            }
            if(connector != null) 
            { 
                //Get the ConnectorViewModel from the connector
                ConnectorViewModel connectorVM = connector.DataContext as ConnectorViewModel;
            }
        }
    }
}
