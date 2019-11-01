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
        public double Calculate(int[] oldData, int[] newData)
        {
            //varibles ar init here:
            double Velocity = 0, diffX = 0, diffY = 0, totalTime = 0, travelDistance = 0;


            //difference between the differnt data points are calculated here
            diffX = Math.Abs(oldData[0] - newData[0]);
            diffY = Math.Abs(oldData[1] - newData[1]);
            totalTime = Math.Abs(oldData[2] - newData[2]);

            if (totalTime > 55000)
            {
                totalTime = Math.Abs(oldData[2] - (newData[2] + 60000));
            }

            //the difference in placement, travelled distance using pythagorian math
            travelDistance = Math.Sqrt(((diffX * diffX) + (diffY * diffY)));

            //Here to velocity is calculated as distance over time, but because the time scale is 10^-3 seconds
            //is must be converted to seconds by dividing w. 1,000
            if (totalTime == 0)
                return 0;
            if (travelDistance != 0 && totalTime != 0)
            {
                Velocity = (travelDistance / (totalTime / 1000)); // m/s
            }
            else
            {
                return 0;
            }

            return Velocity;
        }
    }
}
