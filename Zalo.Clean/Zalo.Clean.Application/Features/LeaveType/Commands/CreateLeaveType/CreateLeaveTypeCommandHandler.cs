using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Logging;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Exceptions;

namespace Zalo.Clean.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IAppLogger<CreateLeaveTypeCommandHandler> logger;

        public CreateLeaveTypeCommandHandler(
            IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<CreateLeaveTypeCommandHandler> logger
        )
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.logger = logger;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (! validationResult.IsValid)
            {
                logger.warn("Validation errors in create request for {0} - {1}", nameof(LeaveType), request.Name);
                throw new BadRequestException("Invalid LeaveType", validationResult);
            }

            var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);

            await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            return leaveTypeToCreate.Id;
        }
    }
}
