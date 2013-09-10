#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	DefaultLogger.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Core.Logging
{
    public class DefaultLogger : ILoggingMaster,
                                 ILoggingProvider
    {
        public DefaultLogger ( )
        {
            IsDebugEnabled = DefaultLoggerSettings.Default.IsDebugEnabled;
            IsErrorEnabled = DefaultLoggerSettings.Default.IsErrorEnabled;
            IsFatalEnabled = DefaultLoggerSettings.Default.IsFatalEnabled;
            IsWarnEnabled = DefaultLoggerSettings.Default.IsWarnEnabled;
            IsInfoEnabled = DefaultLoggerSettings.Default.IsInfoEnabled;
            IsCustomEnabled = DefaultLoggerSettings.Default.IsCustomEnabled;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is custom enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is custom enabled; otherwise, <c>false</c> .
        /// </value>
        public bool IsCustomEnabled { get; set; }

        /// <summary>
        ///     Gets the <see cref="ILog" /> with the specified name.
        /// </summary>
        /// <value> </value>
        public ILog this [ string name ]
        {
            get { throw new NotSupportedException ( ); }
        }

        #region ILoggingMaster Members

        /// <summary>
        ///     Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is debug enabled; otherwise, <c>false</c> .
        /// </value>
        public bool IsDebugEnabled { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is warn enabled; otherwise, <c>false</c> .
        /// </value>
        public bool IsWarnEnabled { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is info enabled; otherwise, <c>false</c> .
        /// </value>
        public bool IsInfoEnabled { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is error enabled; otherwise, <c>false</c> .
        /// </value>
        public bool IsErrorEnabled { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is fatal enabled; otherwise, <c>false</c> .
        /// </value>
        public bool IsFatalEnabled { get; private set; }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        ILoggingProvider ILoggingMaster.GetLoggerProvider ( Type type )
        {
            return this;
        }

        /// <summary>
        ///     Gets the named logger.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        ILoggingProvider ILoggingMaster.GetNamedLoggerProvider ( string name )
        {
            return this;
        }

        #endregion

        #region ILoggingProvider Members

        /// <summary>
        ///     Gets the named logger.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        ILog ILoggingProvider.GetNamedLogger ( string name )
        {
            return this;
        }

        #endregion

        /// <summary>
        ///     Customs the specified custom.
        /// </summary>
        /// <param name="custom"> The custom. </param>
        /// <param name="color"> The color. </param>
        public void Custom ( string custom, ConsoleColor color )
        {
            if ( !IsCustomEnabled )
            {
                return;
            }

            Console.ForegroundColor = color;
            Console.WriteLine ( custom );
        }

        /// <summary>
        ///     Gets the exception MSG.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        /// <returns> </returns>
        private static string GetExceptionMsg ( Exception ex )
        {
            return string.Format ( "Exception [ Type{0}: Message: {1} ]", ex.GetType ( ), ex.Message );
        }

        #region Debug

        public void Debug ( Exception ex )
        {
            if ( !IsCustomEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ( "Debug " + GetExceptionMsg ( ex ) );
        }

        public void Debug ( object obj )
        {
            if ( !IsDebugEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ( "Debug " + obj );
        }

        public void Debug ( object obj, Exception ex )
        {
            if ( !IsDebugEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ( "Warn " + obj + GetExceptionMsg ( ex ) );
        }

        public void DebugFormat ( string format, object arg0, object arg1, object arg2 )
        {
            if ( !IsDebugEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ( "Debug " + string.Format ( format, arg0, arg1, arg2 ) );
        }

        public void DebugFormat ( string format, object arg0, object arg1 )
        {
            if ( !IsDebugEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ( "Debug " + string.Format ( format, arg0, arg1 ) );
        }

        public void DebugFormat ( string format, object arg0 )
        {
            if ( !IsDebugEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ( "Debug " + string.Format ( format, arg0 ) );
        }

        public void DebugFormat ( string format, params object[] arg0 )
        {
            if ( !IsDebugEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ( "Debug " + string.Format ( format, arg0 ) );
        }

        #endregion

        #region Warn

        public void Warn ( Exception ex )
        {
            if ( !IsWarnEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ( "Warn " + GetExceptionMsg ( ex ) );
        }

        public void Warn ( object obj )
        {
            if ( !IsWarnEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ( "Warn " + obj );
        }

        public void Warn ( object obj, Exception ex )
        {
            if ( !IsWarnEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ( "Warn " + obj + GetExceptionMsg ( ex ) );
        }

        public void WarnFormat ( string format, object arg0, object arg1, object arg2 )
        {
            if ( !IsWarnEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ( "Warn " + string.Format ( format, arg0, arg1, arg2 ) );
        }

        public void WarnFormat ( string format, object arg0, object arg1 )
        {
            if ( !IsWarnEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ( "Warn " + string.Format ( format, arg0, arg1 ) );
        }

        public void WarnFormat ( string format, object arg0 )
        {
            if ( !IsWarnEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ( "Warn " + string.Format ( format, arg0 ) );
        }

        public void WarnFormat ( string format, params object[] arg0 )
        {
            if ( !IsWarnEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ( "Warn " + string.Format ( format, arg0 ) );
        }

        #endregion

        #region Info

        public void Info ( object obj )
        {
            if ( !IsInfoEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ( "Info " + obj );
        }

        public void Info ( Exception ex )
        {
            if ( !IsInfoEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ( "Info " + GetExceptionMsg ( ex ) );
        }

        public void Info ( object obj, Exception ex )
        {
            if ( !IsInfoEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ( "Info " + obj + GetExceptionMsg ( ex ) );
        }

        public void InfoFormat ( string format, object arg0, object arg1, object arg2 )
        {
            if ( !IsInfoEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ( "Info " + string.Format ( format, arg0, arg1, arg2 ) );
        }

        public void InfoFormat ( string format, object arg0, object arg1 )
        {
            if ( !IsInfoEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ( "Info " + string.Format ( format, arg0, arg1 ) );
        }

        public void InfoFormat ( string format, object arg0 )
        {
            if ( !IsInfoEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ( "Info " + string.Format ( format, arg0 ) );
        }

        public void InfoFormat ( string format, params object[] arg0 )
        {
            if ( !IsInfoEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ( "Info " + string.Format ( format, arg0 ) );
        }

        #endregion

        #region Error

        public void Error ( object obj )
        {
            if ( !IsErrorEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ( "Error " + obj );
        }

        public void Error ( Exception ex )
        {
            if ( !IsErrorEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ( "Error " + GetExceptionMsg ( ex ) );
        }

        public void Error ( object obj, Exception ex )
        {
            if ( !IsErrorEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ( "Error " + obj + GetExceptionMsg ( ex ) );
        }

        public void ErrorFormat ( string format, object arg0, object arg1, object arg2 )
        {
            if ( !IsErrorEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ( "Error " + string.Format ( format, arg0, arg1, arg2 ) );
        }

        public void ErrorFormat ( string format, object arg0, object arg1 )
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ( "Error " + string.Format ( format, arg0, arg1 ) );
        }

        public void ErrorFormat ( string format, object arg0 )
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ( "Error " + string.Format ( format, arg0 ) );
        }

        public void ErrorFormat ( string format, params object[] arg0 )
        {
            if ( !IsErrorEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ( "Error " + string.Format ( format, arg0 ) );
        }

        #endregion

        #region Fatal

        public void Fatal ( object obj )
        {
            if ( !IsFatalEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ( "Fatal " + obj );
        }

        public void Fatal ( Exception ex )
        {
            if ( !IsFatalEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ( "Fatal " + GetExceptionMsg ( ex ) );
        }

        public void Fatal ( object obj, Exception ex )
        {
            if ( !IsFatalEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ( "Fatal " + obj + GetExceptionMsg ( ex ) );
        }

        public void FatalFormat ( string format, object arg0, object arg1, object arg2 )
        {
            if ( !IsFatalEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ( "Fatal " + string.Format ( format, arg0, arg1, arg2 ) );
        }

        public void FatalFormat ( string format, object arg0, object arg1 )
        {
            if ( !IsFatalEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ( "Fatal " + string.Format ( format, arg0, arg1 ) );
        }

        public void FatalFormat ( string format, object arg0 )
        {
            if ( !IsFatalEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ( "Fatal " + string.Format ( format, arg0 ) );
        }

        public void FatalFormat ( string format, params object[] arg0 )
        {
            if ( !IsFatalEnabled )
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ( "Fatal " + string.Format ( format, arg0 ) );
        }

        #endregion
    }
}