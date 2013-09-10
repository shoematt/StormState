#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	StringExtensions.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System.Text;

namespace SW.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Checks if a string is not null or empty
        /// </summary>
        /// <param name="value"> A string instance </param>
        /// <returns> True if the string has a value </returns>
        public static bool IsNotEmpty ( this string value )
        {
            return !string.IsNullOrEmpty ( value );
        }

        /// <summary>
        ///     Check if a string is null or empty
        /// </summary>
        /// <param name="value"> A string instance </param>
        /// <returns> True if the string is null or empty, otherwise false </returns>
        public static bool IsEmpty ( this string value )
        {
            return string.IsNullOrEmpty ( value );
        }

        /// <summary>
        ///     Returns true if a string is null (the string can, however, be empty)
        /// </summary>
        /// <param name="value"> A string value </param>
        /// <returns> True if the string value is null, otherwise false </returns>
        public static bool IsNull ( this string value )
        {
            return value == null;
        }

        /// <summary>
        ///     Uses the string as a template and applies the specified arguments
        /// </summary>
        /// <param name="format"> The format string </param>
        /// <param name="args"> The arguments to pass to the format provider </param>
        /// <returns> The formatted string </returns>
        public static string FormatWith ( this string format, params object[] args )
        {
            return format.IsEmpty ( ) ? format : string.Format ( format, args );
        }

        /// <summary>
        ///     Returns the UTF-8 encoded string from the specified byte array
        /// </summary>
        /// <param name="data"> The byte array </param>
        /// <returns> The UTF-8 string </returns>
        public static string ToUtf8String ( this byte[] data )
        {
            return Encoding.UTF8.GetString ( data );
        }
    }
}