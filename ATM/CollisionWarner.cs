using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfacess;

namespace ATM
{
    public class CollisionWarner : ISubscriber
    {
        private IPrinter _seperationPrinter;
        public void Update(List<Plane> planes)
        {
            WarningCheck(planes);
        }

        private void Print(List<Plane> planes)
        {
            _seperationPrinter.Print(planes);
        }

        private void WarningCheck(List<Plane> planes)
        {
            bool collisionImminent = false;

            if (collisionImminent == true)
            { }
        }
    }
}
