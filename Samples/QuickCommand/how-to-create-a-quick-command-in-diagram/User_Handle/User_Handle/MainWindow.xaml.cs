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

namespace User_Handle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Element to represent the frequently used commands
            QuickCommandViewModel quick = new QuickCommandViewModel()
            {
                // Outer part of quick command.
                Shape = this.Resources["Ellipse"],
               // appearence of shape.
                ShapeStyle = this.Resources["QuickCommandstyle"] as Style,
                //Inner part of quick command and it allows to host any UI elements
                Content =
                    "M3.7399902,0L16,12.258972 28.26001,0 32,3.7399902 19.73999,16 32,28.258972 28.26001,32 16,19.73999 3.7399902,32 0,28.258972 12.26001,16 0,3.7399902z",

                // Align the QuickCommand based on fractions X and Y-Axis
                OffsetX = 1,
                OffsetY = 0.5,
                // It is an absolute value used to add some blank space in any one of its four sides.
                Margin = new Thickness(30,0,0,0),
                // Assign Delete Command. Custom Command can also be assigned.
                Command = (Diagram.Info as IGraphInfo).Commands.Delete
            };


            // Adding new QuickCommand object in Commands collection
            (Diagram.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection()
            {
                quick
            };
        }
    }
}
