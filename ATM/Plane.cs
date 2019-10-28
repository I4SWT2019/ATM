using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Plane
    {
        public double _velocity { get; set; }
        public int _heading { get; set; }
        public int _longitude { get; set; }
        public int _latitude { get; set; }
        public int _altitude { get; set; }
        public string _tag { get; set; }
        public string _timestamp { get; set; }

        public Plane()
        {
        }

        public void setAll(Plane thisPlane, string tag, int latitude ,int longitude ,int altitude, string timestamp, double velocity, int heading)
        {
            thisPlane._tag = tag;
            thisPlane._latitude = latitude;
            thisPlane._longitude = longitude;
            thisPlane._altitude = altitude;
            thisPlane._timestamp = timestamp;
            thisPlane._velocity = velocity;
            thisPlane._heading = heading;
            
        }
        public void setAll(Plane thisPlane, string tag, int latitude, int longitude, int altitude, string timestamp)
        {
            thisPlane._tag = tag;
            thisPlane._latitude = latitude;
            thisPlane._longitude = longitude;
            thisPlane._altitude = altitude;
            thisPlane._timestamp = timestamp;
        }
    }
}
