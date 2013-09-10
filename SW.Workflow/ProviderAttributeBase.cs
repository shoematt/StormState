#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	ProviderAttributeBase.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow
{
    public abstract class ProviderAttributeBase : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProviderAttributeBase" /> class.
        /// </summary>
        /// <param name="providerServiceProviderType"> Type of the provider. </param>
        /// <param name="providerBaseType"> Type of the provider base. </param>
        protected ProviderAttributeBase ( string providerServiceProviderType, Type providerBaseType )
        {
            ProviderBaseType = providerBaseType;
            ProviderType = Type.GetType ( providerServiceProviderType );
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProviderAttributeBase" /> class.
        /// </summary>
        /// <param name="providerType"> The action context. </param>
        /// <param name="providerBaseType"> Type of the provider base. </param>
        protected ProviderAttributeBase ( Type providerType, Type providerBaseType )
        {
            ProviderType = providerType;
            ProviderBaseType = providerBaseType;
        }

        /// <summary>
        ///     Gets or sets the type of the provider base.
        /// </summary>
        /// <value> The type of the provider base. </value>
        public Type ProviderBaseType { get; set; }

        /// <summary>
        ///     Gets the action context.
        /// </summary>
        /// <value> The action context. </value>
        public Type ProviderType { get; private set; }
    }
}