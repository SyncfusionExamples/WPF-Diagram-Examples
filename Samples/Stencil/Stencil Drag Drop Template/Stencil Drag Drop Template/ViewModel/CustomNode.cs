using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stencil_Drag_Drop_Template.ViewModel
{
    public class CustomNode : NodeViewModel
    {
        private CustomContent _customcontent;
        private string _customcontenttemplate;
        [DataMember]
        public CustomContent CustomContent
        {
            get
            {
                return _customcontent;
            }
            set
            {
                if (value != _customcontent)
                {
                    _customcontent = value;
                    OnPropertyChanged("CustomContent");
                }
            }
        }

        [DataMember]
        public string CustomContentTemplate
        {
            get
            {
                return _customcontenttemplate;
            }
            set
            {
                if (value != _customcontenttemplate)
                {
                    _customcontenttemplate = value;
                    OnPropertyChanged("CustomContentTemplate");
                }
            }
        }
    }

    //Serializer class for custom class
    [DataContract]
    public class CustomContent
    {
        private string _contenttext;
        private string _contentimage;

        [DataMember]
        public string ContentText
        {
            get
            {
                return _contenttext;
            }
            set
            {
                if (value != _contenttext)
                {
                    _contenttext = value;
                }
            }
        }

        [DataMember]
        public string ContentImage
        {
            get
            {
                return _contentimage;
            }
            set
            {
                if (value != _contentimage)
                {
                    _contentimage = value;
                }
            }
        }
    }
}
