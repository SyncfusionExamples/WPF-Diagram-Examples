using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Syncfusion.UI.Xaml.Diagram.Utility;
using Syncfusion.Windows.Tools.Controls;
using Syncfusion.UI.Xaml.TreeView.Engine;
using Syncfusion.UI.Xaml.TreeView;

namespace Different_Objects_from_SfTreeView_To_Diagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            dockingManager.UseDocumentContainer = true;

            ContentControl DiagramControl = new ContentControl();

            //Creating SfDiagram instance with nods and connectors collection.
            SfDiagram diagram = new SfDiagram()
            {
                Nodes = new NodeCollection(),
                Connectors = new ConnectorCollection(),
            };

            //Adding DragEnter event.
            (diagram.Info as IGraphInfo).DragEnter += MainWindow_DragEnter;

            //Adding ItemDrop event.
            (diagram.Info as IGraphInfo).ItemDropEvent += MainWindow_ItemDropEvent;

            DiagramControl.Content = diagram;

            DockingManager.SetHeader(DiagramControl, "Diagram Control");
            DockingManager.SetState(DiagramControl, DockState.Document);

            dockingManager.Children.Add(DiagramControl);

            //Adding ItemDragStarting event. 
            sftreeview.ItemDragStarting += Sftreeview_ItemDragStarting;
        }

        //Method to execute ItemDropEvent.
        private void MainWindow_ItemDropEvent(object sender, ItemDropEventArgs args)
        {
            if (args.Source is INode)
            {
                INode item = args.Source as INode;
                //Getting position of dropped node.
                double offsetX = item.OffsetX;
                double offsetY = item.OffsetY;
            }
        }

        //Method to execute ItemDragStarting event.
        private void Sftreeview_ItemDragStarting(object sender, TreeViewItemDragStartingEventArgs e)
        {
            DragObject<TreeViewNode> dataObject = new DragObject<TreeViewNode>(e.DraggingNodes.First() as TreeViewNode);
            DragDrop.DoDragDrop(sender as DependencyObject, dataObject, DragDropEffects.Copy);

            //This e.Cancel is used to stop the position changes in treeview
            e.Cancel = true;
        }

        //Method to execute DragEnter event.
        private void MainWindow_DragEnter(object sender, ItemDropEventArgs args)
        {
            // args.Source have the data which is dragged for drop.
            if (args.Source is DataObject)
            {
                object dataObject = (args.Source as DataObject).GetData(typeof(DragObject<TreeViewNode>));
                TreeViewNode treeViewItem = (dataObject as DragObject<TreeViewNode>).Source;

                // Based on the TreeView Item you can add different types of Node.              
                if (treeViewItem.Level.ToString() == "0")
                {
                    args.Source = new NodeViewModel()
                    {
                        UnitHeight = 40,
                        UnitWidth = 120,
                        Shape = this.Resources["Rectangle"],
                        ShapeStyle = this.Resources["Level1NodeStyle"] as Style,
                        Annotations = new AnnotationCollection()
                        {
                            new AnnotationEditorViewModel()
                            {
                                Content = treeViewItem.Content.ToString(),
                                Offset = new Point(0,0),
                                Margin = new Thickness(23,10,0,0),
                            },
                        },
                    };
                }
                else
                {
                    args.Source = new NodeViewModel()
                    {
                        UnitHeight = 40,
                        UnitWidth = 120,
                        Shape = this.Resources["Ellipse"],
                        ShapeStyle = this.Resources["OtherLevelNodeStyle"] as Style,
                        Annotations = new AnnotationCollection()
                        {
                            new AnnotationEditorViewModel()
                            {
                                Content = treeViewItem.Content.ToString(),
                            },
                        },
                    };
                }
            }
        }
    }
}