using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Identity;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Exceptions;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IUserService userService;

        public CreateLeaveAllocationCommandHandler(
            IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository,
            IUserService userService)
        {
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.userService = userService;
        }

        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validation = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
            var validationResult = await validation.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
            }

            var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            var employees = await userService.GetEmployeesAsync();

            var period = DateTime.Now.Year;


            var allocations = new List<Domain.LeaveAllocation>();

            foreach(var employee in employees)
            {
                var allocatinoExists = await leaveAllocationRepository.HasLeaveAllocationAsync(
                    employee.Id, request.LeaveTypeId, period
                );

                if (! allocatinoExists)
                {
                    allocations.Add(new Domain.LeaveAllocation
                    {
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays= leaveType.DefaultDay,
                        Period= period,
                        UserId=employee.Id
                    });
                }
            }

            if (allocations.Any())
            {
                await leaveAllocationRepository.AddAllocations(allocations);            
            }

            return leaveType.Id;
        }
    }
}
