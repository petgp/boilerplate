using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // IdentityDbContext
using Microsoft.EntityFrameworkCore;
//db
using Boilerplate.SQLite;

namespace Boilerplate.Models
{
  public class AuthenticationContext : IdentityDbContext
  {
    public AuthenticationContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
  }
}
