using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PJWSTK.SCAIML.BE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PJWSTK.SCAIML.BE.Configuration
{
    
    //public class EFConfiguration : FunctionsStartup
    //{
    //    //public override void Configure(IFunctionsHostBuilder builder)
    //    //{
    //    //    //string connectionString = "Data Source=(localdb)\\Local;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    //    //    //builder.Services.AddDbContext<DataContext>(
    //    //    //  options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

    //    //}

    //}
}
