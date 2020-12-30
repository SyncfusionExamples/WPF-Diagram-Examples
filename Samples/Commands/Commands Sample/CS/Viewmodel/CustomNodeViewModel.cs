using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.Windows.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Getting_Started_Commands.Viewmodel
{
    public class CustomNodeViewModel : NodeViewModel
    {
        private Brush _FillColor;
        public CustomNodeViewModel()
        {

        }

        [DataMember]
        public Brush FillColor
        {
            get
            {
                return _FillColor;
            }
            set
            {
                if(_FillColor != value)
                {
                    _FillColor = value;
                    OnPropertyChanged("FillColor");
                }
            }
        }
    }
}
