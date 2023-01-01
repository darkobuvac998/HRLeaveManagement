using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> GetLeaveAllocationsAsync()
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveAllocations);
        }

        [HttpGet("id")]
        public async Task<ActionResult<LeaveAllocationDto>> GetLeaveAllocationAsync(int id)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsRequest { Id = id });
            return Ok(leaveAllocation);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateLeaveAllocationAsync([FromBody] CreateLeaveAllocationDto createLeaveAllocationDto)
        {
            var command = new CreateLeaveAllocationCommand { CreateLeaveAllocationDto = createLeaveAllocationDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLeaveAllocationAsync([FromBody] UpdateLeaveAllocationDto updateLeaveAllocationDto)
        {
            var command = new UpdateLeaveAllocationCommand { UpdateLeaveAllocationDto = updateLeaveAllocationDto };
            _ = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteLeaveAllocationAsync(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
