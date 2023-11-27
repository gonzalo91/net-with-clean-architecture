
using Zalo.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Zalo.Clean.Application.Contracts.Identity;
using Zalo.Clean.Application.Modules.Identity;
using Zalo.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Zalo.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor httpContextAccesor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccesor)
        {
            _userManager = userManager;
            this.httpContextAccesor = httpContextAccesor;
        }

        public string UserId { get => httpContextAccesor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier); }

        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            var employee = await _userManager.FindByIdAsync(id);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }
        

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(q => new Employee
            {
                Id = q.Id,
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName
            }).ToList();
        }
    }
}