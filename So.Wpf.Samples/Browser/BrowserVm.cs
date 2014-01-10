namespace So.Wpf.Samples.Browser
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    public class BrowserVm
    {
        public BrowserVm()
        {
            RootFolder = new ObservableCollection<DummyFolder>(new List<DummyFolder> { new DummyFolder() });
        }
        public ObservableCollection<DummyFolder> RootFolder { get; private set; }

        public string Type
        {
            get
            {
                return "Root";
            }
        }

        public string Name
        {
            get
            {
                return RootFolder.First().Name;
            }
        }
    }
}