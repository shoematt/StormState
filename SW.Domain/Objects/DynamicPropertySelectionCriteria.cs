using System;
using System.Collections.Generic;
using System.Linq;
using Orca.Core.Domain;
using Orca.Domain.Cache;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{

    [Serializable]
    public class ItemSelectionCriteria : DomainObject, IDomainObject
    {
        private IList<DynamicPropertyCriterion> _selectionCriterion = new List<DynamicPropertyCriterion>( );

   //     private IList<WorkType> workTypes = new List<WorkType>( );

        protected internal ItemSelectionCriteria( )
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkTypeItemSelectionCriteria"></see> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ItemSelectionCriteria( string name )
            : base( name )
        {

        }


        public virtual IList<DynamicPropertyCriterion> SelectionCriterion
        {
            get
            {
                return _selectionCriterion;
            }
            protected internal set
            {
                _selectionCriterion = value;
            }
        }

        //public IList<WorkType> WorkTypes
        //{
        //    get
        //    {
        //        return workTypes;
        //    }
        //    set
        //    {
        //        workTypes = value;
        //    }
        //}

        public virtual void AddDynamicPropertyCriterion( DynamicPropertyCriterion Criteria )
        {
            if ( !HasDynamicPropertyCriterion( Criteria ) )
            {
                _selectionCriterion.Add( Criteria );
            }
        }

        public virtual void RemoveDynamicPropertyCriterion( DynamicPropertyCriterion Criteria )
        {
            if ( HasDynamicPropertyCriterion( Criteria ) )
            {
                _selectionCriterion.Remove( Criteria );
            }
        }


        public virtual bool HasDynamicPropertyCriterion( DynamicPropertyCriterion Criteria )
        {
            var item = _selectionCriterion.Where( x => x.PropertyNameProvider.StaticInstanceID == Criteria.PropertyNameProvider.StaticInstanceID ).FirstOrDefault( );

            return item != null;
        }


        public virtual bool UsesWorkTypeProperty( DefaultPropertyValue Criteria )
        {
            var item = _selectionCriterion.Where( x => x.PropertyValueProvider.StaticInstanceID == Criteria.StaticInstanceID ).FirstOrDefault( );

            return item != null;
        }


        public virtual bool UsesItemProperty( DefaultPropertyValue Criteria )
        {
            var item = _selectionCriterion.Where( x => x.PropertyNameProvider.StaticInstanceID == Criteria.StaticInstanceID ).FirstOrDefault( );

            return item != null;
        }



        public virtual void RemoveByWorkTypeProperty( DefaultPropertyValue workTypePropertyValue )
        {
            if ( UsesWorkTypeProperty( workTypePropertyValue ) )
            {
                var item = _selectionCriterion.Where( x => x.PropertyValueProvider.StaticInstanceID == workTypePropertyValue.StaticInstanceID ).FirstOrDefault( );

                if ( item == null ) { return; }

                RemoveDynamicPropertyCriterion( item );
            }
        }


        public PropertyValueSearchCriteria ToPropertyValueSearchCriteria( )
        {
            PropertyValueSearchCriteria result = new PropertyValueSearchCriteria( );

            foreach ( DynamicPropertyCriterion info in _selectionCriterion )
            {
                PropertyValueSearchCriterion searchValue = new PropertyValueSearchCriterion( info.PropertyNameProvider.Name, string.Empty );
                result.Add( searchValue );
    }

            return result;
}
    }
}
