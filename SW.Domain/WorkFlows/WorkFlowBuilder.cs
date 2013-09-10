using System;
using System.Collections.Generic;
using Orca.Domain.Exceptions;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects;
using Orca.Domain.Objects.PlaceHolders;
using Orca.Persistence.Core.Exceptions;
using Orca.Persistence.Core.Interfaces;
using StructureMap;

namespace Orca.Domain.WorkFlows
{
    public class WorkFlowBuilder
    {
        private readonly IEntityRepository _repository;
        private readonly INameManager nameMgr;
        private readonly Type workFlowTemplatetypeForNameMgr = typeof ( WorkFlowTemplate );

        private IContainer container;

        private IPersistor persistor;
        private IWorkTypeManager worktypeManager;

        /// <summary>
        ///   Initializes a new instance of the WorkTypeManager class.
        /// </summary>
        /// <param name = "repository"></param>
        public WorkFlowBuilder( IEntityRepository repository, IPersistor Persistor, INameManager nameMgr, IWorkTypeManager worktypeManager, IContainer container )
        {
            _repository = repository;

            persistor = Persistor;

            this.nameMgr = nameMgr;

            this.worktypeManager = worktypeManager;

            this.container = container;
        }


        public WorkFlowTemplate CreateWorkFlowTemplate( string Name )
        {
            if ( nameMgr.IsNameAvailable( workFlowTemplatetypeForNameMgr, Name ) )
            {
                nameMgr.RegisterName( workFlowTemplatetypeForNameMgr, Name );

                var workflowTemplate = new WorkFlowTemplate( Name );

                var result = persistor.SaveOrUpdateAndPersist( workflowTemplate );

                if ( !result.Success )
                {
                    throw new PersistenceTransActionException(string.Format("Unable to save the {0} template", Name));
                }

                return workflowTemplate;
            }
            throw new NameAlreadyUsedException( Name, workFlowTemplatetypeForNameMgr );
        }


        public WorkFlowTemplate CreateWorkFlowTemplate( string Name, List<WorkType> workTypes )
        {
            WorkFlowTemplate template = CreateWorkFlowTemplate( Name );

            foreach ( WorkType item in workTypes )
            {
                AddWorkType( template, item );
            }

            var result = persistor.SaveOrUpdateAndPersist( template );

            if ( !result.Success )
            {
                throw new PersistenceTransActionException(string.Format("Unable to save the {0} template", Name));
            }

            return template;
        }

        public bool CanAddWorkType( WorkFlowTemplate template, WorkType workType )
        {
            var workFlowNode = CreateWorkFlowTypeNode( template, workType );
            return CanAddWorkType( template, workFlowNode );
        }


        public bool CanAddWorkType( WorkFlowTemplate template, WorkflowWorkTypeNode workflowWorkType )
        {
            return template.CanAddWorkType( workflowWorkType );
        }


        private void ValidateAdd( WorkFlowTemplate template, WorkflowWorkTypeNode Node )
        {
            //vaidate rules for publish and such

            if ( !CanAddWorkType( template, Node ) )
            {
                throw new WorkTypeAlreadyExistInWorkFlowException( Node.Name );
            }
        }


        public WorkflowWorkTypeNode CreateWorkFlowTypeNode( WorkFlowTemplate template, WorkType workType )
        {
            return new WorkflowWorkTypeNode( workType, template );
        }

        public void AddWorkType( WorkFlowTemplate template, WorkType workType )
        {
            //worktype has to be published before allow to add to workflow.
            //and workflowtemplate can not be published.
            //Use an interface of IValidateAddWorkTypeForWorkFlow.Execute(template,worktype)

            var workFlowNode = CreateWorkFlowTypeNode( template, workType );

            AddWorkFlowTypeNode( template, workFlowNode );
        }


        public void AddWorkFlowTypeNode( WorkFlowTemplate template, WorkflowWorkTypeNode workflowWorkFlowNode )
        {
            ValidateAdd( template, workflowWorkFlowNode );
            template.Add( workflowWorkFlowNode );
        }


        public void AddFirst( WorkFlowTemplate template, WorkflowWorkTypeNode workflowWorkFlowNode )
        {
            ValidateAdd( template, workflowWorkFlowNode );
            template.AddFirst( workflowWorkFlowNode );
        }

        public void AddFirst( WorkFlowTemplate template, WorkType workType )
        {
            var workFlowNode = CreateWorkFlowTypeNode( template, workType );
            AddFirst( template, workFlowNode );
        }


        public void AddLast( WorkFlowTemplate template, WorkflowWorkTypeNode workflowWorkFlowNode )
        {
            ValidateAdd( template, workflowWorkFlowNode );
            template.AddLast( workflowWorkFlowNode );
        }

        public void AddLast( WorkFlowTemplate template, WorkType workType )
        {
            var workFlowNode = CreateWorkFlowTypeNode( template, workType );
            AddLast( template, workFlowNode );
        }


        public void AddBefore( WorkFlowTemplate template, WorkflowWorkTypeNode existingWorkflowWorkTypeNode, WorkflowWorkTypeNode newWorkflowWorkFlowTypeNode )
        {
            ValidateAdd( template, newWorkflowWorkFlowTypeNode );
            template.AddBefore( existingWorkflowWorkTypeNode, newWorkflowWorkFlowTypeNode );
        }

        public void AddBefore( WorkFlowTemplate template, WorkType existingWorkType, WorkType newWorkType )
        {
            var workFlowNodeexisting = CreateWorkFlowTypeNode( template, existingWorkType );
            var newNodeWorkFlow = CreateWorkFlowTypeNode( template, newWorkType );
            AddBefore( template, workFlowNodeexisting, newNodeWorkFlow );
        }


        public void AddAfter( WorkFlowTemplate template, WorkflowWorkTypeNode existingWorkflowWorkTypeNode, WorkflowWorkTypeNode newWorkflowWorkFlowTypeNode )
        {
            ValidateAdd( template, newWorkflowWorkFlowTypeNode );
            template.AddAfter( existingWorkflowWorkTypeNode, newWorkflowWorkFlowTypeNode );
        }

        public void AddAfter( WorkFlowTemplate template, WorkType existingWorkType, WorkType newWorkType )
        {
            var workFlowNodeexisting = CreateWorkFlowTypeNode( template, existingWorkType );
            var newNodeWorkFlow = CreateWorkFlowTypeNode( template, newWorkType );

            AddAfter( template, workFlowNodeexisting, newNodeWorkFlow );
        }


        public void RemoveFirst( WorkFlowTemplate template )
        {
            template.RemoveFirst( );
        }


        public void RemoveLast( WorkFlowTemplate template )
        {
            template.RemoveLast( );
        }


        public void Remove( WorkFlowTemplate template, WorkflowWorkTypeNode workflowWorkTypeNode )
        {
            template.Remove( workflowWorkTypeNode );
        }


        public void Remove( WorkFlowTemplate template, WorkType workType )
        {
            var workFlowNodeexisting = CreateWorkFlowTypeNode( template, workType );
            template.Remove( workFlowNodeexisting );
        }


        public void Clear( WorkFlowTemplate template )
        {
            template.Clear( );
        }


        public WorkFlowInstance CreateWorkFlowInstance( WorkFlowTemplate template, IExternalMessageData messageData )
        {
            return null;
        }


        private bool AbleToEditTemplate( WorkFlowTemplate template )
        {
            return true;
        }
    }
}