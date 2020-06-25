using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.Exceptions
{
    public class EventAlreadyStartedException : Exception
    {
        public EventAlreadyStartedException(string text) : base(text)
        {
        }
    }
}
