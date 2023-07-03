using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PJWSTK.SCAIML.BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Member> Member { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=(localdb)\\Local;Initial Catalog=PJWSTK.SCAIML.DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}