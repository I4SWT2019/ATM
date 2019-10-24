using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;


namespace ATM.Calculators
{
    public class VelocityCalculator : ICalculator
    {
        public double Calculate(int[] x, int[] y)
        {
            //varibles ar init here:
            double Velocity = 0, diffX = 0, diffY = 0, totalTime = 0, travelDistance = 0;


            //difference between the differnt data points are calculated here
            diffX = Math.Abs(x[0] - y[0]);
            diffY = Math.Abs(x[1] - y[1]);
            totalTime = Math.Abs(x[2] - y[2]);

            //the difference in placement, travelled distance using pythagorian math
            travelDistance = Math.Sqrt(((diffX * diffX) + (diffY * diffY)));


            Velocity += (travelDistance / totalTime);

            return Velocity;
        }
    }
}
