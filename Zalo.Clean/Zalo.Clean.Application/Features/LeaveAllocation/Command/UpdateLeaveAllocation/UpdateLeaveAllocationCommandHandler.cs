using AutoMapper;
using MediatR;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Exceptions;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<int> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Allocation", validationResult);
            }

            var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(request.Id);

            if(leaveAllocation is null)
            {
                throw new NotFoundException(nameof(leaveAllocation), request.Id);
            }

            mapper.Map(request, leaveAllocationRepository);

            await leaveAllocationRepository.UpdateAsync(leaveAllocation);

            return leaveAllocation.Id;
        }
    }

    
}
