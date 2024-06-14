using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace GridlinesCustomSpace
{
    public class GridlinesViewModel : DiagramViewModel
    {
        public GridlinesViewModel()
        {
            //Initialize the double collection
            Intervals intervals = new Intervals { 0.25, 10, 0.5, 20, 1, 30, 1.25, 40, 1.5, 50 };

            //Initialize SnapSettings constraints to show Gridlines
            this.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.ShowLines,
                HorizontalGridlines = new Syncfusion.UI.Xaml.Diagram.Gridlines()
                {
                    //Define lines interval value
                    LinesInterval = intervals,
                },
                VerticalGridlines = new Syncfusion.UI.Xaml.Diagram.Gridlines()
                {
                    //Define lines interval value
                    LinesInterval = intervals,
                },
            };
        }
    }

    //Creates collection for the double values.
    public class Intervals : List<double>
    {
    }
}

