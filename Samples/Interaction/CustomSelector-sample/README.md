# CustomSelector sample
This repository contains sample which shows how to customize the appearance of the selector in [WPF Diagram](https://www.syncfusion.com/wpf-controls/diagram) (SfDiagram).

[WPF Diagram](https://www.syncfusion.com/wpf-controls/diagram) (SfDiagram) has a predefined style for selector but you can customize the selector style by creating a new [Selector](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Diagram.Selector.html) with a custom style. This new custom selector will be returned by overriding the virtual [SfDiagram.GetSelectorForItemOverride](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Diagram.SfDiagram.html#Syncfusion_UI_Xaml_Diagram_SfDiagram_GetSelectorForItemOverride_System_Object_) method.

## Project pre-requisites
To run this application, you need to have the below two in your system

* [Visual Studio 2019](https://www.visualstudio.com/wpf-vs)
* [Syncfusion.SfDiagram.WPF](https://www.nuget.org/packages/Syncfusion.SfDiagram.WPF/) nuget package. To install the package using NuGet Package Manager, refer this [link](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio#nuget-package-manager).

## Deploying and running the sample
* To debug the sample and then run it, press F5 or select Debug > Start Debugging. To run the sample without debugging, press Ctrl+F5 or selectDebug > Start Without Debugging.

KB article - [CustomSelector sample](https://www.syncfusion.com/kb/12282/how-to-customize-the-appearance-of-the-selector-in-wpf-diagram-sfdiagram)
