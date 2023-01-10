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

        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeViewModel leaveType)
        {
            try
            {
                var response = new Response<int>();
                var leaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                var apiResponse = await _httpClient.LeaveTypesPOSTAsync(leaveTypeDto);
                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = apiResponse.Success;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConverApiExceptions<int>(ex);
            }
        }

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                await _httpClient.IdDELETE2Async(id);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConverApiExceptions<int>(ex);
            }
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
