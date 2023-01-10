using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Application.Profiles;
using HRLeaveManagement.Application.Responses;
using HRLeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ValidationException = HRLeaveManagement.Application.Exceptions.ValidationException;

namespace HRLeaveManagement.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly CreateLeaveTypeCommandHandler _sut;
        private readonly CreateLeaveTypeDto _createLeaveTypeDto;
        private readonly Mock<IValidator<CreateLeaveTypeDto>> _mockValidator;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetLelaveTypeRepository();
            var mapperConfig = new MapperConfiguration(
                c =>
                {
                    c.AddProfile<MappingProfile>();
                }
            );

            _mapper = mapperConfig.CreateMapper();

            _createLeaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 4,
                Name = "Create leave type test"
            };

            _mockValidator = MockLeaveTypeRepository.GetCreateLeaveTypeValidator(
                _createLeaveTypeDto
            );

            _sut = new CreateLeaveTypeCommandHandler(
                _mockRepo.Object,
                _mapper,
                _mockValidator.Object
            );
        }

        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _sut.Handle(
                new CreateLeaveTypeCommand { LeaveTypeDto = _createLeaveTypeDto },
                CancellationToken.None
            );

            var leaveTypes = await _mockRepo.Object.GetAllAsync();

            result.ShouldBeOfType<BaseCommandResponse>();

            leaveTypes.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InValid_LeaveType_Added()
        {
            _createLeaveTypeDto.DefaultDays = -1;

            //ValidationException ex = await Should.ThrowAsync<ValidationException>(
            //    async () =>
            //        await _sut.Handle(
            //            new CreateLeaveTypeCommand { LeaveTypeDto = _createLeaveTypeDto },
            //            CancellationToken.None
            //        )
            //);

            var result = await _sut.Handle(
                new CreateLeaveTypeCommand { LeaveTypeDto = _createLeaveTypeDto },
                CancellationToken.None
            );

            var leaveTypes = await _mockRepo.Object.GetAllAsync();

            leaveTypes.Count.ShouldBe(3);

            //ex.ShouldNotBeNull();
            result.ShouldBeOfType<BaseCommandResponse>();
        }
    }
}
