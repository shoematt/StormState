using System;

namespace Orca.Domain
{
    public class SystemDefaultObjectIDs
    {
        public static readonly Guid DefaultItemTemplateID = new Guid( "{5DAA8A7C-8A89-4C02-B508-25FD55818D35}" );
        public static readonly Guid DefaultLocationID = new Guid( "{48EFBE72-058F-4324-A3EB-F44A8B7592EB}" );
        public static readonly Guid DefaultItemID = new Guid( "{9766DA31-7AD7-4C4D-8158-F53ACD33FE7F}" );

        public static readonly Guid DefaultFlowLoadID = new Guid( "{E62D6871-989A-4050-85B8-A22DDFDACF3F}" );
        public static readonly Guid DefaultFlowUnloadID = new Guid( "{87E9F0EE-FCDD-4E22-BA24-07885699DCD2}" );
        public static readonly Guid DefaultFLowLocationChangeID = new Guid( "{A73D8FA7-185F-4953-BBD7-6D1A45FAC444}" );

        #region System Variables 

        public static readonly Guid CurrentWorkInstructionSysVarID = new Guid( "{361a469d-429f-4ee8-ab2f-9e5300fa868c}" );
        public static readonly Guid CurrentWorkTypeSysVarID = new Guid( "{12ea31fb-6126-4cdb-afd5-9e5300fa8690}" );
        public static readonly Guid PreviousWorkInstructionSysVarID = new Guid( "{350791dc-d36f-49c9-974c-9e5300fa869a}" );
        public static readonly Guid PreviousWorkTypeSysVarID = new Guid( "{058bf3d2-b08c-46bf-9890-9e5300fa869f}" );
        public static readonly Guid WorkInstructionResultSetSysVarID = new Guid( "{476c62cd-12bc-4619-94bc-9e5300fa86a3}" );
        public static readonly Guid ActiveWorkInstructionsSysVarID = new Guid( "{ef1fbdf1-2109-4714-a07d-9e5300fa86a8}" );
        public static readonly Guid UnfilteredWorkInstructionResultSetSysVarID = new Guid( "{860b0e95-2af5-4918-b254-9e5300fa86a8}" );
        public static readonly Guid LoadEventSysVarID = new Guid( "{41df43f6-fa91-4f6d-aff3-9e5300fa88d6}" );
        public static readonly Guid UnloadEventSysVarID = new Guid( "{a390d463-2443-4c35-ab99-9e5300fa88da}" );
        public static readonly Guid LocationChangeEventSysVarID = new Guid( "{ac092d86-dd51-4c96-8047-9e5300fa88df}" );
        public static readonly Guid FilterEventSysVarID = new Guid( "{92e31dba-0433-40db-8b34-9e5300fa88df}" );

        #region Selection Criteria Variables

        /// <summary>
        ///   The selection criteria uses the current epc to set the value of the epc to use in the executed selection criteria flow.
        /// </summary>
        public static readonly Guid CurrentEPCSysVarID = new Guid( "{FA15C588-E9C9-4149-8048-E0B5B389178B}" );

        /// <summary>
        ///   Used to store the return result of a 'returminator' that has been set as part of a selection criteria flow.
        /// </summary>
        public static readonly Guid LastResultSysVarID = new Guid( "{F2EFA526-40BB-47f6-92D0-448204BF28E7}" );

        /// <summary>
        ///   set of work instructions that match the 'CurrentEPCValue' coming from an event trigger.
        /// </summary>
        public static readonly Guid MatchingWorkInstructionsSetSysVarID = new Guid( "{20ECD045-5F60-4c75-AACE-BA00FB6A96E3}" );

        #endregion

        #endregion
    }
}