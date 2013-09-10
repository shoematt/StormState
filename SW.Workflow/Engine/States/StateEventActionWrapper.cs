#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateEventActionWrapper.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Xml.Serialization;

using SW.Workflow.States;

using StructureMap;

namespace SW.Workflow.Engine.States
{
    [ Serializable ]
    public class StateEventActionWrapper : IStateEvent
    {
        [ NonSerialized ] private readonly Action _action;

        [ NonSerialized ] private IContainer _container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StateEventActionWrapper" /> class.
        /// </summary>
        /// <param name="action"> The action. </param>
        public StateEventActionWrapper ( Action action )
        {
            _action = action;
        }

        [ XmlIgnore ]
        //     [SetterProperty]
        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        #region IStateEvent Members

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        public void InitializeContainer ( IContainer container )
        {
            Container = container;
        }

        /// <summary>
        ///     Executes the behavior for this instance.
        /// </summary>
        /// <param name="container"> </param>
        /// <param name="stateMap"> </param>
        public void Execute ( IContainer container, StateMap stateMap )
        {
            _action.Invoke ( );
        }

        #endregion
    }
}