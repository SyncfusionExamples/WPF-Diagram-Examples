using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GroupScenario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Diagram.Nodes = new NodeCollection();
            Diagram.Connectors = new ConnectorCollection();
            Diagram.Groups = new GroupCollection();
           
            NodeViewModel node1 = AddNode(950, 400,"Rectangle");

            NodeViewModel node2 = AddNode(850, 500, "Rectangle");

            NodeViewModel node3 = AddNode(650, 200, "Ellipse");
            NodeViewModel node4 = AddNode(750, 300, "Card");

            NodeViewModel node5 = AddNode(200, 300,"Ellipse");
            NodeViewModel node6 = AddNode(200, 500,"Card");

            NodeViewModel node7 = AddNode(400, 300,"RoundedRectangle");
            NodeViewModel node8 = AddNode(400, 500,"PaperTap");

            ConnectorViewModel cvm = new ConnectorViewModel() { SourceNode=node1, TargetNode = node2 };

            GroupViewModel group = new GroupViewModel()
            {
                Nodes = new NodeCollection()
                {
                    node1,node2
                },
                Connectors=new ConnectorCollection()
                {
                    cvm
                },               
            };
           
            (Diagram.Groups as GroupCollection).Add(group);

            GroupViewModel grp = new GroupViewModel()
            {
                Nodes = new NodeCollection()
                {
                  node3,node4
                }
            };
            (Diagram.Groups as GroupCollection).Add(grp);

            ConnectorViewModel cvm1 = new ConnectorViewModel() { SourceNode = group, TargetNode = grp };
            (Diagram.Connectors as ConnectorCollection).Add(cvm1);

            DiagramMenuItem Groupmenu = new DiagramMenuItem()
            {
                Content = "Group",
                Command = (Diagram.Info as IGraphInfo).Commands.Group,
            };

            DiagramMenuItem UnGroupmenu = new DiagramMenuItem()
            {
                Content = "UnGroup",
                Command = (Diagram.Info as IGraphInfo).Commands.UnGroup,
            };
            Diagram.Menu.MenuItems.Add(Groupmenu);
            Diagram.Menu.MenuItems.Add(UnGroupmenu);            

            (Diagram.Info as IGraphInfo).ItemDropEvent += MainWindow_ItemDropEvent;           
        }

       
        private void MainWindow_ItemDropEvent(object sender, ItemDropEventArgs args)
        {
            if(args.Target is IEnumerable<object> && !(args.Target is GroupViewModel) && (args.Target as IEnumerable<object>).ElementAt(0) is NodeViewModel)
            {
                var sourcenode = args.Source as NodeViewModel;
                foreach(var grp in Diagram.Groups as GroupCollection)
                {
                    (grp.Nodes as NodeCollection).Add(sourcenode);
                }            
            }
        }

        private NodeViewModel AddNode(double x, double y,string shape)
        {
            NodeViewModel Begin = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = x,
                OffsetY = y,
                Shape = App.Current.Resources[shape] ,               
            };
            (Diagram.Nodes as NodeCollection).Add(Begin);
            return Begin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).Commands.Group.Execute(null);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).Commands.UnGroup.Execute(null);
        }
    }

    public class SymbolCollection:ObservableCollection<object>
    {

    }
}
