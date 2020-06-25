using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.Exceptions
{
    public class NoArtistsEventException : Exception
    {
        public NoArtistsEventException(string text) : base(text)
        {
        }
    }
}
