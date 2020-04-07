using OrgChart_with_drag_and_dropped_nodes.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OrgChart_with_drag_and_dropped_nodes.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        public DiagramVM()
        {
            Nodes = new ObservableCollection<CustomNode>();

            // Initialize Commands
            ItemAddedCommand = new Command(OnItemAdded);
            DropCommand = new Command(OnItemDrop);

            CreateNode("Node1", "");
            CreateNode("Node2", "Node1");
            CreateNode("Node3", "Node1");
            CreateNode("Node4", "Node1");
            CreateNode("Node5", "Node2");
            CreateNode("Node6", "Node2");
            CreateNode("Node11", "Node2");
            CreateNode("Node12", "Node2");
            CreateNode("Node13", "Node2");
            CreateNode("Node14", "Node2");
            CreateNode("Node7", "Node3");
            CreateNode("Node8", "Node3");
            CreateNode("Node9", "Node4");
            CreateNode("Node10", "Node4");

            CreateConnector("Node1", "Node2");
            CreateConnector("Node1", "Node3");
            CreateConnector("Node1", "Node4");
            CreateConnector("Node2", "Node5");
            CreateConnector("Node2", "Node6");
            CreateConnector("Node2", "Node11");
            CreateConnector("Node2", "Node12");
            CreateConnector("Node2", "Node13");
            CreateConnector("Node2", "Node14");
            CreateConnector("Node3", "Node7");
            CreateConnector("Node3", "Node8");
            CreateConnector("Node4", "Node9");
            CreateConnector("Node4", "Node10");


            LayoutManager = new LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    HorizontalSpacing = 50,
                    VerticalSpacing = 40,
                    Orientation = TreeOrientation.TopToBottom,
                    Type = LayoutType.Organization,
                },
            };
        }

        // Method used to add the Allowdrop constraints to the dropped node
        // Allowdrop constraints is used to allow the itemdropped event to get the element as target element.
        private void OnItemAdded(object obj)
        {
            ItemAddedEventArgs args = obj as ItemAddedEventArgs;
            if (args.Item is CustomNode)
            {
                (args.Item as CustomNode).Constraints = (args.Item as CustomNode).Constraints.Add(NodeConstraints.AllowDrop);
            }
        }


        // Mehtod to create relation between drag and dropped nodes
        private void OnItemDrop(object obj)
        {
            ItemDropEventArgs args = obj as ItemDropEventArgs;
            if (!(args.Target is SfDiagram))
            {
                foreach (object targetElement in args.Target as IEnumerable<object>)
                {
                    if (targetElement is CustomNode)
                    {
                        if ((args.Source as CustomNode).ParentId == null)
                        {
                            (args.Source as CustomNode).Id = "Node" + (Nodes as ObservableCollection<CustomNode>).Count.ToString();
                            (args.Source as CustomNode).ID = (args.Source as CustomNode).Id;
                            (args.Source as CustomNode).ParentId = (targetElement as CustomNode).Id;

                            CreateConnector((args.Source as CustomNode).ParentId, (args.Source as CustomNode).Id);
                            LayoutManager.Layout.UpdateLayout();
                        }
                    }
                }
            }

            else if (args.Target is SfDiagram)
            {
                if ((args.Source as CustomNode).ParentId == null)
                {
                    (args.Source as CustomNode).Id = "Node" + (Nodes as ObservableCollection<CustomNode>).Count.ToString();
                    (args.Source as CustomNode).ID = (args.Source as CustomNode).Id;
                    (args.Source as CustomNode).ParentId = "";
                    LayoutManager.Layout.UpdateLayout();
                }
            }
        }

        private void CreateConnector(string v1, string v2)
        {
            ConnectorViewModel con = new ConnectorViewModel()
            {
                SourceNodeID = v1,
                TargetNodeID = v2,
            };

            (Connectors as ConnectorCollection).Add(con);
        }

        private void CreateNode(string Id, string ParentId)
        {
            CustomNode node = new CustomNode()
            {
                UnitHeight = 40,
                UnitWidth = 100,
                Id = Id,
                ParentId = ParentId,
                ID = Id,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                ShapeStyle = App.Current.MainWindow.Resources["NodeStyle"] as Style,
            };
            (Nodes as ObservableCollection<CustomNode>).Add(node);
        }
    }
}
