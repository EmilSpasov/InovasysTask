using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Inovasys.Data.Models;

namespace Inovasys.Data
{
    public class ApplicationDbContext : IdentityDbContext<Contragent>
    {
        public DbSet<Contragent> Contragents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contragent>().ToTable("Contragents");
        }
    }
}
