using System;
using System.ComponentModel;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;
using Orca.Domain.Objects.Constants;

namespace Orca.Domain.Objects
{

    [Serializable]
    public abstract class PropertyBase : PublishableDomainObject, IPublishableDomainObject, IEquatable<PropertyBase>, INotifyPropertyChanged, IDisposable
    {




        //     private OrcaValueObject _valueObject;

        string description = string.Empty;

        bool required;

        string externalMessageKey;

        protected internal PropertyBase( )
        {
        }


        public PropertyBase( DynamicPropertyDataTypes DataType, string name )
            : base( name )
        {
            ExternalMessageKey = DomainConstants.DefaultExternalPropertyKey;
            CreateDynamaicValue( DataType );
        }


        internal PropertyBase( Type DataType, string name )
            : base( name )
        {
            CreateDynamaicValue( DataType );
        }


        public PropertyBase( string name, object DefaultValue )
            : this( DefaultValue.GetType( ), name )
        {
            ValueObject.Value = DefaultValue;
        }


        protected override void OnDispose( )
        {
            base.OnDispose( );
            ValueObject.PropertyChanged -= _valueObject_PropertyChanged;
        }


        public virtual string Description
        {
            get
            {
                return description;
            }
            set
            {
                if ( string.Compare( description, value, false ) == 0 )
                {
                    return;
                }
                description = value;
                OnPropertyChanged( "Description" );
            }
        }


        public virtual bool Required
        {
            get
            {
                return required;
            }
            set
            {
                if ( required == value )
                    return;
                required = value;
                OnPropertyChanged( "Required" );
            }
        }


        public virtual string ExternalMessageKey
        {
            get
            {
                return externalMessageKey;
            }
            set
            {
                if ( string.Compare( externalMessageKey, value, false ) == 0 )
                {
                    return;
                }
                externalMessageKey = value;
                OnPropertyChanged( "ExternalMessageKey" );
            }
        }

        [Browsable( false )]
        public virtual Type DataType
        {
            get { return ValueObject.DataType; }

            protected internal set
            {
            }
        }

        [DisplayName( "DataType" )]
        public virtual string OrcaDataType
        {
            get
            {
                if ( DataType == typeof( int ) )
                {
                    return DynamicPropertyDataTypes.Integer.ToString( );

                }
                else if ( DataType == typeof( decimal ) )
                {
                    return DynamicPropertyDataTypes.Number.ToString( );
                }
                else if ( DataType == typeof( double ) )
                {
                    return DynamicPropertyDataTypes.Number.ToString( );
                }
                else if ( DataType == typeof( string ) )
                {
                    return DynamicPropertyDataTypes.Text.ToString( );
                }
                else if ( DataType == typeof( bool ) )
                {
                    return DynamicPropertyDataTypes.Boolean.ToString( );
                }
                return DataType != null ? DataType.Name : string.Empty;
            }
        }

        [EditorBrowsable( EditorBrowsableState.Never )]
        public virtual OrcaValueObject ValueObject { get; set; }

        [Browsable( false )]
        public virtual object Value
        {
            get
            {

                return ValueObject != null ? ValueObject.Value : null;
            }
            set
            {
                if ( ValueObject == null )
                {
                    if ( value == null )
                    {
                        return;
                    }
                    CreateDynamaicValue( value.GetType( ) );
                }
                ValueObject.Value = value;
            }
        }


        [Browsable( false )]
        public virtual string StringValue
        {
            get
            {
                if ( ValueObject == null )
                {
                    return null;
                }
                return ValueObject.ToString( );
            }
            set { }
        }



        private void CreateDynamaicValue( DynamicPropertyDataTypes ValueDataType )
        {
            switch ( ValueDataType )
            {
                case DynamicPropertyDataTypes.Text:
                    CreateDynamaicValue( typeof( string ) );
                    break;
                case DynamicPropertyDataTypes.Integer:
                    CreateDynamaicValue( typeof( int ) );
                    break;
                case DynamicPropertyDataTypes.Number:
                    CreateDynamaicValue( typeof( double ) );
                    break;
                case DynamicPropertyDataTypes.Boolean:
                    CreateDynamaicValue( typeof( bool ) );
                    break;
            }
        }

        private void CreateDynamaicValue( Type ValueDataType )
        {

            ValueObject = new OrcaValueObject( ValueDataType );
            //        _defaultValueObject = new OrcaValueObject(ValueDataType);

            ValueObject.PropertyChanged += _valueObject_PropertyChanged;

        }


        //protected virtual void OnPropertyChanged(string PropertyName)
        //{
        //    this.ModifiedDate = DateTime.Now;
        //    if (this.PropertyChanged != null)
        //    {
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        //    }
        //}


        void _valueObject_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            OnPropertyChanged( this.Name );
        }

        public new bool Equals( object x, object y )
        {
            if ( x == y ) return true;
            if ( x == null || y == null ) return false;
            PropertyBase dp1 = (PropertyBase)x;
            PropertyBase dp2 = (PropertyBase)y;

            if ( dp1 == null || dp2 == null ) return false;

            return dp1.ValueObject.Equals( dp2.ValueObject );
        }


        public bool Equals( PropertyBase other )
        {
            return Equals( this, other as PropertyBase );
        }


    }








    public static class OrcaPropertyExtensions
    {

        public static DynamicPropertyDataTypes GetDynamicPropertyDataType( this PropertyBase element )
        {
            if ( element.DataType == typeof( int ) )
            {
                return DynamicPropertyDataTypes.Integer;

            }
            else if ( element.DataType == typeof( decimal ) )
            {
                return DynamicPropertyDataTypes.Number;
            }
            else if ( element.DataType == typeof( string ) )
            {
                return DynamicPropertyDataTypes.Text;
            }
            else if ( element.DataType == typeof( bool ) )
            {
                return DynamicPropertyDataTypes.Boolean;
            }
            return DynamicPropertyDataTypes.Text;
        }
    }






}