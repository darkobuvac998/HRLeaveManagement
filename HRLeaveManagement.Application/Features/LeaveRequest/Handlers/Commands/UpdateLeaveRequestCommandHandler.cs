using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HRLeaveManagement.Application.Persistence.Contracts;
using HRLeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateLeaveRequestDto> _validator;

        public UpdateLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository, 
            IMapper mapper,
            IValidator<UpdateLeaveRequestDto> validator)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);

            if(request.UpdateLeaveRequestDto != null)
            {
                var validationResult = await _validator.ValidateAsync(request.UpdateLeaveRequestDto);

                if (!validationResult.IsValid)
                    throw new Exceptions.ValidationException(validationResult);
                
                _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);

                await _leaveRequestRepository.UpdateAsync(leaveRequest);
            }
            else if(request.ChangeLeaveRequestApprovalDto != null)
            {
                _mapper.Map(request.ChangeLeaveRequestApprovalDto, leaveRequest);

                await _leaveRequestRepository.UpdateAsync(leaveRequest);
            }

            return Unit.Value;
        }
    }
}
