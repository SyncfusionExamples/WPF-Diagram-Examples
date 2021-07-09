# Organization chart with drag drop from stencil sample
This sample demonstrate the automatic arrangements of the nodes to create oraganization chart by dragging and droppings nodes from stencil.

The [ItemDropEvent](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.IGraphInfo~ItemDropEvent_EV.html) is triggered when the [Node](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.NodeViewModel.html) or [Connector](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.ConnectorViewModel.html) is dragged and dropped to another once the [AllowDrop](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.NodeConstraints.html) Constraints is enabled for Node or Connector in [WPF Diagram](https://www.syncfusion.com/wpf-controls/diagram) (SfDiagram). Here, you get the source of the argument as drag item and Target as dropped items (List of items). This event helps us to reposition the nodes and relationship.

__*Documentation*__: https://help.syncfusion.com/wpf/diagram/automatic-layouts#how-to-create-a-parent---child-relation-with-dropped-nodes-from-stencil

## Project pre-requisites
To run this application, you need to have the below two in your system

* [Visual Studio 2019](https://www.visualstudio.com/wpf-vs)
* [Syncfusion.SfDiagram.WPF](https://www.nuget.org/packages/Syncfusion.SfDiagram.WPF/) nuget package. To install the package using NuGet Package Manager, refer this [link](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio#nuget-package-manager).

## Deploying and running the sample
* To debug the sample and then run it, press F5 or select Debug > Start Debugging. To run the sample without debugging, press Ctrl+F5 or selectDebug > Start Without Debugging.
