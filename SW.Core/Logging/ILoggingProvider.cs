#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	ILoggingProvider.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

namespace SW.Core.Logging
{
    public interface ILoggingProvider : ILog
    {
        /// <summary>
        ///     Gets the named logger.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        ILog GetNamedLogger ( string name );
    }
}