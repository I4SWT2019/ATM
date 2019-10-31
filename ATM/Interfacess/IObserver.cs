using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfacess
{
    public interface IObserver
    {
        void Update(List<Plane> planes);
    }
}
