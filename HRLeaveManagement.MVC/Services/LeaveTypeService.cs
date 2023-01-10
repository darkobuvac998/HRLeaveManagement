using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IMapper _mapper;

        public LeaveTypeService(IClient client, ILocalStorageService localStorage, IMapper mapper)
            : base(client, localStorage)
        {
            _httpClient = client;
            _localStorage = localStorage;
            _mapper = mapper;
        }

        public Task<Response<int>> CreateLeaveType(CreateLeaveTypeViewModel leaveType)
        {
            throw new NotImplementedException();
        }

        public Task<Response<int>> DeleteLeaveType(LeaveTypeViewModel leaveType)
        {
            throw new NotImplementedException();
        }

        public async Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id)
        {
            var leaveType = await _httpClient.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeViewModel>(leaveType);
        }

        public async Task<List<LeaveTypeViewModel>> GetLeaveTypes()
        {
            var leaveTypes = await _httpClient.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);
        }

        public Task<Response<int>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType)
        {
            throw new NotImplementedException();
        }
    }
}
