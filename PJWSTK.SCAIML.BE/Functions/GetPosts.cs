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
    public class GetPosts
    {
        private readonly DataContext _dataContext;
        public GetPosts(DataContext dataContext) => _dataContext = dataContext;

        [FunctionName("GetPosts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var posts = _dataContext.Post.Include(x => x.Member).OrderBy(x => x.Id).ToList();

            if (posts == null)
                throw new ResourceNotFoundException("Any posts don't exist");

            var resposne = posts.Select(post => new GetPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Description = post.Description,
                MainPhoto = post.PhotoBlobUrl,
                MemberIndex = post.Member.Index,
            }).ToList();

            return new OkObjectResult(resposne);
        }
    }
}
