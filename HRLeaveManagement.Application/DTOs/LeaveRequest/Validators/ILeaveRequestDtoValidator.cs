using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRLeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(x => x.DateRequested)
                .GreaterThan(DateTime.Now)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(x => x.RequestComments)
                .MaximumLength(500)
                .WithMessage("{PropertyName} shouldn't have more than {ComparisonValue} characters");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    return await _leaveTypeRepository.Exists(id);
                }).WithMessage("{PropertyName} does not exists.");
        }
    }
}
