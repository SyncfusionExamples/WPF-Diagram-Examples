using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ImageAnnotation
{
    public class ImageAnnotationViewModel: DiagramViewModel
    {
        public ImageAnnotationViewModel()
        {
            //Initialize the nodes and connectors collection
            this.Nodes = new NodeCollection();
            this.Connectors = new ConnectorCollection();

            //Create the node
            NodeViewModel node = new NodeViewModel();
            node.OffsetX = 150;
            node.OffsetY = 150;
            node.UnitHeight = 150;
            node.UnitWidth = 150;

            //Create the annotation collection to the node
            node.Annotations = new AnnotationCollection()
            {
                new AnnotationEditorViewModel()
                {
                    //adding image path as annotation content
                    Content=@"pack://application:,,,/Image/image43.png",
                    VerticalAlignment=VerticalAlignment.Center,
                    HorizontalAlignment=HorizontalAlignment.Center,

                    //adding ViewTemplate property 
                    ViewTemplate=App.Current.Resources["viewtemplate"]as DataTemplate,
                    
                    //adding EditTemplate property
                    EditTemplate=App.Current.Resources["edittemplate"] as DataTemplate

                }
            };
            node.Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 150, 150) };

            //adding shape style to the node
            node.ShapeStyle = App.Current.Resources["nodeshapestyle"] as Style;
            //Adding the created node into node's collection
            (this.Nodes as NodeCollection).Add(node);
        }
    }
}
