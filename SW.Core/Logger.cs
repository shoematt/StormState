#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	Logger.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

using SW.Core.Logging;

using StructureMap;

namespace SW.Core
{
    /// <summary>
    ///     Provides logging services for this namespace.
    /// </summary>
    public class Logger
    {
        private static ILog _default;

        /// <summary>
        ///     Creates the <see cref="Logging" /> instance.
        /// </summary>
        /// <param name="logManager"> The log manager. </param>
        public Logger ( ILoggingMaster logManager )
        {
            Default = logManager;
        }

        /// <summary>
        ///     Gets the logging mechanism
        /// </summary>
        /// <value> The logging mechanism </value>
        public static ILog Default
        {
            get
            {
                if ( _default == null || _default.GetType ( ) == typeof ( DefaultLogger ) )
                {
                    try
                    {
                        _default = ObjectFactory.TryGetInstance<ILoggingMaster> ( );
                    }
                    catch ( Exception ex )
                    {
                        _default = new DefaultLogger ( );

                        _default.Error ( ex );
                    }

                    if ( _default == null )
                    {
                        _default = new DefaultLogger ( );
                    }
                }

                return _default;
            }
            set { _default = value; }
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        public static ILog GetLogger ( Type type )
        {
            if ( Default is ILoggingMaster )
            {
                return ( Default as ILoggingMaster ).GetLoggerProvider ( type ) ?? ( Default as ILoggingMaster ).GetNamedLoggerProvider ( type.Name );
            }

            return Default;
        }

        /// <summary>
        ///     Gets the named logger.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        public static ILog GetNamedLogger ( string name )
        {
            if ( Default is ILoggingMaster )
            {
                return ( Default as ILoggingMaster ).GetNamedLoggerProvider ( name );
            }

            return Default;
        }
    }

    public static class LoggerExtensions
    {
        /// <summary>
        ///     Logs the specified item.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> The item. </param>
        /// <param name="loggingAction"> The logging action. </param>
        /// <param name="logLevel"> The log level. </param>
        public static void Log<T> ( this T item, Action<ILog> loggingAction, LogLevel logLevel )
        {
            if ( Logger.Default.IsDebugEnabled )
            {
                loggingAction ( Logger.Default );
            }
        }

        /// <summary>
        ///     Logs the debug.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> The item. </param>
        /// <param name="ex"> The ex. </param>
        public static void LogDebug<T> ( this T item, Exception ex )
        {
            if ( Logger.Default.IsDebugEnabled )
            {
                Logger.Default.Debug ( ex );
            }
        }

        /// <summary>
        ///     Logs the error.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> The item. </param>
        /// <param name="ex"> The ex. </param>
        public static void LogError<T> ( this T item, Exception ex )
        {
            if ( Logger.Default.IsErrorEnabled )
            {
                Logger.Default.Error ( ex );
            }
        }

        /// <summary>
        ///     Logs the info.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> The item. </param>
        /// <param name="ex"> The ex. </param>
        public static void LogInfo<T> ( this T item, Exception ex )
        {
            if ( Logger.Default.IsInfoEnabled )
            {
                Logger.Default.Info ( ex );
            }
        }

        /// <summary>
        ///     Logs the fatal.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> The item. </param>
        /// <param name="ex"> The ex. </param>
        public static void LogFatal<T> ( this T item, Exception ex )
        {
            if ( Logger.Default.IsFatalEnabled )
            {
                Logger.Default.Fatal ( ex );
            }
        }

        /// <summary>
        ///     Logs the warn.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="item"> The item. </param>
        /// <param name="ex"> The ex. </param>
        public static void LogWarn<T> ( this T item, Exception ex )
        {
            if ( Logger.Default.IsWarnEnabled )
            {
                Logger.Default.Warn ( ex );
            }
        }
    }
}