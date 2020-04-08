using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zoom_Command.Utility;

namespace Zoom_Command.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        private ICommand _ZoomIn;
        private ICommand _ZoomOut;
        private ICommand _ZoomTo;
        private ICommand _HorizontalScroll;
        private ICommand _VerticalScroll;
        private ICommand _Pan;
        private ICommand _Reset;

        public ICommand ZoomIn
        {
            get { return _ZoomIn; }
            set { _ZoomIn = value; }
        }

        public ICommand ZoomOut
        {
            get { return _ZoomOut; }
            set { _ZoomOut = value; }
        }

        public ICommand ZoomTo
        {
            get { return _ZoomTo; }
            set { _ZoomTo = value; }
        }
        public ICommand HorizontalScroll
        {
            get { return _HorizontalScroll; }
            set { _HorizontalScroll = value; }
        }

        public ICommand VerticalScroll
        {
            get { return _VerticalScroll; }
            set { _VerticalScroll = value; }
        }

        public ICommand Pan
        {
            get { return _Pan; }
            set { _Pan = value; }
        }

        public ICommand Reset
        {
            get { return _Reset; }
            set { _Reset = value; }
        }
        public DiagramVM()
        {

            ZoomIn = new Command(OnZoomIn);
            ZoomOut = new Command(OnZoomOut);
            ZoomTo = new Command(OnZoomTo);
            VerticalScroll = new Command(OnVerticalScroll);
            HorizontalScroll = new Command(OnHorizontalScroll);
            Pan = new Command(OnPan);
            Reset = new Command(OnReset);

            Nodes = new NodeCollection();
            Theme = new OfficeTheme();

            NodeViewModel node1 = new NodeViewModel()
            {
                OffsetX = 300,
                OffsetY = 100,
                UnitHeight = 40,
                UnitWidth = 80,
                Shape = App.Current.Resources["Rectangle"],
            };

            NodeViewModel node2 = new NodeViewModel()
            {
                OffsetX = 400,
                OffsetY = 100,
                UnitHeight = 40,
                UnitWidth = 80,
                Shape = App.Current.Resources["Rectangle"],
            };

            NodeViewModel node3 = new NodeViewModel()
            {
                OffsetX = 350,
                OffsetY = 200,
                UnitHeight = 40,
                UnitWidth = 80,
                Shape = App.Current.Resources["Rectangle"],
            };

            (Nodes as NodeCollection).Add(node1);
            (Nodes as NodeCollection).Add(node2);
            (Nodes as NodeCollection).Add(node3);
        }

        private void OnReset(object obj)
        {
            (Info as IGraphInfo).Commands.Reset.Execute(null);
        }

        private void OnPan(object obj)
        {
            (Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter() { ZoomCommand = ZoomCommand.Pan, PanDelta = new Point(50, 50) });
        }

        private void OnHorizontalScroll(object obj)
        {
            (Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter() { ZoomCommand = ZoomCommand.HorizondalScroll, ScrollDelta = 50 });
        }

        private void OnVerticalScroll(object obj)
        {
            (Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter() { ZoomCommand = ZoomCommand.VerticalScroll, ScrollDelta = 50 });
        }

        private void OnZoomTo(object obj)
        {
            (Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter() { ZoomCommand = ZoomCommand.Zoom, ZoomTo = 2 });
        }

        private void OnZoomOut(object obj)
        {
            (Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter() { ZoomCommand = ZoomCommand.ZoomOut, ZoomFactor = 0.2 });
        }

        private void OnZoomIn(object obj)
        {
            (Info as IGraphInfo).Commands.Zoom.Execute(new ZoomPositionParameter() { ZoomCommand = ZoomCommand.ZoomIn, ZoomFactor = 0.2 });
        }
    }
}
