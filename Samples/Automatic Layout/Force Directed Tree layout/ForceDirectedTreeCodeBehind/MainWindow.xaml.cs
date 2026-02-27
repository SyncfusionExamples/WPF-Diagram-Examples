using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
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

namespace ForceDirectedTreeCodeBehind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Diagram.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler();
            Diagram.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = Orientation.Vertical,
            };
            Diagram.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.ShowLines,
            };


            Diagram.Loaded += Diagram_Loaded;
            Diagram.Nodes = new NodeCollection();
            Diagram.Connectors = new ConnectorCollection();


            CreatedNode();
            Diagram.LayoutManager = new LayoutManager()
            {
                Layout = new ForceDirectedTree()
                {
                    AttractionStrength = 0.6,
                    RepulsionStrength = 25000,
                    MaximumIteration = 2500,
                }
            };
        }

        private void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).Commands.FitToPage.Execute(null);
        }

        private void CreatedNode()
        {
            // create nodes first
            string[] labels = new[]
            {
                "Team",    // 1 root
                "PO-1",    // 2
                "PO-2",    // 3
                "PO-3",    // 4
                "PO-4",    // 5
                "PO-5",    // 6
                "Team-1",  // 7
                "Team-2",  // 8
                "Team-3",  // 9
                "Team-4",  //10
                "Team-1",  //11
                "Team-2",  //12
                "Team-3",  //13
                "Team-4",  //14
                "Team-1",  //15
                "Team-2",  //16
                "Team-3",  //17
                "Team-4",  //18
                "Team-1",  //19
                "Team-2",  //20
                "Team-3",  //21
                "Team-4",  //22
                "Team-1",  //23
                "Team-2",  //24
                "Sales Team", //25
                "AGM-1",   //26
                "AGM-2",   //27
                "Team-4",  //28
                "Team-3",  //29
                "Team-2"   //30
            };

            for (int i = 1; i <= 30; i++)
            {
                var role = GetRoleForIndex(i); // "Root","Parent","Child"
                var id = $"Node{i}";
                var label = labels[i - 1];
                (Diagram.Nodes as NodeCollection).Add(CreateNode(id, role, label));
            }

            // Connectors (parent -> child)
            var cons = Diagram.Connectors as ConnectorCollection;

            // Level 0 -> Level 1
            cons.Add(Edge("Node1", "Node2"));
            cons.Add(Edge("Node1", "Node3"));
            cons.Add(Edge("Node1", "Node4"));
            cons.Add(Edge("Node1", "Node5"));

            // Level 1 -> Level 2
            cons.Add(Edge("Node2", "Node6"));
            cons.Add(Edge("Node2", "Node7"));
            cons.Add(Edge("Node2", "Node8"));

            cons.Add(Edge("Node3", "Node9"));
            cons.Add(Edge("Node3", "Node10"));
            cons.Add(Edge("Node3", "Node11"));

            cons.Add(Edge("Node4", "Node12"));
            cons.Add(Edge("Node4", "Node13"));
            cons.Add(Edge("Node4", "Node14"));

            cons.Add(Edge("Node5", "Node15"));
            cons.Add(Edge("Node5", "Node16"));
            cons.Add(Edge("Node5", "Node17"));

            // Leaves and deeper children
            cons.Add(Edge("Node7", "Node18"));
            cons.Add(Edge("Node10", "Node19"));
            cons.Add(Edge("Node14", "Node20"));
            cons.Add(Edge("Node11", "Node21"));

            cons.Add(Edge("Node12", "Node22"));
            cons.Add(Edge("Node12", "Node23"));
            cons.Add(Edge("Node12", "Node24"));

            cons.Add(Edge("Node13", "Node25"));
            cons.Add(Edge("Node13", "Node26"));
            cons.Add(Edge("Node13", "Node27"));

            cons.Add(Edge("Node14", "Node28"));
            cons.Add(Edge("Node14", "Node29"));
            cons.Add(Edge("Node14", "Node30"));
        }

        // Determine role by index (adjust ranges as desired)
        private string GetRoleForIndex(int index)
        {
            if (index == 1) return "Root";
            if (index >= 2 && index <= 5) return "Parent";
            return "Child";
        }

        private CustomNodeViewModel CreateNode(string id, string role, string label)
        {
            double size = role == "Root" ? 140 : role == "Parent" ? 100 : 60;
            var geom = new EllipseGeometry(new Point(size / 2, size / 2), size / 2, size / 2);

            var node = new CustomNodeViewModel
            {
                ID = id,
                UnitWidth = size,
                UnitHeight = size,
                Tag = role,
                Shape = geom,
                Annotations = new ObservableCollection<IAnnotation>
                {
                    new AnnotationEditorViewModel
                    {
                        Content = label
                    }
                }
            };

            return node;
        }

        private ConnectorViewModel Edge(string sourceId, string targetId)
        {
            return new ConnectorViewModel
            {
                ID = $"{sourceId}->{targetId}",
                SourceNodeID = sourceId,
                TargetNodeID = targetId,
                // If your API exposes decorator properties, enable arrowheads here.
                // e.g. TargetDecorator = new DecoratorViewModel { Shape = DecoratorShapes.Arrow }
            };
        }
        private void UpDown_ValueChanged_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Diagram.LayoutManager != null)
                (Diagram.LayoutManager.Layout as ForceDirectedTree).MaximumIteration = (int)(double)e.NewValue;
        }

        private void UpDown_ValueChanged_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Diagram.LayoutManager != null)
                (Diagram.LayoutManager.Layout as ForceDirectedTree).RepulsionStrength = (int)(double)e.NewValue;
        }

        private void UpDown_ValueChanged_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Diagram.LayoutManager != null)
                (Diagram.LayoutManager.Layout as ForceDirectedTree).AttractionStrength = (double)e.NewValue;
        }
    }

    public class CustomNodeViewModel : NodeViewModel
    {
        public string Tag { get; set; }
    }
}

