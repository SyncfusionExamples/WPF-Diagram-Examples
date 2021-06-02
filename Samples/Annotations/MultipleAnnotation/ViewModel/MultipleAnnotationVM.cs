using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultipleAnnotation
{
    public class MultipleAnnotationVM : DiagramViewModel
    {

        ResourceDictionary resourceDictionary = new ResourceDictionary()
        {
            Source = new Uri(@"/Syncfusion.SfDiagram.Wpf;component/Resources/BasicShapes.xaml", UriKind.RelativeOrAbsolute)
        };

        public MultipleAnnotationVM()
        {
            //Define the NodeCollection
            this.Nodes = new NodeCollection();

            //Define the connectors collection
            this.Connectors = new ConnectorCollection();

            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 100,
                OffsetY = 100,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = resourceDictionary["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Annotation",
                        Offset = new Point(0,0),
                    },
                    new AnnotationEditorViewModel()
                    {
                        Content = "Annotation",
                        Offset = new Point(0.5,0.5),
                    },
                    new AnnotationEditorViewModel()
                    {
                        Content = "Annotation",
                        Offset = new Point(1,1),
                    },
                }
            };
            (Nodes as NodeCollection).Add(node);

            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourcePoint = new Point(250, 50),
                TargetPoint = new Point(350, 150),
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Annotation",
                        Offset = new Point(0,0),
                        Length = 0,
                    },
                    new AnnotationEditorViewModel()
                    {
                        Content = "Annotation",
                        Offset = new Point(0,0),
                        Length = 0.5,
                    },
                     new AnnotationEditorViewModel()
                    {
                        Content = "Annotation",
                        Offset = new Point(0,0),
                        Length = 1,
                    },
                },
            };

            (Connectors as ConnectorCollection).Add(connector);
        }
    }
}
