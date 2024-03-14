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

namespace PageBreaksSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.PageSettings.PageWidth = 1000;
            diagram.PageSettings.PageHeight = 1000;
            diagram.PageSettings.PrintMargin = new Thickness(0);
        
            diagram.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { };
            diagram.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Vertical };
            diagram.PageSettings.ShowPageBreaks = true;
            (diagram.Info as IGraphInfo).ItemDropEvent += MainWindow_ItemDropEvent;
               
        }

        

        private void MainWindow_ItemDropEvent(object sender, ItemDropEventArgs args)
        {
            //For Offset X
          
            //Store the Offset value for move the Node Offset
            var OldOffsetX = (args.Source as NodeViewModel).OffsetX;

            //Rounded off the Offset value
            var RoundedNumberX = Math.Round((args.Source as NodeViewModel).OffsetX);
            
            var numberX = RoundedNumberX / diagram.PageSettings.PageWidth;

            //Convert to string
            var numberStringX = numberX.ToString();
          
            //Find the index value of the decimal point
            var decimalIndexForX = numberStringX.IndexOf('.');

            //Get the values after the decimal index if the index is -1 i have change this to 1
            var stringAfterDecimalForX = decimalIndexForX != -1 ? numberStringX.Substring(decimalIndexForX + 1) : "1";

            //Covert the string to int
            int finalNumberX = int.Parse(stringAfterDecimalForX);

            if (args.Source is INode)
            {
                if ((args.Source as NodeViewModel).OffsetX > (diagram.PageSettings.PageWidth/2))
                {
                    //Move the dropped node if the off set value is less than 1000 
                    if (finalNumberX >= (diagram.PageSettings.PageWidth - (args.Source as NodeViewModel).UnitWidth)) //&& num1 < diagram.PageSettings.PageWidth)
                    {
                        (args.Source as NodeViewModel).OffsetX = OldOffsetX - (args.Source as NodeViewModel).UnitWidth;
                        return;

                    }
                    //Move the dropped node if the off set value is greater than or equal 1000 
                    if (finalNumberX >= 1 && finalNumberX < (args.Source as NodeViewModel).UnitWidth)
                    {
                        (args.Source as NodeViewModel).OffsetX = OldOffsetX + (args.Source as NodeViewModel).UnitWidth;
                        return;
                    }
                }
                
            }


            //For Offset Y

            //Store the Offset value for move the Node Offset
            var OldOffsetY = (args.Source as NodeViewModel).OffsetY;

            //Rounded off the Offset value
            var RoundedNumberY = Math.Round((args.Source as NodeViewModel).OffsetY);

            var numberY = RoundedNumberY / diagram.PageSettings.PageWidth;

            //Convert to string
            var numberStringY = numberY.ToString();

            //Find the index value of the decimal point
            var decimalIndexForY = numberStringY.IndexOf('.');

            //Get the values after the decimal index if the index is -1 i have change this to 1
            var stringAfterDecimalForY = decimalIndexForY != -1 ? numberStringY.Substring(decimalIndexForY + 1) : "1";

            //Covert the string to int
            int finalNumberY = int.Parse(stringAfterDecimalForY);

            if (args.Source is INode)
            {
                //Move the dropped node if the off set value is less than 1000 
                if (finalNumberY >= (diagram.PageSettings.PageWidth - (args.Source as NodeViewModel).UnitWidth))
                {
                    (args.Source as NodeViewModel).OffsetY = OldOffsetY - (args.Source as NodeViewModel).UnitWidth;
                }
                //Move the dropped node if the off set value is greater than or equal 1000 
                if (finalNumberY >= 1 && finalNumberY < (args.Source as NodeViewModel).UnitWidth)
                {
                    (args.Source as NodeViewModel).OffsetY = OldOffsetY + (args.Source as NodeViewModel).UnitWidth;
                }
            }

        }
    }
}
