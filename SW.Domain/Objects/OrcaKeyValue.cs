using System;

namespace Orca.Domain.Objects
{
    /// <summary>
    /// Represents the properties from the external systems the translater is integrating with.
    /// </summary>
    /// 
    [Serializable]
    public class OrcaKeyValue
    {
        public virtual Guid Id { get; set; }

        public virtual string KeyName { get; set; }

        public virtual string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the OrcaKeyValue class.
        /// </summary>
        public OrcaKeyValue()
        {

        }

        /// <summary>
        /// Initializes a new instance of the OrcaKeyValue class.
        /// </summary>
        /// <param name="key"></param>
        public OrcaKeyValue(string key)
        {
            this.KeyName = key;
        }

        /// <summary>
        /// Initializes a new instance of the OrcaKeyValue class.
        /// </summary>
        public OrcaKeyValue(string key, string value)
            : this(key)
        {
            this.Value = value;
        }

    }
}
