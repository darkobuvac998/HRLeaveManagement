using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRLeaveManagement.Persistance.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly LeaveManagementDbContext _dbContext;

        public LeaveTypeRepository(LeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
