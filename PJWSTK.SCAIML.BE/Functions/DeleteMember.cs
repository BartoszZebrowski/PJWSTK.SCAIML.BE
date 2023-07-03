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
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req)
        {
            var memberIndex = await new StreamReader(req.Body).ReadToEndAsync();

            var member = _dataContext.Member.FirstOrDefault(x => x.Index == memberIndex) ??
                throw new ResourceNotFoundException("This member can't exist");

            _dataContext.Member.Remove(member);
            _dataContext.SaveChanges();

            return new OkResult();
        }
    }
}
