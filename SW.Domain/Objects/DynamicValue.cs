using System;

namespace Orca.Domain.Objects
{
    [ Serializable ]
    public class DynamicValue<T> : IDynamicValue
    {
        private T _valueObject;

        /// <summary>
        ///   Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public virtual T Value
        {
            get { return _valueObject; }
            set { _valueObject = value; }
        }

        #region IDynamicValue Members

        public override string ToString( )
        {
            return _valueObject.ToString( );
        }

        public virtual Type DataType
        {
            get { return typeof ( T ); }
            set
            {
                // = value;
            }
        }

        object IDynamicValue.Value
        {
            get { return Value; }
            set { Value = (T) value; }
        }

        #endregion

        //public bool TryParse(string stringValue,ref object Value)
        //{
        //    return  InternalParse<T>(stringValue,ref Value);
        //}
        //public bool InternalParse<T>(string stringValue, ref object Value)
        //{
        //    T value;

        //    bool Result = false;
        //    try
        //    {
        //        value = (T)Convert.ChangeType(stringValue, typeof(T));

        //        Result = true;
        //    }
        //    catch
        //    {
        //        value = default(T);
        //    }

        //    Value = value;

        //    return Result;
        //}

        public static implicit operator T( DynamicValue<T> value )
        {
            return value.Value;
        }

        public static implicit operator DynamicValue<T>( T value )
        {
            return new DynamicValue<T> {Value = value};
        }
    }
}