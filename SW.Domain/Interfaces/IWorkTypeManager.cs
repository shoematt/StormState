using System.Collections.Generic;
using Orca.Core.Persistence;
using Orca.Domain.Objects;

namespace Orca.Domain.Interfaces
{
    public interface IWorkTypeManager
    {
        ItemTemplate SystemWideItemTemplate { get; }
        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset( );
        /// <summary>
        /// Creates the type of the work.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        WorkType CreateWorkType( string Name );
        /// <summary>
        /// Works the type name avaliable.
        /// </summary>
        /// <param name="WorkTypeName">Name of the work type.</param>
        /// <returns></returns>
        bool WorkTypeNameAvaliable( string WorkTypeName );
        /// <summary>
        /// Gets the type of the editable work.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        WorkType GetEditableWorkType( string name );
        /// <summary>
        /// Gets the type of the published work.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        WorkType GetPublishedWorkType( string name );
        /// <summary>
        /// Gets the published work types.
        /// </summary>
        /// <returns></returns>
        IList<WorkType> GetPublishedWorkTypes( );
        /// <summary>
        /// Gets the non published work types.
        /// </summary>
        /// <returns></returns>
        IList<WorkType> GetNonPublishedWorkTypes( );
        /// <summary>
        /// Determines whether this instance can publish the specified work type.
        /// </summary>
        /// <param name="workType">Type of the work.</param>
        /// <returns></returns>
        OperationReport CanPublish( WorkType workType );
        /// <summary>
        /// Publishes the type of the work.
        /// </summary>
        /// <param name="workType">Type of the work.</param>
        /// <returns></returns>
        OperationReport PublishWorkType( WorkType workType );
        /// <summary>
        /// Adds the property definition.
        /// </summary>
        /// <param name="workType">Type of the work.</param>
        /// <param name="propDef">The prop def.</param>
        /// <returns></returns>
        DefaultPropertyValue AddPropertyDefinition( WorkType workType, PropertyDefinition propDef, OperationReport newOperationReport );
        OperationReport RemoveDefaultPropertyValue( WorkType workType, DefaultPropertyValue propDefValue );
        OperationReport UpdatePropertyDefinition( WorkType workType, DefaultPropertyValue propDefValue );
        /// <summary>
        /// Saves the specified work type.
        /// </summary>
        /// <param name="workType">Type of the work.</param>
        /// <returns></returns>
        OperationReport Save( WorkType workType );
        /// <summary>
        /// Deletes the specified work type.
        /// </summary>
        /// <param name="workType">Type of the work.</param>
        /// <returns></returns>
        OperationReport Delete( WorkType workType );
        void AddItemSelectionCriterion( WorkType workType, DefaultPropertyValue workTypeProperty, DefaultPropertyValue itemProperty );
        void AddFilterProperty( WorkType workType, DefaultPropertyValue workTypeProperty );
    }
}
