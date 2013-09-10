#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateMetaContainer.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;

using SW.Workflow.States;

namespace SW.Workflow.Engine.States
{
    [ Serializable ]
    public class StateMetaContainer : IStateMetaContainer
    {
        private readonly Dictionary<Guid, object> _values = new Dictionary<Guid, object> ( );

        #region IStateMetaContainer Members

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="data"> The data. </param>
        /// <returns> </returns>
        public object GetValue ( IStateMetaProperty data )
        {
            return _values.ContainsKey ( data.ID ) ? _values [data.ID] : data.Default;
        }

        /// <summary>
        ///     Sets the value.
        /// </summary>
        /// <param name="data"> The data. </param>
        /// <param name="value"> The value. </param>
        public void SetValue ( IStateMetaProperty data, object value )
        {
            if ( value != null && data.Type != value.GetType ( ) )
            {
                throw new Exception ( "Value provided does not match the meta property value type" );
            }

            if ( _values.ContainsKey ( data.ID ) )
            {
                _values.Remove ( data.ID );
            }

            _values.Add ( data.ID, value );
        }

        #endregion
    }
}