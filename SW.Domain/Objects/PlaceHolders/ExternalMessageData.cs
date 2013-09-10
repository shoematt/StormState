using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Orca.Domain.Exceptions;
using Orca.Domain.Objects.Constants;

namespace Orca.Domain.Objects.PlaceHolders
{
    public interface IExternalMessageData
    {
        string MsgType { get; set; }

        Dictionary<string, object> PropertyValues { get; }
    }


    [ Serializable ]
    public class ExternalMessageData : IExternalMessageData, IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        private readonly Dictionary<string, object> propertyValues = new Dictionary<string, object>( );


        protected ExternalMessageData( SerializationInfo info, StreamingContext context )
        {
            MsgType = info.GetString( "MsgType" );
            propertyValues = (Dictionary<string, object>) info.GetValue( "PropertyValues", typeof ( Dictionary<string, object> ) );
        }

        /// <summary>
        ///   Initializes a new instance of the ExternalMessageData class.
        /// </summary>
        public ExternalMessageData( )
        {
            MsgType = DomainConstants.ExternalMessageType;
        }

        /// <summary>
        ///   Initializes a new instance of the ExternalMessageData class.
        /// </summary>
        public ExternalMessageData( string MsgType )
        {
            this.MsgType = MsgType;
        }

        #region IEnumerable<KeyValuePair<string,object>> Members

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator( )
        {
            return propertyValues.GetEnumerator( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        #endregion

        #region IExternalMessageData Members

        public string MsgType { get; set; }

        public Dictionary<string, object> PropertyValues
        {
            get { return propertyValues; }
        }

        #endregion

        public object GetProperty( string propertyName )
        {
            if ( propertyValues.ContainsKey( propertyName ) )
            {
                return propertyValues[propertyName];
            }

            throw new PropertyDoesNotExistException( propertyName );
        }

        public void AddProperty( string propertyName, object propertyValue )
        {
            if ( propertyValues.ContainsKey( propertyName ) )
            {
                propertyValues[propertyName] = propertyValue;
            }
            else
            {
                propertyValues.Add( propertyName, propertyValue );
            }
        }
    }
}