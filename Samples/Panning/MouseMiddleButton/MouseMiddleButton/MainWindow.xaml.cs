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
            diagram.PreviewMouseWheel += Diagram_PreviewMouseWheel;
            diagram.PreviewMouseDown += Diagram_PreviewMouseDown;
        }

        private void Diagram_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Getting mouse initial location when middle button pressed down.
            InitialLocation = e.GetPosition(diagram.Page);
            //Disabling intelli mouse wheel action when pressing middle button.
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                e.Handled = true;
            }
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
        private void Diagram_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollSettings scrollSettings = diagram.ScrollSettings;
            //to skip the zooming operation when control+mouse scroll is used
            if (Keyboard.Modifiers == ModifierKeys.Control || scrollSettings == null || scrollSettings.ScrollInfo == null)
                return;
            //current mouse position 
            double focusX = e.GetPosition(diagram.Page).X;
            double focusY = e.GetPosition(diagram.Page).Y;
            ZoomCommand zoomCommand = e.Delta > 0 ? ZoomCommand.ZoomIn : ZoomCommand.ZoomOut;
            (diagram.Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter
            {
                ZoomCommand = zoomCommand,
                ZoomFactor = 0.2,
                //to zoom the page with currnet mouse position as a center
                FocusPoint = new Point(focusX, focusY),
            });
            //Disabling the zooming action when pressing control and mouse wheel.
            e.Handled = true;
        }
    }
}
