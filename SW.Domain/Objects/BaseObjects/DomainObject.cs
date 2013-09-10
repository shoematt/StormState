using System;
using System.ComponentModel;
using System.Threading;
using Orca.Core.Domain;

namespace Orca.Domain.Objects.BaseObjects
{
    [Serializable]
    public abstract class DomainObject : IDomainObject, IEquatable<DomainObject>, IDisposable, INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        private int suspendPropChangeCalls = 0;
        private bool _isDisposed;
        private string _name;

        protected DomainObject( )
        {
            _name = "unnamed";
            ModifiedDate = DateTime.Now;
        }

        protected DomainObject( string name )
            : this( )
        {
            _name = name;
        }



        public void Dispose( )
        {
            Dispose( true );
        }

        private void Dispose( bool isDisposing )
        {
            if ( _isDisposed )
            {
                // don't dispose of multiple times.
                return;
            }

            _isDisposed = true;

            OnDispose( );

            GC.SuppressFinalize( this );
        }

        protected virtual void OnDispose( )
        {

        }

        public virtual void BeginUpdate( )
        {
            Interlocked.Increment( ref suspendPropChangeCalls );
        }

        public virtual void EndUpdate( )
        {

            Interlocked.Decrement( ref suspendPropChangeCalls );
            if ( suspendPropChangeCalls <= 0 )
            {
                Interlocked.Exchange( ref suspendPropChangeCalls, 0 );
                OnPropertyChanged( "EndUpdate" );
            }
        }

        [Browsable( false )]
        public virtual Guid Id { get; set; }

        [Browsable( true )]
        public virtual DateTime ModifiedDate { get; set; }

        [Browsable( false )]
        public virtual Guid UserTypeID { get; set; }


        [Browsable( true )]
        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if ( string.Compare( _name, value, false ) == 0 )
                {
                    return;
                }
                _name = value;
                OnPropertyChanged( "Name" );
            }
        }




        protected virtual void OnPropertyChanged( string PropertyName )
        {
            if ( !Interlocked.Equals( suspendPropChangeCalls, 0 ) )
            {
                return;
            }
            this.ModifiedDate = DateTime.Now;
            if ( this.PropertyChanged != null )
            {
                this.PropertyChanged( this, new PropertyChangedEventArgs( PropertyName ) );
            }
        }

        //[Browsable(false)]
        //public virtual int Version
        //{
        //    get { return _version; }
        //    protected set { _version = value; }
        //}


        #region IEquatable<DomainObject> Members

        /// <summary>
        ///   Checks for equality against the specified other.
        /// </summary>
        /// <param name = "other">The other.</param>
        /// <returns></returns>
        public virtual bool Equals( DomainObject other )
        {
            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            if ( other == null ) { return false; }

            if ( Id.Equals( other.Id ) ) { return true; }

            return true;
        }

        #endregion

        /// <summary>
        ///   Determines whether the specified <see cref = "System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name = "obj">The <see cref = "System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref = "System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals( object obj )
        {

            return Equals( obj as DomainObject );

        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///   A hash code for this instance, suitable for use in hashing algorithms and updatedData structures like a hash table. 
        /// </returns>
        public override int GetHashCode( )
        {
            unchecked
            {

                int result = 2;

                result = 29 * result + Id.GetHashCode( );

                return result;
            }
        }
    }
}