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

namespace NodeCreation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string selectedNode;
        public MainWindow()
        {
            InitializeComponent();
        }

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
            List<IGroupable> deleteableNodes = new List<IGroupable>();
            if (selectedNode != null)
            {
                foreach (NodeViewModel node in diagram.Nodes as IEnumerable<object>)
                {
                    if (node.ID.ToString().Equals(selectedNode.ToString()))
                    {
                        deleteableNodes.Add(node);
                    }
                }
            }
            if (deleteableNodes.Count > 0)
            {
                var parameter = new DeleteParameter() { Items = deleteableNodes };
                (diagram.Info as IGraphInfo).Commands.Delete.Execute(parameter);

            }
            else
                (diagram.Info as IGraphInfo).Commands.Delete.Execute(null);
        }
    }
}
