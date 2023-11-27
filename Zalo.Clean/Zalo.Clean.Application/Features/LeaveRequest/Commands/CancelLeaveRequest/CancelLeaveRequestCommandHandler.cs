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

namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler: IRequestHandler<CancelLeaveRequestCommand, bool>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<CancelLeaveRequestCommandHandler> appLogger;

        public CancelLeaveRequestCommandHandler(
            ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.leaveRequestRepository = leaveRequestRepository;
            this.emailSender = emailSender;
            this.appLogger = appLogger;
        }

        public async Task<bool> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null) { throw new NotFoundException(nameof(leaveRequest), request.Id); }

            leaveRequest.Cancelled = true;

            if(leaveRequest.Approved == true)
            {
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await leaveAllocationRepository.GetUserAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);

                if (allocation != null)
                {
                    allocation.NumberOfDays += daysRequested;
                    await leaveAllocationRepository.UpdateAsync(allocation);
                }
            }


            await leaveRequestRepository.UpdateAsync(leaveRequest);

            var email = new EmailMessage
            {
                To = string.Empty,
                From = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully",
                Subject = "Leave Request Cancelled",
            };

            try
            {
                await emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                appLogger.warn(ex.Message);
            }


            return true;
        }
    }
}
