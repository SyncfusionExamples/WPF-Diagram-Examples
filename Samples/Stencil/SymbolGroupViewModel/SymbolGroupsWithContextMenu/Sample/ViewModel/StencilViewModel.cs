using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StencilCategory.ViewModel
{
    public class StencilViewModel : DiagramViewModel
    {
        
        private SymbolGroupDisplayMode symbolGroupDisplayMode = SymbolGroupDisplayMode.Accordion;
       

        public StencilViewModel()
        {
            //Symbolfilters = new SymbolFilters();
            //symbolfilters.Add(AddNewFilter("All", true));
            //symbolfilters.Add(AddNewFilter("Basic Shapes Filter", true));
            //symbolfilters.Add(AddNewFilter("Flow Shapes Filter", true));
            //symbolfilters.Add(AddNewFilter("Arrow Shapes Filter", true));
            //symbolfilters.Add(AddNewFilter("DataFlow Shapes Filter"));
            //symbolfilters.Add(AddNewFilter("UMLActivity Shapes Filter"));
            //symbolfilters.Add(AddNewFilter("UMLUseCase Shapes Filter", true));
            //symbolfilters.Add(AddNewFilter("UMLRelationship Shapes Filter"));
            //symbolfilters.Add(AddNewFilter("Swimlane Shapes Filter", true));
            //symbolfilters.Add(AddNewFilter("BPMNEditor Shapes Filter"));
            //this.Selectedfilter = Symbolfilters[0];
        }

        //private SymbolFilterProvider AddNewFilter(string name, bool isChecked = false)
        //{
        //    var filter = new SymbolFilterProvider
        //    {
        //        Content = name,
        //        IsChecked = isChecked,
        //        SymbolFilter = Filter,
        //    };


        //    return filter;
        //}

        //private bool Filter(SymbolFilterProvider sender, object symbol)
        //{
        //    var content = sender.Content.ToString();
        //    content = content.Replace(" Filter", "");
        //    if(content == "All") return true;
        //    if (content == (symbol as DiagramElementViewModel).Key.ToString())
        //        return true;

        //    return false;
        //}

        //private SymbolFilters symbolfilters;

        //public SymbolFilters Symbolfilters
        //{
        //    get
        //    {
        //        return symbolfilters;
        //    }
        //    set
        //    {
        //        symbolfilters = value;
        //        symbolfilters.CollectionChanged += Symbolfilters_CollectionChanged;
        //        OnPropertyChanged("Symbolfilters");
        //    }
        //}

        //private void Symbolfilters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
            
        //}

        //private SymbolFilterProvider selectedfilter;

        //public SymbolFilterProvider Selectedfilter
        //{
        //    get
        //    {
        //        return selectedfilter;
        //    }
        //    set
        //    {
        //        selectedfilter = value;
        //        OnPropertyChanged("Selectedfilter");
        //    }
        //}
        
        public SymbolGroupDisplayMode SymbolGroupDisplayMode
        {
            get
            {
                return symbolGroupDisplayMode;
            }
            set
            {
                symbolGroupDisplayMode = value;
                OnPropertyChanged("SymbolGroupDisplayMode");
            }
        }

      

        


    }
}
