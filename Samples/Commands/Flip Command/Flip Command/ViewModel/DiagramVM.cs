using Flip_Command.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Flip_Command.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        private ICommand _Flip;
        private ICommand _HorizontalFlip;
        private ICommand _VerticalFlip;

        public ICommand Flip
        {
            get { return _Flip; }
            set { _Flip = value; }
        }

        public ICommand HorizontalFlip
        {
            get { return _HorizontalFlip; }
            set { _HorizontalFlip = value; }
        }

        public ICommand VerticalFlip
        {
            get { return _VerticalFlip; }
            set { _VerticalFlip = value; }
        }

        public DiagramVM()
        {
            Flip = new Command(OnFlip);
            HorizontalFlip = new Command(OnHorizontalFlip);
            VerticalFlip = new Command(OnVerticalFlip);
        }

        private void OnVerticalFlip(object obj)
        {
            (Info as IGraphInfo).Commands.Flip.Execute(new FlipParameter() { Flip = Syncfusion.UI.Xaml.Diagram.Flip.VerticalFlip, FlipMode = FlipMode.FlipMode });
        }

        private void OnHorizontalFlip(object obj)
        {
            (Info as IGraphInfo).Commands.Flip.Execute(new FlipParameter() { Flip = Syncfusion.UI.Xaml.Diagram.Flip.HorizontalFlip, FlipMode = FlipMode.FlipMode });
        }

        private void OnFlip(object obj)
        {
            (Info as IGraphInfo).Commands.Flip.Execute(null);
        }
    }
}
