using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;

namespace ATM
{
    public class ATM : IObserver
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

            if (_planes != null && _previousPlanes != null)
            {
                CalcVelocity();
                CalcHeading();
                Print();
            }

        }

        private void Print()
        {
            _areaPrinter.Print(_planes);
        }

        // Time complexity: O(n1*n2)
        // Where n1 = size of _planes
        // and n2 = size of _previousPlanes
        private void CalcVelocity()
        {
            int[] previous = new int[3];
            int[] current = new int[3];

            if (_planes == null)
                return;

            foreach (Plane p in _planes)
            {
                // Finding and saving previous readings from tag of _planes
                Plane tempPlane;
                tempPlane = _previousPlanes.Find(prevPlane => prevPlane._tag == p._tag);
                if (tempPlane == null)
                    return;
                previous[0] = tempPlane._longitude;
                previous[1] = tempPlane._latitude;
                if(tempPlane._timestamp != null)
                    previous[2] = Int32.Parse(tempPlane._timestamp.Substring(12, 5));

                current[0] = p._longitude;
                current[1] = p._latitude;
                if (p._timestamp != null)
                {
                    current[2] = Int32.Parse(p._timestamp.Substring(12, 5));
                    p._velocity = Convert.ToInt32(Math.Round(_velocityCalc.Calculate(previous, current)));
                }
            }
        }

        // Time complexity: O(n1*n2)
        // Where n1 = size of _planes
        // and n2 = size of _previousPlanes
        private void CalcHeading()
        {
            int[] previous = new int[2];
            int[] current = new int[2];

            if (_planes == null)
                return;

            foreach (Plane p in _planes)
            {
                // Finding and saving previous readings from tag of _planes
                Plane tempPlane; 
                tempPlane = _previousPlanes.Find(prevPlane => prevPlane._tag == p._tag);
                if (tempPlane == null)
                    return;
                previous[0] = tempPlane._longitude;
                previous[1] = tempPlane._latitude;

                current[0] = p._longitude;
                current[1] = p._latitude;

                p._heading = Convert.ToInt32(_headingCalc.Calculate(previous, current));
            }
        }

    }
}
