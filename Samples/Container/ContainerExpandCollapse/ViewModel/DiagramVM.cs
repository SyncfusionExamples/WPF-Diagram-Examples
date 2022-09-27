using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ContainerExpandCollapse
{
    public class DiagramVM : DiagramViewModel
    {
        public ICommand GroupExpandCollapseCommand { get; set; }
        public DiagramVM()
        {
            //Initialize the node, connector collection.
            this.Nodes = new ObservableCollection<CustomNode>();
            this.Connectors = new ConnectorCollection();
            this.Groups = new GroupCollection();
            //Initialize the rulers
            this.HorizontalRuler = new Ruler();
            this.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            //Initialize the snap settings.
            this.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.All,
            };

            #region Create and add nodes, connectors, Containers
            CustomNode start = new CustomNode()
            {
                OffsetX = 400,
                OffsetY = 100,
                UnitHeight = 50,
                UnitWidth = 50,
                Shape = Application.Current.MainWindow.Resources["Rectangle"],
                Annotations = new AnnotationCollection()
                {
                    new TextAnnotationViewModel()
                    {
                        Text = "Start",
                        Foreground = new SolidColorBrush(Colors.White),
                    }
                }
            };
            CustomNode subflowitem1 = new CustomNode()
            {
                UnitWidth = 50,
                UnitHeight = 50,
                OffsetX = 400,
                OffsetY = 250,
                Shape = Application.Current.MainWindow.Resources["Rectangle"],
                Annotations = new AnnotationCollection()
                {
                    new TextAnnotationViewModel()
                    {
                        Text = "Item1",
                        Foreground = new SolidColorBrush(Colors.White),
                    }
                }
            };
           
            CustomContainer subflow1 = new CustomContainer()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 250,
                ExpandedHeight = 100,
                ExpandedWidth = 100,
                Annotations = new AnnotationCollection()
                {
                    new TextAnnotationViewModel()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        TextVerticalAlignment = VerticalAlignment.Top,
                        Offset = new Point(0.5,0),
                        Text = "SubFlow 1",
                        Foreground = new SolidColorBrush(Colors.Black),
                    }
                },
                Nodes = new ObservableCollection<CustomNode>()
                {
                    subflowitem1
                },
            };
            CustomNode subflowItem2 = new CustomNode()
            {
                UnitWidth = 50,
                UnitHeight = 50,
                OffsetX = 400,
                OffsetY = 400,
                Shape = Application.Current.MainWindow.Resources["Rectangle"],
                Annotations = new AnnotationCollection()
                {
                    new TextAnnotationViewModel()
                    {
                        Text = "Item 2",
                        Foreground = new SolidColorBrush(Colors.White),
                    }
                }
            };
            
            CustomContainer subFlow2 = new CustomContainer()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 350,
                ExpandedHeight = 100,
                ExpandedWidth = 100,
                Annotations = new AnnotationCollection()
                {
                    new TextAnnotationViewModel()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        TextVerticalAlignment = VerticalAlignment.Top,
                        Offset = new Point(0.5,0),
                        Text = "Subflow 2",
                        Foreground = new SolidColorBrush(Colors.Black),
                    }
                },
                Nodes = new ObservableCollection<CustomNode>()
                {
                    subflowItem2
                },
            };
            CustomNode subFlowItem3= new CustomNode()
            {
                UnitWidth = 50,
                UnitHeight = 50,
                OffsetX = 400,
                OffsetY = 550,
                Shape = Application.Current.MainWindow.Resources["Rectangle"],
                Annotations = new AnnotationCollection()
                {
                    new TextAnnotationViewModel()
                    {
                        Text = "Item 3",
                        Foreground = new SolidColorBrush(Colors.White),
                    }
                },
            };
            CustomContainer subFlow3 = new CustomContainer()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 550,
                ExpandedHeight = 100,
                ExpandedWidth = 100,
                Annotations = new AnnotationCollection()
                {
                    new TextAnnotationViewModel()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        TextVerticalAlignment = VerticalAlignment.Top,
                        Offset = new Point(0.5,0),
                        Text = "Subflow 3",
                        Foreground = new SolidColorBrush(Colors.Black),
                    }
                },
                Nodes = new ObservableCollection<CustomNode>()
                {
                    subFlowItem3
                },
            };
            CustomNode end = new CustomNode()
            {
                UnitWidth = 50,
                UnitHeight = 50,
                OffsetX = 400,
                OffsetY = 700,
                Shape = Application.Current.MainWindow.Resources["Rectangle"],
                Annotations = new AnnotationCollection()
                {
                    new TextAnnotationViewModel()
                    {
                        Text = "End",
                        Foreground = new SolidColorBrush(Colors.White),
                    }
                },
            };
            (this.Nodes as ObservableCollection<CustomNode>).Add(start);
            (this.Groups as GroupCollection).Add(subflow1);
            (this.Groups as GroupCollection).Add(subFlow2);
            (this.Groups as GroupCollection).Add(subFlow3);
            (this.Nodes as ObservableCollection<CustomNode>).Add(end);

            ConnectorViewModel startToSubFLow1 = new ConnectorViewModel()
            {
                SourceNode = start,
                TargetNode = subflow1,
            };
            ConnectorViewModel subFlow1To2 = new ConnectorViewModel()
            {
                SourceNode = subflow1,
                TargetNode = subFlow2,
            };
            ConnectorViewModel subFlow2To3 = new ConnectorViewModel()
            {
                SourceNode = subFlow2,
                TargetNode = subFlow3,
            };
            ConnectorViewModel subFlow3ToEnd = new ConnectorViewModel()
            {
                SourceNode = subFlow3,
                TargetNode = end,
            };
           
            (this.Connectors as ConnectorCollection).Add(startToSubFLow1);
            (this.Connectors as ConnectorCollection).Add(subFlow1To2);
            (this.Connectors as ConnectorCollection).Add(subFlow2To3);
            (this.Connectors as ConnectorCollection).Add(subFlow3ToEnd);
            #endregion

            GroupExpandCollapseCommand = new DelegateCommand(OnGroupExpandCollapseCommand);
        }

        private void OnGroupExpandCollapseCommand(object parameter)
        {
            CustomContainer container = (parameter as Container).DataContext as CustomContainer;
            if (container.IsParentExpanded)
            {
                var groupNodes = container.Nodes;
                container.UnitHeight = 50;
                container.UnitWidth = 100;
                container.IsParentExpanded = false;
                foreach (CustomNode node in groupNodes as IEnumerable<object>)
                {
                    node.NodeVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                var groupNodes = container.Nodes;
                container.UnitHeight = container.ExpandedHeight;
                container.UnitWidth = container.ExpandedWidth;
                container.IsParentExpanded = true;
                foreach (CustomNode node in groupNodes as IEnumerable<object>)
                {
                    node.NodeVisibility = Visibility.Visible;
                }
            }
        }
    }

    public class BoolToGeometeryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is true)
            {
                return "M0,0L32,0 32,3.606 0,3.606z";
            }
            else
            {
                return "M14,0L18,0 18,14 32,14 32,18 18,18 18,32 14,32 14,18 0,18 0,14 14,14z";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomNode : NodeViewModel
    {
        private Visibility visibility = Visibility.Visible;

        public Visibility NodeVisibility
        {
            get
            {
                return visibility;
            }
            set
            {
                if (visibility != value)
                {
                    visibility = value;
                    OnPropertyChanged("NodeVisibility");
                }
            }
        }
    }

    public class CustomContainer : ContainerViewModel
    {
        private bool isExpanded = true;

        public bool IsParentExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsParentExpanded");
                }
            }
        }

        private double expandedWidth = 0;

        public double ExpandedWidth
        {
            get
            {
                return expandedWidth;
            }
            set
            {
                if (expandedWidth != value)
                {
                    expandedWidth = value;
                    OnPropertyChanged("ExpandedWidth");
                }
            }
        }

        private double expandedHeight = 0;

        public double ExpandedHeight
        {
            get
            {
                return expandedHeight;
            }
            set
            {
                if (expandedHeight != value)
                {
                    expandedHeight = value;
                    OnPropertyChanged("ExpandedHeight");
                }
            }
        }
    }

    public class CustomDiagram : SfDiagram
    {
        protected override Group GetGroupForItemOverride(object item)
        {
            return item is IContainer ? new Container() : base.GetGroupForItemOverride(item);
        }
        
    }
}
