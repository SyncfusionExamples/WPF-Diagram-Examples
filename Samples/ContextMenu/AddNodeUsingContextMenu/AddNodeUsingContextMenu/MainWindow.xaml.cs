using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
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
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;

namespace AddNodesInContainerHoldingShiftKey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point mousePosition;
        public MainWindow()
        {
            InitializeComponent();

            // Add Node Into Container Context Menu Option
            DiagramMenuItem addNodeToContainer = new DiagramMenuItem()
            {
                Content = "Add Node into Container",
                Command = new DelegateCommand(OnAddNodeIntoContainerExecute, CanAddNodeIntoContainerExecute),
            };
            Diagram.Menu.MenuItems.Add(addNodeToContainer);

            // Delete Node Context Menu Option
            DiagramMenuItem deleteNode = new DiagramMenuItem()
            {
                Content = "Delete",
                Command = new DelegateCommand(OnDeleteExecute, CanDeleteExecute),
            };
            Diagram.Menu.MenuItems.Add(deleteNode);
           
            Diagram.MouseDown += Diagram_MouseDown;
        }

        
        private void Diagram_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the mouse position relative to the canvas
            mousePosition = e.GetPosition(sender as SfDiagram);
        }


        public void OnAddNodeIntoContainerExecute(object prameter)
        {
            if (mousePosition == null) { return ; }

            ContainerViewModel selectedContainer = ((Diagram.SelectedItems as SelectorViewModel).Groups as IEnumerable<object>).FirstOrDefault() as ContainerViewModel;
            
            NodeViewModel node = new NodeViewModel()
            {
                ID = "node",
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = mousePosition.X,
                OffsetY = mousePosition.Y,
                Shape = this.Resources["Ellipse"],
            };
            (Diagram.Nodes as NodeCollection).Add(node);

            (selectedContainer.Nodes as NodeCollection).Add(node);           
        }
        public bool CanAddNodeIntoContainerExecute(object param)
        {
            //Node selection count.
            int selctedContainerCount = ((Diagram.SelectedItems as SelectorViewModel).Groups as IEnumerable<object>).Count();
            if (selctedContainerCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDeleteExecute(object obj)
        {
            (Diagram.Info as IGraphInfo).Commands.Delete.Execute(null);

            // Removing the Selection Adorner 
            if (((Diagram.SelectedItems as SelectorViewModel).SelectedItem as NodeViewModel) != null)
            {
                ((Diagram.SelectedItems as SelectorViewModel).SelectedItem as NodeViewModel).IsSelected = false;
            }

        }
        private bool CanDeleteExecute(object obj)
        {
            if ((Diagram.Info as IGraphInfo).Commands.Delete.CanExecute(null))
            {
                return true;
            }
            return false;
        }
    }
}
