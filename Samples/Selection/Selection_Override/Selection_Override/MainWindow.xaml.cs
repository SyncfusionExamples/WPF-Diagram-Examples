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
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;


namespace Selection_Override
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Diagram.Nodes=new ObservableCollection<CustomNode>();
            Diagram.Connectors = new ObservableCollection<Connector>();
            Diagram.Groups=new ObservableCollection<Group>();
            CustomNode cn = new CustomNode()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                Shape = new RectangleGeometry() {Rect = new Rect(10, 10, 10, 10)},
                ShapeStyle = this.Resources["nodestyle"] as Style,
                OffsetX = 200,
                OffsetY = 200
            };

            (Diagram.Nodes as ICollection<CustomNode>).Add(cn);

            Connector c = new Connector()
            {
                SourcePoint = new Point(300, 300),
                TargetPoint = new Point(400, 400)
            };

            c.Annotations = new ObservableCollection<IAnnotation>()
            {
                new AnnotationEditorViewModel()
                {
                    Content = "connector"
                }
            };
            (Diagram.Connectors as ICollection<Connector>).Add(c);        
            Diagram.PageSettings.PageWidth = 1000;
            Diagram.PageSettings.PageHeight = 1000;


        }
    }

    public class CustomNode : Node
    {
        
    }

    public class CustomConnector : Connector
    {
        
    }


    public class CustomSelector : Selector
    {
        public CustomSelector()
        {

        }

        //Method to decide whether Selection should be done in PointerDown.
        protected override void PointerSelection(PointerSelectionArgs args)
        {
            if (args.PointerMode == PointerMode.Down)
            {

                Selection(element: args.Source);

                if (args.Source is SfDiagram)
                {
                    ClearSelection(Element: args.Source);
                }
            }
        }
    }

    public class CustomDiagram : SfDiagram
    {
        public CustomDiagram()
        {
      
        }

        //Method to return the selector for diagram.
        protected override Selector GetSelectorForItemOverride(object item)
        {
            //assigning custom selector to the diagram.
            CustomSelector selector = new CustomSelector();
            return selector;
        }
      
    }
}
