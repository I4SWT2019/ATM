using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;

namespace ATM
{
    public class ATM : ISubscriber
    {
        private List<Plane> _planes;
        private List<Plane> _previousPlanes;
        private ICalculator _velocityCalc;
        private ICalculator _headingCalc;
        private IPrinter _areaPrinter;

        public ATM(IPrinter areaPrinter, ICalculator velocityCalc, ICalculator headingCalc)
        {
            _areaPrinter = areaPrinter;
            _velocityCalc = velocityCalc;
            _headingCalc = headingCalc;
        }
        public void Update(List<Plane> planes)
        {
            _previousPlanes = _planes;
            _planes = planes;
        }

        public void Run()
        {
        }

        public void Print()
        {
            _areaPrinter.Print(_planes);
        }

        // Time complexity: O(n1*n2)
        // Where n1 = size of _planes
        // and n2 = size of _previousPlanes
        public void CalcVelocity()
        {
            int[] previous = new int[3];
            int[] current = new int[3];

            foreach (Plane p in _planes)
            {
                // Finding and saving previous readings from tag of _planes
                Plane tempPlane;
                tempPlane = _previousPlanes.Find(prevPlane => prevPlane._tag == p._tag);
                previous[0] = tempPlane._longitude;
                previous[1] = tempPlane._latitude;
                previous[2] = Int32.Parse(tempPlane._timestamp.Substring(12, 5));

                // Saving current readings
                current[0] = p._longitude;
                current[1] = p._latitude;
                current[2] = Int32.Parse(p._timestamp.Substring(12, 5));

                p._velocity = Convert.ToInt32(_velocityCalc.Calculate(previous, current));
            }
        }

        // Time complexity: O(n1*n2)
        // Where n1 = size of _planes
        // and n2 = size of _previousPlanes
        public void CalcHeading()
        {
            int[] previous = new int[2];
            int[] current = new int[2];

            foreach (Plane p in _planes)
            {
                // Finding and saving previous readings from tag of _planes
                Plane tempPlane;
                tempPlane = _previousPlanes.Find(prevPlane => prevPlane._tag == p._tag);
                previous[0] = tempPlane._longitude;
                previous[1] = tempPlane._latitude;

                // Saving current readings
                current[0] = p._longitude;
                current[1] = p._latitude;

                p._heading = Convert.ToInt32(_headingCalc.Calculate(previous, current));
            }
        }

    }
}
