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
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Localization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //To Represent ResourceManager
        System.Resources.ResourceManager manager;

        public MainWindow()
        {
            InitializeComponent();

            //Get CultureInfo 
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");//French

            //Initialize Assembly
            Assembly assembly = Application.Current.GetType().Assembly;
            manager = new System.Resources.ResourceManager("Localization.Resources.Syncfusion.SfDiagram.WPF", assembly);

            //Create Nodes and Connections
            CreateNodesAndConnectors();
        }

        //Create Nodes and Connections
        private void CreateNodesAndConnectors()
        {
            ///Add and Creating Nodes
            //Create and Add NewIdea Node  
            NodeViewModel NewIdea = AddNode(150, 60, 245, 40, "Ellipse", "NewIdea");

            //Create and Add Meeting Node 
            NodeViewModel Meeting = AddNode(150, 60, 245, 145, "Process", "Meeting");

            //Create and Add project Node 
            NodeViewModel Project = AddNode(160, 100, 245, 265, "Decision", "project");

            //Create and Add End Node 
            NodeViewModel End = AddNode(120, 60, 245, 400, "Process", "End");

            ///Add and Creating Connectors
            //Create Connections between NewIdea Node and Meeting Node
            AddConnector(NewIdea, Meeting, "");

            //Create Connections between BoardDecision Node and Project Node
            AddConnector(Meeting, Project, "Yes");

            //Create Connections between Project Node and End Node
            AddConnector(Project, End, "Yes");
        }

        //Create and Add Nodes
        private NodeViewModel AddNode(double width, double height, double offsetx, double offsety, string shape, string text)
        {
            NodeViewModel node = new NodeViewModel();
            node.UnitWidth = width;
            node.UnitHeight = height;
            node.OffsetX = offsetx;
            node.OffsetY = offsety;
            node.Annotations = new ObservableCollection<IAnnotation>()
            {
                new TextAnnotationViewModel()
                {
                    //Passing the resource to the text
                    Text = manager.GetString(text),
                    //Make the text as non editable
                    ReadOnly = true,
                    RotationReference= Syncfusion.UI.Xaml.Diagram.RotationReference.Page,
                    TextWrapping=TextWrapping.Wrap,
                    //Aligning the text
                    TextHorizontalAlignment=TextAlignment.Center,
                    TextVerticalAlignment=VerticalAlignment.Center,
                    //Setting the width to the text
                    UnitWidth=90,
                    //Setting the font family to the text
                    FontFamily = new FontFamily("Arial"),
                }
            };
            node.Shape = Application.Current.Resources[shape];
            node.ShapeStyle = Application.Current.Resources["Nodestyle"] as Style;
            (diagram.Nodes as ObservableCollection<NodeViewModel>).Add(node);
            return node;
        }

        //Create and Add Connectors
        private void AddConnector(NodeViewModel sourceNode, NodeViewModel targetNode, string text)
        {
            TextAnnotationViewModel textannotation = new TextAnnotationViewModel();
            textannotation.Text = manager.GetString(text);
            textannotation.ReadOnly = true;
            textannotation.RotationReference = Syncfusion.UI.Xaml.Diagram.RotationReference.Page;
            if (text == "Yes")
            {
                //Setting the margin to the text
                textannotation.Margin = new Thickness(10, 0, 0, 0);
            }
            else
            {
                textannotation.Margin = new Thickness(0, 10, 0, 0);
            }

            ConnectorViewModel connector = new ConnectorViewModel();
            connector.SourceNode = sourceNode;
            connector.TargetNode = targetNode;
            connector.Annotations = new ObservableCollection<IAnnotation>()
            {
                textannotation
            };
            (diagram.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector);
        }
    }
}
