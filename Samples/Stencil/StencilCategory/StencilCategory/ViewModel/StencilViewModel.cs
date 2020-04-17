using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StencilCategorySample
{
    /// <summary>
    /// Custom class for stencil view model.
    /// </summary>
    public class StencilViewModel : Stencil
    {
        /// <summary>
        /// Overridden method to change the symbol.
        /// </summary>
        /// <param name="Item">Item value of the shape</param>
        /// <param name="SymbolName">Name of the symbol</param>
        /// <param name="CategoryName">Name of the category</param>
        /// <returns>Return the item of the shape</returns>
        protected override object PrepareSymbolViewModel(object Item, string SymbolName, string CategoryName)
        {
            if (SymbolName == "Rectangle")
            {
                (Item as INode).UnitWidth = 80;
                (Item as INode).UnitHeight = 40;
                return Item;
            }
            else
                return base.PrepareSymbolViewModel(Item, SymbolName, CategoryName);
        }

        /// <summary>
        /// Overidden method to decide whether a symbol can be added or not
        /// </summary>
        /// <param name="symbolName">Name of the symbol</param>
        /// <param name="categoryName">Name of the category</param>
        /// <returns>Return the boolean</returns>
        protected override bool CanAddSymbol(string symbolName, string categoryName)
        {
            if (symbolName == "Triangle")
            {
                return false;
            }

            return base.CanAddSymbol(symbolName, categoryName);
        }
    }

    public class SymbolCollection : ObservableCollection<object>
    {
    }
}
