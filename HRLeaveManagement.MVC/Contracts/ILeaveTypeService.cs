using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeViewModel>> GetLeaveTypes();
        Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id);
        Task<Response<int>> CreateLeaveType(CreateLeaveTypeViewModel leaveType);
        Task<Response<int>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType);
        Task<Response<int>> DeleteLeaveType(LeaveTypeViewModel leaveType);
    }
}
