using System;
using System.ComponentModel;
using Orca.Core.Domain;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects.BaseObjects;
using Orca.Domain.Objects.Constants;

namespace Orca.Domain.Objects
{
    [Serializable]
    [Editor( "Orca.UI.Design.Editors.WorkTypeChooser, Orca.UI.Design", "Orca.Workflow.UI.Design.ProxyPropertyWrapperUITypeEditor, Orca.Workflow.UI.Design" )]
    [DisplayName( "Work Type" )]
    public class WorkType : DomainObjectWithDefaultProperties, IDomainObject, ISupportDefaultPropertyValues, IEquatable<WorkType>
    {
        private string _description;

        private ItemTemplate _itemTemplate; //this needs to be moved to some sort of provider object, but not enough time to code that

        private ItemSelectionCriteria _itemSelectionCriteria; //this needs to be moved to some sort of provider object, but not enough time to code that

        private DisplayFilter displayFilter;  //this needs to be moved to some sort of provider object, but not enough time to code that


        protected internal WorkType( )
        {
            EPC = String.Empty;
            SystemStatus = WorkInstructionSystemState.Incomplete;
        }


        public WorkType( string name )
            : base( name )
        {

        }


        public virtual ItemTemplate Item
        {
            get { return _itemTemplate; }
            set { _itemTemplate = value; }
        }

        //TODO: look at moving these types of associations out of the objects
        public virtual ItemSelectionCriteria ItemSelectionCriteria
        {
            get
            {
                return _itemSelectionCriteria;
            }
            set
            {
                _itemSelectionCriteria = value;
            }
        }


        public virtual DisplayFilter DisplayFilter
        {
            get
            {

                return displayFilter;
            }
            set
            {
                if ( displayFilter == value )
                    return;
                displayFilter = value;
            }
        }


        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual Location OriginLocation
        {
            get { return _itemTemplate != null ? _itemTemplate.CurrentLocation : null; }
            set { }
        }

        //just a place holder so it has the same props as workinstruction for the designer... not stored in db for
        public virtual string EPC { get; set; }

        //just a place holder so it has the same props as workinstruction for the designer... not stored in db for
        public virtual WorkInstructionSystemState SystemStatus { get; set; }

        //just a place holder so it has the same props as workinstruction for the designer... not stored in db for
        public virtual Guid TemplateId { get; set; }

        //just a place holder so it has the same props as workinstruction for the designer... not stored in db for
        public virtual Location DestinationLocation { get; set; }


        public override string ToString( )
        {
            return Name;
        }

        public virtual bool Equals( WorkType other )
        {
            return base.Equals( other );
        }
    }
}