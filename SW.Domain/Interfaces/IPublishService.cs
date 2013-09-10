using System;
using Orca.Core.Persistence;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Interfaces
{
    //public interface ISupportPublish
    //{
    //    bool IsPublished { get; }

    //    void SetPublishValue( bool value );
    //}


    public interface IPublishService
    {
        OperationReport Publish<T>(T domainObject)
                where T : PublishableDomainObject;
        OperationReport CanPublish(PublishableDomainObject domainObject);
        T GetCurrentPublishedObject<T>(T objectToPublish)
                where T : PublishableDomainObject;
        T GetCurrentPublishedObject<T>(Guid staticInstanceID)
                where T : PublishableDomainObject;
    }
}
