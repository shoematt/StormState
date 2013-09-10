using System;
using Orca.Core;
using Orca.Core.Domain;
using Orca.Core.Logging;
using Orca.Domain.Objects;

namespace Orca.Domain.DynamicProperties
{
    public class PropertyDefinitionStateObject
    {
        private static readonly ILog log = Logger.GetNamedLogger(LoggerNames.PropertyDefinitionStateObject);

        private DynamicPropertyDataTypes dataType;
        internal PropertyDefinition orignalDef;

        // private Type systemType;

        //internal bool isPublished;

        //internal int revision;

        //internal Guid id;

        //internal Guid userTypeID;

        //internal Guid staticInstanceID;


        /// <summary>
        ///   Initializes a new instance of the PropertyDefinitionStateObject class.
        /// </summary>
        public PropertyDefinitionStateObject()
        {
            SetEmptyDefaultValues();
        }

        /// <summary>
        ///   Initializes a new instance of the PropertyDefinitionStateObject class.
        /// </summary>
        /// <param name = "orignalDef"></param>
        public PropertyDefinitionStateObject(PropertyDefinition orignalDef)
        {
            this.orignalDef = orignalDef;

            SetValues();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public Type SystemType
        {
            get { return GetDataType(DataType); }
        }


        public object Value { get; set; }

        public int Revision
        {
            get
            {
                if (orignalDef == null)
                {
                    return 0;
                }
                return orignalDef.Revision;
            }
        }

        public Guid Id
        {
            get
            {
                if (orignalDef == null)
                {
                    return Guid.Empty;
                }
                return orignalDef.Id;
            }
        }

        //public Guid UserTypeID
        //{
        //    get
        //    {
        //        if ( orignalDef == null )
        //        {
        //            return Guid.Empty;
        //        }
        //        return orignalDef.UserTypeID;
        //    }
        //}

        public Guid StaticInstanceID
        {
            get
            {
                if (orignalDef == null)
                {
                    return Guid.Empty;
                }
                return orignalDef.StaticInstanceID;
            }
        }


        public bool IsPublished
        {
            get
            {
                if (orignalDef == null)
                {
                    return false;
                }
                return orignalDef.IsPublished;
            }
        }

        public DynamicPropertyDataTypes DataType
        {
            get { return dataType; }
            set
            {
                if (dataType == value)
                {
                    return;
                }

                dataType = value;

                if (Value == null) //protect against null.
                {
                    return;
                }

                UpdateValue(dataType);
            }
        }

        public PropertyDefinition OrignalDef
        {
            get { return orignalDef; }
            set
            {
                orignalDef = value;
                SetValues();
            }
        }

        private void SetEmptyDefaultValues()
        {
            Name = string.Empty;
            Description = string.Empty;
            SetDataType(typeof(string));
            Value = string.Empty;
        }

        private void SetValues()
        {
            if (orignalDef == null)
            {
                //should reset the values or let them carry over if they are creating multiple.

                //Name = string.Empty;
                //Description = string.Empty;
                //SetDataType(typeof(string));
                //this.Value = string.Empty;
            }
            else
            {
                Name = orignalDef.Name;

                Description = orignalDef.Description;

                Value = orignalDef.Value;

                SetDataType(orignalDef.DataType);
            }
        }

        private void SetDataType(Type objectType)
        {
            if (objectType == typeof(int))
            {
                dataType = DynamicPropertyDataTypes.Integer;
            }
            else if (objectType == typeof(string))
            {
                dataType = DynamicPropertyDataTypes.Text;
            }
            else if (objectType == typeof(double))
            {
                dataType = DynamicPropertyDataTypes.Number;
            }
            else if (objectType == typeof(decimal))
            {
                dataType = DynamicPropertyDataTypes.Number;
            }
            else if (objectType == typeof(bool))
            {
                dataType = DynamicPropertyDataTypes.Boolean;
            }
        }

        private Type GetDataType(DynamicPropertyDataTypes objectType)
        {
            switch (objectType)
            {
                case DynamicPropertyDataTypes.Text:
                    return typeof(string);

                case DynamicPropertyDataTypes.Integer:
                    return typeof(int);

                case DynamicPropertyDataTypes.Number:
                    return typeof(decimal);

                case DynamicPropertyDataTypes.Boolean:
                    return typeof(bool);
            }

            return typeof(string);
        }


        public bool IsNew()
        {
            return orignalDef == null;
        }

        public void Revert()
        {
            if (orignalDef != null)
            {
                OrignalDef = orignalDef;
            }
            else
            {
                SetEmptyDefaultValues();
            }
        }

        public PropertyDefinition ApplyChanges()
        {
            if (orignalDef == null)
            {
                throw new Exception("There is not a DynamicPropertyDefinition to Update");
            }

            //     orignalDef.Value = null;
            orignalDef.DataType = GetDataType(DataType);
            orignalDef.Value = Value;
            orignalDef.Name = Name;
            orignalDef.Description = Description;

            return orignalDef;
        }


        private void UpdateValue(DynamicPropertyDataTypes changeType)
        {
            object SavedValue = Value;

            try
            {
                switch (changeType)
                {
                    case DynamicPropertyDataTypes.Text:

                        try
                        {
                            Value = Convert.ToString(Value);
                        }
                        catch (Exception)
                        {
                            Value = string.Empty;
                        }
                        break;
                    case DynamicPropertyDataTypes.Integer:

                        try
                        {
                            Value = Convert.ToInt32(Value);
                        }
                        catch (Exception)
                        {
                            Value = 0;
                        }

                        break;
                    case DynamicPropertyDataTypes.Number:

                        try
                        {
                            Value = Convert.ToDecimal(Value);
                        }
                        catch (Exception)
                        {
                            Value = 0.0;
                        }

                        break;
                    case DynamicPropertyDataTypes.Boolean:
                        try
                        {
                            Value = Convert.ToBoolean(Value);
                        }
                        catch (Exception)
                        {
                            Value = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error("Exception in PropertyDefinitionStateObject.ChangeDataType ", ex);
                }
                //  this.Value = SavedValue; //reset the value.
            }
        }
    }
}