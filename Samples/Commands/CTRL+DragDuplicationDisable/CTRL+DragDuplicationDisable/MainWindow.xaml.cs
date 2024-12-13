using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
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

namespace Simple_SfDiagram_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DiagramMenuItem menu = new DiagramMenuItem()
            {
                Content = "Duplicate",
                Command = (Diagram.Info as IGraphInfo).Commands.Duplicate,
                
            };
            Diagram.Menu.MenuItems.Add(menu);


            ObservableCollection<NodeViewModel> nodes = new ObservableCollection<NodeViewModel>();
            NodeViewModel node1 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 1000,
                OffsetY = 450,
                Content = "Node1",
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                ShapeStyle = this.Resources["ShapeStyle"] as Style
            };
            NodeViewModel node2 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 1050,
                OffsetY = 560,
                Content="Node2",
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                ShapeStyle = this.Resources["ShapeStyle"] as Style
            };

            ObservableCollection<GroupViewModel> group1 = new ObservableCollection<GroupViewModel>();
            ContainerViewModel container = new ContainerViewModel()
            {
                UnitHeight=400,
                UnitWidth=400,
                OffsetX=1000, OffsetY=500,
                Nodes = new ObservableCollection<NodeViewModel>()
              {
                    node1,
                    node2
                 },
            };

            container.Header = new ContainerHeaderViewModel()
            {
                UnitHeight = 40,
                Annotation = new AnnotationEditorViewModel()
                {
                    Content = "Container",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#343434"))
                },
            };

            group1.Add(container);

            this.Diagram.Groups = group1;






            ObservableCollection<NodeViewModel> node = new ObservableCollection<NodeViewModel>();
            NodeViewModel node3 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 1250,
                OffsetY = 100,
                Content = "Node3",
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                ShapeStyle = this.Resources["ShapeStyle"] as Style
            };
            NodeViewModel node4 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 1250,
                OffsetY = 200,
                Content = "Node1",
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                ShapeStyle = this.Resources["ShapeStyle"] as Style
            };
            ConnectorViewModel connectorViewModel = new ConnectorViewModel()
            {
                SourcePoint = new Point(1350, 200),
                TargetPoint = new Point(1400, 200),


            };

            GroupViewModel group = new GroupViewModel()
            {
                UnitHeight = 200,
                UnitWidth = 200,
                OffsetX = 1200,
                OffsetY = 250,
                Content = "Group",
                Nodes = new ObservableCollection<NodeViewModel>()
                {
                 node3,
                 node4
                 },
                Connectors = new ObservableCollection<ConnectorViewModel>()
                {
                    connectorViewModel
                }
            };

            group1.Add(group);
            Diagram.Groups = group1;


            
            (Diagram.Info as IGraphInfo).NodeChangedEvent += MainWindow_NodeChangedEvent;
            (Diagram.Info as IGraphInfo).ItemSelectedEvent += MainWindow_ItemSelectedEvent;
            (Diagram.Info as IGraphInfo).ItemUnSelectedEvent += MainWindow_ItemUnSelectedEvent;
            (Diagram.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;
            (Diagram.Info as IGraphInfo).ItemAdding += MainWindow_ItemAdding;
            (Diagram.Info as IGraphInfo).ItemSelectingEvent += MainWindow_ItemSelectingEvent;
            (Diagram.Info as IGraphInfo).ItemUnSelectingEvent += MainWindow_ItemUnSelectingEvent;

        }
        

        private void MainWindow_ItemUnSelectingEvent(object sender, DiagramPreviewEventArgs args)
        {
            
        }

        private void MainWindow_ItemSelectingEvent(object sender, DiagramPreviewEventArgs args)
        {
          
        }

        private void MainWindow_ItemAdding(object sender, Syncfusion.UI.Xaml.Diagram.ItemAddingEventArgs args)
        {
            
        }

        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            
        }

        private void MainWindow_ItemUnSelectedEvent(object sender, DiagramEventArgs args)
        {
           
        }

        private void MainWindow_ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
           
        }

        private void MainWindow_NodeChangedEvent(object sender, ChangeEventArgs<object, NodeChangedEventArgs> args)
        {
           
        }

       
    }
    public class ViewModel : SfDiagram
    {
        protected override bool CanExecuteCommand(ICommand command)
        {
            if (command == (Info as IGraphInfo).Commands.Duplicate)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    return Keyboard.IsKeyDown(System.Windows.Input.Key.D);
                }
            }

            return true;
        }
    }
}
