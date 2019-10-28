using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    class DataHandler : Plane
    {
        private string _receivedData;

        public List<Plane> _planes = new List<Plane>();

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Create planes from DataEvent
            foreach (var data in e.TransponderData)
            {
                Plane thisPlane = new Plane();
                thisPlane._tag = data.Substring(0, 6);
                thisPlane._latitude = int.Parse(data.Substring(8, 5));
                thisPlane._longitude = int.Parse(data.Substring(13, 5));
                thisPlane._altitude = int.Parse(data.Substring(20, 5));
                thisPlane._timestamp = data.Substring(27, 17);

                _planes.Add(thisPlane); 
            }
        }


    }
}
