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
using PJWSTK.SCAIML.BE.Data.Dto;
using PJWSTK.SCAIML.BE.Data.Entities;
using System.Linq;
using PJWSTK.SCAIML.BE.Exceptions;

namespace PJWSTK.SCAIML.BE
{
    public class CreatePost
    {
        private readonly DataContext _dataContext;
        public CreatePost(DataContext dataContext) => _dataContext = dataContext;

        [FunctionName("CreatePost")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            CreatePostDto createPostDto)
        {
            var user =_dataContext.Member.FirstOrDefault(x => x.Index == createPostDto.UserIndex) ??
                throw new ResourceNotFoundException("User not found");

            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                Description = createPostDto.Description,
                PhotoBlobUrl = createPostDto.PhotoBlobUrl,
                User = user
            };

            _dataContext.Post.Add(post);
            _dataContext.SaveChanges();

            return new OkResult();
        }
    }
}
