using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler
        : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly IRepositoryManager _repository;

        public DeleteLeaveAllocationCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(
            DeleteLeaveAllocationCommand request,
            CancellationToken cancellationToken
        )
        {
            var leaveAllocation = await _repository.LeaveAllocation.GetAsync(request.Id);

            if (leaveAllocation == null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            await _repository.LeaveAllocation.DeleteAsync(leaveAllocation);

            //var query = from lt in _repository.LeaveType.GetQueryableEntity()
            //            from lr in _repository.LeaveRequest.GetQueryableEntity().Where(x => x.LeaveTypeId == lt.Id)
            //            select new
            //            {
            //                LeaveTypeName = lt.Name,
            //                CreatedAt = lr.DateCreated
            //            };

            //var result = await _repository.ExecuteQueryAsync(query);

            return Unit.Value;
        }
    }
}
