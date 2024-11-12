using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.Windows.Shared;
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

namespace MultipleQuickCommands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public QuickCommandViewModel deleteCommand;

        public QuickCommandViewModel duplicateCommand;
        public MainWindow()
        {
            InitializeComponent();

            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();

            NodeA nodeA = new NodeA()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 400,
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content="nodeA"
                    }
                },

            };
            (diagram.Nodes as NodeCollection).Add(nodeA);


            NodeB nodeB = new NodeB()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 600,
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content="nodeB"
                    }
                },

            };
            (diagram.Nodes as NodeCollection).Add(nodeB);




            NodeC nodeC = new NodeC()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 600,
                OffsetY = 600,
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content="nodeC"
                    }
                },

            };
            (diagram.Nodes as NodeCollection).Add(nodeC);






            deleteCommand = new QuickCommandViewModel()
            {
                Shape = this.Resources["Ellipse"],
                ShapeStyle = this.Resources["QuickCommandsStyle"] as Style,
                Content = "M0,2.4879999 L0.986,2.4879999 0.986,9.0139999 6.9950027,9.0139999 6.9950027,10 0.986,10 C0.70400238,10 0.47000122,9.9060001 0.28100207,9.7180004 0.09400177,9.5300007 0,9.2959995 0,9.0139999 z M3.0050011,0 L9.0140038,0 C9.2960014,0 9.5300026,0.093999863 9.7190018,0.28199956 9.906002,0.47000027 10,0.70399952 10,0.986 L10,6.9949989 C10,7.2770004 9.906002,7.5160007 9.7190018,7.7110004 9.5300026,7.9069996 9.2960014,8.0049992 9.0140038,8.0049992 L3.0050011,8.0049992 C2.7070007,8.0049992 2.4650002,7.9069996 2.2770004,7.7110004 2.0890007,7.5160007 1.9950027,7.2770004 1.9950027,6.9949989 L1.9950027,0.986 C1.9950027,0.70399952 2.0890007,0.47000027 2.2770004,0.28199956 2.4650002,0.093999863 2.7070007,0 3.0050011,0 z",
                OffsetX = 1,
                OffsetY = 0.5,
                Margin = new Thickness(30, 0, 0, 0),
                Command = (diagram.Info as IGraphInfo).Commands.Delete,
            };



            // Create QuickCommand for 'duplicate'

            duplicateCommand = new QuickCommandViewModel()
            {
                Shape = this.Resources["Rectangle"],
                ShapeStyle = this.Resources["QuickCommandsStyle1"] as Style,
                Content = "M 4.977 8.027 L 2.985 6 H 5 H 21 M 18.992 8.045 H 5 Z M 9 10 H 15 V 18 H 9 V 10 Z L 9.005 10.026 Z M 2.985 10.025 L 2.985 10.02 L 21.001 10.002 M 2.985 10.037 L 4.995 19.001 L 19.027 18.984 L 21.001 10.002 M 18.992 8.062 L 21.019 6",
                OffsetX = 0.5,
                OffsetY = 1,
                Margin = new Thickness(0, 30, 0, 0),
                Command = (diagram.Info as IGraphInfo).Commands.Duplicate,

            };
            (diagram.Info as IGraphInfo).SelectorChangedEvent += MainWindow_SelectorChangedEvent;

        }

        private void MainWindow_SelectorChangedEvent(object sender, SelectorChangedEventArgs args)
        {
            SelectorViewModel selector = diagram.SelectedItems as SelectorViewModel;

            if (selector.Nodes as IEnumerable<object> != null && (selector.Nodes as IEnumerable<object>).Count() > 0)

            {

                object node = (selector.Nodes as IEnumerable<object>).First() as object;

                if (node is NodeA)
                {
                    (diagram.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection()
                    {
                        deleteCommand,
                        duplicateCommand
                    };
               }
                else if (node is NodeB || node is NodeC)
                {
                    (diagram.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection()
                    {
                        deleteCommand
                    };
                }

            }
        }
    }
    public class NodeA : NodeViewModel
    {
        public NodeA() { }
    }
    public class NodeB : NodeViewModel
    {
        public NodeB() { }
    }
    public class NodeC : NodeViewModel
    {
        public NodeC() { }
    }
}
