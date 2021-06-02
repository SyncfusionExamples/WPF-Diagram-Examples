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

namespace MultipleAnnotation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.PageSettings.PageWidth = 1000;
            diagram.PageSettings.PageHeight = 1000;
            //Define the AnnotationCollection
            ObservableCollection<AnnotationEditorViewModel> annotations = new ObservableCollection<AnnotationEditorViewModel>();
            //Define Annotation
            AnnotationEditorViewModel annotation1 = AddAnnotation("Name", this.Resources["viewtemplate"] as DataTemplate, new Point(0.2, 0.3));
            AnnotationEditorViewModel annotation2 = AddAnnotation(":", this.Resources["viewtemplate"] as DataTemplate, new Point(0.5, 0.3));
            AnnotationEditorViewModel annotation3 = AddAnnotation("John", this.Resources["viewtemplate"] as DataTemplate, new Point(0.7, 0.3));
            AnnotationEditorViewModel annotation4 = AddAnnotation("DOB", this.Resources["viewtemplate"] as DataTemplate, new Point(0.2, 0.6));
            AnnotationEditorViewModel annotation5 = AddAnnotation(":", this.Resources["viewtemplate"] as DataTemplate, new Point(0.5, 0.6));
            AnnotationEditorViewModel annotation6 = AddAnnotation("3/3/95", this.Resources["viewtemplate"] as DataTemplate, new Point(0.7, 0.6));
            //Adding Annotation to Collection
            annotations.Add(annotation1);
            annotations.Add(annotation2);
            annotations.Add(annotation3);
            annotations.Add(annotation4);
            annotations.Add(annotation5);
            annotations.Add(annotation6);
            //Define the NodeCollection
            diagram.Nodes = new NodeCollection();
            //Define the Node
            NodeViewModel nodes = new NodeViewModel()
            {
                UnitHeight = 65,
                UnitWidth = 100,
                OffsetX = 100,
                OffsetY = 100,
                Shape=App.Current.Resources["Terminator"],
               Annotations=annotations,
            };
            //Adding Node to Collection
            (diagram.Nodes as NodeCollection).Add(nodes);

        }
       //Method for Annotation
        private AnnotationEditorViewModel AddAnnotation(string content,DataTemplate Template,Point offset)
        {
            AnnotationEditorViewModel text = new AnnotationEditorViewModel();
            text.Content = content;
            text.ViewTemplate = Template;
            text.Offset = offset;
            return text;
        }
    }
}
