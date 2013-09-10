#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	SerializationInfoExtensions.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

using SW.Core.Logging;

namespace SW.Core.Extensions
{
    public static class XmlSerializerExtensions
    {
        [ NonSerialized ] private static readonly ILog Log = Logger.GetNamedLogger ( MethodBase.GetCurrentMethod ( )
                                                                                               .DeclaringType.Name );

        public static object Deserialize ( XmlReader reader, string element )
        {
            // if the result is a null data item
            if ( reader.NodeType == XmlNodeType.EndElement )
            {
                reader.ReadEndElement ( );
                reader.MoveToContent ( );

                return null;
            }

            string entityTypeStr = reader.GetAttribute ( "Type" );
            Debug.Assert ( entityTypeStr != null, "entityTypeStr != null" );
            Type entityType = Type.GetType ( entityTypeStr );

            Debug.Assert ( entityType != null, "entityType != null" );
            if ( entityType.IsPrimitive || typeof ( string ).IsAssignableFrom ( entityType ) )
            {
                string value = reader.GetAttribute ( "Value" );
                reader.ReadStartElement ( element );
                reader.MoveToContent ( );

                return Convert.ChangeType ( value, entityType );
            }

            if ( typeof ( Guid ).IsAssignableFrom ( entityType ) )
            {
                string value = reader.GetAttribute ( "Value" );
                reader.ReadStartElement ( element );
                reader.MoveToContent ( );

                Debug.Assert ( value != null, "value != null" );
                return new Guid ( value );
            }

            reader.ReadStartElement ( );
            reader.MoveToContent ( );

            var serializer = new XmlSerializer ( entityType );
            object obj = serializer.Deserialize ( reader );

            if ( !reader.EOF )
            {
                reader.MoveToContent ( );
                reader.ReadEndElement ( );
                reader.MoveToContent ( );
            }

            return obj;
        }

        /// <summary>
        ///     Serializes the specified writer.
        /// </summary>
        /// <param name="serializer"> The serializer. </param>
        /// <param name="writer"> The writer. </param>
        /// <param name="value"> The value. </param>
        /// <param name="element"> The element. </param>
        public static void Normalize ( this XmlSerializer serializer, XmlWriter writer, object value, string element )
        {
            if ( value == null )
            {
                return;
            }

            try
            {
                foreach ( var property in value.GetType ( )
                                               .GetProperties ( ) )
                {
                    if ( property.GetType ( )
                                 .IsPrimitive || value is Guid || value is string )
                    {
                        writer.WriteStartElement ( element );
                        writer.WriteValue ( Convert.ToString ( value ) );
                        writer.WriteEndElement ( );
                    }
                }
            }
            catch
            {
                // object cannot be serialized
                if ( Log.IsWarnEnabled )
                {
                    Log.WarnFormat ( "Object Type : {0} cannot be XmlSerialized", value.GetType ( ) );
                }
            }
        }

        /// <summary>
        ///     Serializes the specified writer.
        /// </summary>
        /// <param name="serializer"> The serializer. </param>
        /// <param name="writer"> The writer. </param>
        /// <param name="value"> The value. </param>
        /// <param name="element"> The element. </param>
        public static void Serialize ( this XmlSerializer serializer, XmlWriter writer, object value, string element )
        {
            if ( value == null )
            {
                return;
            }

            try
            {
                if ( value.GetType ( )
                          .IsPrimitive || value is Guid || value is string )
                {
                    writer.WriteStartElement ( element );

                    writer.WriteStartAttribute ( "Type" );
                    writer.WriteValue ( value.GetType ( )
                                             .IsPrimitive ? TypeEnumerator.GetShortTypeNameFor ( value.GetType ( ) ) : value.GetType ( )
                                                                                                                            .AssemblyQualifiedName );
                    writer.WriteEndAttribute ( );

                    writer.WriteStartAttribute ( "Value" );
                    writer.WriteValue ( Convert.ToString ( value ) );
                    writer.WriteEndAttribute ( );
                }
                else
                {
                    var xmlSerializer = new XmlSerializer ( value.GetType ( ) );

                    writer.WriteStartElement ( element );

                    writer.WriteStartAttribute ( "Type" );
                    writer.WriteValue ( value.GetType ( )
                                             .IsPrimitive ? TypeEnumerator.GetShortTypeNameFor ( value.GetType ( ) ) : value.GetType ( )
                                                                                                                            .AssemblyQualifiedName );
                    writer.WriteEndAttribute ( );

                    xmlSerializer.Serialize ( writer, value );
                }
            }
            catch
            {
                // object cannot be serialized
                if ( Log.IsWarnEnabled )
                {
                    Log.WarnFormat ( "Object Type : {0} cannot be XmlSerialized", value.GetType ( ) );
                }
            }

            writer.WriteEndElement ( );
        }
    }

