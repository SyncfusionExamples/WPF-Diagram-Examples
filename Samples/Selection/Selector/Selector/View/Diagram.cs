using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GettingStarted_Selector.View
{    
    public class Diagram : SfDiagram
    {       
        //Apply Selectors

        public Selector SFSelector = new Selector();
        protected override Selector GetSelectorForItemOverride(object item)
        {
            return SFSelector;
        }
    }
}
