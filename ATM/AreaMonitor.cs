using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;
using TransponderReceiver;

namespace ATM
{
    class AreaMonitor : ISubject
    {
        public List<Plane> _planesInArea = new List<Plane>();

        private List<IObserver> _observers = new List<IObserver>();

        public AreaMonitor(DataHandler _dataHandler)
        {
            _dataHandler.PlaneAddedEvent += HandleReceivedData;
            
            //_planesFromDataHandler = _dataHandler._eventPlaneList;
        }

        public void HandleReceivedData(object sender, PlaneAddedEventArgs e)
        {
            // Get the first element in the list


            bool IN_AREA = true;
            bool NOT_IN_AREA = false;

            // Check if planes in List are in monitoring area
            if (((90000 > e.Plane._latitude) && (e.Plane._latitude > 10000)) &&
                ((90000 > e.Plane._longitude) && (e.Plane._longitude > 10000)))
                UpdateArea(e.Plane, IN_AREA);
            else UpdateArea(e.Plane, NOT_IN_AREA);

            // Remove the handled element from _plans list
        }

        public void UpdateArea(Plane _plane, bool PlaneInArea)
        {
            bool NeedToNotify = false;
            Plane planeInList = _planesInArea.Find(i => i._tag == _plane._tag);

            if (planeInList!= null)
            {
                _planesInArea.Remove(planeInList);
                if (PlaneInArea)
                    _planesInArea.Add(_plane);
                NeedToNotify = true;
            }

            if (NeedToNotify)
                Notify();
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
