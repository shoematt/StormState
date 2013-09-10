using System;
using System.Linq;
using System.Reflection;
using Orca.Core;

namespace Orca.Expressions.Extensions
{
    public static class ReflectionExtensions
    {
        public static string ToSerializableForm( this MethodInfo method )
        {
            string serializableName = TypeEnumerator.GetShortTypeNameFor(method.DeclaringType) + Environment.NewLine;

            if ( !method.IsGenericMethod )
            {
                serializableName += method.ToString( );
            }
            else
            {
                serializableName += method.GetGenericMethodDefinition( ) + Environment.NewLine +
                                    String.Join( Environment.NewLine, method.GetGenericArguments( ).Select( ty => ty.ToSerializableForm( ) ).ToArray( ) );
            }
            
            return serializableName;
        }

        public static MethodInfo FromSerializableForm( this MethodInfo methodInfo, string serializedValue )
        {
            string[] fullName = SplitString( serializedValue );
            string name = fullName[1];
            MethodInfo method = ( from m in Type.GetType( fullName[0] ).GetMethods( )
                                  where m.ToString( ) == name
                                  select m ).First( );

            if ( method.IsGenericMethod )
            {
                method = method.MakeGenericMethod( fullName.Skip( 2 ).Select( s => typeof ( string ).FromSerializableForm( s ) ).ToArray( ) );
            }
            return method;
        }

        public static string ToSerializableForm( this MemberInfo member )
        {
            //return member.DeclaringType.AssemblyQualifiedName + Environment.NewLine + member;
            return TypeEnumerator.GetShortTypeNameFor(member.DeclaringType) + Environment.NewLine + member;
        }

        public static MemberInfo FromSerializableForm( this MemberInfo memberInfo, string serializedValue )
        {
            string[] fullName = SplitString( serializedValue );
            string name = fullName[1];
            MemberInfo member = ( from m in Type.GetType( fullName[0] ).GetMembers( )
                                  where m.ToString( ) == name
                                  select m ).First( );
            return member;
        }

        //public static string ToSerializableForm( this ConstructorInfo obj )
        //{
        //    return obj == null ? null : TypeEnumerator.GetShortTypeNameFor(obj.DeclaringType)/*.AssemblyQualifiedName*/ + Environment.NewLine + obj;
        //}

        //public static ConstructorInfo FromSerializableForm( this ConstructorInfo obj, string serializedValue )
        //{
        //    if ( serializedValue == null )
        //        return null;
        //    else
        //    {
        //        string[] fullName = SplitString( serializedValue );
        //        string name = fullName[1];
        //        ConstructorInfo newObj = ( from m in Type.GetType( fullName[0] ).GetConstructors( )
        //                                   where m.ToString( ) == name
        //                                   select m ).First( );
        //        return newObj;
        //    }
        //}

        private static String[] SplitString( string str )
        {
            return str.Split( str.Contains( Environment.NewLine ) ? new[] {Environment.NewLine} : new[] {"\n"}, StringSplitOptions.None );
        }
    }
}