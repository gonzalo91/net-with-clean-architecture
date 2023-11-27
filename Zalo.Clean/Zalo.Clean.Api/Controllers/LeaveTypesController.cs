using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zalo.Clean.Application.Features.LeaveType.Commands.CreateLeaveType;
using Zalo.Clean.Application.Features.LeaveType.Commands.DeleteLeaveType;
using Zalo.Clean.Application.Features.LeaveType.Commands.UpdateLeaveType;
using Zalo.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Zalo.Clean.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Zalo.Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveTypesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<List<LeaveTypeDto>> Get()
        {
            var leaveTypes = await mediator.Send(new GetLeaveTypesQuery());

            return leaveTypes;
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
        {
            return Ok(await mediator.Send(new GetLeaveTypeDetailsQuery(id)));
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        public async Task<ActionResult> Post(CreateLeaveTypeCommand command)
        {
            var response = await mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = response });

        }

        // PUT api/<LeaveTypesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateLeaveTypeCommand command)
        {
            await mediator.Send(command);

            return NoContent();
        }

        // DELETE api/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteLeaveTypeCommand { Id = id });

            return NoContent();
        }
    }
}
