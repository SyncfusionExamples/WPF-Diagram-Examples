using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace CustomContextMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // create a node
            Createnode();

            // create and add submenu items
            Addsubmenu();
        }

        private void Addsubmenu()
        {
            IGraphInfo diagramInfo = (Diagram.Info as IGraphInfo);

            //Initialize  the new custom Menu with zoom functionalities
            DiagramMenuItem Zoom = new DiagramMenuItem()
            {
                Content = "Zoom",
                Icon = @"pack://application:,,,/Icons/zoom.jpg",
            };

            //Intialize the sub-menu item for Zooming enu with ZoomIn command
            DiagramMenuItem zoomIn = new DiagramMenuItem()
            {
                // Defines the content of menu item
                Content = "ZoomIn",
                // Defines the icon that appears in menu item - it accepts the image path.
                Icon = @"pack://application:,,,/Icons/zoomin.png",
                // Defines the action of menu item
                Command = diagramInfo.Commands.Zoom,
                // Defines the parameter of the menu item
                CommandParameter = new ZoomPositionParameter() { ZoomFactor = 0.5, ZoomCommand = ZoomCommand.ZoomIn }
            };

            //Intialize the sub-menu item for Zooming enu with ZoomOut command
            DiagramMenuItem zoomOut = new DiagramMenuItem()
            {
                Content = "ZoomOut",
                Icon = @"pack://application:,,,/Icons/zoomout.png",
                Command = diagramInfo.Commands.Zoom,
                CommandParameter = new ZoomPositionParameter() { ZoomFactor = 0.5, ZoomCommand = ZoomCommand.ZoomOut }
            };


            Zoom.Items = new ObservableCollection<DiagramMenuItem>();
            //Adding zooming menus into diagram menu as to add sub-menu items.
            Zoom.Items.Add(zoomIn);
            Zoom.Items.Add(zoomOut);

            //Adding zooming menu into diagram default menu.
            Diagram.Menu.MenuItems.Add(Zoom);
        }

        private void Createnode()
        {
            //Initialize the node 
            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 200,
                OffsetY = 100,
                UnitWidth = 100,
                UnitHeight = 100,
                Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content ="Node",
                    }
                 },
            };

            //Adding the node into nodes collection
            (Diagram.Nodes as NodeCollection).Add(node);
        }
    }
 
}
