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

namespace MouseMiddleButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //To hold the initial location to process pan action.
        Point InitialLocation = new Point();
        public MainWindow()
        {
            InitializeComponent();
            diagram.PreviewMouseMove += Diagram_PreviewMouseMove;
        }

        private void Diagram_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //Getting current mouse point position of diagram..

            Point currentMousePoint = e.GetPosition(diagram.Page);

            Point panDelta = new Point(currentMousePoint.X - InitialLocation.X, currentMousePoint.Y - InitialLocation.Y);

            if (e.MiddleButton == MouseButtonState.Pressed)

            {

                //Disabling intelli mouse wheel action when pressing middle button.

                e.Handled = true;

                //Getting current zoom scale value.

                double currentZoomLevel = diagram.ScrollSettings.ScrollInfo.CurrentZoom;

                //Translating pan delta changes to current zoom scale value.

                panDelta.X = panDelta.X * currentZoomLevel;

                panDelta.Y = panDelta.Y * currentZoomLevel;

                //setting hand cursor for panning action

                Mouse.SetCursor(Cursors.Hand);

                //Executing pan process when middle mouse button moving.

                (diagram.Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter() { ZoomCommand = ZoomCommand.Pan, PanDelta = panDelta });

            }
        }
    }
}
