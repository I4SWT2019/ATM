﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class AreaMonitor : Interfacess.ISubject
    {
        public List<Plane> _planesInArea = new List<Plane>();

        public List<Plane> _planesFromDataHandler = new List<Plane>();

        private List<Interfacess.IObserver> _observers = new List<Interfacess.IObserver>();

        public AreaMonitor()
        {

        }

        public void HandleReceivedData()
        {
            // Get the first element in the list
            Plane FirstPLane = _planesFromDataHandler.First();

            // Check if planes in List are in monitoring area
            if (((90000 > FirstPLane._latitude) && (FirstPLane._latitude > 10000)) && 
                ((90000 > FirstPLane._longitude) && (FirstPLane._longitude > 10000)))
                UpdateArea(FirstPLane);
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
        public void Attach(Interfacess.IObserver observer)
        {
            this._observers.Add(observer);
        }
        public void Detach(Interfacess.IObserver observer)
        {
            this._observers.Remove(observer);
        }
    }
}
