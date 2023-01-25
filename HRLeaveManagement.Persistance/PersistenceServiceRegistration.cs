using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistanceServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<LeaveManagementDbContext>(
                options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("Db"),
                        builder => builder.MigrationsAssembly("HRLeaveManagement.API")
                    )
            );

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();

            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
