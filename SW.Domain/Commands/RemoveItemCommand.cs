using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orca.Domain.Objects;
using System.Runtime.Serialization;

namespace Orca.Domain.Commands
{
    [Serializable]
    public class RemoveItemCommand : BasePropertyValueCacheCommand<Item>
    {

        public RemoveItemCommand(Item objectValue)
            : base(objectValue)
        {

        }
        public RemoveItemCommand(List<Item> objects)
            : base(objects)
        {

        }
        public RemoveItemCommand()
        {

        }


    }
}
