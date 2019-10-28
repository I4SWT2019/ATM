using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;


namespace ATM
{
    public /*sealed*/ class DataHandler
    {
        public /*private*/ DataHandler()
        {

        }
                /*

        private static DataHandler instance = null;

        public static DataHandler instance
        {
            get
            {
                if(instance ==null)
                {
                    instance = new DataHandler();
                }
                return instance;
            }

        }
        */
        public event EventHandler PlanesReady;

        public void FormattedDataReady()
        {
            if (PlanesReady != null)
                PlanesReady(this, EventArgs.Empty);
            Console.WriteLine("Formatted Data ready");
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
