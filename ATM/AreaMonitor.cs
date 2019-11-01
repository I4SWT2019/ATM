using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;
using TransponderReceiver;

namespace ATM
{
    public class AreaMonitor : ISubject
    {
        public List<Plane> _planesInArea = new List<Plane>();

        public List<IObserver> _observers = new List<IObserver>();

        public bool EventFromDataHandlerReceived = false;


        public AreaMonitor(IDataHandler _dataHandler)
        {
            _dataHandler.PlaneAddedEvent += HandleReceivedData;
        }

        public void HandleReceivedData(object sender, PlaneAddedEventArgs e)
        {
            EventFromDataHandlerReceived = true;

            bool IN_AREA = true;
            bool NOT_IN_AREA = false;

            // Check if planes in List are in monitoring area
            if (((90000 > e.Plane._latitude) && (e.Plane._latitude > 10000)) &&
                ((90000 > e.Plane._longitude) && (e.Plane._longitude > 10000)))
                UpdateArea(e.Plane, IN_AREA);
            else UpdateArea(e.Plane, NOT_IN_AREA);
        }

        public void UpdateArea(Plane plane, bool PlaneInArea)
        {
            Plane planeInList = _planesInArea.Find(i => i._tag == plane._tag);

            if (planeInList == null)
            {
                if (PlaneInArea)
                {
                    _planesInArea.Add(plane);
                    Notify();
                }
            }
            else if (planeInList._tag == plane._tag)
            {
                _planesInArea.Remove(planeInList);
                if (PlaneInArea)
                {
                    _planesInArea.Add(plane);
                    Notify();
                }
            }
        }

        /* 
        Observer Pattern
        */
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_planesInArea);
            }

        }
        public void Attach(IObserver observer)
        {
            this._observers.Add(observer);
        }
        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
        }
    }
}
