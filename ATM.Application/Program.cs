﻿using System.Reflection.Metadata.Ecma335;
using System.Threading;
using ATM.Calculators;
using ATM.Printers;
using TransponderReceiver;

namespace ATM.Application
{
    public class Program
    {
        public static int Main(string[] args)
        {
            ATM airTrafficMonitor = new ATM(new AreaMonitorPrinter(),
                new VelocityCalculator(), new HeadingCalculator());

            CollisionWarner separationWarningSystem = new CollisionWarner(new SeparationPrinter());

            // Using the real transponder data receiver
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            AreaMonitor areaMonitorReceiver = new AreaMonitor(new DataHandler(receiver));

            areaMonitorReceiver.Attach(airTrafficMonitor);
            areaMonitorReceiver.Attach(separationWarningSystem);
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
