using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    class DataHandler
    {
        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Create planes from DataEvent
            foreach (var data in e.TransponderData)
            {
                Plane thePlane = new Plane();
                thePlane._tag = data.Substring(0, 6);
                thePlane._latitude = int.Parse(data.Substring(8, 5));
                thePlane._longitude = int.Parse(data.Substring(13, 5));
                thePlane._altitude = int.Parse(data.Substring(20, 5));
                thePlane._timestamp = data.Substring(27, 17);
            }
        }
    }
}
