using System.Collections.Generic;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    public class WorkFlowInstance : PublishableDomainObject
    {
        internal LinkedList<WorkflowWorkTypeNode> workTypes = new LinkedList<WorkflowWorkTypeNode>( );

        internal WorkFlowInstance( string Name, WorkFlowTemplate FlowTemplate )
        {
        }
    }
}