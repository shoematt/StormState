using System;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class WorkflowWorkTypeNode : PublishableDomainObject, IPublishableDomainObject, IEquatable<WorkflowWorkTypeNode>
    {
        /// <summary>
        ///   Initializes a new instance of the WorkFlowWorkTypeNode class.
        /// </summary>
        public WorkflowWorkTypeNode(WorkType workType, WorkFlowTemplate template)
        {
            WorkTypeInstanceTypeID = workType.StaticInstanceID;
            WorkFlowTemplateID = template.StaticInstanceID;
            Name = workType.Name;
        }

        public virtual Guid WorkTypeInstanceTypeID { get; set; }

        public virtual Guid WorkFlowTemplateID { get; set; }


        public override string Name
        {
            get
            {
                if (string.IsNullOrEmpty(base.Name))
                {
                    base.Name = string.Format("WorkFlowNode for workflow template id {0} and worktype instance id {1}", WorkFlowTemplateID, WorkTypeInstanceTypeID);
                }

                return base.Name;
            }
            set { base.Name = value; }
        }

        #region IEquatable<WorkflowWorkTypeNode> Members

        public virtual bool Equals(WorkflowWorkTypeNode other)
        {
            return (WorkTypeInstanceTypeID == other.WorkTypeInstanceTypeID && WorkFlowTemplateID == other.WorkFlowTemplateID);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as WorkflowWorkTypeNode);
        }


        public override int GetHashCode()
        {
            int result = 2;

            result = 29 * result + WorkTypeInstanceTypeID.GetHashCode();
            result = 29 * result + WorkFlowTemplateID.GetHashCode();
            return result;
        }
    }
}