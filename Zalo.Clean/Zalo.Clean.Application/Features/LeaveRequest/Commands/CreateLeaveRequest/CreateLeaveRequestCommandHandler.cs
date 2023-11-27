using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Email;
using Zalo.Clean.Application.Contracts.Identity;
using Zalo.Clean.Application.Contracts.Logging;
using Zalo.Clean.Application.Contracts.Persistance;
using Zalo.Clean.Application.Exceptions;
using Zalo.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using Zalo.Clean.Application.Modules.Email;
using Zalo.Clean.Domain;

namespace Zalo.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> appLogger;
        private readonly IUserService userService;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> appLogger,
            IUserService userService, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.appLogger = appLogger;
            this.userService = userService;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request Data", validationResult);
            }


            var employeeId = userService.UserId;

            var allocation = await leaveAllocationRepository.GetUserAllocation(employeeId, request.LeaveTypeId);


            if (allocation == null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId), "You do not have allocations for this type"));
                throw new BadRequestException("Invalid Leave Request Data", validationResult);
            }

            int daysRequest = (int)(request.EndDate - request.StartDate).TotalDays;
            if(daysRequest > allocation.NumberOfDays) {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId), "You do not have allocations for this type"));
                throw new BadRequestException("You don\"t have enought days for this request", validationResult);
            }

            var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
            leaveRequest.RequestingEmployeeId = employeeId;
            leaveRequest.DateRequested = DateTime.Now;
            await leaveRequestRepository.CreateAsync(leaveRequest);

            var email = new EmailMessage
            {
                To = string.Empty,
                From = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been submited successfully",
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

            return leaveRequest.Id;
        }
    }
}
