using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orca.Domain.Objects.Constants;

namespace Orca.Domain.DTO
{
    public class WorkInstructionDto
    {
        Dictionary<string, string> dynamicPropertyValues = new Dictionary<string, string>( );

        public virtual Guid Id { get; set; }
        public virtual Guid DestinationLocation_id { get; set; }
        public virtual Guid OriginLocation_id { get; set; }
        public virtual Guid Item_id { get; set; }
        public virtual string ItemName { get; set; }

        public virtual string Name { get; set; }
        public virtual string DestinationLocationName { get; set; }


        public virtual string OriginLocationName { get; set; }
        public virtual WorkInstructionSystemState SystemStatus { get; set; }
        public virtual Guid TemplateId { get; set; }
        public virtual DateTime? CompleteDate { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual Dictionary<string, string> DynamicPropertyValues { get; set; }
    }

 
}