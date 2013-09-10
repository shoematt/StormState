#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateMetaContainer.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

namespace SW.Workflow.States
{
    public interface IStateMetaContainer
    {
        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="data"> The data. </param>
        object GetValue ( IStateMetaProperty data );

        /// <summary>
        ///     Sets the value.
        /// </summary>
        /// <param name="data"> The data. </param>
        /// <param name="value"> The value. </param>
        void SetValue ( IStateMetaProperty data, object value );
    }
}