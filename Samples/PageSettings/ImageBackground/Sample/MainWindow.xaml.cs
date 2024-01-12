using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Xml.Linq;

namespace Simple_SfDiagram_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();

            // Approach 1
            // Set a image for a diagram background.
            Diagram.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Asset/Museum.png", UriKind.RelativeOrAbsolute)));

            // Set a transparent color for the page background. Default Value is White.
            Diagram.PageSettings = new PageSettings()
            {
                PageBackground = Brushes.Transparent,
            };

            // Approach 2
            // Set a image for the page background for each Diagram Page.
            //Diagram.PageSettings = new PageSettings()
            //{
            //    PageBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Asset/Lock.png", UriKind.RelativeOrAbsolute))),
            //};
        }
    }
}
