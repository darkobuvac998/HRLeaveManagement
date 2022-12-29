using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Handlers.Common;
using HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
    public class GetLeaveAllocationDetailsRequestHandler : LeaveAllocationBaseHandler, IRequestHandler<GetLeaveAllocationDetailsRequest, LeaveAllocationDto>
    {
        public GetLeaveAllocationDetailsRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper) : base (leaveAllocationRepository, mapper) { }
        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _repository.GetLeaveAllocationWithDetails(request.Id);
            return _mapper.Map<LeaveAllocationDto>(leaveAllocation);
        }
    }
}
