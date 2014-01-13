namespace So.Wpf.Samples
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using Annotations;
    using AttachedProperties;
    using Browser;
    public class Vm : INotifyPropertyChanged
    {
        private readonly BrowserVm _browserVm = new BrowserVm();
        private List<string> _scrollItems = Enumerable.Range(0, 50).Select(x => "Item " + x).ToList();
        private string _dummyText = "Sample text";
        private double _dummyDouble = 3.141592;
        private XpositionedRelativeTo _xpositionedRelative;
        private YpositionedRelativeTo _ypositionedRelative;
        private double _y;
        private double _x;
        private double _sizeY;
        private double _sizeX;
        private Point _renderTransformOrigin;

        public Vm()
        {
            Y = 100;
            X = 100;
            SizeY = 100;
            SizeX = 100;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public BrowserVm BrowserVm
        {
            get { return _browserVm; }
        }
        public List<string> ScrollItems
        {
            get { return _scrollItems; }
            set { _scrollItems = value; }
        }
        public string DummyText
        {
            get
            {
                return _dummyText;
            }
            set
            {
                if (value == _dummyText)
                {
                    return;
                }
                _dummyText = value;
                OnPropertyChanged();
            }
        }
        public XpositionedRelativeTo XpositionedRelative
        {
            get
            {
                return _xpositionedRelative;
            }
            set
            {
                if (value == _xpositionedRelative)
                {
                    return;
                }
                _xpositionedRelative = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<XpositionedRelativeTo> XRelatives
        {
            get
            {
                return Enum.GetValues(typeof(XpositionedRelativeTo)).Cast<XpositionedRelativeTo>();
            }
        }
        public YpositionedRelativeTo YpositionedRelative
        {
            get
            {
                return _ypositionedRelative;
            }
            set
            {
                if (value == _ypositionedRelative)
                {
                    return;
                }
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
            get
            {
                return _dummyDouble;
            }
            set
            {
                if (value.Equals(_dummyDouble))
                {
                    return;
                }
                _dummyDouble = value;
                OnPropertyChanged();
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value.Equals(_y))
                {
                    return;
                }
                _y = value;
                OnPropertyChanged();
            }
        }
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value.Equals(_x))
                {
                    return;
                }
                _x = value;
                OnPropertyChanged();
            }
        }
        public double SizeY
        {
            get
            {
                return _sizeY;
            }
            set
            {
                if (value.Equals(_sizeY))
                {
                    return;
                }
                _sizeY = value;
                OnPropertyChanged();
            }
        }
        public double SizeX
        {
            get
            {
                return _sizeX;
            }
            set
            {
                if (value.Equals(_sizeX))
                {
                    return;
                }
                _sizeX = value;
                OnPropertyChanged();
            }
        }
        public double RenderTransformOriginX
        {
            get { return RenderTransformOrigin.X; }
            set
            {
                RenderTransformOrigin = new Point(value, RenderTransformOrigin.Y);
                OnPropertyChanged();
            }
        }
        public double RenderTransformOriginY
        {
            get { return RenderTransformOrigin.Y; }
            set
            {
                RenderTransformOrigin = new Point(RenderTransformOrigin.X, value);
                OnPropertyChanged();
            }
        }
        public Point RenderTransformOrigin
        {
            get { return _renderTransformOrigin; }
            private set
            {
                if (Equals(_renderTransformOrigin, value))
                    return;
                _renderTransformOrigin = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
