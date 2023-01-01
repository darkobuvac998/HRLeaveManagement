using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface IRepositoryManager
    {
        public ILeaveTypeRepository LeaveType { get; }
        public ILeaveAllocationRepository LeaveAllocation { get; }
        public ILeaveRequestRepository LeaveRequest { get; }
        Task SaveChangesAsync();
    }
}
