using Fit_to_page_command.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fit_to_page_command.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        private ICommand _CanZoomIn;
        private ICommand _FitToWidth;
        private ICommand _FitToHeight;
        private ICommand _FitToPage;
        private ICommand _PageSettings;
        private ICommand _Content;
        private ICommand _CustomRegion;

        public ICommand CanZoomIn
        {
            get { return _CanZoomIn; }
            set { _CanZoomIn = value; }
        }

        public ICommand FitToWidth
        {
            get { return _FitToWidth; }
            set { _FitToWidth = value; }
        }

        public ICommand FitToHeight
        {
            get { return _FitToHeight; }
            set { _FitToHeight = value; }
        }
        public ICommand FitToPage
        {
            get { return _FitToPage; }
            set { _FitToPage = value; }
        }

        public ICommand PageSettingsCommand
        {
            get { return _PageSettings; }
            set { _PageSettings = value; }
        }

        public ICommand Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        public ICommand CustomRegion
        {
            get { return _CustomRegion; }
            set { _CustomRegion = value; }
        }

        public DiagramVM()
        {
            CanZoomIn = new Command(OnCanZoomIn);
            FitToWidth = new Command(OnFitToWidth);
            FitToHeight = new Command(OnFitToHeight);
            FitToPage = new Command(OnFitToPage);
            PageSettingsCommand = new Command(OnPageSettings);
            Content = new Command(OnContentCommand);
            CustomRegion = new Command(OnCustomRegion);

            Nodes = new NodeCollection();
            Theme = new OfficeTheme();
            PageSettings = new PageSettings();
            PageSettings.PageHeight = 1000;
            PageSettings.PageWidth = 800;
            PageSettings.MultiplePage = true;
            PageSettings.ShowPageBreaks = true;


            NodeViewModel node1 = new NodeViewModel()
            {
                OffsetX = 300,
                OffsetY = 100,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Rectangle"],
            };

            NodeViewModel node2 = new NodeViewModel()
            {
                OffsetX = 700,
                OffsetY = 900,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Rectangle"],
            };

            NodeViewModel node3 = new NodeViewModel()
            {
                OffsetX = 1500,
                OffsetY = 200,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Rectangle"],
            };

            NodeViewModel node4 = new NodeViewModel()
            {
                OffsetX = 500,
                OffsetY = 300,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Rectangle"],
            };

            NodeViewModel node5 = new NodeViewModel()
            {
                OffsetX = 900,
                OffsetY = 400,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Rectangle"],
            };

            NodeViewModel node6 = new NodeViewModel()
            {
                OffsetX = 400,
                OffsetY = 500,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Rectangle"],
            };

            (Nodes as NodeCollection).Add(node1);
            (Nodes as NodeCollection).Add(node2);
            (Nodes as NodeCollection).Add(node3);
            (Nodes as NodeCollection).Add(node4);
            (Nodes as NodeCollection).Add(node5);
            (Nodes as NodeCollection).Add(node6);
        }

        private void OnCustomRegion(object obj)
        {
            (Info as IGraphInfo).Commands.FitToPage.Execute(new FitToPageParameter() { Region = Region.Custom, FocusArea = new Rect(100, 100, 500, 500) });
        }

        private void OnContentCommand(object obj)
        {
            (Info as IGraphInfo).Commands.FitToPage.Execute(new FitToPageParameter() { Region = Region.Content });
        }

        private void OnPageSettings(object obj)
        {
            (Info as IGraphInfo).Commands.FitToPage.Execute(new FitToPageParameter() { Region = Region.PageSettings });
        }

        private void OnFitToPage(object obj)
        {
            (Info as IGraphInfo).Commands.FitToPage.Execute(null);
        }

        private void OnFitToHeight(object obj)
        {
            (Info as IGraphInfo).Commands.FitToPage.Execute(new FitToPageParameter() { FitToPage = Syncfusion.UI.Xaml.Diagram.FitToPage.FitToHeight });
        }

        private void OnFitToWidth(object obj)
        {
            (Info as IGraphInfo).Commands.FitToPage.Execute(new FitToPageParameter() { FitToPage = Syncfusion.UI.Xaml.Diagram.FitToPage.FitToWidth });
        }

        private void OnCanZoomIn(object obj)
        {
            (Info as IGraphInfo).Commands.FitToPage.Execute(new FitToPageParameter() { CanZoomIn = true });
        }
    }
}
