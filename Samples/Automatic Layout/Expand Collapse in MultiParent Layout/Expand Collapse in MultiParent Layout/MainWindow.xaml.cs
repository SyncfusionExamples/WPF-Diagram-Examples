using Syncfusion.UI.Xaml.Diagram;
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

namespace Expand_Collapse_in_MultiParent_Layout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<object> SelectedNodeCollection => (sfdiagram.SelectedItems as SelectorViewModel)?.Nodes as ObservableCollection<object>;

        private ObservableCollection<NodeViewModel> NodeCollection => sfdiagram.Nodes as ObservableCollection<NodeViewModel>;

        public ObservableCollection<ItemInfo> AllCards => sfdiagram.DataSourceSettings.DataSource as ObservableCollection<ItemInfo>;

        private ObservableCollection<ConnectorVM> ConnectorsCollection => sfdiagram.Connectors as ObservableCollection<ConnectorVM>;

        private void ExpandCollapse_Click(object sender, RoutedEventArgs e)
        {
            var workingCollection = SelectedNodeCollection;
            var selectednode = (sfdiagram.SelectedItems as SelectorViewModel).SelectedItem;
            ItemInfo obj = null;
            if(selectednode != null && selectednode is NodeViewModel)
            {
                obj = (ItemInfo)(selectednode as NodeViewModel).Content;
            }

            if (obj != null)
            {
                workingCollection = new ObservableCollection<object> { NodeCollection.FirstOrDefault(j => j.Content == obj) };
            }
            if (workingCollection.Count <= 0) return;
            foreach (NodeViewModel selectedItem in workingCollection) //Multiple Nodes can be selected and their child node can be collapsed/expanded at the same time
            {
                var expandCollapseParameter = new ExpandCollapseParameter
                {
                    Node = selectedItem,
                    IsUpdateLayout = true
                };
                var graphInfo = sfdiagram.Info as IGraphInfo;
                var isExpanded = selectedItem.IsExpanded; // Without this get operation, the IsExpanded property of the node is not updated.
                graphInfo?.Commands.ExpandCollapse.Execute(expandCollapseParameter);

                var currentCardTree = new List<ItemInfo>();
                var content = selectedItem.Content as ItemInfo;
                currentCardTree.AddRange(FindChildren(AllCards.ToList(), content));

                var childNodes = NodeCollection.Where(j => currentCardTree.Contains(j.Content)).ToList();
                var childConnectors = ConnectorsCollection.Where(j => childNodes.Contains(j.TargetNode)).ToList();
                childConnectors.ForEach(j => j.IsVisible = !isExpanded);
            }
        }

        private List<ItemInfo> FindChildren(List<ItemInfo> source, ItemInfo root)
        {
            return source.Where(j => j.ReportingPerson.Contains(root.Name))
                .Union(source.Where(j => j.ReportingPerson.Contains(root.Name)).SelectMany(j => FindChildren(source, j)))
                .ToList();
        }
    }
}
