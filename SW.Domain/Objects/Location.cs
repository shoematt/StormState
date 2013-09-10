using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Orca.Core.Domain;
using Orca.Domain.Interfaces;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{

    public enum Restrictions
    {
        NotRestricted = 0,
        Restricted = 1
    }

    /// <summary>
    /// Represents an Orca Location object which can contain dynamic properties for properies that exist in the Master Location System. (ie OAT, SAP)
    /// </summary>
    /// 
    [Serializable]
    public class Location : DomainObjectWithPropertyValues
        // , IComparable
        , IEquatable<Location>
        , ISupportPropertyValues
        , IDomainObject
        //, INotifyPropertyChanged
        , IRaiseItemChangedEvents
    {


        //Keep the categories the way they are, but create a custom mapper to persist the data.

        Restrictions restriction = Restrictions.NotRestricted;

        //     SkyTraxLocation skyTraxLocation = null;

        Dictionary<Guid, LocationCategory> _categories = new Dictionary<Guid, LocationCategory>( );

        Dictionary<Guid, Location> _childLocations = new Dictionary<Guid, Location>( );

        private string sgln;



        /// <summary>
        /// Initializes a new instance of the DynamicPropertyDomainObject class.
        /// </summary>
        protected internal Location( )
        {

        }





        public Location( string Name )
            : base( Name )
        {

        }




        public virtual string Sgln
        {
            get
            {
                return sgln;
            }
            set
            {

                sgln = value;
            }
        }

        public virtual Restrictions Restriction
        {
            get
            {
                return restriction;
            }
            set
            {
                if ( restriction == value )
                    return;
                restriction = value;
            }
        }


        //public virtual SkyTraxLocation SkyTraxLocation
        //{
        //    get
        //    {
        //        return skyTraxLocation;
        //    }
        //    set
        //    {
        //        if ( skyTraxLocation == value )
        //            return;
        //        skyTraxLocation = value;
        //        // skyTraxLocation.Location = this;
        //    }
        //}


        // public virtual LocationCategory Category { get; set; }

        //  public virtual string CategoryName
        // {
        //      get { return Category.Name ?? "Unknown"; }
        //      set { }
        //   }



        /// <summary>
        /// Checks to see if the location is in the category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public virtual bool IsCategory( LocationCategory category )
        {
            return _categories.ContainsKey( category.Id );
        }


        public virtual IList<LocationCategory> Categories
        {
            get
            {
                lock ( _categories )
                {
                    return _categories.Values.ToList( );
                }
            }
            protected internal set
            {
                lock ( _categories )
                {
                    _categories.Clear( );

                    foreach ( LocationCategory item in value )
                    {

                        try
                        {
                            // shoe: was being added more than once in trigger sim when loaded for some reason
                            if ( _categories.ContainsKey( item.Id ) ) _categories.Remove( item.Id );

                            _categories.Add( item.Id, item );
                        }
                        catch ( Exception )
                        {
                        }
                    }
                }
            }
        }


        public virtual void AddCategory( LocationCategory category )
        {
            lock ( _categories )
            {
                if ( !_categories.ContainsKey( category.Id ) )
                {
                    _categories.Add( category.Id, category );
                }
            }
            //onchange event;
        }


        public virtual void RemoveCategory( LocationCategory category )
        {
            lock ( _categories )
            {
                if ( _categories.ContainsKey( category.Id ) )
                {
                    _categories.Remove( category.Id );
                }
            }
            //onchange event;
        }

        /// <summary>
        /// Method only to be called by some sort of manager that has validated the location is not in the parent chain.
        /// 
        /// </summary>
        /// <param name="location"> is expected to have a valid id</param>
        public virtual void AddChildLocation( Location location )
        {
            if ( _childLocations.ContainsKey( location.Id ) )
            {
                return;
            }
            //   location.ParentID = this.Id;
            lock ( _childLocations )
            {
                _childLocations.Add( location.Id, location );
            }


        }

        public virtual void RemoveChildLocation( Location location )
        {
            if ( !_childLocations.ContainsKey( location.Id ) )
            {
                return;
            }

            lock ( _childLocations )
            {
                _childLocations.Remove( location.Id );
            }

        }


        public virtual IList<Location> ChildLocations
        {
            get
            {
                lock ( _childLocations )
                {
                    return _childLocations.Values.ToList( );
                }
            }

            protected internal set
            {
                lock ( _childLocations )
                {
                    _childLocations.Clear( );

                    foreach ( Location item in value )
                    {
                        _childLocations.Add( item.Id, item );
                    }
                }
            }

        }



        public virtual bool Equals( Location other )
        {
            return base.Equals( other );
        }




        public virtual int CompareTo( object obj )
        {
            Location tmp = obj as Location;
            if ( tmp == null )
            {
                return -1;
            }
            return this.Id.CompareTo( tmp.Id );
        }

        public virtual bool RaisesItemChangedEvents
        {
            get { return true; }
        }



    }







    // public interface IExternalLocationAssociation
    // {
    //     object ExternalId { get; set; }
    // }


    // [Serializable]
    // public class ExternalLocationAssociation : DomainObjectWithDefaultProperties
    //     , IExternalLocationAssociation
    //     , ISupportDefaultPropertyValues
    // {
    //     object externalId;

    //     DynamicPropertyManager propertyManager;

    //     bool defaultPropertiesCreated;


    //     /// <summary>
    //     /// Initializes a new instance of the ExternalLocationAssociationDTO class.
    //     /// </summary>
    //     public ExternalLocationAssociation(DynamicPropertyManager PropertyManager)
    //     {
    //         propertyManager = PropertyManager;

    //     }

    //     public virtual void InitDefaultProperties()
    //     {
    //         if (defaultPropertiesCreated)
    //         {
    //             return;
    //         }
    //     }

    //     protected virtual void DefinePropertyDefinitions()
    //     {
    //         propertyManager.GetAllDynamicPropertyDefintions()
    //     }

    //


    //     public object ExternalId
    //     {
    //         get
    //         {
    //             return externalId;
    //         }
    //         set
    //         {
    //             if (externalId == value)
    //                 return;
    //             externalId = value;
    //         }
    //     }



    // }

    // [Serializable]
    // public class SkyTraxLocationAssociation : ExternalLocationAssociation
    //     , IExternalLocationAssociation
    //     , ISupportDefaultPropertyValues
    // {

    //  //skytrax property names
    //     //int id
    //     //string name
    //     //double x1
    //     //double x2
    //     //double y1
    //     //double y2
    //     //double z
    //     //double height
    //     //double azimuth
    //     //boo active

    //     Guid id = new Guid("{25C74552-57B0-4729-8055-5A7E0A07EFE1}");
    //     /// <summary>
    //     /// Initializes a new instance of the SkyTraxLocationAssociationDTO class.
    //     /// </summary>
    //     public SkyTraxLocationAssociation(DynamicPropertyManager PropertyManager)
    //         : base(PropertyManager)
    //     {
    //         
    //     }
    //                                                                  




    // }


}
