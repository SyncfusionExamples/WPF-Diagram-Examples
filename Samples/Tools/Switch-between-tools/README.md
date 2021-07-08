# Switch between tools at runtime through SetTool sample

This sample demonstrates how to set tool to an element at runtime through SetTool() method.

Each object in the [WPF Diagram](https://www.syncfusion.com/wpf-controls/diagram) (SfDiagram) control has default action when interacting on them. Those default actions can be customized by overriding the virtual method [SfDiagram.SetTool](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.SfDiagram~SetTool.html). The SetTool method takes the [SetToolArgs](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.SetToolArgs.html) as an argument that is used to know the objects under the mouse when modifying the tools of them.

[Source](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.SetToolArgs~Source.html) –  To know the object on which item the mouse is interacting.
[Action](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.SetToolArgs~Action.html) - To customize the tools of the diagram object.


__*Documentation*__: https://help.syncfusion.com/wpf/diagram/tools

## Project pre-requisites

To run this application, you need to have the below two in your system

* [Visual Studio 2019](https://www.visualstudio.com/wpf-vs)
* [Syncfusion.SfDiagram.WPF](https://www.nuget.org/packages/Syncfusion.SfDiagram.WPF/) nuget package. To install the package using NuGet Package Manager, refer this [link](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio#nuget-package-manager).

## Deploying and running the sample

* To debug the sample and then run it, press F5 or select Debug > Start Debugging. To run the sample without debugging, press Ctrl+F5 or selectDebug > Start Without Debugging.
