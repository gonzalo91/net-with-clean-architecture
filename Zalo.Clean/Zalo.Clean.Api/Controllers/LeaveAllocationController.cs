using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zalo.Clean.Application.Features.LeaveAllocation.Command.CreateLeaveAllocation;
using Zalo.Clean.Application.Features.LeaveAllocation.Command.DeleteLeaveAllocation;
using Zalo.Clean.Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation;
using Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using Zalo.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationWithDetails;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Zalo.Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveAllocationController(IMediator mediator) {
            this.mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var leaveAllocations = await mediator.Send(new GetLeaveAllocationsListQuery());
            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
        {
            var leaveAllocation = await mediator.Send(new GetLeaveAllocationQuery { Id = id});
            return Ok(leaveAllocation);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        public async Task<ActionResult<int>> Post(CreateLeaveAllocationCommand request)
        {
            var leaveAllocationId = await mediator.Send(request);
            
            return Ok(leaveAllocationId);
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, UpdateLeaveAllocationCommand request)
        {
            var leaveAllocationId = await mediator.Send(request);

            return Ok(leaveAllocationId);
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var leaveAllocationId = await mediator.Send(new DeleteLeaveAllocationCommand { Id = id });

            return Ok(leaveAllocationId);
        }
    }
}
