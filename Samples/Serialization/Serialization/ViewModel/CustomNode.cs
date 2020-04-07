using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Serialization_WPF.ViewModel
{
    // CustomNode class for node
    // No need to mention DataContract as this class is derived from SfDiagram's ViewModel
    public class CustomNode : NodeViewModel
    {
        private string _mcustomshape;
        private string _mcustomstyle;
        private NodeContent _mcustomcontent;

        [DataMember]
        public string CustomShape
        {
            get
            {
                return _mcustomshape;
            }
            set
            {
                if (_mcustomshape != value)
                {
                    _mcustomshape = value;
                    Shape = Application.Current.Resources[_mcustomshape];
                    OnPropertyChanged("CustomShape");
                }
            }
        }

        [DataMember]
        public string CustomStyle
        {
            get
            {
                return _mcustomstyle;
            }
            set
            {
                if (_mcustomstyle != value)
                {
                    _mcustomstyle = value;
                    ShapeStyle = App.Current.Resources[_mcustomstyle] as Style;
                    OnPropertyChanged("CustomStyle");
                }
            }
        }

        [DataMember]
        public NodeContent CustomContent
        {
            get
            {
                return _mcustomcontent;
            }
            set
            {
                if (_mcustomcontent != value)
                {
                    _mcustomcontent = value;
                    OnPropertyChanged("CustomContent");
                }
            }
        }
    }

    // Custom Class for Content
    // Need to mention the DataContract as this class is not derived from any of our Viewmodel.

    [DataContract]
    public class NodeContent
    {
        private string _mcontent;

        [DataMember]
        public string Content
        {
            get
            {
                return _mcontent;
            }
            set
            {
                if (_mcontent != value)
                {
                    _mcontent = value;
                }
            }
        }
    }
}
