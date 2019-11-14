using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;

namespace ATM.Printers
{
    public class SeparationPrinter : IPrinter
    {
        public void Print(List<Plane> planes)
        {
            if (planes.Count > 0)
            {
                Console.WriteLine("!!! Separation Warning !!!");
                Console.WriteLine($"Plane {planes[0]._tag} and {planes[1]._tag} are too close");
            }
        }
    }
}
