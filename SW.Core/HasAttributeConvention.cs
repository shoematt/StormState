#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Core
// 
// File:	HasAttributeConvention.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;

using SW.Core.Extensions;

using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SW.Core
{
    public class HasAttributeConvention<T> : IRegistrationConvention where T : Attribute
    {
        #region IRegistrationConvention Members

        /// <summary>
        ///     Processes the specified type.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <param name="registry"> The registry. </param>
        public void Process ( Type type, Registry registry )
        {
            Attribute attr = ReflectionHelper.GetAttribute<T> ( type );

            if ( attr != null )
            {
                List<Type> typeList = ObjectFactory.TryGetInstance<List<Type>> ( typeof ( T ).AssemblyQualifiedName );
                typeList.Add ( type );
            }
        }

        #endregion
    }
}