using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Features.LeaveRequest.Shared;

namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveRequestRepository leaveRequestReporitory;

        public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestReporitory)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveRequestReporitory = leaveRequestReporitory;

            Include(new BaseLeaveRequestValidator(leaveTypeRepository));

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveRequestMustExist);
        }

        private async Task<bool> LeaveRequestMustExist(int arg1, CancellationToken arg2)
        {
            var leaveRequest = await leaveRequestReporitory.GetByIdAsync(arg1);

            return leaveRequest != null;
        }
    }
}
