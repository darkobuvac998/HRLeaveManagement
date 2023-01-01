using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface IRepositoryManager
    {
        public ILeaveTypeRepository LeaveType { get; }
        public ILeaveAllocationRepository LeaveAllocation { get; }
        public ILeaveRequestRepository LeaveRequest { get; }
        Task<IReadOnlyCollection<T>> ExecuteQueryAsync<T>(IQueryable<T> query);
        Task SaveChangesAsync();
    }
}
