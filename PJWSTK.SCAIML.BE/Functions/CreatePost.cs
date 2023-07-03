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
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createPostDto = JsonConvert.DeserializeObject<CreatePostDto>(requestBody);

            if (createPostDto is not
                {
                    Title.Length: > 0,
                    Content.Length: > 0,
                    Description.Length: > 0,
                    MemberIndex.Length: > 0,
                    PhotoBlobUrl.Length: > 0,
                })
                throw new BadRequestException("Bad request");

            var member =_dataContext.Member.FirstOrDefault(x => x.Index == createPostDto.MemberIndex) ??
                throw new ResourceNotFoundException("Member not found");

            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                Description = createPostDto.Description,
                PhotoBlobUrl = createPostDto.PhotoBlobUrl,
                Member = member
            };

            _dataContext.Post.Add(post);
            _dataContext.SaveChanges();

            return new OkResult();
        }
    }
}
