using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace AnnotationVisibility
{
    public class Annotation : AnnotationEditorViewModel
    {
        private Visibility visibility;


        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {

                visibility = value;
            }
        }



    }
}
