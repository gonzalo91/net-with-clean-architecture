using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;

namespace Zalo.Clean.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator: AbstractValidator<CreateLeaveTypeCommand>
    {
        public readonly ILeaveTypeRepository leaveTypeRepository;
        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository= leaveTypeRepository;

            RuleFor(p => p.Name)
                .NotEmpty().NotNull().MaximumLength(70);

            RuleFor(p => p.DefaultDays)
                .LessThan(100)
                .GreaterThan(1);

            RuleFor(q => q)
                   .MustAsync(LeaveTypeUniqueName)
                   .WithMessage("LeaveType already exists");
        }


        private async Task<bool> LeaveTypeUniqueName(CreateLeaveTypeCommand command, CancellationToken arg2)
        {
            return await leaveTypeRepository.IsUniqueNameAsync(command.Name);
        }
    }
}
