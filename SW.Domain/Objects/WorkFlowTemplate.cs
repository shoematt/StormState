using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class WorkFlowTemplate : PublishableDomainObject, IPublishableDomainObject, IEnumerable<WorkflowWorkTypeNode>, IEquatable<WorkFlowTemplate>
    {
        private LinkedListNode<WorkflowWorkTypeNode> current;
        internal LinkedList<WorkflowWorkTypeNode> workTypes = new LinkedList<WorkflowWorkTypeNode>( );

        /// <summary>
        ///   Initializes a new instance of the WorkFlowTemplate class.
        /// </summary>
        protected WorkFlowTemplate( )
        {
        }

        protected WorkFlowTemplate( SerializationInfo info, StreamingContext context )
        {
            workTypes = (LinkedList<WorkflowWorkTypeNode>)info.GetValue( "workTypes", typeof( LinkedList<WorkflowWorkTypeNode> ) );
            current = (LinkedListNode<WorkflowWorkTypeNode>)info.GetValue( "current", typeof( LinkedListNode<WorkflowWorkTypeNode> ) );
        }


        /// <summary>
        ///   Initializes a new instance of the WorkFlowTemplate class.
        /// </summary>
        public WorkFlowTemplate( string Name )
            : base( Name )
        {
        }

        public LinkedListNode<WorkflowWorkTypeNode> Current
        {
            get
            {
                if ( current == null )
                {
                    if ( workTypes.Count > 0 )
                    {
                        current = workTypes.First;
                    }
                }

                return current;
            }
        }

        #region IEnumerable<WorkflowWorkTypeNode> Members

        public IEnumerator<WorkflowWorkTypeNode> GetEnumerator( )
        {
            return workTypes.GetEnumerator( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return workTypes.GetEnumerator( );
        }

        #endregion

        public int WorkFlowSteps( )
        {
            return workTypes.Count;
        }

        public bool CanAddWorkType( WorkflowWorkTypeNode Node )
        {
            return !workTypes.Contains<WorkflowWorkTypeNode>( Node );
        }


        public bool ContainsWorkType( WorkflowWorkTypeNode Node )
        {
            return workTypes.Contains<WorkflowWorkTypeNode>( Node );
        }

        public LinkedListNode<WorkflowWorkTypeNode> Add( WorkflowWorkTypeNode workflowWorkFlowNode )
        {
            if ( workTypes.First == null )
            {
                AddFirst( workflowWorkFlowNode );
            }
            else
            {
                AddLast( workflowWorkFlowNode );
            }

            return Current;
        }


        public void AddFirst( WorkflowWorkTypeNode workflowWorkTypeNode )
        {
            workTypes.AddFirst( new LinkedListNode<WorkflowWorkTypeNode>( workflowWorkTypeNode ) );
            current = workTypes.First;
        }

        public void AddLast( WorkflowWorkTypeNode workflowWorkTypeNode )
        {
            workTypes.AddLast( new LinkedListNode<WorkflowWorkTypeNode>( workflowWorkTypeNode ) );

            current = workTypes.Last;
        }

        public void AddBefore( WorkflowWorkTypeNode existingWorkflowWorkTypeNode, WorkflowWorkTypeNode newWorkflowWorkFlowTypeNode )
        {
            var node = workTypes.Find( existingWorkflowWorkTypeNode );

            LinkedListNode<WorkflowWorkTypeNode> newLinkedListNode = new LinkedListNode<WorkflowWorkTypeNode>( newWorkflowWorkFlowTypeNode );

            workTypes.AddBefore( node, newLinkedListNode );

            current = newLinkedListNode;
        }

        public void AddAfter( WorkflowWorkTypeNode existingWorkflowWorkTypeNode, WorkflowWorkTypeNode newWorkflowWorkFlowTypeNode )
        {
            var node = workTypes.Find( existingWorkflowWorkTypeNode );

            LinkedListNode<WorkflowWorkTypeNode> newLinkedListNode = new LinkedListNode<WorkflowWorkTypeNode>( newWorkflowWorkFlowTypeNode );

            workTypes.AddAfter( node, newLinkedListNode );

            current = newLinkedListNode;
        }


        public void RemoveFirst( )
        {
            if ( workTypes.Count == 0 )
            {
                return;
            }

            if ( Current == workTypes.First )
            {
                workTypes.RemoveFirst( );
                current = workTypes.First;
            }
            else
            {
                workTypes.RemoveFirst( );
            }
        }


        public void RemoveLast( )
        {
            if ( workTypes.Count == 0 )
            {
                return;
            }

            if ( Current == workTypes.Last )
            {
                workTypes.RemoveLast( );
                current = workTypes.Last;
            }
            else
            {
                workTypes.RemoveLast( );
            }
        }


        public void Remove( WorkflowWorkTypeNode workflowWorkTypeNode )
        {
            if ( workTypes.Count == 0 )
            {
                return;
            }

            if ( Current.Value == workflowWorkTypeNode )
            {
                workTypes.Remove( workflowWorkTypeNode );
                current = workTypes.First;
            }
            else
            {
                workTypes.Remove( workflowWorkTypeNode );
            }
        }

        public void Clear( )
        {
            workTypes.Clear( );
        }


        public void SetCurrent( WorkflowWorkTypeNode workflowWorkTypeNode )
        {
            current = workTypes.Find( workflowWorkTypeNode );
        }

        public WorkflowWorkTypeNode First( )
        {
            return workTypes.First.Value;
        }

        public WorkflowWorkTypeNode Last( )
        {
            return workTypes.Last.Value;
        }



        public bool Equals( WorkFlowTemplate other )
        {
            return base.Equals( other );
        }
    }
}