using System;
using System.ComponentModel;
using Orca.Core.Extensions;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class OrcaValueObject : IEquatable<OrcaValueObject>, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Type _dataType = typeof(string);
        private object _value = null;

        public virtual DateTime Modified { get; set; }

        public OrcaValueObject()
        {
            Modified = DateTime.Now;
            _value = string.Empty;
        }

        public OrcaValueObject(Type dataType)
        {
            _dataType = dataType;
            _value = dataType.DefaultValue();


        }

        public OrcaValueObject(Type dataType, object objectValue)
        {

            _dataType = dataType;
            _value = objectValue;
        }

        public virtual Guid Id { get; set; }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override int GetHashCode()
        {
            return _dataType.GetHashCode() + 31 * _value.GetHashCode();
        }

        public new bool Equals(object x, object y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            OrcaValueObject dp1 = (OrcaValueObject)x;
            OrcaValueObject dp2 = (OrcaValueObject)y;

            if (dp1 == null || dp2 == null) return false;

            return dp1.Value.Equals(dp2.Value) && dp1.DataType.Equals(dp2.DataType);
        }

        public bool Equals(OrcaValueObject other)
        {
            return Equals(this, other as OrcaValueObject);
        }


        public virtual object Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetValue(value);

            }
        }

        protected virtual void SetValue(object value)
        {
            if (_value == value)
            {
                return;
            }

            string valueString = value.ToString();

            this._value = Convert.ChangeType(valueString, DataType);

            OnPropertyChanged();
        }


        public virtual Type DataType
        {
            get
            {
                return _dataType;
            }

            protected internal set
            { }
        }


        public virtual void OnPropertyChanged()
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("value"));
        }





    }
}
