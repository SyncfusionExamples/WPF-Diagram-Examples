using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Gridlines
{
    public class GridlinesViewModel : DiagramViewModel
    {
        public GridlinesViewModel()
        {
            //Initialize the rulers
            this.HorizontalRuler = new Ruler();
            this.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };

            //Style for Gridlines
            Style pathStyle = new Style(typeof(Path));
            pathStyle.Setters.Add(new Setter(Shape.StrokeProperty, new SolidColorBrush(Colors.Blue)));
            pathStyle.Setters.Add(new Setter(Shape.StrokeDashArrayProperty, new DoubleCollection() { 3, 3 }));

            //Initialize the double collection
            Intervals intervals = new Intervals { 0.25, 10, 0.5, 20, 1, 30, 1.25, 40, 1.5, 50 };

            //Initialize SnapSettings constraints to show Gridlines
            this.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.ShowLines,
                HorizontalGridlines = new Syncfusion.UI.Xaml.Diagram.Gridlines()
                {
                    Strokes = new Gridlinestyle { pathStyle },
                    //Define lines interval value
                    LinesInterval = intervals,
                },
                VerticalGridlines = new Syncfusion.UI.Xaml.Diagram.Gridlines()
                {
                    Strokes = new Gridlinestyle { pathStyle },
                    //Define lines interval value
                    LinesInterval = intervals,
                },
            };
        }
    }

    //Creates collection for the style.
    public class Gridlinestyle : List<Style>
    {
    }

    //Creates collection for the double values.
    public class Intervals : List<double>
    {
    }
}