    public static class SerializationInfoExtensions
    {
        private const string _prefix = "_{0}_";

        /// <summary>
        ///     Adds the type of the value with.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="name"> The name. </param>
        /// <param name="value"> The value. </param>
        public static void AddValueWithType ( this SerializationInfo info, string name, object value )
        {
            info.AddValue ( name + "IsNull", value == null );

            if ( value != null )
            {
                info.AddValue ( name + "Type", value.GetType ( )
                                                    .AssemblyQualifiedName );
                info.AddValue ( name + "Value", value );
            }
        }

        /// <summary>
        ///     Adds the typed collection.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="name"> The name. </param>
        /// <param name="value"> The value. </param>
        public static void AddTypedCollection ( this SerializationInfo info, string name, IList value )
        {
            info.AddValue ( name + "_IsNull", value == null );

            if ( value != null )
            {
                info.AddValue ( name + "_Length", value.Count );
                info.AddValue ( name + "_Type", value.GetType ( )
                                                     .AssemblyQualifiedName );

                int index = 0;

                foreach ( object item in value )
                {
                    if ( item == null )
                    {
                        continue;
                    }

                    info.AddValueWithType ( string.Format ( name + _prefix, index ), item );

                    index++;
                }
            }
        }

        /// <summary>
        ///     Gets the typed collection.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        public static IList GetTypedCollection ( this SerializationInfo info, string name )
        {
            bool isNull = info.GetBoolean ( name + "_IsNull" );

            if ( !isNull )
            {
                int length = info.GetInt32 ( name + "_Length" );

                Type collectionType = Type.GetType ( info.GetString ( name + "_Type" ) );

                var list = ( IList ) Activator.CreateInstance ( collectionType );

                for ( int i = 0; i < length; i++ )
                {
                    try
                    {
                        list.Add ( info.GetValueWithType ( string.Format ( name + _prefix, i ) ) );
                    }
                    catch
                    {
                    }
                }

                return list;
            }

            return null;
        }

        /// <summary>
        ///     Gets the type of the value with.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        public static object GetValueWithType ( this SerializationInfo info, string name )
        {
            bool isNull = info.GetBoolean ( name + "IsNull" );

            if ( !isNull )
            {
                Type instanceType = Type.GetType ( info.GetString ( name + "Type" ) );
                return info.GetValue ( name + "Value", instanceType );
            }

            return null;
        }

        /// <summary>
        ///     Gets the short type value safely.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="name"> The name. </param>
        /// <returns> null if the type is not present, the type otherwise </returns>
        public static Type GetShortTypeValueSafe ( this SerializationInfo info, string name )
        {
            if ( info.GetBoolean ( "Has" + name + "Type" ) )
            {
                string typeName = info.GetString ( name );
                Type type = Type.GetType ( typeName );
                return type;
            }

            return null;
        }

        /// <summary>
        ///     Adds the short type value safely.
        /// </summary>
        /// <param name="info"> The info. </param>
        /// <param name="name"> The name. </param>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        public static void AddShortTypeValueSafe ( this SerializationInfo info, string name, Type type )
        {
            info.AddValue ( "Has" + name + "Type", type != null );
            if ( type != null )
            {
                info.AddValue ( name, type.AssemblyQualifiedName );
            }
        }
    }
}