using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zalo.Clean.Application.Contracts.Logging
{
    public interface IAppLogger<T>
    {

        void info(string message, params object[] args);

        void warn(string message, params object[] args);

    }
}
