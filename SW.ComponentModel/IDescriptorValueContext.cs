using System;
using System.ComponentModel;

namespace Orca.Workflow.ComponentModel
{
    /// <summary>
    /// Describes an instance member and provides access to its values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDescriptorValueContext<out T> where T : MemberDescriptor
    {
        /// <summary>
        ///   Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        string Category { get; }

        /// <summary>
        ///   Gets or sets the type of the component.
        /// </summary>
        /// <value>The type of the component.</value>
        Type ComponentType { get; }

        /// <summary>
        ///   Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        string DisplayName { get; }

        /// <summary>
        ///   Gets or sets the descriptor.
        /// </summary>
        /// <value>The descriptor.</value>
        T Descriptor { get; }

        /// <summary>
        ///   Gets the value.
        /// </summary>
        /// <returns></returns>
        object GetValue( );
    }
}