using System;
using System.Collections.Generic;
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
    }
}
