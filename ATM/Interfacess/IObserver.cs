using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfacess
{
    interface IObserver
    {
        void Update(List<Plane> planes);
    }
}
