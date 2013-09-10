using System.ComponentModel;

namespace Orca.Workflow.ComponentModel
{
    public interface IExpressionContext
    {
        /// <summary>
        ///   Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        object Container { get; set; }

        /// <summary>
        ///   Gets or sets the object instance.
        /// </summary>
        /// <value>The object instance.</value>
        object ObjectInstance { get; set; }

        /// <summary>
        ///   Gets or sets the descriptor.
        /// </summary>
        /// <value>The descriptor.</value>
        PropertyDescriptor Descriptor { get; set; }
    }
}