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

namespace PreviewEffectWithOutDrag
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Horizontal };
            diagram.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Vertical };
            (diagram.Info as IGraphInfo).ItemTappedEvent += MainWindow_ItemTappedEvent;
            (diagram.Info as IGraphInfo).ItemSelectingEvent += MainWindow_ItemSelectingEvent;
            diagram.PreviewMouseMove += Diagram_PreviewMouseMove;
        }
        private void MainWindow_ItemSelectingEvent(object sender, DiagramPreviewEventArgs args)
        {
            ContentPresenter presenter = DiagramGrid.Children[1] as ContentPresenter;
            presenter.Content = null;
            stencil.SelectedItems.Clear();
        }

        private void Diagram_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            ContentPresenter presenter = DiagramGrid.Children[1] as ContentPresenter;
            if (stencil.SelectedItems != null && stencil.SelectedItems.Any() && stencil.SelectedItems.Count > 0)
            {
                if (stencil.SelectedSymbol != null)
                {
                    ImageSource img = (stencil.SelectedSymbol.Content as Image).Source;
                    Image previewimage = new Image() { Source = img };
                    presenter.Content = previewimage;
                    presenter.Height = 100;
                    presenter.Width = 100;
                    presenter.HorizontalAlignment = HorizontalAlignment.Left;
                    presenter.VerticalAlignment = VerticalAlignment.Top;
                    presenter.RenderTransformOrigin = new Point(0, 0);
                    presenter.RenderTransform = new TranslateTransform() { X = e.GetPosition(diagram).X, Y = e.GetPosition(diagram).Y };
                }
                else
                {
                    presenter.Content = null;
                }
            }
        }

        private void MainWindow_ItemTappedEvent(object sender, ItemTappedEventArgs args)
        {
            if (args.Item is SfDiagram)
            {
                if (stencil.SelectedItems != null && stencil.SelectedItems.Any() && stencil.SelectedItems.Count > 0)
                {
                    foreach (object obj in stencil.SelectedItems)
                    {
                        if (obj is NodeViewModel)
                        {
                            NodeViewModel stencilnode = (NodeViewModel)obj;
                            NodeViewModel node = new NodeViewModel()
                            {
                                UnitHeight = stencilnode.UnitHeight,
                                UnitWidth = stencilnode.UnitWidth,
                                Shape = stencilnode.Shape,
                                OffsetX = (args.MouseEventArgs as MouseButtonEventArgs).GetPosition(diagram).X,
                                OffsetY = (args.MouseEventArgs as MouseButtonEventArgs).GetPosition(diagram).Y,
                            };

                            (diagram.Nodes as NodeCollection).Add(node);
                            ContentPresenter presenter = DiagramGrid.Children[1] as ContentPresenter;
                            presenter.Content = null;
                            stencil.SelectedItems.Clear();
                            break;
                        }
                    }
                }
            }
        }
    }
}
