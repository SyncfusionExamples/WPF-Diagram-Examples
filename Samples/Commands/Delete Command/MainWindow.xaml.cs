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

namespace DeleteCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //To hold selected nodes.
        string selectedNode;
        //To hold selected connectors
        string selectedConnector;
        public MainWindow()
        {
            InitializeComponent();
            (diagram.Info as IGraphInfo).ItemDeletingEvent += MainWindow_ItemDeletingEvent;
        }

        private void MainWindow_ItemDeletingEvent(object sender, DiagramPreviewEventArgs args)
        {
            //For Deleting Node Without its Dependent Connector
            (args as ItemDeletingEventArgs).DeleteDependentConnector = false;
        }

        //Method to add selected node's ID.
        private void nodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           int selectedIndex = (sender as ComboBox).SelectedIndex;
            if (selectedIndex == 0)
                selectedNode = "Node1";
            else if (selectedIndex == 1)
                selectedNode = "Node2";
            else if (selectedIndex == 2)
                selectedNode = "Node3";
            else if (selectedIndex == 3)
                selectedNode = "Node4";
            else
                selectedNode = "None";
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            List<IGroupable> deleteableObjects = new List<IGroupable>();

            //Finding selected nodes based on IDs.
            if (selectedNode != null)
            {
                foreach (NodeViewModel node in diagram.Nodes as IEnumerable<object>)
                {
                    if (node.ID.ToString().Equals(selectedNode.ToString()))
                    {
                        deleteableObjects.Add(node);
                    }
                }
            }

            //Finding selected connectors based on IDs.
            if (selectedConnector != null)
            {
                foreach (ConnectorViewModel connector in diagram.Connectors as IEnumerable<object>)
                {
                    if (connector.ID.ToString().Equals(selectedConnector.ToString()))
                    {
                        deleteableObjects.Add(connector);
                    }
                }
            }

            if (deleteableObjects.Count > 0)
            {
                //Adding deleteable objects to the DeleteParameter class using Items property.
                var parameter = new DeleteParameter() { Items = deleteableObjects };
                //Executing delete command with DeleteParameter items.
                (diagram.Info as IGraphInfo).Commands.Delete.Execute(parameter);
            }
            else
                //Executing delete command with null value and currently selected items will be deleted.
                (diagram.Info as IGraphInfo).Commands.Delete.Execute(null);
        }

        //Method to add selected connector's ID.
        private void Connectors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = (sender as ComboBox).SelectedIndex;
            if (selectedIndex == 0)
                selectedConnector = "1to2";
            else if (selectedIndex == 1)
                selectedConnector = "2to4";
            else if (selectedIndex == 2)
                selectedConnector = "4to3";
            else if (selectedIndex == 3)
                selectedConnector = "3to1";
            else
                selectedConnector = "None";
        }
    }
}
