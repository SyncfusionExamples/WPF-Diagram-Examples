using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;

namespace DiagrammingApplication.ViewModel
{
    public class CustomDiagramViewModel : DiagramViewModel
    {
        public CustomDiagramViewModel()
        {
            Symbolfilters = new SymbolFilters();

            SymbolFilterProvider basicshapes = new SymbolFilterProvider { Content = "Basic Shapes", SymbolFilter = Filter };
            SymbolFilterProvider flowshapes = new SymbolFilterProvider { Content = "Flow Shapes", SymbolFilter = Filter };
            SymbolFilterProvider dataflowshapes = new SymbolFilterProvider { Content = "DataFlow Shapes", SymbolFilter = Filter };
            SymbolFilterProvider arrowshapes = new SymbolFilterProvider { Content = "Arrow Shapes", SymbolFilter = Filter };

            this.Symbolfilters.Add(basicshapes);
            this.Symbolfilters.Add(flowshapes);
            this.symbolfilters.Add(dataflowshapes);
            this.Symbolfilters.Add(arrowshapes);
            this.Selectedfilter = Symbolfilters[0];
        }

        // Define filtering of Symbols
        private bool Filter(SymbolFilterProvider sender, object symbol)
        {
            if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
            {
                return true;
            }
            if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
            {
                return true;
            }
            if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
            {
                return true;
            }
            return false;
        }

        private SymbolFilters symbolfilters;

        public SymbolFilters Symbolfilters
        {
            get
            {
                return symbolfilters;
            }
            set
            {
                symbolfilters = value;
                OnPropertyChanged("Symbolfilters");
            }
        }

        private SymbolFilterProvider selectedfilter;

        public SymbolFilterProvider Selectedfilter
        {
            get
            {
                return selectedfilter;
            }
            set
            {
                selectedfilter = value;
                OnPropertyChanged("Selectedfilter");
            }
        }
    }
}
