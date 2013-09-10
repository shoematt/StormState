#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateMachineException.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.Engine
{
    [ Serializable ]
    public class StateMachineException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StateMachineException" /> class.
        /// </summary>
        /// <param name="error"> The error. </param>
        public StateMachineException ( string error ) : base ( error )
        {
        }
    }
}