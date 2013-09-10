using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;
using System.Security.Principal;
using Orca.Domain.DynamicProperties;
using AutoMapper;
using Orca.Domain.Objects;



namespace Orca.Domain.DTO
{
    public class DTOMappingRegistry : Registry
    {
        public DTOMappingRegistry( )
        {
           //Mapper.CreateMap<PropertyValue, KeyValuePair<string, string>>( )
           //    .ConstructUsing(x => new KeyValuePair<string, string>(x.Name, x.StringValue));;
           //    

           // Mapper.CreateMap<WorkInstruction,WorkInstructionDto>()

          //  AutoMapper.Mapper.CreateMap<IList<PropertyValue>, KeyValuePair<string, string>>( )
		        //
    
        }

    }
}
