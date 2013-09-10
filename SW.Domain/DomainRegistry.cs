using System.Security.Principal;
using Orca.Domain.DynamicProperties;
using StructureMap.Configuration.DSL;

namespace Orca.Domain
{
    public class DomainRegistry : Registry
    {
        /// <summary>
        ///   Initializes a new instance of the DomainRegistry class.
        /// </summary>
        public DomainRegistry( )
        {
            For<DynamicPropertyManager>( ).Singleton( ).Use<DynamicPropertyManager>( );

            For<WindowsIdentity>( ).Singleton( ).Use( WindowsIdentity.GetCurrent( ) );

            For<PropertyValueActivator>( ).Singleton( ).Use<PropertyValueActivator>( );

        }
    }
}