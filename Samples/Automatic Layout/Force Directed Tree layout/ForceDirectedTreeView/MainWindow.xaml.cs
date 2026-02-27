using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
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

namespace ForceDirectedTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool temp;

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
        }
        private void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).Commands.FitToPage.Execute(null);
            temp = true;
        }
        private void UpDown_ValueChanged_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (temp)
            {
                if ((Diagram.LayoutManager.Layout as ForceDirectedTree).MaximumIteration != (int)(double)e.NewValue)
                    (Diagram.LayoutManager.Layout as ForceDirectedTree).MaximumIteration = (int)(double)e.NewValue;
            }
        }

        private void UpDown_ValueChanged_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (temp)
            {
                if ((Diagram.LayoutManager.Layout as ForceDirectedTree).RepulsionStrength != (int)(double)e.NewValue)
                    (Diagram.LayoutManager.Layout as ForceDirectedTree).RepulsionStrength = (int)(double)e.NewValue;
            }
        }

        private void UpDown_ValueChanged_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (temp)
            {
                if ((Diagram.LayoutManager.Layout as ForceDirectedTree).AttractionStrength != (double)e.NewValue)
                    (Diagram.LayoutManager.Layout as ForceDirectedTree).AttractionStrength = (double)e.NewValue;
            }
        }
    }

    public class CustomNodeViewModel : NodeViewModel
    {
        public string Tag { get; set; }
    }
}
