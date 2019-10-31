using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class PlaneAddedEventArgs : EventArgs
    {
        public Plane Plane { get; set; }
    }
}
