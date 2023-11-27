using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Features.LeaveType.Commands.CreateLeaveType;
using Zalo.Clean.Application.Features.LeaveType.Commands.UpdateLeaveType;
using Zalo.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Zalo.Clean.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using Zalo.Clean.Domain;

namespace Zalo.Clean.Application.MappingProfiles
{
    public class LeaveTypeProfile: Profile
    {

        public LeaveTypeProfile() { 
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>();
            CreateMap<CreateLeaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>();
        }

    }
}
