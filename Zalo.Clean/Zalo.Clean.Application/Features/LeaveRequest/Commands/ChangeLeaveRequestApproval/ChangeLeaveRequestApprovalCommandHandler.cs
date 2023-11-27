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
using Zalo.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using Zalo.Clean.Application.Modules.Email;

namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler: IRequestHandler<ChangeLeaveRequestApprovalCommand, bool>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> appLogger;

        public ChangeLeaveRequestApprovalCommandHandler(
            ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.appLogger = appLogger;
        }
       

        async Task<bool> IRequestHandler<ChangeLeaveRequestApprovalCommand, bool>.Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null) { throw new NotFoundException(nameof(leaveRequest), request.Id); }

            leaveRequest.Approved = request.Approved;

            await leaveRequestRepository.UpdateAsync(leaveRequest);

            if (request.Approved)
            {
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await leaveAllocationRepository.GetUserAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);

                if(allocation != null)
                {
                    allocation.NumberOfDays -= daysRequested;
                    await leaveAllocationRepository.UpdateAsync(allocation);
                }
            }

            var email = new EmailMessage
            {
                To = string.Empty,
                From = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated successfully",
                Subject = "Leave Request Submitted",
            };

            try
            {
                //await emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                appLogger.warn(ex.Message);
            }

            return true;
        }
    }
}
