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

        public void Update(List<Plane>)
        {
        }

        public void Run()
        {
        }

        private void Print()
        {
        }

        private double CalcVelocity()
        {
        }

        private double CalcHeading()
        {
        }

    }
}
