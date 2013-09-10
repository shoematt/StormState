using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orca.Domain.Objects;
using System.Runtime.Serialization;

namespace Orca.Domain.Commands
{

    [Serializable]
    public class AddItemCommand : BasePropertyValueCacheCommand<Item>
    {

        public AddItemCommand(Item objectValue)
            : base(objectValue)
        {

        }
        public AddItemCommand(List<Item> objects)
            : base(objects)
        {

        }
        public AddItemCommand()
        {

        }

    }
}
