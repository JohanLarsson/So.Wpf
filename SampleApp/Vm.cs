using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Annotations;
using SampleApp.Browser;

namespace SampleApp
{
    public class Vm : INotifyPropertyChanged
    {
        private readonly BrowserVm _browserVm = new BrowserVm();
        private List<string> _scrollItems = Enumerable.Range(0,50).Select(x=>"Item " + x).ToList();
        private string _dummyText ="Sample text";
        private double _dummyDouble =3.141592;

        public Browser.BrowserVm BrowserVm
        {
            get { return _browserVm; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> ScrollItems
        {
            get { return _scrollItems; }
            set { _scrollItems = value; }
        }
        public string DummyText
        {
            get { return _dummyText; }
            set
            {
                if (value == _dummyText) return;
                _dummyText = value;
                OnPropertyChanged();
            }
        }

        public double DummyDouble
        {
            get { return _dummyDouble; }
            set
            {
                if (value.Equals(_dummyDouble)) return;
                _dummyDouble = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
