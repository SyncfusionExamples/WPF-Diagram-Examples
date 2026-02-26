using Force_directed_tree.ViewModel;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

namespace Force_directed_tree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Diagram.HorizontalRuler = new Ruler();
            Diagram.VerticalRuler = new Ruler()
            {
                Orientation = Orientation.Vertical
            };
            Diagram.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.ShowLines,
            };
           
            Diagram.Loaded += Diagram_Loaded;
            
        }


        private void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).Commands.FitToPage.Execute(null);
        }

    }
}
