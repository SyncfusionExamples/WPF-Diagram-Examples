using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Stencil_Style
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            stencil.SelectedFilter = new SymbolFilterProvider { SymbolFilter = SymbolFilter };
        }

        private bool SymbolFilter(SymbolFilterProvider sender, object symbol)
        {
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                ShapeStyle = this.Resources["shapestyle"] as Style,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Key = "Nodes",
            };
            Symbolcollection.Add(node);
            stencil.SymbolGroups = new SymbolGroups()
            {
                new SymbolGroupProvider()
                {
                    MappingName = "Key",
                },
            };
            
            stencil.SelectedFilter = new SymbolFilterProvider { SymbolFilter = SymbolFilter };
        }
    }
}
