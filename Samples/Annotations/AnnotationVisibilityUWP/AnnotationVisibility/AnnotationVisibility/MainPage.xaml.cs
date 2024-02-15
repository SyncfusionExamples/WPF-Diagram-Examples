using Syncfusion.UI.Xaml.Diagram.Serializer;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnnotationVisibility
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Diagram.Nodes = new NodeCollection();
            NodeViewModel node = new NodeViewModel()
            {
                UnitWidth = 150,
                UnitHeight = 150,
                OffsetX = 500,
                OffsetY = 300,
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                //Initialize the AnnotationCollection
                Annotations = new ObservableCollection<AnnotationEditorViewModel>()
                 {

                  new Annotation()
                {
                 Content="Annotation",
                 //set visibility as Visible|Collapsed to Annotation content
                 Visibility=Visibility.Collapsed,
                 ViewTemplate=this.Resources["viewTemplate"] as DataTemplate
                }
                }
            };

            (Diagram.Nodes as NodeCollection).Add(node);

        }
    }
}
