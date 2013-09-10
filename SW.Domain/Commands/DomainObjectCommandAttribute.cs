using System;

namespace Orca.Domain.Commands
{
    [ AttributeUsage( AttributeTargets.Class ) ]
    public class DomainObjectCommandAttribute : Attribute
    {
    }
}