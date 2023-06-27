using Microsoft.EntityFrameworkCore;
using PJWSTK.SCAIML.BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Member> Member { get; set; }
    }
}
