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
using PJWSTK.SCAIML.BE.Exceptions;

namespace PJWSTK.SCAIML.BE.Functions
{
    public class DeleteMember
    {
        private readonly DataContext _dataContext;
        public DeleteMember(DataContext dataContext) => _dataContext = dataContext;


        [FunctionName("DeleteMember")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            string index )
        {
            var member = _dataContext.Member.FirstOrDefault(x => x.Index == index) ??
                throw new ResourceNotFoundException("This member can't exist");

            _dataContext.Member.Remove(member);

            return new OkResult();
        }
    }
}
