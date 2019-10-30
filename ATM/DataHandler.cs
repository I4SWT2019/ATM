using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using ATM.Interfacess;


namespace ATM
{
    public class DataHandler : IDataHandler
    {
        private ITransponderReceiver _receiver;

        public event EventHandler<PlaneAddedEventArgs> PlaneAddedEvent;

        public DataHandler(ITransponderReceiver receiver)
        {
            _receiver = receiver;

            _receiver.TransponderDataReady += ReceivedData;
        }
        public DataHandler()
        {

        }

       

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

            OnPlaneListUpdateEvent(new PlaneAddedEventArgs { Plane = thisPlane });
        }

        protected virtual void OnPlaneListUpdateEvent(PlaneAddedEventArgs e)
        {
            PlaneAddedEvent?.Invoke(this, e);
        }
    }
}
