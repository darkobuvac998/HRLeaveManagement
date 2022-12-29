using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRLeaveManagement.Application.Contracts.Infrastructure;
using HRLeaveManagement.Application.Models;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateLeaveRequestDto> _validator;
        private readonly IEmailSender _emailSender;

        public CreateLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveTypeRepository, 
            IMapper mapper, 
            IValidator<CreateLeaveRequestDto> validator,
            IEmailSender emailSender)
        {
            _leaveRequestRepository = leaveTypeRepository;
            _mapper = mapper;
            _validator = validator;
            _emailSender = emailSender;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CreateLeaveRequestDto);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request.CreateLeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);

            try
            {
                await _emailSender.SendEmail(new Email { Body = "", Subject = "", To = "" });
            }
            catch (Exception)
            {
            }
            
            return leaveRequest.Id;
        }
    }
}
