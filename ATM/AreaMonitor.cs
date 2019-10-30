﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class AreaMonitor : DataHandler
    {
        public List<Plane> _planesInArea = new List<Plane>();

        public DataHandler _dataHandler = new DataHandler();

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
        }

        public void UpdateArea(Plane _plane)
        {
            _planesInArea.Add(_plane);
            
            // TELL SUBBIE THAT PLANES ARE IN LIST
        }

    }
}
