using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Exceptions;

namespace Zalo.Clean.Application.Features.LeaveAllocation.Command.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;        

        public DeleteLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository) {
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;            
        }

        public async Task<int> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(request.Id);

            if (leaveAllocation == null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            await leaveAllocationRepository.DeleteAsync(leaveAllocation);

            return leaveAllocation.Id;
        }
    }
}
