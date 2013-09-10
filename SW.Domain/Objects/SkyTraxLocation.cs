using System;
using Orca.Domain.Objects.BaseObjects;

namespace Orca.Domain.Objects
{
    [Serializable]
    public class SkyTraxLocation : PublishableDomainObject
    {

        int skyTraxID;
        double x1;
        double x2;
        double y1;
        double y2;
        double z;
        double height;
        double azimuth;
        bool active;
      

        /// <summary>
        /// Initializes a new instance of the SkyTraxLocationDTO class.
        /// </summary>
        public SkyTraxLocation(string name)
            : base(name)
        {

        }

        /// <summary>
        /// Initializes a new instance of the SkyTraxLocation class.
        /// </summary>
        public SkyTraxLocation()
        {

        }



        public virtual int SkyTraxID
        {
            get
            {
                return skyTraxID;
            }
            set
            {
                if (skyTraxID == value)
                    return;
                skyTraxID = value;
            }
        }
        public virtual double X1
        {
            get
            {
                return x1;
            }
            set
            {
                if (x1 == value)
                    return;
                x1 = value;
            }
        }
        public virtual double X2
        {
            get
            {
                return x2;
            }
            set
            {
                if (x2 == value)
                    return;
                x2 = value;
            }
        }
        public virtual double Y1
        {
            get
            {
                return y1;
            }
            set
            {
                if (y1 == value)
                    return;
                y1 = value;
            }
        }
        public virtual double Y2
        {
            get
            {
                return y2;
            }
            set
            {
                if (y2 == value)
                    return;
                y2 = value;
            }
        }
        public virtual double Z
        {
            get
            {
                return z;
            }
            set
            {
                if (z == value)
                    return;
                z = value;
            }
        }
        public virtual double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height == value)
                    return;
                height = value;
            }
        }
        public virtual double Azimuth
        {
            get
            {
                return azimuth;
            }
            set
            {
                if (azimuth == value)
                    return;
                azimuth = value;
            }
        }
        public virtual bool Active
        {
            get
            {
                return active;
            }
            set
            {
                if (active == value)
                    return;
                active = value;
            }
        }


    }
}
