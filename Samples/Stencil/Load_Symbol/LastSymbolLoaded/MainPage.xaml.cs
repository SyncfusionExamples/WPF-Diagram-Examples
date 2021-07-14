using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace stencilcheck
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //Initialize the Selected Filter 
            symbolstencil.SelectedFilter = new SymbolFilterProvider() { Content = "All", Filter = Filter };
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        //Filter Predicate
        private bool Filter(SymbolFilterProvider sender, ISymbol symbol)
        {
            if (sender.Content.Equals("All"))
            {
                return true;
            }
            return false;
        }

        //Button Click - To Search the Symbols
        private async void Serach_OnClick(object sender, RoutedEventArgs e)
        {
            progress.Visibility = Visibility.Visible;

            SymbolCollection search = (SymbolCollection)symbolstencil.SymbolSource;
            SymbolCollection newCollection = new SymbolCollection();

            //Event to notify the LastSymbol Loaded.
            symbolstencil.SymbolsLoaded += symbolstencil_SymbolsLoaded;

            await Task.Delay(500);

            //Search Symbol Based on Key Value
            foreach (ISymbol symbol in search)
            {
                string s = symbol.Key.ToString().ToLowerInvariant();
                if (s.Contains(symname.Text))
                {
                    newCollection.Add(symbol);
                }
            }

            //Initialize the Selected Filter 
            symbolstencil.SelectedFilter = new SymbolFilterProvider() { Content = "All", Filter = Filter };
            symbolstencil.SymbolSource = newCollection;          
        }

        //Event to notify the Symbol Loaded.
        void symbolstencil_SymbolsLoaded(object sender, SymbolsLoadedEventArgs args)
        {
            //Updating the Progress Bar
            progress.Visibility = Visibility.Collapsed;

        }
    }

    public class FloorPlanSymbolItem : ISymbol
    {
        public object Symbol
        {
            get;
            set;
        }

        public object Key { get; set; }

        public DataTemplate SymbolTemplate
        {
            get;
            set;
        }

        public string GroupName { get; set; }

        public ISymbol Clone()
        {
            return new FloorPlanSymbolItem()
            {
                Symbol = this.Symbol,
                SymbolTemplate = this.SymbolTemplate
            };
        }
    }

    public class SymbolCollection : ObservableCollection<ISymbol>
    {

    }

    public class Con : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
