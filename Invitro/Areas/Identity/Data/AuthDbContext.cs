using Invitro.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Invitro.Areas.Identity.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    
}
