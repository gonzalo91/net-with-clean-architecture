using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Logging;
using Zalo.Clean.Application.Contracts.Persistance;

namespace Zalo.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> logger;

        public GetLeaveTypesQueryHandler(
            IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepository, 
            IAppLogger<GetLeaveTypesQueryHandler> logger
        )
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.logger = logger;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {

            var leaveTypes = await leaveTypeRepository.GetAsync();

            var data = mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            logger.info("Leave types were retrieved succesfully");

            return data;
        }
    }
}
