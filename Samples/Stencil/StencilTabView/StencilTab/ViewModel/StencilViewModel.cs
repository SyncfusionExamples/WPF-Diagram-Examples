using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StencilCategory.ViewModel
{
    public class StencilViewModel : DiagramViewModel
    {
        private SymbolSelectionMode symbolSelectionMode = SymbolSelectionMode.Multiple;
        private SymbolGroupDisplayMode symbolGroupDisplayMode = SymbolGroupDisplayMode.Tab;
        private SymbolsDisplayMode symbolDisplayMode = SymbolsDisplayMode.IconsOnly;
        private DisplayMode displayMode = DisplayMode.Expanded;
        private bool showSearchTextBox = true;
        private bool showDisplayModeToggleButton = false;
        private bool showPreview = false;
        private bool enableReorder = true;
        private StencilConstraints stencilConstraints = StencilConstraints.Default;
        private Dictionary<string, string> icons = new Dictionary<string, string>()
        {
            {"Basic Shapes", "basicheader" },
            {"Flow Shapes", "flowheader" },
            {"Arrow Shapes", "arrowheader" },
            {"UMLUseCase Shapes", "umlheader" },
            {"Swimlane Shapes", "swimlaneheader" },
        };

        public StencilViewModel()
        {
            Symbolfilters = new SymbolFilters();
            symbolfilters.Add(AddNewFilter("All", true));
            symbolfilters.Add(AddNewFilter("Basic Shapes Filter", true));
            symbolfilters.Add(AddNewFilter("Flow Shapes Filter", true));
            symbolfilters.Add(AddNewFilter("Arrow Shapes Filter", true));
            symbolfilters.Add(AddNewFilter("DataFlow Shapes Filter"));
            symbolfilters.Add(AddNewFilter("UMLActivity Shapes Filter"));
            symbolfilters.Add(AddNewFilter("UMLUseCase Shapes Filter", true));
            symbolfilters.Add(AddNewFilter("UMLRelationship Shapes Filter"));
            symbolfilters.Add(AddNewFilter("Swimlane Shapes Filter", true));
            symbolfilters.Add(AddNewFilter("BPMNEditor Shapes Filter"));
            this.Selectedfilter = Symbolfilters[0];
        }

        private SymbolFilterProvider AddNewFilter(string name, bool isChecked = false)
        {
            var filter = new SymbolFilterProvider
            {
                Content = name,
                IsChecked = isChecked,
                SymbolFilter = Filter,
            };

            //if (this.SymbolFilterDisplayMode == SymbolFilterDisplayMode.Tab)
            //{
            //    var resource = icons.ContainsKey(name) ? icons[name] : icons.Values.FirstOrDefault();
            //    //filter.TitleTemplate = App.Current.MainWindow.Resources["textheader"] as DataTemplate;
            //    filter.IconTemplate = App.Current.MainWindow.Resources[resource] as DataTemplate;
            //}

            return filter;
        }

        private bool Filter(SymbolFilterProvider sender, object symbol)
        {
            var content = sender.Content.ToString();
            content = content.Replace(" Filter", "");
            if(content == "All") return true;
            if (content == (symbol as DiagramElementViewModel).Key.ToString())
                return true;

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
                symbolfilters.CollectionChanged += Symbolfilters_CollectionChanged;
                OnPropertyChanged("Symbolfilters");
            }
        }

        private void Symbolfilters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
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
        public SymbolSelectionMode SymbolSelectionMode
        {
            get
            {
                return symbolSelectionMode;
            }
            set
            {
                symbolSelectionMode = value;
                OnPropertyChanged("SymbolSelectionMode");
            }
        }
        public SymbolGroupDisplayMode SymbolGroupDisplayMode
        {
            get
            {
                return symbolGroupDisplayMode;
            }
            set
            {
                symbolGroupDisplayMode = value;
                OnPropertyChanged("SymbolGroupDisplayMode");
            }
        }

        public SymbolsDisplayMode SymbolsDisplayMode
        {
            get
            {
                return symbolDisplayMode;
            }
            set
            {
                symbolDisplayMode = value;
                OnPropertyChanged("SymbolsDisplayMode");
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
