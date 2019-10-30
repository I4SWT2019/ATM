using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace ATM
{
    public class DataHandler
    {
        public  DataHandler()
        {

        }


        public void FormattedDataReady()
        {
            // EVENT TO AREAMONITOR
        }

        public List<Plane> _planes = new List<Plane>();

        // Subscribe/Receive event with RawData from Transponder
        public void ReceivedData(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                HandleData(data);
            }
        }

        public void HandleData(string data)
        {
            Plane thisPlane = new Plane();

            thisPlane.setAll(thisPlane,
                data.Substring(0, 6),
                int.Parse(data.Substring(8, 5)),
                int.Parse(data.Substring(13, 5)),
                int.Parse(data.Substring(20, 5)),
                data.Substring(27, 17));

            _planes.Add(thisPlane);

            FormattedDataReady();
        }
    }
}
