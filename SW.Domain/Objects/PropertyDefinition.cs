using System;
using Orca.Core.Domain;
using Orca.Domain.Interfaces;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class PropertyDefinition : PropertyBase, IPropertyDefinition, IEquatable<PropertyDefinition>
    {


        protected internal PropertyDefinition()
        {
        }


        public PropertyDefinition(string name, object DefaultValue)
            : base(name, DefaultValue)
        {
        }

        public PropertyDefinition(DynamicPropertyDataTypes DataType, string name)
            : base(DataType, name)
        {

        }

        internal PropertyDefinition(Type DataType, string name)
            : base(DataType, name)
        {
        }

        public virtual bool Equals(PropertyDefinition other)
        {
            return base.Equals(other);
        }

        public virtual DefaultPropertyValue CreateDefaultPropertyValue()
        {
            var dynamicPropValueObject = new DefaultPropertyValue(this);

            return dynamicPropValueObject;
        }



            }
}