using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;

namespace Zalo.Clean.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator: AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;


            RuleFor(p => p.StartDate)
                .LessThan(p => p.EndDate);

            RuleFor(p => p.EndDate)
                .GreaterThan(p => p.StartDate);

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist);

        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
        {
            var leaveType = await leaveTypeRepository.GetByIdAsync(id);

            return leaveType != null;
        }
    }
}
