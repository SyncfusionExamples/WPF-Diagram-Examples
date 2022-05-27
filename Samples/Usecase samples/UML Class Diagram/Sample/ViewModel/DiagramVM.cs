using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.UI.Xaml.Diagram.Theming;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Class_Diagram.ViewModel
{
    // ViewModel Class for SfDiagram
    public class DiagramVM : DiagramViewModel
    {
        // Constructor
        public DiagramVM()
        {
            //Adding ItemAddedCommand, DropCommand, SelectorChangedCommand, SelectorChangedCommand
            ItemAddedCommand = new DelegateCommand(OnItemAddedCommand);
            DropCommand = new DelegateCommand(OnDropCommand);
            SelectorChangedCommand = new DelegateCommand(OnSelectorChangedCommand);
            ItemDeletingCommand = new DelegateCommand(OnItemDeletingCommand);
            //Removing quick commands, rotators from selector.
            this.SelectedItems = new SelectorViewModel()
            {
                SelectorConstraints = SelectorConstraints.Default & ~(SelectorConstraints.QuickCommands | SelectorConstraints.Rotator),
            };

            //Adding vertical and horizontal rulers.
            VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = System.Windows.Controls.Orientation.Vertical,
            };

            HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = System.Windows.Controls.Orientation.Vertical,
            };

            //Adding gridlines to diagram.
            SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.All,
            };

            //Defining default connector type as line.
            DefaultConnectorType = ConnectorType.Line;

            //Initialize nodes, groups and connectors collection
            Nodes = new NodeCollection();
            Connectors = new ConnectorCollection();
            Groups = new GroupCollection();

            //Adding Office Theme to the diagram 
            DiagramTheme theme = new OfficeTheme();
            this.Theme = theme;

            //Creating car nodes and adding nodes into nodes collection.
            NodeViewModel car = CreateNodes(150, 40, 500, 200, "Car");
            (this.Nodes as NodeCollection).Add(car);

            NodeViewModel carInfo = CreateNodes(150, 20, 500, 230, "Car Info");
            (this.Nodes as NodeCollection).Add(carInfo);

            NodeViewModel carDetails = CreateNodes(150, 20, 500, 250, "Car Details");
            (this.Nodes as NodeCollection).Add(carDetails);

            //Creating group for Car related nodes and adding group to GroupsCollection.
            GroupViewModel CarGroup = new GroupViewModel()
            {
                Nodes = new NodeCollection()
                {
                    car,
                    carInfo,
                    carDetails,
                },
            };

            (this.Groups as GroupCollection).Add(CarGroup);

            //Creating body parts nodes and adding nodes into nodes collection.
            NodeViewModel BodyParts = CreateNodes(150, 40, 300, 500, "Body Parts");
            (this.Nodes as NodeCollection).Add(BodyParts);

            NodeViewModel BodyPartsInfo = CreateNodes(150, 20, 300, 530, "Body Parts Info");
            (this.Nodes as NodeCollection).Add(BodyPartsInfo);

            //Creating group for body parts related nodes and adding group to GroupsCollection.
            GroupViewModel BodyPartsGroup = new GroupViewModel()
            {
                Nodes = new NodeCollection()
                {
                    BodyParts,
                    BodyPartsInfo,
                },
            };

            (this.Groups as GroupCollection).Add(BodyPartsGroup);

            //Creating nodes for other parts related nodes and adding nodes to NodesCollection.
            NodeViewModel OtherParts = CreateNodes(150, 40, 500, 500, "Other Parts");
            (this.Nodes as NodeCollection).Add(OtherParts);

            NodeViewModel OtherPartsInfo = CreateNodes(150, 20, 500, 530, "Others Parts Info");
            (this.Nodes as NodeCollection).Add(OtherPartsInfo);

            //Creating group for other parts related nodes and adding nodes to GroupsCollection.
            GroupViewModel OtherPartsGroup = new GroupViewModel()
            {
                Nodes = new NodeCollection()
                {
                    OtherParts,
                    OtherPartsInfo,
                },
            };

            (this.Groups as GroupCollection).Add(OtherPartsGroup);

            //Creating nodes for engine parts related nodes and adding nodes to NodesCollection.
            NodeViewModel Engine = CreateNodes(150, 40, 700, 500, "Engine Parts");
            (this.Nodes as NodeCollection).Add(Engine);

            NodeViewModel EngineInfo = CreateNodes(150, 20, 700, 530, "Engine Parts Info");
            (this.Nodes as NodeCollection).Add(EngineInfo);

            GroupViewModel EngineGroup = new GroupViewModel()
            {
                Nodes = new NodeCollection()
                {
                    Engine,
                    EngineInfo,
                },
            };

            (this.Groups as GroupCollection).Add(EngineGroup);

            //Creating connectors.
            ConnectorViewModel CarToBodyParts = CreateConnector(BodyPartsGroup, CarGroup);
            ConnectorViewModel CarToOtherParts = CreateConnector(OtherPartsGroup, CarGroup);
            ConnectorViewModel CarToEngine = CreateConnector(EngineGroup, CarGroup);
            //Adding connector to the collection.
            (this.Connectors as ConnectorCollection).Add(CarToBodyParts);
            (this.Connectors as ConnectorCollection).Add(CarToOtherParts);
            (this.Connectors as ConnectorCollection).Add(CarToEngine);
        }

        //Method to execute ItemaddedCommand.
        private void OnItemAddedCommand(object args)
        {
            ItemAddedEventArgs parameter = args as ItemAddedEventArgs;
            if (parameter.Item is NodeViewModel)
            {
                //Enable AllowDrop constraint to all nodes.
                (parameter.Item as NodeViewModel).Constraints |= NodeConstraints.AllowDrop;
            }
        }

        //Method to execute DropCommand.
        private void OnDropCommand(object args)
        {
            ItemDropEventArgs parameter = args as ItemDropEventArgs;
            if (parameter.Source is NodeViewModel && parameter.ItemSource == Cause.Stencil)
            {
                if (!(parameter.Target is SfDiagram))
                {
                    foreach (object targetElement in parameter.Target as IEnumerable<object>)
                    {
                        //Adding memeber node to the group element where it is dropped
                        if (targetElement is GroupViewModel)
                        {
                            NodeViewModel sourceNode = parameter.Source as NodeViewModel;
                            sourceNode.OffsetX = (targetElement as GroupViewModel).OffsetX;
                            sourceNode.OffsetY = ((targetElement as GroupViewModel).Info as IGroupInfo).Bounds.Bottom+10;
                            ((targetElement as GroupViewModel).Nodes as NodeCollection).Add(sourceNode);
                        }
                    }
                }
            }
        }

        //Method to execute SelectorChangedCommand.
        private void OnSelectorChangedCommand(object args)
        {
            if ((args as SelectorChangedEventArgs).NewValue.InteractionState == NodeChangedInteractionState.Dragging)
            {
                var item = ((args as SelectorChangedEventArgs).Item as SelectorViewModel).Nodes;
                if ((item as IEnumerable<object>).Count() > 0)
                {
                    foreach (NodeViewModel node in item as IEnumerable<object>)
                    {
                        if (node.ParentGroup != null && node.ParentGroup is GroupViewModel)
                        {
                            //Restricting node dragging from group.
                            (args as SelectorChangedEventArgs).Cancel = true;
                        }
                    }
                }
            }
        }

        //Method to execute ItemDeletingCommand.
        private void OnItemDeletingCommand(object parameter)
        {
            Syncfusion.UI.Xaml.Diagram.ItemDeletingEventArgs args = parameter as Syncfusion.UI.Xaml.Diagram.ItemDeletingEventArgs;
            if (args.Item is NodeViewModel)
            {
                NodeViewModel node = args.Item as NodeViewModel;
                if (node.ParentGroup != null && node.ParentGroup is GroupViewModel)
                {
                    //Rearranging child nodes of group when its any elements is being deleted.
                    GroupViewModel parentGroup = node.ParentGroup as GroupViewModel;
                    foreach (NodeViewModel child in parentGroup.Nodes as IEnumerable<object>)
                    {
                        if (child.OffsetY > node.OffsetY)
                        {
                            child.OffsetY -= node.UnitHeight;
                        }
                    }
                }
            }
        }

        //Method to create nodes.
        private NodeViewModel CreateNodes(double width, double height, double offsetx, double offsety, string label)
        {
            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = height,
                UnitWidth = width,
                OffsetX = offsetx,
                OffsetY = offsety,
                Shape = App.Current.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new TextAnnotationViewModel
                    {
                        Text = label
                    }
                }
            };

            return node;
        }

        //Method to create Connectors.
        private ConnectorViewModel CreateConnector(IGroupable sourceNode, IGroupable targetNode)
        {
            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourceNode = sourceNode,
                TargetNode = targetNode,
                ConnectorGeometryStyle = App.Current.Resources["ConnectorGeometryPathStyle"] as Style,
                TargetDecorator = App.Current.Resources["ClosedSharp"], 
                TargetDecoratorStyle = App.Current.Resources["DecoratorFillStyle"] as Style,
            };

            return connector;
        }
    }

    /// <summary>
    /// Represents a class to add symbol filters and selected filetrs to the Stencil.
    /// </summary>
    public class StencilVM : INotifyPropertyChanged
    {
        public StencilVM()
        {
            Symbolfilters = new SymbolFilters();

            SymbolFilterProvider node4 = new SymbolFilterProvider { Content = "DataFlow Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node2 = new SymbolFilterProvider { Content = "UML Class", SymbolFilter = Filter };
            SymbolFilterProvider node5 = new SymbolFilterProvider { Content = "UMLActivity Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node6 = new SymbolFilterProvider { Content = "UMLUseCase Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node7 = new SymbolFilterProvider { Content = "UMLRelationship Shapes", SymbolFilter = Filter };

            this.Symbolfilters.Add(node2);
            this.Symbolfilters.Add(node4);
            this.Symbolfilters.Add(node5);
            this.Symbolfilters.Add(node6);
            this.Symbolfilters.Add(node7);
            this.Selectedfilter = Symbolfilters[0];
        }

        // Define filtering of Symbols
        private bool Filter(SymbolFilterProvider sender, object symbol)
        {
            if (symbol is NodeViewModel && (symbol as NodeViewModel).ParentGroup == null)
            {
                if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
                    return true;
            }
            if (symbol is LaneViewModel)
            {
                if (sender.Content.ToString() == (symbol as LaneViewModel).Key.ToString())
                    return true;
            }
            if (symbol is PhaseViewModel)
            {
                if (sender.Content.ToString() == (symbol as PhaseViewModel).Key.ToString())
                    return true;
            }
            if (symbol is ConnectorViewModel)
            {
                if (sender.Content.ToString() == (symbol as ConnectorViewModel).Key.ToString())
                    return true;
            }
            if (sender.Content.ToString() == "All")
            {
                return true;
            }
            return false;
        }
        public ObservableCollection<SymbolFilterProvider> Symbolfilters { get; set; }

        public SymbolFilterProvider Selectedfilter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
