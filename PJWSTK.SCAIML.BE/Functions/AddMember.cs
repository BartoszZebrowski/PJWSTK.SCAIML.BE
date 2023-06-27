using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PJWSTK.SCAIML.BE.Data.Dto;
using PJWSTK.SCAIML.BE.Data.Entities;
using PJWSTK.SCAIML.BE.Data;
using System.Linq;
using System.Runtime.Serialization;
using PJWSTK.SCAIML.BE.Exceptions;

namespace PJWSTK.SCAIML.BE.Functions
{
    
    public class AddMember
    {


        [FunctionName("AddMember")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {

            Console.WriteLine(req.Body.ToString());

            //var user = _dataContext.Member.FirstOrDefault(x => x.Index == createMemberDto.Index);

            //if (user != null)
            //    throw new ResourceExistException("This user exist");


            //var member = new Member()
            //{
            //    Id = Guid.NewGuid(),
            //    FirstName = createMemberDto.FirsName,
            //    LastName = createMemberDto.LastName,
            //    Index = createMemberDto.Index,
            //};

            //_dataContext.Member.Add(member);

            return new OkResult();
        }
    }

    
}
