# Sample to drag diagram objects in positive side

This repository contains sample shows how to drag diagram elements in positive side of the page in [WPF Diagram](https://www.syncfusion.com/wpf-controls/diagram) (SfDiagram).

[WPF Diagram](https://www.syncfusion.com/wpf-controls/diagram) (SfDiagram) objects can be restricted to drag on positive regions of the diagram page alone by using the [DragLimit](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.ScrollSettings~DragLimit.html) and [EditableArea](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.ScrollSettings~EditableArea.html) properties of the [ScrollSettings](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.ScrollSettings.html) class and [SelectorChangedEvent](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.IGraphInfo~SelectorChangedEvent_EV.html) in [WPF Diagram](https://www.syncfusion.com/wpf-controls/diagram) (SfDiagram) control. Drag limitation will be enabled by the [Block](https://help.syncfusion.com/cr/wpf/Syncfusion.SfDiagram.WPF~Syncfusion.UI.Xaml.Diagram.SelectorChangedEventArgs~Block.html) argument of SelectorChangedEvent.

__*Documentation*__: https://help.syncfusion.com/wpf/diagram/scroll-settings/draglimit

## Project pre-requisites

To run this application, you need to have the below two in your system

* [Visual Studio 2019](https://www.visualstudio.com/wpf-vs)
* [Syncfusion.SfDiagram.WPF](https://www.nuget.org/packages/Syncfusion.SfDiagram.WPF/) nuget package. To install the package using NuGet Package Manager, refer this [link](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio#nuget-package-manager).

## Deploying and running the sample

* To debug the sample and then run it, press F5 or select Debug > Start Debugging. To run the sample without debugging, press Ctrl+F5 or selectDebug > Start Without Debugging.

KB article - [Sample to drag diagram objects in positive side](https://www.syncfusion.com/kb/11521/how-to-restrict-the-diagram-objects-dragging-in-the-positive-side-in-the-wpf-diagram)
