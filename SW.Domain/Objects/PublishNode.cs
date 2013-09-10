using System;
using System.Security.Principal;
using Orca.Core.Domain;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    public class PublishNode : PublishableDomainObject, IPublishableDomainObject, IDomainObject
    {
        private readonly WindowsIdentity _currentIdentity = WindowsIdentity.GetCurrent( );
        private Guid domainObjectID = Guid.Empty;
        private Guid domainObjectInstanceID = Guid.Empty;

        private Type domainObjectType;
        private Guid prevPublishedDomainObjectID = Guid.Empty;
        private string publishedBy = string.Empty;

        /// <summary>
        ///   Initializes a new instance of the PublishNode class.
        /// </summary>
        protected internal PublishNode( )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the PublishNode class.
        /// </summary>
        public PublishNode( PublishableDomainObject ToPublishObject, PublishableDomainObject ToReplaceObject )
        {
            domainObjectType = ToPublishObject.GetType( );

            if ( ToReplaceObject != null )
            {
                if ( domainObjectType != ToReplaceObject.GetType( ) )
                {
                    throw new ArgumentException( string.Format( "The type of publish object {0} does not match the ReplaceObject {1}", domainObjectType, ToReplaceObject.GetType( ) ) );
                }

                PrevPublishedDomainObjectID = ToReplaceObject.Id;
                ToReplaceObject.IsPublished = false;
            }

            DatePublished = DateTime.Now;

            DomainObjectInstanceID = ToPublishObject.StaticInstanceID;

            DomainObjectID = ToPublishObject.Id;

            IsPublished = true;

            PublishedBy = _currentIdentity.Name;
        }


        public virtual DateTime DatePublished { get; protected internal set; }

        public virtual string PublishedBy
        {
            get { return publishedBy; }
            protected internal set { publishedBy = value; }
        }

        public virtual Guid DomainObjectInstanceID
        {
            get { return domainObjectInstanceID; }
            protected internal set { domainObjectInstanceID = value; }
        }

        public virtual Guid DomainObjectID
        {
            get { return domainObjectID; }
            protected internal set { domainObjectID = value; }
        }

        public virtual Guid PrevPublishedDomainObjectID
        {
            get { return prevPublishedDomainObjectID; }
            protected internal set { prevPublishedDomainObjectID = value; }
        }

        public virtual Type DomainObjectType
        {
            get { return domainObjectType; }
            protected internal set { domainObjectType = value; }
        }
    }
}