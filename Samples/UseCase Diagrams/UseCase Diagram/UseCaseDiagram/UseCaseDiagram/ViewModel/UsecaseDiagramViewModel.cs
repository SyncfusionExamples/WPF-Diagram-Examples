using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UseCaseDiagram.ViewModel
{
    public class UsecaseDiagramViewModel : DiagramViewModel
    {
        private static Style _mStaticGeometryStyle;
        private static Style _mStaticTargetDecoratorStyle;
        ResourceDictionary resourceDictionary = new ResourceDictionary()
        {
            Source = new Uri(@"/Syncfusion.SfDiagram.Wpf;component/Resources/BasicShapes.xaml", UriKind.RelativeOrAbsolute)
        };
       

        public UsecaseDiagramViewModel()
        {
            this.Nodes = new ObservableCollection<NodeViewModel>();
            this.Connectors = new ObservableCollection<ConnectorViewModel>();
          
            InitializeDiagram();
        }
        private void InitializeDiagram()
        {
            ContainerViewModel container = CreateContainer("<<Sub System>> \n Online Shopping", 250, 560, 540, 350);
            NodeViewModel n2 = CreateUserNode("User1", 130, 330, 70, 50, "User", "Web \n Customer");
            NodeViewModel n3 = CreateUserNode("User2", 270, 220, 70, 50, "User", "Registered \n Customer");
            NodeViewModel n4 = CreateUserNode("User3", 270, 500, 70, 50, "User", "New Customer");
            NodeViewModel n5 = CreateNode("UseCase1", 560, 160, 40, 120, "UseCase", "View Items");
            NodeViewModel n6 = CreateNode("UseCase2", 560, 290, 50, 120, "UseCase", "Make Purchase");
            NodeViewModel n7 = CreateNode("UseCase3", 560, 420, 40, 120, "UseCase", "Checkout");
            NodeViewModel n8 = CreateNode("UseCase4", 560, 550, 40, 120, "UseCase", "Client Register");
            NodeViewModel n9 = CreateUserNode("User4", 850, 120, 70, 50, "User", "<<Service>> \n Authentication");
            NodeViewModel n10 = CreateUserNode("User5", 850, 330, 70, 50, "User", "Identity Provider");
            NodeViewModel n11 = CreateUserNode("User6", 850, 470, 70, 50, "User", "Credit Payment \n Service");
            NodeViewModel n1 = CreateUserNode("User7", 850, 600, 70, 50, "User", "Paypal");
      
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n1);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n2);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n3);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n4);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n5);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n6);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n7);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n8);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n9);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n10);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(n11);
            container.Nodes = new NodeCollection()
            {
                n8, n5,n6, n7
            };
            CreateConnectors("c1", "User2", "UseCase1");
            CreateConnectors("c2", "User2", "UseCase2");
            CreateConnectors("c3", "User3", "UseCase1");
            CreateConnectors("c4", "User3", "UseCase4");
            CreateConnectors("c5", "User2", "User1");
            CreateConnectors("c6", "User3", "User1");
            CreateConnectors("c7", "UseCase2", "UseCase1");
            CreateConnectors("c8", "UseCase2", "UseCase3");
            CreateConnectors("c9", "User4", "UseCase1");
            CreateConnectors("c10", "User4", "UseCase3");
            CreateConnectors("c11", "User4", "UseCase4");
            CreateConnectors("c12", "User5", "UseCase1");
            CreateConnectors("c13", "User5", "UseCase3");
            CreateConnectors("c14", "User6", "UseCase3");
            CreateConnectors("c15", "User7", "UseCase3");
        }
        private NodeViewModel CreateNode(string name, double offsetX, double offsetY, double height, double width, string pathData, string annotation)
        {
            NodeViewModel node = new NodeViewModel()
            {
                ID = name,
                Name = name,
                OffsetX = offsetX ,
                OffsetY = offsetY,
                UnitHeight = height,
                UnitWidth = width,
                Shape = resourceDictionary[pathData],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                   new TextAnnotationViewModel()
                   {
                       Text = annotation,
                         FontSize = 14,
                       Foreground = new SolidColorBrush(Colors.Black),
                        
                   },
                }


            };
            return node;
        }
        private NodeViewModel CreateUserNode(string name, double offsetX, double offsetY, double height, double width, string pathData, string annotation)
        {
            NodeViewModel node = new NodeViewModel()
            {
                ID = name,
                Name = name,
                OffsetX = offsetX,
                OffsetY = offsetY,
                UnitHeight = height,
                UnitWidth = width,
                Shape = resourceDictionary[pathData],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                   new TextAnnotationViewModel()
                   {
                       Text = annotation,
                         FontSize = 12,
                         WrapText = TextWrapping.NoWrap,
                       Foreground = new SolidColorBrush(Colors.Black),
                       Background = new SolidColorBrush(Colors.White),
                       FontWeight = FontWeights.Bold,   
                        Margin = new Thickness(0, 0, 0, -55)
                   },
                }


            };
            return node;
        }
        private ContainerViewModel CreateContainer(string content, double width, double height, double offsetX, double offsetY)
        {
            ContainerViewModel container = new ContainerViewModel()
            {
                UnitHeight = height,
                UnitWidth = width,
                OffsetX = offsetX,
                OffsetY = offsetY,
                Constraints = NodeConstraints.Default  & ~NodeConstraints.AllowDrop,

            };
            container.Header = new ContainerHeaderViewModel()
            {
                UnitHeight = 40,
                Annotation = new AnnotationEditorViewModel()
                {
                    Content = content,
                    WrapText = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(-120, 0, 0, 0),
                    FontWeight= FontWeights.Bold,
                },

            };
            (Groups as GroupCollection).Add(container);

            return container;
        }
        private void CreateConnectors(string id, string sourceID, string targetID)
        {
            ConnectorViewModel connector = new ConnectorViewModel()
            {
                ID = id,
                SourceNodeID = sourceID,
                TargetNodeID = targetID,
            };
            if(connector.ID.ToString() == "c1" || connector.ID.ToString() == "c2" || connector.ID.ToString() == "c3" || connector.ID.ToString() == "c4" || connector.ID.ToString() == "c9" || connector.ID.ToString() == "c10" || connector.ID.ToString() == "c11" || connector.ID.ToString() == "c14" || connector.ID.ToString() == "c12" || connector.ID.ToString() == "c13" || connector.ID.ToString() == "c15")
            {
                SetDefultConnectorGeometryStyle(connector.ID.ToString());
                connector.ConnectorGeometryStyle = _mStaticGeometryStyle;
            }
            if(connector.ID.ToString() == "c7")
            {
                SetDefultConnectorGeometryStyle(connector.ID.ToString());
                SetTargetDecoratorStyle();
                connector.ConnectorGeometryStyle = _mStaticGeometryStyle;

                connector.TargetDecoratorStyle = _mStaticTargetDecoratorStyle;
                connector.TargetDecorator = resourceDictionary["OpenSharp"];
      
                connector.Annotations = new ObservableCollection<IAnnotation>()
                {
                   new AnnotationEditorViewModel()
                   {
                        Content="<<include>>",
                        Background= new SolidColorBrush(Colors.White),
                        RotateAngle = 90,
                   }
                };
            }
            if (connector.ID.ToString() == "c8")
            {
                SetDefultConnectorGeometryStyle(connector.ID.ToString());
                SetTargetDecoratorStyle();
                connector.ConnectorGeometryStyle = _mStaticGeometryStyle;

                connector.TargetDecoratorStyle = _mStaticTargetDecoratorStyle;
                connector.TargetDecorator = resourceDictionary["OpenSharp"];

                connector.Annotations = new ObservableCollection<IAnnotation>()
                {
                   new AnnotationEditorViewModel()
                   {
                        Content="<<include>>",
                        Background= new SolidColorBrush(Colors.White),
                       RotateAngle = -90,
                   }
                };

            }
            if (connector.ID.ToString() == "c5" || connector.ID.ToString() == "c6")
            {
                SetDefultConnectorGeometryStyle(connector.ID.ToString());
                connector.ConnectorGeometryStyle = _mStaticGeometryStyle;
                SetTargetDecoratorStyle();
                connector.TargetDecoratorStyle = _mStaticTargetDecoratorStyle;
                connector.TargetDecorator = resourceDictionary["ClosedASME"];
            }
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector);
        }
        private static void SetTargetDecoratorStyle()
        {
            _mStaticTargetDecoratorStyle = new Style()
            {
                TargetType = typeof(Shape),
            };
            _mStaticTargetDecoratorStyle.Setters.Add(
                new Setter()
                {
                    Property = Shape.StrokeProperty,
                    Value = new SolidColorBrush(Colors.Black),
                });
            _mStaticTargetDecoratorStyle.Setters.Add(
           new Setter()
           {
               Property = Shape.StretchProperty,
               Value = Stretch.Fill
           });
            _mStaticTargetDecoratorStyle.Setters.Add(
          new Setter()
          {
              Property = Shape.FillProperty,
              Value = new SolidColorBrush(Colors.White)
          });
        }
        private static void SetDefultConnectorGeometryStyle(string d)
        {
            DoubleCollection param = new DoubleCollection() { 5.0, 5.0 };
            _mStaticGeometryStyle = new Style()
            {
                TargetType = typeof(Shape),
            };
            _mStaticGeometryStyle.Setters.Add(
                new Setter()
                {
                    Property = Shape.StrokeProperty,
                    Value = new SolidColorBrush(Colors.Black)
                });
            _mStaticGeometryStyle.Setters.Add(
                new Setter()
                {
                    Property = Shape.StrokeThicknessProperty,
                    Value = 1d
                });
            if (d == "c7" || d == "c8")
            {
                _mStaticGeometryStyle.Setters.Add(
                    new Setter()
                    {
                        Property = Shape.StrokeDashArrayProperty,
                        Value = param
                    });
            }
        }
       
    }
}
