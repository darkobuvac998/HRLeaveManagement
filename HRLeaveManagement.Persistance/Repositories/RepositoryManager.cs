using HRLeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistance.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly LeaveManagementDbContext _dbContext;
        private ILeaveTypeRepository _leaveType;
        private ILeaveAllocationRepository _leaveAllocation;
        private ILeaveRequestRepository _leaveRequest;

        public RepositoryManager(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ILeaveTypeRepository LeaveType
        {
            get
            {
                if (_leaveType == null)
                    _leaveType = new LeaveTypeRepository(_dbContext);

                return _leaveType;
            }
        }

        public ILeaveAllocationRepository LeaveAllocation
        {
            get
            {
                if (_leaveAllocation == null)
                    _leaveAllocation = new LeaveAllocationRepository(_dbContext);

                return _leaveAllocation;
            }
        }

        public ILeaveRequestRepository LeaveRequest
        {
            get
            {
                if (_leaveRequest == null)
                    _leaveRequest = new LeaveRequestRepository(_dbContext);

                return _leaveRequest;
            }
        }

        public async Task<IReadOnlyCollection<T>> ExecuteQueryAsync<T>(IQueryable<T> query) =>
            await query.ToListAsync();

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
