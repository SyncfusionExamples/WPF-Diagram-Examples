using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
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

namespace Flip_Command
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Flip_Click(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).Commands.Flip.Execute(null);
        }

        private void HorizontalFlip_Click(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).Commands.Flip.Execute(new FlipParameter() { Flip = Syncfusion.UI.Xaml.Diagram.Flip.HorizontalFlip, FlipMode = FlipMode.FlipMode});
        }

        private void VerticalFlip_Click(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).Commands.Flip.Execute(new FlipParameter() { Flip = Syncfusion.UI.Xaml.Diagram.Flip.VerticalFlip, FlipMode = FlipMode.FlipMode });
        }
    }
}
