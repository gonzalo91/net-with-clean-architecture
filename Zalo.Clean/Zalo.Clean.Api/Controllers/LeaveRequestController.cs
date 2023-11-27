using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zalo.Clean.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using Zalo.Clean.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using Zalo.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using Zalo.Clean.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using Zalo.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestDetail;
using Zalo.Clean.Application.Features.LeaveRequest.GetLeaveRequestList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Zalo.Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveRequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<List<LeaveRequestListDto>> Get()
        {
            var leaveRequests = await mediator.Send(new GetLeaveRequestListQuery());

            return leaveRequests;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetLeaveRequestDetailDto>> Get(int id)
        {
            var leaveRequest = await mediator.Send(new GetLeaveRequestDetailQuery { Id = id});

            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<ActionResult<int>> Post(CreateLeaveRequestCommand command)
        {
            var leaveRequestId = await mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = leaveRequestId});
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateLeaveRequestCommand command)
        {
            await mediator.Send(command);

            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteLeaveRequestCommand { Id= id });

            return NoContent();
        }

        [HttpPut]
        [Route("CancelRequest")]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("UpdateApproval")]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
