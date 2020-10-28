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
using System.Collections.Generic;

namespace AutomaticLayout_MindmapLayout.ViewModel
{
    public class MindMapViewModel : DiagramViewModel
    {
        #region fields, properties and command

        ResourceDictionary resourceDictionary = new ResourceDictionary()
        {
            Source = new Uri(@"/Syncfusion.SfDiagram.Wpf;component/Resources/BasicShapes.xaml", UriKind.RelativeOrAbsolute)
        };

        ICommand _AddLeftCommand;
        ICommand _AddRightCommand;
        ICommand _DeleteCommand;
        ICommand _ExpandCollapseCommand;

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

        public ICommand ExpandCollapseCommand
        {
            get { return _ExpandCollapseCommand; }
            set
            {
                if (_ExpandCollapseCommand != value)
                {
                    _ExpandCollapseCommand = value;
                }
            }
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
            this.Constraints = GraphConstraints.Default & ~GraphConstraints.Selectable;
            SelectedItems = new SelectorViewModel()
            {
                Commands = null,
                SelectorConstraints = SelectorConstraints.Default & ~SelectorConstraints.Rotator & ~SelectorConstraints.Resizer & ~SelectorConstraints.Pivot,
            };

            ItemAddedCommand = new DelegateCommand(OnItemAdded);
            NodeChangedCommand = new DelegateCommand(OnNodeChanged);
            ExpandCollapseCommand = new DelegateCommand(OnExpandCollaseCommand);
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// The method to execute the expand and collapse operation.
        /// </summary>
        /// <param name="args">The parent node to be expanded or collapsed</param>
        private void OnExpandCollaseCommand(object args)
        {
            if (args is Node && (args as Node).DataContext is NodeViewModel)
            {
                ExpandCollapseParameter obj = new ExpandCollapseParameter();
                obj.Node = (args as Node).DataContext as NodeViewModel;
                obj.IsUpdateLayout = true;
                obj.IsEnableAnimation = true;
                IGraphInfo graphinfo = Info as IGraphInfo;
                if (((args as Node).DataContext as NodeViewModel).IsExpanded)
                {
                    graphinfo.Commands.ExpandCollapse.Execute(obj);
                    ((args as Node).DataContext as NodeViewModel).IsExpanded = false;
                    ((args as Node).Content as MindmapDataItem).IsExpand = State.Collapse;
                }
                else
                {
                    graphinfo.Commands.ExpandCollapse.Execute(obj);
                    ((args as Node).DataContext as NodeViewModel).IsExpanded = true;
                    ((args as Node).Content as MindmapDataItem).IsExpand = State.Expand;
                }
                (((this.LayoutManager.Layout as MindMapTreeLayout).LayoutRoot as INode).Info as INodeInfo).BringIntoCenter();
            }
        }

        private void OnNodeChanged(object obj)
        {
            var args = obj as ChangeEventArgs<object, NodeChangedEventArgs>;
            var layout = this.LayoutManager.Layout as SfMindMapTreeLayout;
            NodeViewModel node = args.Item as NodeViewModel;
            if (layout.LayoutRoot != null)
            {
                if (node != layout.LayoutRoot)
                {
                    if (layout.Orientation == Orientation.Horizontal)
                    {
                        bool isLeftNode = (node.OffsetX < (layout.LayoutRoot as INode).OffsetX);
                        (node.Content as MindmapDataItem).Direction = isLeftNode ? RootChildDirection.Left : RootChildDirection.Right;
                    }
                    else
                    {
                        bool isTopNode = node.OffsetY < (layout.LayoutRoot as INode).OffsetY;
                        (node.Content as MindmapDataItem).Direction = isTopNode ? RootChildDirection.Top : RootChildDirection.Bottom;
                    }
                }
            }
            if (args.NewValue.InteractionState == NodeChangedInteractionState.Dragged)
            {
                this.LayoutManager.Layout.InvalidateLayout();
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
            return item;
        }
                        
        private void OnItemAdded(object obj)
        {
            var args = obj as ItemAddedEventArgs;

            if (args.Item != null && args.Item is NodeViewModel && args.ItemSource == ItemSource.ClipBoard)
            {
                (this.Info as IGraphInfo).Commands.Delete.Execute(null);
            }
            if (args.Item != null && args.Item is NodeViewModel && args.ItemSource != ItemSource.ClipBoard)
            {
                (args.Item as NodeViewModel).Annotations = null;
                (args.Item as NodeViewModel).Constraints = NodeConstraints.Default & ~NodeConstraints.Selectable;
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
