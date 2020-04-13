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
    public class HomeRibbonGalleryItem : RibbonGalleryItem
    {
        private StyleId _mThemeStyleID;
        /// <summary>
        /// Initializes a new instance of the HomeRibbonGalleryItem class.
        /// </summary>
        public HomeRibbonGalleryItem()
        {
            this.Loaded += HomeRibbonGalleryItem_Loaded;
            this.IsEnabledChanged += HomeRibbonGalleryItem_IsEnabledChanged;
        }
        /// <summary>
        /// Occurs when the element is enabled or disables in the user interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeRibbonGalleryItem_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                this.ContentTemplate = App.Current.Resources["effectStyleItemTemplate"] as DataTemplate;
            }
            else
            {
                this.ContentTemplate = App.Current.Resources["disabledeffectStyleItemTemplate"] as DataTemplate;
            }
        }
        /// <summary>
        /// Occurs when the element is laid out, rendered, and ready for interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeRibbonGalleryItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsEnabled)
            {
                this.ContentTemplate = App.Current.Resources["effectStyleItemTemplate"] as DataTemplate;
            }
            else
            {
                this.ContentTemplate = App.Current.Resources["disabledeffectStyleItemTemplate"] as DataTemplate;
            }
        }
        /// <summary>
        /// Gets or sets the ThemeStyleId.
        /// </summary>
        public StyleId ThemeStyleId
        {
            get { return _mThemeStyleID; }
            set
            {
                if (_mThemeStyleID != value)
                {
                    _mThemeStyleID = value;
                }
            }
        }

    }
}
