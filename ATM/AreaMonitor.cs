using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;

namespace ATM
{
    class AreaMonitor : DataHandler, ISubject
    {
        public List<Plane> _planesInArea = new List<Plane>();

        public List<Plane> _planesFromDataHandler = new List<Plane>();

        private List<IObserver> _observers = new List<IObserver>();
        private DataHandler _dataHandler = new DataHandler();

        public AreaMonitor()
        {
            _planesInArea = _dataHandler._eventPlaneList;
        }

        public void HandleReceivedData()
        {
            // Get the first element in the list
            Plane FirstPLane = _planes.First();

            // Check if planes in List are in monitoring area
            if (((90000 > FirstPLane._latitude) && (FirstPLane._latitude > 10000)) &&
                ((90000 > FirstPLane._longitude) && (FirstPLane._longitude > 10000)))
                UpdateArea(FirstPLane);

            // Remove the handled element from _plans list
            _planes.Remove(FirstPLane);

        }

        public void UpdateArea(Plane _plane)
        {
            _planesInArea.Add(_plane);
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
