using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Printers
{
   public  class AreaMonitorPrinter
    {
        public void Print(List<Plane> planes)
        {
            Console.WriteLine("-----Planes in Monitored Area-----");
            foreach (Plane p in planes)
            {
                Console.WriteLine($"Plane with tag:\t {p._tag}");
                Console.WriteLine($"Last reading:\t {p._timestamp}");
                Console.WriteLine($"Coordinates:\t N° {p._latitude} \tE° {p._longitude}");
                Console.WriteLine($"Altitude:\t {p._altitude}m");
                Console.WriteLine($"Heading:\t {p._heading}°");
                Console.WriteLine($"Velocity:\t {p._velocity:0.0}m/s");
            }
        }
    }
}
