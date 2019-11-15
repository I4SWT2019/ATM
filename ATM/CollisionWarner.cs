using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;

namespace ATM
{
    public class CollisionWarner : Interfacess.IObserver
    {
        private IPrinter _separationPrinter;

        public CollisionWarner(IPrinter separationPrinter)
        {
            _separationPrinter = separationPrinter;
        }

        public void Update(List<Plane> planes)
        {
            WarningCheck(planes);
        }

        public void Print(List<Plane> planes)
        { 
            _separationPrinter.Print(planes);
        }

        // Time complexity: O(n^2)
        // Where n = size of planes
        // Planes must be sorted by altitude
        public void WarningCheck(List<Plane> planes)
        {
            if(planes != null)
            { 
                planes.Sort((x,y) => x._altitude.CompareTo(y._altitude));
                double deltaX, deltaY, deltaZ, distance;

                // Runs through List of planes with index i, and compare to planes with index j
                for (int i = 0; i < planes.Count; i++)
                {
                    int j = i + 1;
                    while (true)
                    {
                        if (j == planes.Count) 
                            break;
                        deltaZ = Math.Abs(planes[j]._altitude - planes[i]._altitude);
                        if (deltaZ >= 300) 
                            break;
                        deltaX = Math.Pow(planes[j]._longitude - planes[i]._longitude, 2);
                        deltaY = Math.Pow(planes[j]._latitude - planes[i]._latitude, 2);
                        distance = Math.Sqrt(deltaX + deltaY);

                        // Go to next index i if distance is acceptable
                        // AND index i's and j+1's altitude is different
                        if ((planes.Count < j + 1) && 
                            (distance >= 5000) && planes[j + 1]._altitude != planes[i]._altitude)
                            break;
                        else
                        {
                            List<Plane> collidingPlanes = new List<Plane>() {planes[i], planes[j]};
                            Print(collidingPlanes);
                        }
                        ++j;
                    }
                }
            }
        }
    }
}
