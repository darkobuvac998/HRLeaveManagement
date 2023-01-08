using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveType.Validators;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HRLeaveManagement.Application.Responses;
using FluentValidation;
using HRLeaveManagement.Application.DTOs.LeaveType;
using System.Linq;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler
        : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateLeaveTypeDto> _validator;

        public CreateLeaveTypeCommandHandler(
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper,
            IValidator<CreateLeaveTypeDto> validator
        )
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseCommandResponse> Handle(
            CreateLeaveTypeCommand request,
            CancellationToken cancellationToken
        )
        {
            var validation = new CreateLeaveTypeValidator();
            var validationResult = await _validator.ValidateAsync(request.LeaveTypeDto);
            var response = new BaseCommandResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
                leaveType = await _leaveTypeRepository.AddAsync(leaveType);

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveType.Id;
            }

            return response;
        }
    }
}
