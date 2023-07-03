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
    
    public class CreateMember
    {
        private readonly DataContext _dataContext;
        public CreateMember(DataContext dataContext) => _dataContext = dataContext;

        [FunctionName("CreateMember")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createMemberDto = JsonConvert.DeserializeObject<CreateMemberDto>(requestBody);

            if (createMemberDto is not { FirstName.Length: > 0, LastName.Length: > 0, Index.Length: > 0 })
                throw new BadRequestException("Bad request");

            var existMember = _dataContext.Member.FirstOrDefault(x => x.Index == createMemberDto.Index);

            if (existMember != null)
                throw new ResourceExistException("This user exist");

            var member = new Member()
            {
                Id = Guid.NewGuid(),
                FirstName = createMemberDto.FirstName,
                LastName = createMemberDto.LastName,
                Index = createMemberDto.Index,
            };

            _dataContext.Member.Add(member);
            _dataContext.SaveChanges();

            return new OkResult();
        }
    }
}
