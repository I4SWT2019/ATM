using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfacess
{
    public interface IDataHandler
    {
        event EventHandler<PlaneAddedEventArgs> PlaneAddedEvent;
    }
}
