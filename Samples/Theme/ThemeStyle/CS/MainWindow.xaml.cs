using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Theming;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using ThemeStyle.View;

namespace ThemeStyle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<object> themeRemovedItems = null;
        List<object> allowThemeRemovedItems = null;
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = sfdiagram;
            themeRemovedItems = new List<object>();
            allowThemeRemovedItems = new List<object>();
      
            (sfdiagram.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;
            (sfdiagram.Info as IGraphInfo).ItemSelectedEvent += MainWindow_ItemSelectedEvent;
            (sfdiagram.Info as IGraphInfo).ItemUnSelectedEvent += MainWindow_ItemUnSelectedEvent;
            this.Loaded += MainWindow_Loaded;
        }
        /// <summary>
        /// Occurs when the mainwindow is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Custom1_ThemeRibbon.Theme = App.Current.Resources["Custom1_ThemeRibbon"] as DiagramTheme;
            Custom2_ThemeRibbon.Theme = App.Current.Resources["Custom2_ThemeRibbon"] as DiagramTheme;
        }
        /// <summary>
        ///  Occurs when the mouse pointer is over the elements and a diagram elements is unselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ItemUnSelectedEvent(object sender, DiagramEventArgs args)
        {
            shapeEffectStylesGallery.IsEnabled = false;
            connectorEffectStylesGallery.IsEnabled = false;
        }
        /// <summary>
        ///  Occurs when the mouse pointer is over the elements and a diagram elements is selected.
        /// </summary>
        private void MainWindow_ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
            foreach (var galleryGroup in shapeEffectStylesGallery.GalleryGroups)
            {
                foreach (var galleryItem in galleryGroup.Items)
                {
                    (galleryItem as RibbonGalleryItem).IsEnabled = true;
                }
            }
            foreach (var galleryGroup in connectorEffectStylesGallery.GalleryGroups)
            {
                foreach (var galleryItem in galleryGroup.Items)
                {
                    (galleryItem as RibbonGalleryItem).IsEnabled = true;
                }
            }
            if (themeRemovedItems.Contains(args.Item))
            {

                shapeRemoveThemesCheckbox.IsChecked = true;
            }
            else
            {
                if (args.Item is INode && (args.Item as INode).Constraints.Contains(NodeConstraints.ThemeStyle))
                {
                    shapeRemoveThemesCheckbox.IsChecked = false;
                }

            }
            if (args.Item is INode)
            {

                connectorEffectStylesGallery.Visibility = Visibility.Collapsed;
                shapeEffectStylesGallery.Visibility = Visibility.Visible;
                connectorEffectStylesGallery.IsEnabled = false;
                shapeEffectStylesGallery.IsEnabled = true;
            }
            else if (args.Item is IConnector)
            {
                connectorEffectStylesGallery.Visibility = Visibility.Visible;
                shapeEffectStylesGallery.Visibility = Visibility.Collapsed;
                shapeEffectStylesGallery.IsEnabled = false;
                connectorEffectStylesGallery.IsEnabled = true;
            }
        }
        /// <summary>
        /// Occurs when the item is added to diagram.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.ItemSource == ItemSource.DrawingTool || args.ItemSource == ItemSource.Stencil)
            {
                if (args.Item is INode)
                {
                    (args.Item as INode).ThemeStyleId = StyleId.Variant1;
                }
                else if (args.Item is IConnector)
                {
                    (args.Item as IConnector).ThemeStyleId = StyleId.Subtly | StyleId.Accent1;
                }
            }

            if (args.Item is INode)
            {
                if ((args.Item as INode).Annotations != null && (args.Item as INode).Annotations is ObservableCollection<IAnnotation>)
                {
                    foreach (TextAnnotationViewModel annotaiton in (args.Item as INode).Annotations as ObservableCollection<IAnnotation>)
                    {
                        
                        (annotaiton as TextAnnotationViewModel).FontFamily = new FontFamily("Calibri");
                    }
                }
            }
            if (args.Item is IConnector)
            {
                if ((args.Item as IConnector).Annotations != null && (args.Item as IConnector).Annotations is ObservableCollection<AnnotationEditorViewModel>)
                {
                    foreach (AnnotationEditorViewModel annotaiton in (args.Item as IConnector).Annotations as ObservableCollection<AnnotationEditorViewModel>)
                    {
                        annotaiton.ViewTemplate = this.Resources["viewTemplate"] as DataTemplate;
                    }
                }
            }

            if (!newItemThemeStyleDecision.IsChecked.Value)
            {
                if (args.Item is INode)
                {
                    (args.Item as INode).Constraints &= ~NodeConstraints.ThemeStyle;
                    (args.Item as INode).ShapeStyle = App.Current.Resources["shapeStyle"] as Style;
                }
                else if (args.Item is IConnector)
                {
                    (args.Item as IConnector).Constraints &= ~ConnectorConstraints.ThemeStyle;
                    (args.Item as IConnector).ConnectorGeometryStyle = App.Current.Resources["geometryStyle"] as Style;
                }
                themeRemovedItems.Add(args.Item);
            }
        }
        /// <summary>
        /// Occurs when changed the selection of VariantRibbonGallery item.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void VariantRibbonGallery_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                if ((e.NewValue as DesignVariantRibbonGalleryItem).Content != null)
                    sfdiagram.Theme.NodeStyles = (e.NewValue as DesignVariantRibbonGalleryItem).Content as Dictionary<StyleId, DiagramItemStyle>;
                if ((e.NewValue as DesignVariantRibbonGalleryItem).ConnectorVariant != null)
                    sfdiagram.Theme.ConnectorStyles = (e.NewValue as DesignVariantRibbonGalleryItem).ConnectorVariant;
            }
        }
        /// <summary>
        /// Occurs when changed the selection of RibbonGallery item.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void RibbonGallery_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                foreach (var galleryGroup in shapeEffectStylesGallery.GalleryGroups)
                {
                    foreach (var galleryItem in galleryGroup.Items)
                    {
                        (galleryItem as RibbonGalleryItem).IsEnabled = true;
                    }
                }
                foreach (var galleryGroup in connectorEffectStylesGallery.GalleryGroups)
                {
                    foreach (var galleryItem in galleryGroup.Items)
                    {
                        (galleryItem as RibbonGalleryItem).IsEnabled = true;
                    }
                }
                foreach (var item in themeRemovedItems)
                {
                    if (item is INode)
                    {
                        (item as INode).Constraints |= NodeConstraints.ThemeStyle;
                    }
                    else if (item is IConnector)
                    {
                        (item as IConnector).Constraints |= ConnectorConstraints.ThemeStyle;
                    }
                }
                themeRemovedItems.Clear();
                SelectorViewModel selector = sfdiagram.SelectedItems as SelectorViewModel;
                if ((selector.Nodes as ObservableCollection<object>).Count > 0)
                {
                    foreach (INode node in selector.Nodes as ObservableCollection<object>)
                    {
                        if (node.Constraints.Contains(NodeConstraints.ThemeStyle))
                        {
                            shapeRemoveThemesCheckbox.IsChecked = false;
                        }
                    }
                }

                if ((e.NewValue as DesignRibbonGalleryItem).Name.Equals("Custom1_ThemeRibbon") ||
                    (e.NewValue as DesignRibbonGalleryItem).Name.Equals("Custom2_ThemeRibbon"))
                {
                    sfdiagram.Theme = VariantGallery0.Theme = (e.NewValue as DesignRibbonGalleryItem).Theme;
                    VariantGallery0.IsEnabled = false;
                    VariantGallery1.IsEnabled = false;
                    VariantGallery2.IsEnabled = false;
                    VariantGallery3.IsEnabled = false;
                    VariantRibbonGallery.SelectedItem = null;
                }
                else
                {
                    VariantGallery0.IsEnabled = true;
                    VariantGallery1.IsEnabled = true;
                    VariantGallery2.IsEnabled = true;
                    VariantGallery3.IsEnabled = true;

                    if (themeRemovedItems != null)
                    {
                        foreach (object item in themeRemovedItems)
                        {
                            if (item is INode)
                            {
                                (item as INode).Constraints |= NodeConstraints.ThemeStyle;
                            }
                            else if (item is IConnector)
                            {
                                (item as IConnector).Constraints |= ConnectorConstraints.ThemeStyle;
                            }
                        }
                    }
                    selector = sfdiagram.SelectedItems as SelectorViewModel;
                    if ((selector.Nodes as ObservableCollection<object>).Count > 0)
                    {
                        shapeEffectStylesGallery.IsEnabled = true;
                        connectorEffectStylesGallery.IsEnabled = false;
                    }
                    else if ((selector.Connectors as ObservableCollection<object>).Count > 0)
                    {
                        shapeEffectStylesGallery.IsEnabled = false;
                        connectorEffectStylesGallery.IsEnabled = true;
                    }

                    sfdiagram.Theme = VariantGallery0.Theme = VariantGallery1.Theme = VariantGallery2.Theme = VariantGallery3.Theme = (e.NewValue as DesignRibbonGalleryItem).Theme;
                    VariantRibbonGallery.SelectedItem = VariantGallery0;
                }

            }
        }
        /// <summary>
        /// Occurs when allows the theme for diagram element.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void shapeAllowThemesCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (sfdiagram != null)
            {
                foreach (INode selectedNode in (sfdiagram.SelectedItems as SelectorViewModel).Nodes as ObservableCollection<object>)
                {
                    if (themeRemovedItems.Contains(selectedNode))
                    {
                        shapeRemoveThemesCheckbox.IsEnabled = false;
                    }

                    selectedNode.Constraints |= NodeConstraints.ThemeStyle;
                }
                foreach (IConnector selectedNode in (sfdiagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
                {
                    selectedNode.Constraints |= ConnectorConstraints.ThemeStyle;
                }
                shapeEffectStylesGallery.IsDropDownOpen = false;
                connectorEffectStylesGallery.IsDropDownOpen = false;
            }
        }
        /// <summary>
        /// Occurs when not allows the theme for diagram element.
        /// </summary>
        private void shapeAllowThemesCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (INode selectedNode in (sfdiagram.SelectedItems as SelectorViewModel).Nodes as ObservableCollection<object>)
            {
                if (themeRemovedItems.Contains(selectedNode))
                {
                    themeRemovedItems.Remove(selectedNode);
                }
                allowThemeRemovedItems.Add(selectedNode);
                selectedNode.Constraints &= ~NodeConstraints.ThemeStyle;
            }
            foreach (IConnector selectedNode in (sfdiagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
            {
                if (themeRemovedItems.Contains(selectedNode))
                {
                    themeRemovedItems.Remove(selectedNode);
                }
                allowThemeRemovedItems.Add(selectedNode);
                selectedNode.Constraints &= ~ConnectorConstraints.ThemeStyle;
            }
            shapeEffectStylesGallery.IsDropDownOpen = false;
            connectorEffectStylesGallery.IsDropDownOpen = false;
        }

        private void shapeRemoveThemesCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (INode selectedNode in (sfdiagram.SelectedItems as SelectorViewModel).Nodes as ObservableCollection<object>)
            {
                if (selectedNode.Constraints.Contains(NodeConstraints.ThemeStyle))
                {
                    themeRemovedItems.Add(selectedNode);
                    selectedNode.Constraints &= ~NodeConstraints.ThemeStyle;

                }
                selectedNode.ShapeStyle = App.Current.Resources["shapeStyle"] as Style;
            }
            foreach (IConnector selectedNode in (sfdiagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
            {
                themeRemovedItems.Add(selectedNode);
                selectedNode.Constraints &= ~ConnectorConstraints.ThemeStyle;
                selectedNode.ConnectorGeometryStyle = App.Current.Resources["geometryStyle"] as Style;
            }
            foreach (var galleryGroup in shapeEffectStylesGallery.GalleryGroups)
            {
                foreach (var galleryItem in galleryGroup.Items)
                {
                    (galleryItem as RibbonGalleryItem).IsEnabled = false;
                }
            }
            foreach (var galleryGroup in connectorEffectStylesGallery.GalleryGroups)
            {
                foreach (var galleryItem in galleryGroup.Items)
                {
                    (galleryItem as RibbonGalleryItem).IsEnabled = false;
                }
            }
            shapeEffectStylesGallery.IsEnabled = false;
            connectorEffectStylesGallery.IsEnabled = false;
            shapeEffectStylesGallery.IsDropDownOpen = false;
            connectorEffectStylesGallery.IsDropDownOpen = false;
        }

        private void shapeEffectStylesGallery_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectorViewModel selector = sfdiagram.SelectedItems as SelectorViewModel;
            if (e.NewValue is HomeRibbonGalleryItem)
            {
                StyleId styleId = (e.NewValue as HomeRibbonGalleryItem).ThemeStyleId;
                foreach (INode selectedNode in selector.Nodes as ObservableCollection<object>)
                {
                    bool removeThemeStyle = false;
                    if (!selectedNode.Constraints.Contains(NodeConstraints.ThemeStyle))
                    {
                        removeThemeStyle = true;
                        selectedNode.Constraints |= NodeConstraints.ThemeStyle;
                    }
                    selectedNode.ThemeStyleId = styleId;
                    if (removeThemeStyle)
                    {
                        selectedNode.Constraints &= ~NodeConstraints.ThemeStyle;
                    }
                }
            }
        }
        /// <summary>
        /// Occurs when changed the connectorEffectStyles.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void connectorEffectStylesGallery_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectorViewModel selector = sfdiagram.SelectedItems as SelectorViewModel;
            if (e.NewValue is HomeRibbonGalleryItem)
            {
                StyleId styleId = (e.NewValue as HomeRibbonGalleryItem).ThemeStyleId;
                foreach (IConnector selectedConnector in selector.Connectors as ObservableCollection<object>)
                {
                    selectedConnector.ThemeStyleId = styleId;
                }
            }
        }

        private void newItemThemeStyleDecision_Checked(object sender, RoutedEventArgs e)
        {
            ribbonGallery.IsDropDownOpen = false;
        }

        private void newItemThemeStyleDecision_Unchecked(object sender, RoutedEventArgs e)
        {
            ribbonGallery.IsDropDownOpen = false;
        }
    }
    /// <summary>
    /// OppositeBooleanConverter
    /// </summary>
    public class OppositeBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
