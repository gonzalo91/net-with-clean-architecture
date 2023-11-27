using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Modules.Email;

namespace Zalo.Clean.Application.Contracts.Email
{
    public interface IEmailSender
    {

        Task<bool> SendEmail(EmailMessage email);

    }
}
