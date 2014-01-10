namespace So.Wpf.Tests
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;
    public class FakeInpc : INotifyPropertyChanged, IEquatable<FakeInpc>
    {
        private int _dummyProperty1 = 1;
        private int _dummyProperty2 = 2;

        public event PropertyChangedEventHandler PropertyChanged;
        public int DummyProperty1
        {
            get
            {
                return _dummyProperty1;
            }
            set
            {
                _dummyProperty1 = value;
                OnPropertyChanged();
            }
        }
        public int DummyProperty2
        {
            get
            {
                return _dummyProperty2;
            }
            set
            {
                _dummyProperty2 = value;
                OnPropertyChanged();
            }
        }
        public bool HasHandler
        {
            get
            {
                return PropertyChanged != null;
            }
        }
        public static bool operator ==(FakeInpc left, FakeInpc right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(FakeInpc left, FakeInpc right)
        {
            return !Equals(left, right);
        }
        public void Raise1()
        {
            DummyProperty1 = 1;
        }
        public void Raise2()
        {
            DummyProperty2 = 2;
        }
        public bool Equals(FakeInpc other)
        {
            return ReferenceEquals(this, other);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((FakeInpc)obj);
        }
        public override int GetHashCode()
        {
            return 3;
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}