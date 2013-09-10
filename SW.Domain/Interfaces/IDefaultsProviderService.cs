using Orca.Domain.Objects;

namespace Orca.Domain.Interfaces
{
    public interface IDefaultsProviderService
    {
        ItemTemplate SystemItemTemplate { get; }
        Location DefaultStorageLocation { get; }
        Item DefaultSystemItem { get; }
    }
}
