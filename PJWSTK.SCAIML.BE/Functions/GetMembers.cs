using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PJWSTK.SCAIML.BE.Data;
using System.Linq;

namespace PJWSTK.SCAIML.BE.Functions
{
    public class GetMembers
    {
        private readonly DataContext _dataContext;
        public GetMembers(DataContext dataContext) => _dataContext = dataContext;

        [FunctionName("GetMembers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var members = _dataContext.Member.ToList();

            return new OkObjectResult(members.Select(member => member.FirstName + " " + member.LastName));
        }
    }
}
