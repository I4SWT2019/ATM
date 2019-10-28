using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;

namespace ATM.Calculators
{
    public class HeadingCalculator : ICalculator
    {
        public double Calculate(int[] oldData, int[] newData)
        {
            double diffX = 0, diffY = 0, Radians = 0, Angle = 0, Heading = 0;


            //reverse here, as the order of reading is reversed from the math notation
            diffX = newData[0] - oldData[0];
            diffY = newData[1] - oldData[1];


            Radians = Math.Atan2(diffY, diffX);

            Angle = Radians * (180 / Math.PI);

            if (Angle < 0)
            {
                Angle += 360;
            }
        

            Heading = Angle;

            return Heading;
        }
    }
}
