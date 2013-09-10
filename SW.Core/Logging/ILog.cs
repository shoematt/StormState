#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	ILog.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

#region Using Directives

using System;

#endregion

namespace SW.Core.Logging
{
    public interface ILog
    {
        /// <summary>
        ///     Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is debug enabled; otherwise, <c>false</c> .
        /// </value>
        bool IsDebugEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is warn enabled; otherwise, <c>false</c> .
        /// </value>
        bool IsWarnEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is info enabled; otherwise, <c>false</c> .
        /// </value>
        bool IsInfoEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is error enabled; otherwise, <c>false</c> .
        /// </value>
        bool IsErrorEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is fatal enabled; otherwise, <c>false</c> .
        /// </value>
        bool IsFatalEnabled { get; }

        void Debug ( Exception ex );

        void Debug ( object obj );

        void Debug ( object obj, Exception ex );

        void DebugFormat ( string format, object arg0, object arg1, object arg2 );

        void DebugFormat ( string format, object arg0, object arg1 );

        void DebugFormat ( string format, object arg0 );

        void DebugFormat ( string format, params object[] arg0 );

        void Warn ( Exception ex );

        void Warn ( object obj );

        void Warn ( object obj, Exception ex );

        void WarnFormat ( string format, object arg0, object arg1, object arg2 );

        void WarnFormat ( string format, object arg0, object arg1 );

        void WarnFormat ( string format, object arg0 );

        void WarnFormat ( string format, params object[] arg0 );

        void Info ( object obj );

        void Info ( Exception ex );

        void Info ( object obj, Exception ex );

        void InfoFormat ( string format, object arg0, object arg1, object arg2 );

        void InfoFormat ( string format, object arg0, object arg1 );

        void InfoFormat ( string format, object arg0 );

        void InfoFormat ( string format, params object[] arg0 );

        void Error ( object obj );

        void Error ( Exception ex );

        void Error ( object obj, Exception ex );

        void ErrorFormat ( string format, object arg0, object arg1, object arg2 );

        void ErrorFormat ( string format, object arg0, object arg1 );

        void ErrorFormat ( string format, object arg0 );

        void ErrorFormat ( string format, params object[] arg0 );

        void Fatal ( object obj );

        void Fatal ( Exception ex );

        void Fatal ( object obj, Exception ex );

        void FatalFormat ( string format, object arg0, object arg1, object arg2 );

        void FatalFormat ( string format, object arg0, object arg1 );

        void FatalFormat ( string format, object arg0 );

        void FatalFormat ( string format, params object[] arg0 );
    }
}