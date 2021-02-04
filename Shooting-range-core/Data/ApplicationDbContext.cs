using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shooting_range_core.Models;

namespace Shooting_range_core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ShootingField> ShootingFields { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
    }
}
