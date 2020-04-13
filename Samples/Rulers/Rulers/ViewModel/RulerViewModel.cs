using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RulerCustomization
{
    public class RulerViewModel: DiagramViewModel
    {
        public RulerViewModel()
        {
            //Initialize the horizontal and vertical rulers.
            this.HorizontalRuler = new CustomRuler();
            this.VerticalRuler = new CustomRuler() { Orientation = Orientation.Vertical };
        }
    }

    /// <summary>
    /// Custom class for rulers
    /// </summary>
    public class CustomRuler : Ruler
    {
        protected override RulerSegment GetNewSegment()
        {
            //Creating a custom segment with 12 intervals.
            return new CustomSegment() { Intervals = 12 };
        }
    }

    // Customizing RulerSegment
    public class CustomSegment : RulerSegment
    {
        protected override Tick GetNewTick()
        {
            return new CustomTick();
        }

        public override double GetSegmentWidth()
        {
            // Customizing the ruler segment width.
            return 200;
        }

        // Customizing the label of the RulerSegment
        protected override void UpdateLabel(TextBlock label)
        {
            base.UpdateLabel(label);
        }
    }

    /// <summary>
    /// Custom class for ruler ticks.
    /// </summary>
    public class CustomTick : Tick
    {
        // <summary>
        /// To update the ticks values start value, length, alignment
        /// </summary>
        /// <param name="start">Start value</param>
        /// <param name="length">Length of the tick</param>
        /// <param name="align">Alignment of the tick</param>

        protected override void ArrangeTick(out double start, out double length, out
                                               TickAlignment align)
        {
            start = 0;
            if (Value % 200 == 0)
            {
                length = 20;
            }
            else if (Value % 100 == 0 || Value % 100 < 2)
            {
                length = 14;
            }
            else if (Value % 50 == 0)
            {
                length = 9;
            }
            else
            {
                length = 5;
            }
            align = TickAlignment.RightOrBottom;
        }
    }
}
