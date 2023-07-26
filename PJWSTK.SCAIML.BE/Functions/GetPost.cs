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
using PJWSTK.SCAIML.BE.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace PJWSTK.SCAIML.BE
{
    public class GetPost
    {
        private readonly DataContext _dataContext;
        public GetPost(DataContext dataContext) => _dataContext = dataContext;

        [FunctionName("GetPost")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestBody = req.GetQueryParameterDictionary().First().Value;
            var postId = new Guid(requestBody);

            var post = _dataContext.Post.Include(x => x.Member).FirstOrDefault(x => x.Id == postId);

            if (post == null)
                throw new ResourceNotFoundException("This post don't exist");

            var respone = new GetPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                MainPhoto = post.MainPhoto,
                MemberIndex = post.Member.Index
            };

            return new OkObjectResult(respone);
        }
    }
}
