using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Handlers.Common
{
    public class LeaveAllocationBaseHandler
    {
        protected readonly ILeaveAllocationRepository _repository;
        protected readonly IMapper _mapper;

        public LeaveAllocationBaseHandler(ILeaveAllocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
