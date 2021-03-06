﻿namespace So.Wpf.Samples.Browser
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    public class DummyFolder
    {
        private readonly DirectoryInfo _parent;
        private ObservableCollection<object> _subs;
        public DummyFolder()
            : this(@"C:\temp")
        {
        }
        public DummyFolder(string parent)
        {
            _parent = new DirectoryInfo(parent);
        }
        public string Name
        {
            get
            {
                return _parent.Name;
            }
        }
        public string Type
        {
            get
            {
                return "Folder";
            }
        }
        public ObservableCollection<object> Children
        {
            get { return _subs ?? (_subs = new ObservableCollection<object>(GetFolderChildren(_parent).Cast<object>().Concat(GetFileChildren(_parent).Cast<object>()))); }
        }
        private IEnumerable<DummyFolder> GetFolderChildren(DirectoryInfo parent)
        {
            return parent.GetDirectories().Select(x => new DummyFolder(x.FullName));
        }
        private IEnumerable<DummyFile> GetFileChildren(DirectoryInfo parent)
        {
            return parent.GetFiles().Select(x => new DummyFile(x));
        }
    }
}