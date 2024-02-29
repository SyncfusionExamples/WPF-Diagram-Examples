using Syncfusion.UI.Xaml.Diagram;
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

namespace AnnotationVisibility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //initialize the nodes and connectors collection
            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();

            //Define the Node
            NodeViewModel node = new NodeViewModel()
            {
                //sets the size
                UnitHeight = 100,
                UnitWidth = 100,
                //sets the position
                OffsetX = 100,
                OffsetY = 100,
                Annotations = new ObservableCollection<CustomAnnotation>()
                {
                    //Create annotation.
                    new CustomAnnotation()
                    {
                        Visibility = Visibility.Visible,
                        Content = "Node",
                        //template for the annotation view.
                        ViewTemplate = Application.Current.MainWindow.Resources["viewTemplate"] as DataTemplate,
                    },
                }
            };
            //Adding Node to Collection
            (diagram.Nodes as NodeCollection).Add(node);

            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourcePoint = new Point(200, 200),
                TargetPoint = new Point(300, 300),
                Annotations = new ObservableCollection<CustomAnnotation>()
                {
                    new CustomAnnotation()
                    {
                        Visibility = Visibility.Visible,
                        Content = "Connector",
                        ViewTemplate = Application.Current.MainWindow.Resources["viewTemplate"] as DataTemplate,
                    },
                }
            };

            //Adding Node to Collection
            (diagram.Connectors as ConnectorCollection).Add(connector);
        }

        //event method to switch the visibility of selected node/connector
        private void switchVisibility_Click(object sender, RoutedEventArgs e)
        {
            if ((diagram.SelectedItems as SelectorViewModel).SelectedItem != null)
            {
                //selected item
                var selecteditem = (diagram.SelectedItems as SelectorViewModel).SelectedItem;
                IEnumerable<CustomAnnotation> anntations = null;
                if (selecteditem is NodeViewModel)
                {
                    //selected node
                    var selectedNode = selecteditem as NodeViewModel;
                    //selected node's annotations
                    anntations = selectedNode.Annotations as IEnumerable<CustomAnnotation>;
                }
                else if (selecteditem is ConnectorViewModel)
                {
                    //selected connector
                    var selectedConnector = selecteditem as ConnectorViewModel;
                    //selected connectors' annotation
                    anntations = selectedConnector.Annotations as IEnumerable<CustomAnnotation>;
                }

                //first annotation of the selected item 
                CustomAnnotation a = anntations.FirstOrDefault();
                //switching the visibility.
                if (a.Visibility == Visibility.Visible)
                    a.Visibility = Visibility.Collapsed;
                else
                    a.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Custom class for AnnotationEditorViewModel class to switch the visibility of the node/connector's annotation
        /// </summary>
        public class CustomAnnotation : AnnotationEditorViewModel
        {
            private Visibility visibility = Visibility.Visible;
            /// <summary>
            /// Gets or sets the visibility of the annotation.
            /// </summary>
            public Visibility Visibility
            {
                get
                {
                    return visibility;
                }
                set
                {
                    if (value != visibility)
                    {
                        visibility = value;
                        OnPropertyChanged("Visibility");
                    }
                }
            }
        }
    }
}
