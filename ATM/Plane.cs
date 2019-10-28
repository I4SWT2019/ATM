using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Plane
    {
        public int _velocity { get; set; }
        public int _heading { get; set; }
        public int _longitude { get; set; }
        public int _latitude { get; set; }
        public int _altitude { get; set; }
        public string _tag { get; set; }
        public string _timestamp { get; set; }

        public Plane()
        {
        }
    }
}
