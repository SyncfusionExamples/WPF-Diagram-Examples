using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorSegment
{
    public class StencilViewModel : DiagramViewModel
    {
        private SymbolFilterDisplayMode symbolFilterDisplayMode = SymbolFilterDisplayMode.List;
        private SymbolsDisplayMode symbolDisplayMode = SymbolsDisplayMode.IconsOnly;
        private DisplayMode displayMode = DisplayMode.Expanded;
        private bool showSearchTextBox = true;
        private bool showDisplayModeToggleButton = true;
        private bool showPreview = false;
        private bool enableReorder = true;
        private StencilConstraints stencilConstraints = StencilConstraints.Default;

        public StencilViewModel()
        {
            Symbolfilters = new SymbolFilters();

            SymbolFilterProvider node1 = new SymbolFilterProvider { Content = "Basic Shapes", IsChecked = true, SymbolFilter = Filter };
            SymbolFilterProvider node7 = new SymbolFilterProvider { Content = "UMLRelationship Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node8 = new SymbolFilterProvider { Content = "Swimlane Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node9 = new SymbolFilterProvider { Content = "BPMNEditor Shapes", SymbolFilter = Filter };
            SymbolFilterProvider node10 = new SymbolFilterProvider { Content = "Connectors", SymbolFilter = Filter };
            SymbolFilterProvider node11 = new SymbolFilterProvider { Content = "Groups", SymbolFilter = Filter };
            SymbolFilterProvider node12 = new SymbolFilterProvider { Content = "UISymbols", SymbolFilter = Filter };
            this.Symbolfilters.Add(node1);
            this.Symbolfilters.Add(node8);
            this.Symbolfilters.Add(node9);
            this.Symbolfilters.Add(node10);
            this.Symbolfilters.Add(node11);
            this.Symbolfilters.Add(node12);
            this.Selectedfilter = Symbolfilters[0];
        }

        // Define filtering of Symbols
        private bool Filter(SymbolFilterProvider sender, object symbol)
        {
            if (symbol is NodeViewModel && (symbol as NodeViewModel).ParentGroup == null)
            {
                if (sender.Content.ToString() == (symbol as NodeViewModel).Key.ToString())
                    return true;
            }
            if (symbol is LaneViewModel)
            {
                if (sender.Content.ToString() == (symbol as LaneViewModel).Key.ToString())
                    return true;
            }
            if (symbol is PhaseViewModel)
            {
                if (sender.Content.ToString() == (symbol as PhaseViewModel).Key.ToString())
                    return true;
            }
            if (symbol is ConnectorViewModel)
            {
                if (sender.Content.ToString() == (symbol as ConnectorViewModel).Key.ToString())
                    return true;
            }

            if (symbol is ISymbol)
            {
                if (sender.Content.ToString() == (symbol as ISymbol).Key.ToString())
                    return true;
            }
            return false;
        }

        private SymbolFilters symbolfilters;

        public SymbolFilters Symbolfilters
        {
            get
            {
                return symbolfilters;
            }
            set
            {
                symbolfilters = value;
                OnPropertyChanged("Symbolfilters");
            }
        }

        private SymbolFilterProvider selectedfilter;

        public SymbolFilterProvider Selectedfilter
        {
            get
            {
                return selectedfilter;
            }
            set
            {
                selectedfilter = value;
                OnPropertyChanged("Selectedfilter");
            }
        }

        public SymbolFilterDisplayMode SymbolFilterDisplayMode
        {
            get
            {
                return symbolFilterDisplayMode;
            }
            set
            {
                symbolFilterDisplayMode = value;
                OnPropertyChanged("SymbolFilterDisplayMode");
            }
        }

        public SymbolsDisplayMode SymbolDisplayMode
        {
            get
            {
                return symbolDisplayMode;
            }
            set
            {
                symbolDisplayMode = value;
                OnPropertyChanged("SymbolDisplayMode");
            }
        }

        public DisplayMode DisplayMode
        {
            get
            {
                return displayMode;
            }
            set
            {
                displayMode = value;
                OnPropertyChanged("DisplayMode");
            }
        }

        public bool ShowSearchTextBox
        {
            get
            {
                return showSearchTextBox;
            }
            set
            {
                showSearchTextBox = value;
                OnPropertyChanged("ShowSearchTextBox");
            }
        }

        public bool ShowDisplayModeToggleButton
        {
            get
            {
                return showDisplayModeToggleButton;
            }
            set
            {
                showDisplayModeToggleButton = value;
                OnPropertyChanged("ShowDisplayModeToggleButton");
            }
        }

        public bool ShowPreview
        {
            get
            {
                return showPreview;
            }
            set
            {
                showPreview = value;
                OnPropertyChanged("ShowPreview");
            }
        }

        public bool EnableReorder
        {
            get
            {
                return enableReorder;
            }
            set
            {
                enableReorder = value;
                OnPropertyChanged("EnableReorder");
            }
        }

        public StencilConstraints StencilConstraints
        {
            get
            {
                return stencilConstraints;
            }
            set
            {
                stencilConstraints = value;
                OnPropertyChanged("StencilConstraints");
            }
        }

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            switch (name)
            {
                case "ShowPreview":
                    if (this.StencilConstraints.Contains(StencilConstraints.ShowPreview))
                    {
                        this.StencilConstraints &= ~StencilConstraints.ShowPreview;
                    }
                    else
                    {
                        this.StencilConstraints |= StencilConstraints.ShowPreview;
                    }
                    break;

                case "EnableReorder":
                    if (this.StencilConstraints.Contains(StencilConstraints.AllowDragDrop))
                    {
                        this.StencilConstraints &= ~StencilConstraints.AllowDragDrop;
                    }
                    else
                    {
                        this.StencilConstraints |= StencilConstraints.AllowDragDrop;
                    }
                    break;
            }
        }
    }
}
