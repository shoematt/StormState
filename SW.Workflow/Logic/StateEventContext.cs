#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateEventContext.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Xml.Serialization;

using StructureMap;

namespace SW.Workflow.Logic
{
    [ Serializable ]
    public class StateEventContext : IStateEventContext
    {
        [ NonSerialized ] [ XmlIgnore ] private IContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateEventContext" /> class.
        /// </summary>
        /// <param name="stateMap"> The state map. </param>
        /// <param name="container"> The container. </param>
        /// <param name="stateEvent"> The state event. </param>
        public StateEventContext ( IStateMap stateMap, IContainer container, IStateEventProxy stateEvent )
        {
            StateMap = stateMap;
            Container = container;
            EventProxy = stateEvent;
        }

        #region IStateEventContext Members

        /// <summary>
        ///     Gets the state map.
        /// </summary>
        public IStateMap StateMap { get; private set; }

        /// <summary>
        ///     Gets the container.
        /// </summary>
        [ XmlIgnore ]
        public IContainer Container
        {
            get { return _container; }
            private set { _container = value; }
        }

        /// <summary>
        ///     Gets the state event.
        /// </summary>
        public IStateEventProxy EventProxy { get; set; }

        #endregion
    }
}