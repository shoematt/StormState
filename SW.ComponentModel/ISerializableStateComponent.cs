using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Orca.Workflow.ComponentModel
{
    public interface ISerializableStateComponent : ISerializable, IXmlSerializable
    {
    }
}