using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Panels;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Drawing;

namespace ContainerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.Groups = new GroupCollection();
            diagram.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler();
            diagram.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = Orientation.Vertical,
            };

            diagram.DefaultConnectorType = ConnectorType.Line;

            (diagram.Info as IGraphInfo).AnnotationChanged += MainWindow_AnnotationChanged;


            ObservableCollection<GroupViewModel> groups = new ObservableCollection<GroupViewModel>();
            ContainerViewModel container = new ContainerViewModel()
            {
                OffsetX = 300,
                OffsetY = 300,
                UnitHeight = 250,
                UnitWidth = 300,
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content =  "Container",
                    }
                },
                Nodes = new ObservableCollection<NodeViewModel>()
                {

                },
            };
            container.Header = new ContainerHeaderViewModel()
            {
                UnitHeight = 40,
                Annotation = new AnnotationEditorViewModel()
                {
                    Content = "Container Title",
                    Foreground = new SolidColorBrush(Colors.AliceBlue),
                },
            };


            ContainerViewModel container1 = new ContainerViewModel()
            {
                OffsetX = 800,
                OffsetY = 300,
                UnitHeight = 250,
                UnitWidth = 300,
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {

                        Content =  "Container",
                    }
                },
                Nodes = new ObservableCollection<NodeViewModel>()
                {

                },
            };

            groups.Add(container);
            groups.Add(container1);
            diagram.Groups = groups;

        }

        private void MainWindow_AnnotationChanged(object sender, ChangeEventArgs<object, AnnotationChangedEventArgs> args)
        {
            if (args.Item is AnnotationEditorViewModel && (args.Item as AnnotationEditorViewModel).Content != null && args.InitialValue.OriginalSource != null)
            {
                var Annotation = (args.Item as AnnotationEditorViewModel).Content as string;
                var WidthOfTheAnnotation = getTextSize(Annotation);

                if ((args.InitialValue.OriginalSource as AnnotationEditor).Parent != null)
                {
                    // Get the parent of the Annotation.
                    var ParentOfTheAnnotation = ((args.InitialValue.OriginalSource as AnnotationEditor).Parent as AnnotationPanel).DataContext;


                    if (ParentOfTheAnnotation is ContainerViewModel)
                    {
                        // This for container without header
                        if (WidthOfTheAnnotation >= (ParentOfTheAnnotation as ContainerViewModel).UnitWidth)
                        {
                            var value = WidthOfTheAnnotation - (ParentOfTheAnnotation as ContainerViewModel).UnitWidth;
                            (ParentOfTheAnnotation as ContainerViewModel).UnitWidth += value;
                        }


                    }
                    else
                    {
                        //This for container with header
                        foreach (var container in diagram.Groups as IEnumerable<object>)
                        {
                            if (((container as ContainerViewModel).Header as ContainerHeaderViewModel) == ParentOfTheAnnotation)
                            {
                                if (WidthOfTheAnnotation >= (container as ContainerViewModel).UnitWidth)
                                {
                                    var value = WidthOfTheAnnotation - (container as ContainerViewModel).UnitWidth;
                                    (container as ContainerViewModel).UnitWidth += value;
                                }
                            }
                        }
                    }
                }

            }

        }
        private float getTextSize(string text)
        {
            //If we update the font weight or font size we need to update this value to half of the Font size.
            Font font = new Font("Courier New", 6.0F);
            System.Drawing.Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString(text, font);
            return size.Width;
        }

    }
}

