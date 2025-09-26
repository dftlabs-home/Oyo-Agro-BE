using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Exceptions
{
    public class ConnectionStringException : Exception
    {
        public ConnectionStringException(string message) : base(message)
        {
        }
    }
}
