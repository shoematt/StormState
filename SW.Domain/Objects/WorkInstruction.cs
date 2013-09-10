using System;
using Orca.Domain.Cache;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects.BaseObjects;
using Orca.Domain.Objects.Constants;

namespace Orca.Domain.Objects
{
    /// <summary>
    ///   Defines a type of work.
    ///   Work types are used to define how work instructions are created, displayed, and executed.
    /// </summary>
    [Serializable]
    public class WorkInstruction : DomainObjectWithPropertyValues
                                   , IWorkInstruction
                                   , ISupportPropertyValues
                                   , IHasTemplate
                                   , IEquatable<WorkInstruction>
                                    , ICacheable
    {

        public const string SystemStatusPropertyName = "SystemStatus";


        private Item _item;
        private Guid _wiContainerID;
        private Guid _workTypeId = Guid.Empty;
        private DateTime? _createDate = DateTime.Now;
        private WorkInstructionSystemState _systemStatus = WorkInstructionSystemState.Incomplete;


        public WorkInstruction( )
        {
        }

        public WorkInstruction( string name )
            : base( name )
        {
        }

        public virtual Guid WiContainerID
        {
            get
            {
                return _wiContainerID;
            }
            set
            {
                _wiContainerID = value;
            }
        }

        public virtual DateTime? CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }




        public virtual void Complete( bool isCompleted )
        {
            if ( isCompleted )
            {
                _systemStatus = WorkInstructionSystemState.Complete;
                CompleteDate = DateTime.Now;
            }
            else
            {
                _systemStatus = WorkInstructionSystemState.Incomplete;
            }
        }




        public virtual int TruckID { get; set; }

        public virtual WorkInstructionState State { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? CompleteDate { get; set; }

        public virtual WorkInstructionSystemState SystemStatus
        {
            get
            {
                return _systemStatus;
            }
            set
            {
                _systemStatus = value;
            }
        }


        public virtual Item Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                SetPropertiesFromItem( _item );
            }
        }

        private void SetPropertiesFromItem( Objects.Item _item )
        {
            if(Item != null)
            {
                if(Item.CurrentLocation != null)
                {
                    OriginLocation_Id = Item.CurrentLocation.Id;
                    OriginLocation_Name = Item.CurrentLocation.Name;
                    EPC = Item.Epc;
                    return;
                }
            }
            OriginLocation_Id = Guid.Empty;
            OriginLocation_Name = string.Empty;
            EPC = string.Empty;
           

        }


        public virtual Guid OriginLocation_Id { get; set; }
        public virtual string OriginLocation_Name { get; set; }
        public virtual Guid DestinationLocation_Id { get; set; }
        public virtual string DestinationLocationName { get; set; }
        public virtual string EPC { get; set; }
        


        private Location _destinationLocation;
        public virtual Location DestinationLocation
        {
            get
            {
                return _destinationLocation;
            }
            set
            {
                _destinationLocation = value;


                if ( _destinationLocation != null )
                {
                    DestinationLocation_Id = _destinationLocation.Id;
                    DestinationLocationName = _destinationLocation.Name;
                }
                else
                {
                    DestinationLocation_Id = Guid.Empty;
                    DestinationLocationName = string.Empty;
                }
            }
        }
    




        public virtual Location OriginLocation
        {
            get
            {
                return _item != null ? _item.CurrentLocation : null;
            }
            set
            {
            }
        }







        public virtual bool Equals( WorkInstruction other )
        {
            return base.Equals( other );
        }

        //public virtual Guid CacheId
        //{
        //    get { return Id; }
        //}
        }
}