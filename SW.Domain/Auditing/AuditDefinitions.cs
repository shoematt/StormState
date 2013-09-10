using System;

namespace Orca.Domain.Auditing
{
    public class AuditDefinitions : IDisposable
    {
        public static AuditingDefinition SQL_Statement = new AuditingDefinition { StaticInstanceID = new Guid( "{A16A10F3-252A-489E-8859-E0A9B4D46755}" ),
                                                                 Name = "SQL",
            Description = "SQL Audit" };

        public static AuditingDefinition Save = new AuditingDefinition { StaticInstanceID = new Guid( "{47782736-9B6D-494A-B885-89F04DC20AB5}" ),
                                                        Name = "Save",
            Description = "Entity Audit" };

        public static AuditingDefinition Update = new AuditingDefinition { StaticInstanceID = new Guid( "{2A9D8E0C-2E64-4C84-8B0A-B62C7B8A3395}" ),
                                                          Name = "Update",
            Description = "Entity Audit" };

        public static AuditingDefinition Delete = new AuditingDefinition { StaticInstanceID = new Guid( "{970749AE-8CF3-4564-9453-C0E9817A6868}" ),
            Name = "Delete",
            Description = "Entity Audit" };
        public void Dispose( )
                                                      {
            Dispose( true );
            GC.SuppressFinalize( this );
    }
        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                if ( SQL_Statement != null )
                {
                    SQL_Statement.Dispose( );
                    SQL_Statement = null;
                }
                if ( Save != null )
                {
                    Save.Dispose( );
                    Save = null;
                }
                if ( Update != null )
                {
                    Update.Dispose( );
                    Update = null;
                }
                if ( Delete != null )
                {
                    Delete.Dispose( );
                    Delete = null;
                }
            }
        }
        ~AuditDefinitions( )
        {
            Dispose( false );
        }
    }
}