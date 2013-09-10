#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	StateTransition.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SW.Workflow.States;
using SW.Workflow.Transitions;

using StructureMap;

namespace SW.Workflow.Engine.Transitions
{
    [ Serializable ]
    public class StateTransition : IStateTransition<StateTransition>
    {
        [ NonSerialized ] private IContainer _container;

        public StateTransition ( )
        {
            To = new List<IState> ( );
        }

        [ XmlIgnore ]
        private IContainer Container
        {
            set { _container = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the <see cref="IState" />
        /// </summary>
        /// <value>
        ///     The name of the <see cref="IState" /> .
        /// </value>
        public string Name { get; set; }

        #region IStateTransition<StateTransition> Members

        /// <summary>
        ///     Gets or sets to.
        /// </summary>
        /// <value> To. </value>
        public List<IState> To { get; set; }

        /// <summary>
        ///     Gets or sets from.
        /// </summary>
        /// <value> From. </value>
        public IState From { get; set; }

        /// <summary>
        ///     Executes the behavior for this instance.
        /// </summary>
        /// <param name="transitionAction"> </param>
        public void DoTransition ( Action<List<IState>, IState, IStateTransition> transitionAction )
        {
            OnTransition ( transitionAction );
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        public void Reset ( )
        {
        }

        /// <summary>
        ///     Initializes the container.
        /// </summary>
        /// <param name="container"> The container. </param>
        public void InitializeContainer ( IContainer container )
        {
            Container = container;

            if ( From != null )
            {
                From.InitializeContainer ( container );
            }

            foreach ( var item in To )
            {
                if ( item != null )
                {
                    item.InitializeContainer ( container );
                }
            }
        }

        #endregion

        /// <summary>
        ///     Called when [transition].
        /// </summary>
        public void OnTransition ( Action<List<IState>, IState, IStateTransition> transitionAction )
        {
            if ( transitionAction != null )
            {
                transitionAction ( To, From, this );
            }
        }
    }
}