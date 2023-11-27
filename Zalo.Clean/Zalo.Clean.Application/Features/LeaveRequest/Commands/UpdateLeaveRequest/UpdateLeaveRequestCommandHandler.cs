using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Email;
using Zalo.Clean.Application.Contracts.Logging;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Exceptions;
using Zalo.Clean.Application.Modules.Email;


namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler: IRequestHandler<UpdateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> appLogger;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.appLogger = appLogger;
        }

        public async Task<int> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null) { throw new NotFoundException(nameof(leaveRequest), request.Id); }

            var validator = new UpdateLeaveRequestCommandValidator(leaveTypeRepository, leaveRequestRepository);
            var validationResult = await validator.ValidateAsync(request);

            if(validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            mapper.Map(request, leaveRequest);

            await leaveRequestRepository.UpdateAsync(leaveRequest);


            var email = new EmailMessage
            {
                To = string.Empty,
                From = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been updated successfully",
                Subject = "Leave Request Submitted",
            };

            try
            {
                await emailSender.SendEmail(email);
            }catch(Exception ex)
            {
                appLogger.warn(ex.Message);
            }

            return leaveRequest.Id;
        }
    }
}
