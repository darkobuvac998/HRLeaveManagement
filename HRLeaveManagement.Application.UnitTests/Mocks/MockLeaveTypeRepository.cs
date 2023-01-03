﻿using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    public static class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLelaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Sick"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(leaveTypes);

            mockRepo
                .Setup(x => x.AddAsync(It.IsAny<LeaveType>()))
                .ReturnsAsync(
                    (LeaveType leaveType) =>
                    {
                        leaveTypes.Add(leaveType);
                        return leaveType;
                    }
                );

            return mockRepo;
        }
    }
}
