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

namespace CustomDataSource
{
    /// <summary>
    /// Interaction logic for NodeCreate.xaml
    /// </summary>
    public partial class NodeCreate : UserControl
    {
        public NodeCreate()
        {
            InitializeComponent();
            var ellipseBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4382FF"));
            var ellipsesCount = 4;
            var initialEllipseDiameter = 70;
            var diameterStep = 25;
            var opacityStep = 1d / ellipsesCount;

            for (int i = 0; i < ellipsesCount; i++)
            {
                var size = initialEllipseDiameter + (diameterStep * i);
                var ellipse = new Ellipse()
                {
                    Width = size,
                    Height = size,
                    Fill = new SolidColorBrush(ellipseBrush.Color) { Opacity = 1 - (i * opacityStep) }
                };
                ellipsesPanel.Children.Insert(0, ellipse);
            }

            var ellipsesWidth = ellipsesPanel.Children.OfType<Ellipse>().Max(x => x.Width);
            var offset = (ellipsesWidth / 2) - (mainEllipse.Width / 2);
            Canvas.SetLeft(ellipsesPanel, -offset);
            Canvas.SetTop(ellipsesPanel, -offset);
        }
    }
}
