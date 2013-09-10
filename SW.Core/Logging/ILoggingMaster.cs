#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	ILoggingMaster.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Core.Logging
{
    /// <summary>
    ///     The master logging handler, which provides a single
    ///     source with access to all loaded logging mechanisms
    /// </summary>
    public interface ILoggingMaster : ILog
    {
        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        ILoggingProvider GetLoggerProvider ( Type type );

        /// <summary>
        ///     Gets the named logger.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        ILoggingProvider GetNamedLoggerProvider ( string name );
    }
}