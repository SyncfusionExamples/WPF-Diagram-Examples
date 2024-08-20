using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace StraightSegmentSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            Diagram.ScrollSettings = new ScrollSettings() { ScrollLimit = ScrollLimit.Infinity };

            //Diagram.ScrollSettings.SetHorizontalOffset(200);
        }

        const int WM_MOUSEHWHEEL = 0x020E;

        protected override void OnSourceInitialized(EventArgs e)
        {
            // where diagram is the instance of the SfDiagram control. We can use which control want this behavior.
            var source = PresentationSource.FromVisual(Diagram);
            ((HwndSource)source)?.AddHook(Hook);
        }

        private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_MOUSEHWHEEL:
                    int scrolldelta = (short)HIWORD(wParam);
                    DoHorizontalScroll(scrolldelta);
                    return (IntPtr)1;
            }
            return IntPtr.Zero;
        }

        private static int HIWORD(IntPtr ptr)
        {
            var val32 = ptr.ToInt32();
            return ((val32 >> 16) & 0xFFFF);
        }

        private static int LOWORD(IntPtr ptr)
        {
            var val32 = ptr.ToInt32();
            return (val32 & 0xFFFF);
        }

        // Method to do execute the horizontal scroll in Diagram
        private void DoHorizontalScroll(int scrolldelta)
        {
            (Diagram.Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter() { ZoomCommand = ZoomCommand.HorizondalScroll, ScrollDelta = scrolldelta });
        }
    }
}
