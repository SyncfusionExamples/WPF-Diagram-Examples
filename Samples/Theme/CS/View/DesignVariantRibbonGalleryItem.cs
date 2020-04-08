using Syncfusion.UI.Xaml.Diagram.Theming;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ThemeStyle.View
{
    /// <summary>
    ///  Represents a RibbonGalleryItem control.
    /// </summary>
    /// <remarks>RibbonGalleryItem class represents a control that is used to wrap any content inside the Ribbon gallery control.</remarks>
    public class DesignVariantRibbonGalleryItem : RibbonGalleryItem
    {
        /// <summary>
        /// Initializes a new instance of the DesignVariantRibbonGalleryItem class.
        /// </summary>
        public DesignVariantRibbonGalleryItem()
        {
            this.IsEnabledChanged += DesignVariantRibbonGalleryItem_IsEnabledChanged;
        }
        /// <summary>
        /// Occurs when the element is enabled or disables in the user interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignVariantRibbonGalleryItem_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                this.ContentTemplate = App.Current.Resources["ribbonVariantItemTemplate"] as DataTemplate;
            }
            else
            {
                switch (this.Name)
                {
                    case "VariantGallery0":
                        this.ContentTemplate = App.Current.Resources["disabledribbonVariantItemTemplate0"] as DataTemplate;
                        break;
                    case "VariantGallery1":
                    case "VariantGallery2":
                    case "VariantGallery3":
                        this.ContentTemplate = App.Current.Resources["disabledribbonVariantItemTemplate1"] as DataTemplate;
                        break;
                }
            }
        }
        /// <summary>
        /// Represent the ConnectorVariant.
        /// </summary>
        internal Dictionary<StyleId, DiagramItemStyle> ConnectorVariant
        {
            get { return (Dictionary<StyleId, DiagramItemStyle>)GetValue(ConnectorVariantProperty); }
            set { SetValue(ConnectorVariantProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConnectorVariant.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ConnectorVariantProperty =
            DependencyProperty.Register("ConnectorVariant", typeof(Dictionary<StyleId, DiagramItemStyle>), typeof(DesignVariantRibbonGalleryItem), new PropertyMetadata(null));

        /// <summary>
        /// Represent the theme of the diagram.
        /// </summary>
        internal DiagramTheme Theme
        {
            get { return (DiagramTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Theme.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ThemeProperty =
            DependencyProperty.Register("Theme", typeof(DiagramTheme), typeof(DesignVariantRibbonGalleryItem), new PropertyMetadata(null));

    }
}
