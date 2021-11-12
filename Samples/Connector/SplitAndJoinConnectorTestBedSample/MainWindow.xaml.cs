#region Copyright Syncfusion Inc. 2001-2020.
// Copyright Syncfusion Inc. 2001-2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace UserInteraction_Stencil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SfDiagram diagram = new SfDiagram();
            (diagram.Info as IGraphInfo).ItemDropEvent += MainWindow_ItemDropEvent;
            (diagram.Info as IGraphInfo).SymbolDroppingEvent += MainWindow_SymbolDroppingEvent;
        }

        private void MainWindow_SymbolDroppingEvent(object sender, SymbolDroppingEventArgs args)
        {

        }

        private void MainWindow_ItemDropEvent(object sender, ItemDropEventArgs args)
        {
            if (args.ItemSource == Cause.Stencil && (args.Source as INode).Key.ToString() == "Basic Shapes")
            {
                args.Cancel = true;
            }
        }

        private void ConnectorSplitting_Checked(object sender, RoutedEventArgs e)
        {
            diagram.EnableConnectorSplitting = true;
        }

        private void ConnectorSplitting_Unchecked(object sender, RoutedEventArgs e)
        {
            diagram.EnableConnectorSplitting = false;
        }

        private void Routing_Checked(object sender, RoutedEventArgs e)
        {
            diagram.Constraints |= GraphConstraints.Routing;
        }

        private void Routing_Unchecked(object sender, RoutedEventArgs e)
        {
            diagram.Constraints = GraphConstraints.Default & ~GraphConstraints.Routing;
        }

        private void ConnectorType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 0)
            {
                diagram.DefaultConnectorType = Syncfusion.UI.Xaml.Diagram.ConnectorType.Orthogonal;
            }
            else if ((sender as ComboBox).SelectedIndex == 1)
            {
                diagram.DefaultConnectorType = Syncfusion.UI.Xaml.Diagram.ConnectorType.CubicBezier;
            }
            else if ((sender as ComboBox).SelectedIndex == 2)
            {
                diagram.DefaultConnectorType = Syncfusion.UI.Xaml.Diagram.ConnectorType.QuadraticBezier;
            }
            else
            {
                diagram.DefaultConnectorType = Syncfusion.UI.Xaml.Diagram.ConnectorType.Line;
            }
        }
    }


    //Collection of Symbols
    public class SymbolCollection : ObservableCollection<object>
    {

    }    
}
