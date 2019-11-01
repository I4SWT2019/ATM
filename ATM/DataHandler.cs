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

        public bool EventFromTransponderReceived = false;

        public DataHandler(ITransponderReceiver receiver)
        {
            _receiver = receiver;

            _receiver.TransponderDataReady += ReceivedData;
        }

        public void ReceivedData(object sender, RawTransponderDataEventArgs e)
        {
            EventFromTransponderReceived = true;
            foreach (var data in e.TransponderData)
            {
                HandleData(data);
            }
        }
        public void HandleData(string data)
        {
            var dataStrings = new List<string>();
            int position = 0;
            int start = 0;
            Plane _Plane = new Plane();

            data = data + ";";
            do
            {
                position = data.IndexOf(';', start);
                if (position >= 0)
                {
                    dataStrings.Add(data.Substring(start, position - start + 1).Trim());
                    start = position + 1;
                }
            } while (position > 0);

            if (dataStrings.Count == 5)
            {
                string tag = dataStrings[0];
                string latitude = dataStrings[1];
                string longitude = dataStrings[2];
                string altitude = dataStrings[3];
                string timestamp = dataStrings[4];
                
                tag = tag.Remove(tag.Length - 1);
                latitude = latitude.Remove(latitude.Length - 1);
                longitude = longitude.Remove(longitude.Length - 1);
                altitude = altitude.Remove(altitude.Length - 1);
                timestamp = timestamp.Remove(timestamp.Length - 1);


                _Plane.setAll(_Plane, tag, int.Parse(latitude), int.Parse(longitude), int.Parse(altitude), timestamp);
            }
            OnPlaneListUpdateEvent(new PlaneAddedEventArgs(_Plane)); // was PlaneAddedEventArgs {Plane = _Plane}

        }

        protected virtual void OnPlaneListUpdateEvent(PlaneAddedEventArgs e)
        {
            PlaneAddedEvent?.Invoke(this, e);
        }
    }
}
