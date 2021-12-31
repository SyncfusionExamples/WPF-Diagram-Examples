using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Theming;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Container.ViewModel
{
    internal class DiagramVM : DiagramViewModel
    {
        ResourceDictionary resourceDictionary = new ResourceDictionary()
        {
            Source = new Uri(@"/Syncfusion.SfDiagram.Wpf;component/Resources/BasicShapes.xaml", UriKind.RelativeOrAbsolute)
        };

        public DiagramVM()
        {

            HorizontalRuler = new Ruler() { Orientation = Orientation.Horizontal };

            VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };

            Theme = new SimpleTheme();

            NodeViewModel Node6 = CreateNode(650, 150, "Rectangle", "Process");
            NodeViewModel Node7 = CreateNode(850, 150, "Rectangle", "Process");

            CreateContainer(Node6, Node7, "Container", 340, 100, 750, 150);

            //Initialize the item adde command to container style.
            ItemAddedCommand = new DelegateCommand(OnItemAddedCommandExecute);
        }


        #region Helper Methods

        /// <summary>
        /// Command to item added event.
        /// </summary>
        /// <param name="parameter">ItemAddedEventArgs</param>
        private void OnItemAddedCommandExecute(object parameter)
        {
            if (parameter is ItemAddedEventArgs)
            {
                if ((parameter as ItemAddedEventArgs).Item is IContainer)
                {
                    ((parameter as ItemAddedEventArgs).Item as IContainer).Shape = resourceDictionary["Rectangle"];
                }
            }
        }

        /// <summary>
        /// This method is used to create Group
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        private void CreateContainer(NodeViewModel node1, NodeViewModel node2, string content, double width, double height, double offsetX, double offsetY)
        {
            ContainerViewModel container = new ContainerViewModel()
            {
                UnitHeight = height,
                UnitWidth = width,
                OffsetX = offsetX,
                OffsetY = offsetY,
                Nodes = new NodeCollection()
                {
                    node1,
                    node2
                },
                Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = content,
                    },
                },
            };

            container.Shape = resourceDictionary["Rectangle"];

            (Groups as GroupCollection).Add(container);
        }

        /// <summary>
        /// This method is used to create Node
        /// </summary>
        /// <param name="offsetx"></param>
        /// <param name="offsety"></param>
        /// <param name="shape"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private NodeViewModel CreateNode(double offsetx, double offsety, string shape, string content)
        {
            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 60,
                UnitWidth = 100,
                OffsetX = offsetx,
                OffsetY = offsety,
                Shape = resourceDictionary[shape],
            };
            if (content != "")
            {
                node.Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = content,
                    },
                };
            }

            (Nodes as NodeCollection).Add(node);

            return node;
        }

        #endregion
    }
}
