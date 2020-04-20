using Collision_State.Utilities;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Collision_State.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        NodeViewModel IgnoredNode;
        NodeViewModel CollisionNode1;
        NodeViewModel CollisionNode2;
        NodeViewModel IntersectingNode;
        ConnectorViewModel CollisionConnector1;
        ConnectorViewModel CollisionConnector2;
        public DiagramVM()
        {
            Theme = new OfficeTheme();
            CollisionNode1 = CreateNode(100, 100, 650, 100,"CollisionNode1");
            CollisionNode2 = CreateNode(100, 100, 650, 350, "CollisionNode2");
            IgnoredNode = CreateNode(100, 100, 650, 650,"IgnoreNode");
            IntersectingNode = CreateNode(60, 60, 100, 100,"Drag object");

            (Nodes as NodeCollection).Add(CollisionNode1);
            (Nodes as NodeCollection).Add(CollisionNode2);
            (Nodes as NodeCollection).Add(IgnoredNode);
            (Nodes as NodeCollection).Add(IntersectingNode);

            CollisionConnector1 = new ConnectorViewModel()
            {
                SourceNode = CollisionNode1,
                TargetNode = CollisionNode2,
            };

            CollisionConnector2 = new ConnectorViewModel()
            {
                SourceNode = CollisionNode2,
                TargetNode = IgnoredNode,
            };

            (Connectors as ConnectorCollection).Add(CollisionConnector1);
            (Connectors as ConnectorCollection).Add(CollisionConnector2);

            SelectorChangedCommand = new Command(OnSelectorChanged);
        }

        private void OnSelectorChanged(object obj)
        {
            SelectorChangedEventArgs args = obj as SelectorChangedEventArgs;
            // Need to adjust selected node's position, if it in contact with any other elements on drag complete
            if (args.Item is SelectorViewModel && args.NewValue.InteractionState == NodeChangedInteractionState.Dragged)
            {
                var selectorViewModel = (SelectorViewModel)args.Item;
                if (selectorViewModel.Nodes is IEnumerable<object>)
                {
                    var selectedNodes = ((IEnumerable<object>)selectorViewModel.Nodes).ToList();
                    if (selectedNodes.Count == 1 && selectedNodes[0] is NodeViewModel)
                    {
                        var selectedNode = (NodeViewModel)selectedNodes[0];
                        var collisionState = new CollisionState() { Item = selectedNode };

                        // Finding overlapping nodes & connectors for the selected node.
                        var intercepts = ((IGraphInfo)this.Info).GetOverlappingObjects(collisionState);

                        foreach (var intercept in intercepts)
                        {
                            if (intercept is NodeViewModel && (intercept as NodeViewModel).Content.ToString() == "CollisionNode2")
                            {
                                var intersectingNode = (NodeViewModel)intercept;
                                var collisionState1 = new CollisionState() { Item = intersectingNode };
                                ((IGraphInfo)this.Info).GetCollisionFreeLocation(collisionState1);

                                // Re-arranging CollisionNode's position
                                intersectingNode.OffsetX = collisionState1.Offset.X;
                                intersectingNode.OffsetY = collisionState1.Offset.Y;
                            }
                            else if ((intercept is NodeViewModel && (intercept as NodeViewModel).Content.ToString() == "CollisionNode1")
                                || intercept is ConnectorViewModel )
                            {
                                ((IGraphInfo)this.Info).GetCollisionFreeLocation(collisionState);

                                // Re-arranging Drag Object's position
                                selectedNode.OffsetX = collisionState.Offset.X;
                                selectedNode.OffsetY = collisionState.Offset.Y;
                            }
                            else if(intercept is NodeViewModel && (intercept as NodeViewModel).Content.ToString() == "IgnoreNode")
                            {
                                var collisionstate2 = new CollisionState() { Item = selectedNode, IgnoreList = new ObservableCollection<object>() { intercept } };
                                ((IGraphInfo)this.Info).GetCollisionFreeLocation(collisionstate2);

                                selectedNode.OffsetX = collisionstate2.Offset.X;
                                selectedNode.OffsetY = collisionstate2.Offset.Y;
                            }
                        }
                    }
                }
            }
        }

        private NodeViewModel CreateNode(double height, double width, double offsetx, double offsety,string content)
        {
            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = height,
                UnitWidth = width,
                OffsetX = offsetx,
                OffsetY = offsety,
                Content = content,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
            };

            return node;
        }
    }
}
