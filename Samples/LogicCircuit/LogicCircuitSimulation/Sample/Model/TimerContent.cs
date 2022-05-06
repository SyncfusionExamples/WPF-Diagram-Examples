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
    public class TimerContent : INotifyPropertyChanged
    {
        private int mTimerValue = 2000;

        /// <summary>
        /// Gets or sets select value of Textbox.
        /// </summary>
        [DataMember]
        public int TimerValue
        {
            get
            {
                return mTimerValue;
            }
            set
            {
                if (mTimerValue != value)
                {
                    mTimerValue = value;
                    OnPropertyChanged("TimerValue");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
