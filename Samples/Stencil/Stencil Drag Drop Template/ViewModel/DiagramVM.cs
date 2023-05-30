using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Shared;
using System.Windows;

namespace Stencil_Drag_Drop_Template.ViewModel
{
  public class DiagramVM:DiagramViewModel
    {
    public DiagramVM()
        {
            // Initialize ItemAdded command to add the content for loaded node
            ItemAddedCommand = new DelegateCommand(OnItemAdded);
            // Need to add new class which are not derived from our ViewModel classess
            // Need to add new types

            KnownTypes = () => new List<Type>()
            {
                typeof(CustomContent)
            };
        }
        private void OnItemAdded(object obj)
        {
            var args = obj as ItemAddedEventArgs;           

            if (args.Item is CustomNode)
            {
                CustomNode node = args.Item as CustomNode;
                //content and contenttemplate returns null, so we have used the CustomContent and CustomContentTemplate properties to restore its values. 
                node.Content = node.CustomContent;
                node.ContentTemplate = App.Current.MainWindow.Resources[node.CustomContentTemplate] as DataTemplate;
            }
        }
    }
}
