using AutomaticLayout_MindmapLayout.Model;
using AutomaticLayout_MindmapLayout.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AutomaticLayout_MindmapLayout.ViewModel
{
    public class MindMapViewModel : DiagramViewModel
    {
        #region fields, properties and command

        ResourceDictionary resourceDictionary = new ResourceDictionary()
        {
            Source = new Uri(@"/Syncfusion.SfDiagram.Wpf;component/Resources/BasicShapes.xaml", UriKind.RelativeOrAbsolute)
        };

        public ICommand _AddLeftCommand;
        public ICommand _AddRightCommand;
        public ICommand _DeleteCommand;

        public ICommand AddLeftCommand
        {
            get { return _AddLeftCommand; }
            set { _AddLeftCommand = value; }
        }

        public ICommand AddRightCommand
        {
            get { return _AddRightCommand; }
            set { _AddRightCommand = value; }
        }

        public ICommand DeleteCommand
        {
            get { return _DeleteCommand; }
            set { _DeleteCommand = value; }
        }

        #endregion

        #region constructor

        public MindMapViewModel()
        {
            this.Tool = Tool.SingleSelect;
            this.Nodes = new NodeCollection();
            this.Connectors = new ObservableCollection<CustomMindMapConnector>();
            this.DefaultConnectorType = ConnectorType.CubicBezier;
            this.DataSourceSettings = new DataSourceSettings()
            {
                DataSource = this.GetMindmapDataItemCollection(),
                ParentId = "ParentId",
                Id = "Id"
            };
            this.LayoutManager = new LayoutManager()
            {
                Layout = new SfMindMapTreeLayout()
                {
                    HorizontalSpacing = 50,
                    VerticalSpacing = 30,
                    Orientation = Orientation.Horizontal,
                    SplitMode = MindMapTreeMode.Custom
                },
                RefreshFrequency = RefreshFrequency.ArrangeParsing
            };

            SelectedItems = new SelectorViewModel()
            {
                Commands = null,
                SelectorConstraints = SelectorConstraints.Default & ~SelectorConstraints.Rotator & ~SelectorConstraints.Resizer & ~SelectorConstraints.Pivot,
            };

            ItemAddedCommand = new DelegateCommand(OnItemAdded);
            ItemSelectedCommand = new DelegateCommand(OnItemSelected);
            ItemDeletingCommand = new DelegateCommand(OnItemDeleting);
            NodeChangedCommand = new DelegateCommand(OnNodeChanged);
            AddLeftCommand = new DelegateCommand(OnAddLeftChild);
            AddRightCommand = new DelegateCommand(OnAddRightChild);
            DeleteCommand = new DelegateCommand(OnItemDelete);
        }

        #endregion

        #region Helper Methods

        private void OnNodeChanged(object obj)
        {
            var args = obj as ChangeEventArgs<object, NodeChangedEventArgs>;
            if (args.NewValue.InteractionState == NodeChangedInteractionState.Dragged)
            {
                this.LayoutManager.Layout.InvalidateLayout();
            }

        }

        private void OnItemDeleting(object obj)
        {
            var args = obj as ItemDeletingEventArgs;
            var item = args.Item as NodeViewModel;
            if (item != null && (item.Content as MindmapDataItem).Parent != null)
            {
                (item.Content as MindmapDataItem).Parent.Children.Remove((item.Content as MindmapDataItem));
                args.DeleteDependentConnector = true;
                args.DeleteSuccessors = true;
            }
            else
            {
                args.Cancel = true;
            }
        }

        public MindmapDataItems GetMindmapDataItemCollection()
        {
            var dataItems = new MindmapDataItems();
            var Creativity = GetMindmapDataItem("Creativity", null);
            dataItems.Add(Creativity);
            var Brainstorming = GetMindmapDataItem("Brainstorming", Creativity);
            Brainstorming.Direction = RootChildDirection.Left;
            dataItems.Add(Brainstorming);
            var Complementing = GetMindmapDataItem("Complementing", Creativity);
            Complementing.Direction = RootChildDirection.Right;
            dataItems.Add(Complementing);
            var Sessions = GetMindmapDataItem("Sessions", Brainstorming);
            dataItems.Add(Sessions);
            var Generate = GetMindmapDataItem("Generate", Brainstorming);
            dataItems.Add(Generate);
            var Local = GetMindmapDataItem("Local", Sessions);
            dataItems.Add(Local);
            var Remote = GetMindmapDataItem("Remote", Sessions);
            dataItems.Add(Remote);
            var Individual = GetMindmapDataItem("Individual", Sessions);
            dataItems.Add(Individual);
            var Teams = GetMindmapDataItem("Teams", Sessions);
            dataItems.Add(Teams);
            var Ideas = GetMindmapDataItem("Ideas", Generate);
            dataItems.Add(Ideas);
            var Engagement = GetMindmapDataItem("Engagement", Generate);
            dataItems.Add(Engagement);
            var Product = GetMindmapDataItem("Product", Ideas);
            dataItems.Add(Product);
            var Service = GetMindmapDataItem("Service", Ideas);
            dataItems.Add(Service);
            var BusinessDirection = GetMindmapDataItem("Business Direction", Ideas);
            dataItems.Add(BusinessDirection);
            var Empowering = GetMindmapDataItem("Empowering", Engagement);
            dataItems.Add(Empowering);
            var Ownership = GetMindmapDataItem("Ownership", Engagement);
            dataItems.Add(Ownership);
            var Information = GetMindmapDataItem("Information", Complementing);
            dataItems.Add(Information);
            var Expectations = GetMindmapDataItem("Expectations", Complementing);
            dataItems.Add(Expectations);
            var Competitors = GetMindmapDataItem("Competitors", Information);
            dataItems.Add(Competitors);
            var Products = GetMindmapDataItem("Products", Information);
            dataItems.Add(Products);
            var Features = GetMindmapDataItem("Features", Information);
            dataItems.Add(Features);
            var OtherData = GetMindmapDataItem("Other Data", Information);
            dataItems.Add(OtherData);
            var Organization = GetMindmapDataItem("Organization", Expectations);
            dataItems.Add(Organization);
            var Customer = GetMindmapDataItem("Customer", Expectations);
            dataItems.Add(Customer);
            var Staff = GetMindmapDataItem("Staff", Expectations);
            dataItems.Add(Staff);
            var Stakeholders = GetMindmapDataItem("Stakeholders", Expectations);
            dataItems.Add(Stakeholders);
            return dataItems;
        }
        public MindmapDataItem GetMindmapDataItem(string label, MindmapDataItem parent)
        {
            MindmapDataItem item = new MindmapDataItem()
            {
                Label = label,
                Parent = parent
            };
            if (parent != null)
                parent.Children.Add(item);
            return item;
        }

        private void OnItemDelete(object obj)
        {
            (this.Info as IGraphInfo).Commands.Delete.Execute(null);
        }

        private void OnAddRightChild(object obj)
        {
            if (this.SelectedItems is SelectorViewModel sv)
            {
                if (sv.Nodes is ObservableCollection<object> nodes && nodes.Any())
                {
                    var parent = (nodes.First() as NodeViewModel).Content as MindmapDataItem;
                    var item = new MindmapDataItem()
                    {
                        Label = "New Child",
                        Parent = parent,
                        Direction = RootChildDirection.Right
                    };
                    parent.Children.Add(item);
                    (this.DataSourceSettings.DataSource as MindmapDataItems).Add(item);
                    this.LayoutManager.Layout.InvalidateLayout();
                }
            }
        }

        private void OnAddLeftChild(object obj)
        {
            if (this.SelectedItems is SelectorViewModel sv)
            {
                if (sv.Nodes is ObservableCollection<object> nodes && nodes.Any())
                {
                    var parent = (nodes.First() as NodeViewModel).Content as MindmapDataItem;
                    var item = new MindmapDataItem()
                    {
                        Label = "New Child",
                        Parent = parent,
                        Direction = RootChildDirection.Left
                    };
                    parent.Children.Add(item);
                    (this.DataSourceSettings.DataSource as MindmapDataItems).Add(item);
                    this.LayoutManager.Layout.InvalidateLayout();
                }
            }
        }

        private void OnItemSelected(object obj)
        {
            var args = obj as ItemSelectedEventArgs;

            if (this.SelectedItems is SelectorViewModel)
            {
                SelectorViewModel sv = this.SelectedItems as SelectorViewModel;
                if (sv.Nodes is ObservableCollection<object> nodes && nodes.Any())
                {
                    var layout = this.LayoutManager.Layout as SfMindMapTreeLayout;
                    var node = nodes.First() as NodeViewModel;
                    if (node == layout.LayoutRoot)
                    {
                        (this.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection()
                        {
                            new QuickCommandViewModel()
                            {
                                Shape = resourceDictionary["Ellipse"],
                                OffsetX = layout.Orientation == Orientation.Horizontal ? 0 : 0.5,
                                OffsetY = layout.Orientation == Orientation.Horizontal ? 0.5 : 0,
                                Command = AddLeftCommand,
                                Content = layout.Orientation == Orientation.Horizontal ? "M11.924,6.202 L4.633,6.202 L4.633,9.266 L0,4.633 L4.632,0 L4.632,3.551 L11.923,3.551 L11.923,6.202Z" : "m11.924,6.202l-7.291,0l0,3.064l-4.633,-4.633l4.632,-4.633l0,3.551l7.291,0l0,2.651l0.001,0z",
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = layout.Orientation == Orientation.Horizontal ? new Thickness(-20,0,0,0) : new Thickness(0,-20,0,0),
                            },

                            new QuickCommandViewModel()
                            {
                                Shape = resourceDictionary["Ellipse"],
                                OffsetX = layout.Orientation == Orientation.Horizontal ? 1 : 0.5,
                                OffsetY = layout.Orientation == Orientation.Horizontal ? 0.5 : 1,
                                Command = AddRightCommand,
                                Content = "M0,3.063 L7.292,3.063 L7.292,0 L11.924,4.633 L7.292,9.266 L7.292,5.714 L0.001,5.714 L0.001,3.063Z",
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = layout.Orientation == Orientation.Horizontal ? new Thickness(20,0,0,0) : new Thickness(0,20,0,0),
                            }
                        };
                    }
                    else
                    {
                        if (layout.Orientation == Orientation.Horizontal)
                        {
                            bool isLeftNode = (node.OffsetX < (layout.LayoutRoot as INode).OffsetX);
                            (this.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection()
                                {
                                    new QuickCommandViewModel()
                                    {
                                        Shape = resourceDictionary["Ellipse"],
                                        OffsetX = isLeftNode ? 0 : 1,
                                        OffsetY = 0.5,
                                        Command = isLeftNode?  AddLeftCommand : AddRightCommand,
                                        Content = isLeftNode? "M11.924,6.202 L4.633,6.202 L4.633,9.266 L0,4.633 L4.632,0 L4.632,3.551 L11.923,3.551 L11.923,6.202Z" : "M0,3.063 L7.292,3.063 L7.292,0 L11.924,4.633 L7.292,9.266 L7.292,5.714 L0.001,5.714 L0.001,3.063Z",
                                        HorizontalAlignment = HorizontalAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center,
                                        Margin = isLeftNode? new Thickness(-20,0,0,0): new Thickness(20,0,0,0),
                                    },

                                    new QuickCommandViewModel()
                                    {
                                        Shape = resourceDictionary["Ellipse"],
                                        OffsetX = isLeftNode? 1 : 0,
                                        OffsetY = 0.5,
                                        Command = DeleteCommand,
                                        Content = "M1.0000023,3 L7.0000024,3 7.0000024,8.75 C7.0000024,9.4399996 6.4400025,10 5.7500024,10 L2.2500024,10 C1.5600024,10 1.0000023,9.4399996 1.0000023,8.75 z M2.0699998,0 L5.9300004,0 6.3420029,0.99999994 8.0000001,0.99999994 8.0000001,2 0,2 0,0.99999994 1.6580048,0.99999994 z",
                                        HorizontalAlignment = HorizontalAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center,
                                        Margin = isLeftNode? new Thickness(20,0,0,0) : new Thickness(-20,0,0,0),
                                    }
                                };
                        }
                        else
                        {
                            bool isTopNode = node.OffsetY < (layout.LayoutRoot as INode).OffsetY;
                            (this.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection()
                                {
                                    new QuickCommandViewModel()
                                    {
                                        Shape = resourceDictionary["Ellipse"],
                                        OffsetX = 0.5,
                                        OffsetY = isTopNode? 0 : 1,
                                        Command = isTopNode ? AddLeftCommand : AddRightCommand,
                                        Content = "M4.0000001,0 L6,0 6,4.0000033 10,4.0000033 10,6.0000033 6,6.0000033 6,10 4.0000001,10 4.0000001,6.0000033 0,6.0000033 0,4.0000033 4.0000001,4.0000033 z",
                                        HorizontalAlignment = HorizontalAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center,
                                        Margin = isTopNode ? new Thickness(0,-20,0,0) : new Thickness(0,20,0,0),
                                    },

                                    new QuickCommandViewModel()
                                    {
                                        Shape = resourceDictionary["Ellipse"],
                                        OffsetX = 0.5,
                                        OffsetY = isTopNode ? 1 : 0,
                                        Command = DeleteCommand,
                                        Content = "M1.0000023,3 L7.0000024,3 7.0000024,8.75 C7.0000024,9.4399996 6.4400025,10 5.7500024,10 L2.2500024,10 C1.5600024,10 1.0000023,9.4399996 1.0000023,8.75 z M2.0699998,0 L5.9300004,0 6.3420029,0.99999994 8.0000001,0.99999994 8.0000001,2 0,2 0,0.99999994 1.6580048,0.99999994 z",
                                        HorizontalAlignment = HorizontalAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center,
                                        Margin = isTopNode ? new Thickness(0,20,0,0): new Thickness(0,-20,0,0),
                                    }
                                };
                        }
                    }
                }
            }
        }

        private void OnItemAdded(object obj)
        {
            var args = obj as ItemAddedEventArgs;

            if (args.Item != null && args.Item is NodeViewModel && args.ItemSource == ItemSource.ClipBoard)
            {
                (args.Item as NodeViewModel).IsSelected = true;
                (this.Info as IGraphInfo).Commands.Delete.Execute(null);
            }
            if (args.Item != null && args.Item is NodeViewModel && args.ItemSource != ItemSource.ClipBoard)
            {
                (args.Item as NodeViewModel).Annotations = null;
            }
        }

        #endregion
    }

    public class CustomMindMapConnector : ConnectorViewModel
    {
        public CustomMindMapConnector()
        {
            Constraints = ConnectorConstraints.Default & ~ConnectorConstraints.Selectable;
            Annotations = null;
        }
    }

    public class SfMindMapTreeLayout : MindMapTreeLayout
    {
        protected override RootChildDirection GetRootChildDirection(INode node)
        {
            if (node.Content is MindmapDataItem)
            {
                return (node.Content as MindmapDataItem).Direction;
            }

            return base.GetRootChildDirection(node);
        }
    }

    public class MindMapOrientation : ObservableCollection<Orientation>
    {
        public MindMapOrientation() : base()
        {
            this.Add(Orientation.Horizontal);
            this.Add(Orientation.Vertical);
        }
    }

    public class MindMapSplitMode : ObservableCollection<MindMapTreeMode>
    {
        public MindMapSplitMode() : base()
        {
            this.Add(MindMapTreeMode.RootChildrenCount);
            this.Add(MindMapTreeMode.TreeNodesCount);
            this.Add(MindMapTreeMode.Level);
            this.Add(MindMapTreeMode.Area);
            this.Add(MindMapTreeMode.Custom);
        }
    }
}
