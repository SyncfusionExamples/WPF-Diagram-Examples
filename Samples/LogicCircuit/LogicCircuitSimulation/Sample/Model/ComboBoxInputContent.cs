using LogicCircuitSimulation.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogicCircuitSimulation.Model
{
    /// <summary>
    /// Class to represent content to the combobox type node.
    /// </summary>
    [DataContract]
    public class ComboBoxInputContent : INotifyPropertyChanged
    {
        private List<int> mInputItemSource = new List<int>();
        
        /// <summary>
        /// Gets or sets ItemSource to the combobox input node.
        /// </summary>
        [DataMember]
        public List<int> InputItemSource
        {
            get
            {
                return mInputItemSource;
            }
            set
            {
                if (mInputItemSource != value)
                {
                    mInputItemSource = value;
                    OnPropertyChanged("InputItemSource");
                }
            }
        }


        private int mInputSelectedItem = 0;

        /// <summary>
        /// Gets or sets select value of combobox.
        /// </summary>
        [DataMember]
        public int InputSelectedItem
        {
            get
            {
                return mInputSelectedItem;
            }
            set
            {
                if (mInputSelectedItem != value)
                {
                    mInputSelectedItem = value;
                    OnPropertyChanged("InputSelectedItem");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
