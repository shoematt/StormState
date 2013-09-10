#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IStateComponent.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using StructureMap;

namespace SW.Workflow
{
    public interface IStateComponent : IProvideStatePropertyDescriptors
    {
        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        void InitializeContainer ( IContainer container );
    }
}