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
        private NodeContent _mcustomcontent;
        private string _mcustomcontenttemplate;

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


        [DataMember]
        public string CustomContentTemplate
        {
            get
            {
                return _mcustomcontenttemplate;
            }
            set
            {
                if (_mcustomcontenttemplate != value)
                {
                    _mcustomcontenttemplate = value;
                    OnPropertyChanged("CustomContentTemplate");
                }
            }
        }

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            switch (name)
            {
                case "CustomContent":
                    this.Content = this.CustomContent;
                    break;
                case "CustomContentTemplate":
                    this.ContentTemplate = App.Current.Resources[this.CustomContentTemplate] as DataTemplate;
                    break;
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
