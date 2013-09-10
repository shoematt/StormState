using System;

namespace Orca.Domain.Commands
{
    /// <summary>
    ///   Indicates that changing this property needs some kind of validation or more logic then just changing the value.
    /// </summary>
    [ AttributeUsage( AttributeTargets.Property ) ]
    public class DomainObjectPropertyRequiresValidationAttribute : Attribute
    {
    }
}