using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GroupCollection = Syncfusion.UI.Xaml.Diagram.GroupCollection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ContainerSampleUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Diagram.Nodes = new NodeCollection();
            Diagram.Groups = new GroupCollection();

            (Diagram.Info as IGraphInfo).ItemDropEvent += MainPage_ItemDropEvent;
        }

        private void MainPage_ItemDropEvent(object sender, ItemDropEventArgs args)
        {
            IEnumerable<object> target = args.Target as IEnumerable<object>;

            if (target != null)
            {
                foreach (object item in target)
                {
                    if (item is ContainerViewModel)
                    {
                        if ((item as ContainerViewModel).ID.ToString() == "Horizontal")
                        {
                            NodeViewModel node = args.Source as NodeViewModel;
                            ContainerViewModel container = item as ContainerViewModel;

                            if (container.Nodes != null && (container.Nodes as NodeCollection).Count == 0)
                            {
                                node.OffsetX = container.OffsetX;
                                node.OffsetY = container.OffsetY;
                            }
                            else if (container.Nodes != null)
                            {
                                node.OffsetY = container.OffsetY;
                                node.OffsetX = (container.Nodes as NodeCollection).Last().OffsetX + 50;
                            }
                        }

                        if ((item as ContainerViewModel).ID.ToString() == "Vertical")
                        {
                            NodeViewModel node = args.Source as NodeViewModel;
                            ContainerViewModel container = item as ContainerViewModel;

                            if (container.Nodes != null && (container.Nodes as NodeCollection).Count == 0)
                            {
                                node.OffsetX = container.OffsetX;
                                node.OffsetY = container.OffsetY;
                            }
                            else if (container.Nodes != null)
                            {
                                node.OffsetX = container.OffsetX;
                                node.OffsetY = (container.Nodes as NodeCollection).Last().OffsetY + 50;
                            }
                        }
                    }
                }
            }
        }

        private void Horizontal_Click(object sender, RoutedEventArgs e)
        {
            ContainerViewModel horizontalcontainer = new ContainerViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 200,
                OffsetY = 200,
                ID = "Horizontal",
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Header = new ContainerHeaderViewModel() { Annotation = new AnnotationEditorViewModel() { Content = "Horizontal" } },
                Constraints = NodeConstraints.Default | NodeConstraints.AllowDrop,
            };

            (Diagram.Groups as GroupCollection).Add(horizontalcontainer);
        }

        private void Vertical_Click(object sender, RoutedEventArgs e)
        {
            ContainerViewModel verticalcontainer = new ContainerViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 200,
                ID = "Vertical",
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Header = new ContainerHeaderViewModel() { Annotation = new AnnotationEditorViewModel() { Content = "Vertical" } },
                Constraints = NodeConstraints.Default | NodeConstraints.AllowDrop,
            };

            (Diagram.Groups as GroupCollection).Add(verticalcontainer);
        }
    }
}
