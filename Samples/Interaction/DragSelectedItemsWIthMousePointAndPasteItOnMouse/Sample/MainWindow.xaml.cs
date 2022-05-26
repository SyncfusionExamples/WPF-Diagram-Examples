using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
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

namespace ShowSelectedItemsPreview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Point cursorPosition;
        private Point? intialLocation;
        private Point? selectorPosition;
        public MainWindow()
        {
            InitializeComponent();
            //Adding KeyDown, MouseMove events.
            this.KeyDown += MainWindow_KeyDown;
            this.MouseMove += MainWindow_MouseMove;

            //Adding ItemAdded event of SfDiagram
            (diagram.Info as IGraphInfo).ItemAdded += OnDiagramItemAdded;
        }

        //Method to execute MouseMoveEvent
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //Getting current mouse position of diagram.
            cursorPosition = e.GetPosition(diagram.Page);
            //Setting location to the canvas in which exported stream image is showing.
            if (CopiedImageCanvas.Visibility == Visibility.Visible)
            {
                Canvas.SetLeft(CopiedImage, e.GetPosition(this).X);
                Canvas.SetTop(CopiedImage, e.GetPosition(this).Y);
            }
        }

        //Method to execute KeyDownEvent
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //When Ctrl+C keys are pressed.
            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                MemoryStream stream = new MemoryStream();
                //Getting bounds of selected items using SelectorViewModel.
                Rect SelectedItemsBounds = ((diagram.SelectedItems as SelectorViewModel).Info as ISelectorInfo).Bounds;

                //Setting ExportStream as stream to export the diagram as stream and passing selected elements bounds to Clip property.
                ExportSettings settings = new ExportSettings()
                {
                    Clip = SelectedItemsBounds,
                    ExportStream = stream
                };

                diagram.ExportSettings = settings;
                //Method to export the clipped SfDiagram as stream.
                diagram.Export();
                //Bringing canvas image to visibility
                CopiedImageCanvas.Visibility = Visibility.Visible;

                //Convert stream to image
                stream.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage bitImage = new BitmapImage();
                bitImage.BeginInit();
                //System.IO.MemoryStream MS = new System.IO.MemoryStream();
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                bitImage.StreamSource = stream;
                bitImage.EndInit();
                //setting source to image.
                CopiedImage.Source =  bitImage;
                CopiedImage.Opacity = 0.6;
            }

            //when paste operation is happened by pressing Ctrl+V keys are pressed.
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                //hidding exported copied image.
                CopiedImageCanvas.Visibility = Visibility.Hidden;
                //clearing the image source.
                CopiedImage.Source = null;
            }
        }

        //Method to execute ItemAddedEvent.
        private void OnDiagramItemAdded(object sender, ItemAddedEventArgs args)
        {
            var item = args.Item as IGroupable;
            var itemInfo = item.Info as IGroupableInfo;
            if (item != null && item.ID == null)
            {
                //Generating GUID for pasted items.
                item.ID = Guid.NewGuid();
            }

            if (args.ItemSource == ItemSource.ClipBoard)
            {
                var pasteInfo = args.Info as PasteCommandInfo;
                if (pasteInfo != null)
                {
                    if (pasteInfo.InteractionState == InteractionState.Start || intialLocation == null)
                    {
                        var bounds = itemInfo.Bounds;
                        intialLocation = new Point(bounds.X + bounds.Width * 0.5, bounds.Y + bounds.Height * 0.5);
                    }

                    if (item is INode)
                    {
                        // Set the node in the cursor position
                        var node = item as INode;
                        node.OffsetX = cursorPosition.X + (node.OffsetX - intialLocation.Value.X);
                        node.OffsetY = cursorPosition.Y + (node.OffsetY - intialLocation.Value.Y);
                    }

                    if (pasteInfo.InteractionState == InteractionState.Completed)
                    {
                        // To remove default node translation done for pasted nodes
                        var selector = diagram.SelectedItems as SelectorViewModel;
                        selectorPosition = new Point(selector.OffsetX, selector.OffsetY);
                        selector.PropertyChanged += Selector_PropertyChanged;

                        intialLocation = null;
                    }
                }
            }
        }

        //Method to update the offset x, y value to selectors.
        private void Selector_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var selector = sender as SelectorViewModel;
            if (selectorPosition != null)
            {
                if (e.PropertyName == "OffsetX" && selector.OffsetX != selectorPosition.Value.X)
                {
                    selector.OffsetX = selectorPosition.Value.X;
                }
                else if (e.PropertyName == "OffsetY" && selector.OffsetY != selectorPosition.Value.Y)
                {
                    selector.OffsetY = selectorPosition.Value.Y;
                    selectorPosition = null;
                    selector.PropertyChanged -= Selector_PropertyChanged;
                }
            }
        }
    }
}
