using Syncfusion.UI.Xaml.Diagram;
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

namespace PolylineNode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Node filled Polyline

            var pointsCollection = new PointCollection();
            pointsCollection.Add(new Point(300, 350));
            pointsCollection.Add(new Point(330, 300));
            pointsCollection.Add(new Point(370, 300));
            pointsCollection.Add(new Point(400, 350));
            pointsCollection.Add(new Point(370, 400));
            pointsCollection.Add(new Point(330, 400));

            var pathFigure = new PathFigure()
            {
                StartPoint = pointsCollection.First(),
                Segments = new PathSegmentCollection() { new PolyLineSegment() { Points = pointsCollection } }
            };

            var pathGeometry = new PathGeometry() { Figures = new PathFigureCollection() { pathFigure } };

            NodeViewModel node = new NodeViewModel()
            {
                ID = "NodeID",
                OffsetX = 300,
                OffsetY = 400,
                UnitWidth = 100,
                UnitHeight = 100,
                Shape = pathGeometry,
            };
            (diagram.Nodes as NodeCollection).Add(node);

        }
    }
}
