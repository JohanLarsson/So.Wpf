using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using So.Wpf.AttachedProperties;
using So.Wpf.Samples.Annotations;
using So.Wpf.Samples.Browser;

namespace So.Wpf.Samples
{
    public class Vm : INotifyPropertyChanged
    {
        private readonly BrowserVm _browserVm = new BrowserVm();
        private List<string> _scrollItems = Enumerable.Range(0,50).Select(x=>"Item " + x).ToList();
        private string _dummyText ="Sample text";
        private double _dummyDouble =3.141592;
        private XpositionedRelativeTo _xpositionedRelative;
        private YpositionedRelativeTo _ypositionedRelative;
        private double _y;
        private double _x;
        private double _ySize;
        private double _xSize;

        public Vm()
        {
            Y = 100;
            X = 100;
            YSize = 100;
            XSize = 100;
        }
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

        public XpositionedRelativeTo XpositionedRelative
        {
            get { return _xpositionedRelative; }
            set
            {
                if (value == _xpositionedRelative) return;
                _xpositionedRelative = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<XpositionedRelativeTo> XRelatives
        {
            get
            {
                return Enum.GetValues(typeof (XpositionedRelativeTo)).Cast<XpositionedRelativeTo>();
            }
        }

        public YpositionedRelativeTo YpositionedRelative
        {
            get { return _ypositionedRelative; }
            set
            {
                if (value == _ypositionedRelative) return;
                _ypositionedRelative = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<YpositionedRelativeTo> YRelatives
        {
            get
            {
                return Enum.GetValues(typeof(YpositionedRelativeTo)).Cast<YpositionedRelativeTo>();
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

        public double Y
        {
            get { return _y; }
            set
            {
                if (value.Equals(_y)) return;
                _y = value;
                OnPropertyChanged();
            }
        }

        public double X
        {
            get { return _x; }
            set
            {
                if (value.Equals(_x)) return;
                _x = value;
                OnPropertyChanged();
            }
        }

        public double YSize
        {
            get { return _ySize; }
            set
            {
                if (value.Equals(_ySize)) return;
                _ySize = value;
                OnPropertyChanged();
            }
        }

        public double XSize
        {
            get { return _xSize; }
            set
            {
                if (value.Equals(_xSize)) return;
                _xSize = value;
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
