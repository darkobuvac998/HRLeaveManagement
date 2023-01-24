using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "50980fa0-b12e-43ae-ba4b-addae9ff69f1",
                    UserId = "12d6ee1f-867f-4875-aaf4-2ae55797326f"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "dc6b5000-b3c2-4d9e-a8de-cf5edd89bbf5",
                    UserId = "f3c650df-ef4f-40ef-838a-df3b19547ada"
                }
            );
        }
    }
}
