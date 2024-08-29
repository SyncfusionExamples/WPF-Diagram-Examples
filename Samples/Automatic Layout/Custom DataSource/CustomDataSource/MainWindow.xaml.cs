using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Reflection.Emit;
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
using CustomDataSource.Model;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using Syncfusion.Windows.Shared;

namespace CustomDataSource
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();
            diagram.HorizontalRuler = new Ruler();
            diagram.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };
            (diagram.Info as IGraphInfo).ViewPortChangedEvent += MainWindow_ViewPortChangedEvent;

        }

        private void MainWindow_ViewPortChangedEvent(object sender, ChangeEventArgs<object, ScrollChanged> args)
        {
            if (diagram.IsLoaded)
            {
                diagram.ViewPortCentreX = args.NewValue.ViewPort.Width / 2;
                diagram.ViewPortCentreY = args.NewValue.ViewPort.Y;
            }
        }

        private void diagram_Loaded(object sender, RoutedEventArgs e)
        {
            diagram.CustomData = GetData();

            CustomUpdateLayout();

            (diagram.Info as IGraphInfo).Commands.FitToPage.Execute(null);
        }

        #region Helper Methods

        private void CustomUpdateLayout()
        {
            SetLevelForNodeData();

            CreateDiagramUsingData();

            UpdatePosition();

            CreateConnection();
        }

        /// <summary>
        /// Set the DataItems for the Layout.
        /// </summary>
        private DataItems GetData()
        {
            DataItems ItemsInfo = new DataItems();

            ItemsInfo.Add(new ItemInfo()
            {
                Id = "1",
                Name = "Start",
                Text = "1",
            });

            ItemsInfo.Add(new ItemInfo()
            {
                Id = "2",
                Name = "Invoice Entry",
                ParentId = new List<string> { "1" },
                Text = "1",
            });

            ItemsInfo.Add(new ItemInfo()
            {
                Id = "3",
                Name = "Confirm Payment Received",
                ParentId = new List<string> { "1" },
                BranchDirection = "Right",
                Text = "1",
            });

            ItemsInfo.Add(new ItemInfo()
            {
                Id = "4",

                Name = "Check Customer Payment",
                ParentId = new List<string> { "2" },
                Text = "1",

            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "5",

                Name = "Credit Memo Entry",
                ParentId = new List<string> { "4" },
                Text = "1",

            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "6",
                Name = "Refund Customer",
                ParentId = new List<string> { "5" },
                Text = "1",

            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "8",
                Name = "Refund with standard Voucher",
                ParentId = new List<string> { "3" },

                Text = "5",

            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "9",
                Name = "Complete Payment Customer Memo",
                ParentId = new List<string> { "8" },
                Text = "5",

            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "10",
                Name = "Reject",
                ParentId = new List<string> { "9" },
                BranchDirection = "Right",
                Text = "4",

            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "11",
                Name = "Approve Invoice",
                ParentId = new List<string> { "9" },
                Text = "5",

            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "12",
                Name = "Credit Memo Creation",
                ParentId = new List<string> { "11" },
                Text = "1",


            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "13",
                Name = "Fill Credit Memo",
                ParentId = new List<string> { "12" },
                Text = "1",

            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "14",
                Name = "Re-Issuing the invoice",
                ParentId = new List<string> { "13", "6" },
                BranchDirection = "Left",
                Text = "1",
            });
            ItemsInfo.Add(new ItemInfo()
            {
                Id = "15",
                Name = "End",
                ParentId = new List<string> { "14" },
                Text = "4",
            });
            return ItemsInfo;
        }

        /// <summary>
        /// Set Level for each nodes to render them based on the level used to position the nodes.
        /// </summary>
        private void SetLevelForNodeData()
        {
            foreach (ItemInfo parentItem in diagram.CustomData as IEnumerable<object>)
            {
                if ((parentItem as ItemInfo).ParentId == null)
                {
                    (parentItem as ItemInfo).level = "Root";
                }
                else
                {
                    foreach (var parent in (parentItem as ItemInfo).ParentId)
                    {
                        foreach (ItemInfo childItem in diagram.CustomData as IEnumerable<object>)
                        {
                            if ((childItem as ItemInfo).Id == parent)
                            {
                                if ((childItem as ItemInfo).level == "Root")
                                {
                                    (parentItem as ItemInfo).level = "Level1";
                                }
                                else
                                {
                                    string substring = (childItem as ItemInfo).level.Substring(5);
                                    int substringvalue = int.Parse(substring);
                                    substringvalue += 1;
                                    (parentItem as ItemInfo).level = "Level" + substringvalue.ToString();

                                }                              
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Create Node based on the Data.
        /// </summary>
        private void CreateDiagramUsingData()
        {
            foreach (ItemInfo item in diagram.CustomData as IEnumerable<object>)
            {
                NodeViewModel node = new NodeViewModel()
                {
                    UnitHeight = 60,
                    UnitWidth = 250,
                    Content = item,
                };

                if (node != null)
                {
                    if ((node.Content as ItemInfo).Id.Equals("1") || (node.Content as ItemInfo).Id.ToString().Equals("15"))
                    {
                        //Updating content template to the start and end nodes
                        node.ContentTemplate = Application.Current.MainWindow.Resources["EllipseNodeContentTemplate"] as DataTemplate;
                    }
                    else
                    {
                        //Updating content template to the node

                        node.ContentTemplate = Application.Current.MainWindow.Resources["newCretedNode"] as DataTemplate;
                    }
                }

                (diagram.Nodes as NodeCollection).Add(node);
            }
        }

        /// <summary>
        /// Update the positions for the nodes
        /// </summary>
        private void UpdatePosition()
        {
            foreach(NodeViewModel node in diagram.Nodes as IEnumerable<object>)
            {
                if ((node.Content as ItemInfo).ParentId == null)
                {
                    node.OffsetX = diagram.ViewPortCentreX;
                    node.OffsetY = diagram.ViewPortCentreY + 100;
                }
                else
                {

                    foreach (NodeViewModel nodes in diagram.Nodes as IEnumerable<object>)
                    {
                        List<int> ParentIDs = new List<int>();
                        string ParentID = string.Empty;
                        foreach (var parentID in (node.Content as ItemInfo).ParentId)
                        {
                            ParentIDs.Add(int.Parse(parentID.ToString()));
                            ParentID = ParentIDs.Max().ToString();
                        }

                        //This condition for update the offset value for the node from the parent node. 
                        if (ParentID == ((nodes as NodeViewModel).Content as ItemInfo).Id)
                        {
                            if ((node.Content as ItemInfo).BranchDirection == "Right")
                            {
                                node.OffsetX = (nodes as NodeViewModel).OffsetX + ((nodes as NodeViewModel).UnitWidth / 2) + diagram.HorizontalSpacing + (node.UnitWidth / 2);
                                node.OffsetY = (nodes as NodeViewModel).OffsetY + ((nodes as NodeViewModel).UnitHeight / 2) + diagram.VerticalSpacing + (node.UnitHeight / 2);
                            }
                            else if ((node.Content as ItemInfo).BranchDirection == "Left")
                            {
                                node.OffsetX = (nodes as NodeViewModel).OffsetX - ((nodes as NodeViewModel).UnitWidth / 2) - diagram.HorizontalSpacing - (node.UnitWidth / 2);
                                node.OffsetY = (nodes as NodeViewModel).OffsetY + ((nodes as NodeViewModel).UnitHeight / 2) + diagram.VerticalSpacing + (node.UnitHeight / 2);
                            }
                            else
                            {
                                node.OffsetX = (nodes as NodeViewModel).OffsetX;
                                node.OffsetY = (nodes as NodeViewModel).OffsetY + ((nodes as NodeViewModel).UnitHeight / 2) + diagram.VerticalSpacing + (node.UnitHeight / 2);
                            }
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// Create the connection between the nodes
        /// </summary>
        private void CreateConnection()
        {
            foreach (NodeViewModel node in diagram.Nodes as IEnumerable<object>)
            {
                node.ID = (node.Content as ItemInfo).Id;
                if ((node.Content as ItemInfo).ParentId != null)
                {
                    foreach (var ParentID in (node.Content as ItemInfo).ParentId)
                    {
                        ConnectorViewModel nodeToNodeConnection = new ConnectorViewModel()
                        {

                            SourceNodeID = ParentID,
                            TargetNodeID = (node.Content as ItemInfo).Id,
                            Annotations = new ObservableCollection<IAnnotation>()
                            {
                                new AnnotationEditorViewModel()
                                {
                                    Content="1"
                                }
                            }
                        };

                        (diagram.Connectors as ConnectorCollection).Add(nodeToNodeConnection);

                    }
                }
            }
        }

        #endregion        
    }

    public class CustomDiagram : SfDiagram
    {
        private DataItems _customData;
        private double _horizontalSpacing = 60;
        private double _verticalSpacing = 100;
        private double _viewPortCentreX = 0;
        private double _viewPortCentreY = 0;

        public DataItems CustomData 
        { 
            get
            {
                return _customData;
            }
            set
            {
                _customData = value;
                OnPropertyChanged(nameof(CustomData));
            }
        }



        public double HorizontalSpacing
        {
            get
            {
                return _horizontalSpacing;
            }
            set
            {
                _horizontalSpacing = value;
                OnPropertyChanged(nameof(HorizontalSpacing));
            }
        }

        public double VerticalSpacing
        {
            get
            {
                return _verticalSpacing;
            }
            set
            {
                _verticalSpacing = value;
                OnPropertyChanged(nameof(VerticalSpacing));
            }
        }

        public double ViewPortCentreX
        {
            get
            {
                return _viewPortCentreX;
            }
            set
            {
                _viewPortCentreX = value;
                OnPropertyChanged(nameof(ViewPortCentreX));
            }
        }

        public double ViewPortCentreY
        {
            get
            {
                return _viewPortCentreY;
            }
            set
            {
                _viewPortCentreY = value;
                OnPropertyChanged(nameof(ViewPortCentreY));
            }
        }
    }
}

