using FluentValidation;
using Zalo.Clean.Application.Contracts.Persistance;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;        

        public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository) {
            this.leaveTypeRepository = leaveTypeRepository;            
            RuleFor(p => p.NumberOfDays)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisionValue}");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PropertyName} must be after{ComparisionValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist);


            RuleFor(p => p.Id)
                .NotNull()
                .GreaterThan(0);
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
        {
            var leaveType = await leaveTypeRepository.GetByIdAsync(id);

            return leaveType != null;
        }

        

    }

    
}
