using Syncfusion.Windows.Shared;
using System.Windows;
using System.Windows.Media;

namespace MagnifierSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Magnifier magnifier;
        public MainWindow()
        {
            InitializeComponent();

            magnifier = new Magnifier();
            magnifier.FrameType = FrameType.Circle;
            magnifier.ZoomFactor = 0.3;
            magnifier.FrameBackground = new SolidColorBrush(Colors.White);
            magnifier.RenderTransform = new TranslateTransform()

            {

                X = 10 + (magnifier.FrameWidth * 0.5),

                Y = 10 + (magnifier.FrameHeight * 0.5)

            };
        }

        private void showMagnifier_Checked(object sender, RoutedEventArgs e)
        {   
            magnifier.TargetElement = diagram;
            showMagnifier.Content = "Hide Magnifier";
        }

        private void showMagnifier_Unchecked(object sender, RoutedEventArgs e)
        {
            magnifier.TargetElement = null;
            showMagnifier.Content = "Show Magnifier";
        }
    }
}
