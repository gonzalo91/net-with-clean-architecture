using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Modules.Identity;

namespace Zalo.Clean.Application.Contracts.Identity
{
    public interface IUserService
    {

        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(string id);

        public string UserId { get;  }
    }
}
