using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Modules.Identity;

namespace Zalo.Clean.Application.Contracts.Identity
{
    public interface IAuthService
    {

        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrationResponse> Register(RegistrationRequest request);

    }
}
