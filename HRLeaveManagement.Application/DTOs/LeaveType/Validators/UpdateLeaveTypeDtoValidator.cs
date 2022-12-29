using FluentValidation;
using HRLeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRLeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            Include(new ILeaveTypeDtoValidator());

            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) => {
                    return await _leaveTypeRepository.Exists(id);
                }).WithMessage("{PropertyName} does not exists in database");
        }
    }
}
