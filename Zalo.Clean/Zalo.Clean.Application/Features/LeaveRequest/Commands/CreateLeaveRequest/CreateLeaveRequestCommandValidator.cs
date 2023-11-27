using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Features.LeaveRequest.Shared;

namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandValidator: AbstractValidator<CreateLeaveRequestCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;

            Include(new BaseLeaveRequestValidator(leaveTypeRepository));
        }
    }
}
