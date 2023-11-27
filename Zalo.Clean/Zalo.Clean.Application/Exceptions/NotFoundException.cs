using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalo.Clean.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string type, object id) : base($"{type} ({id}) was not found") { 
        }
    }

}
