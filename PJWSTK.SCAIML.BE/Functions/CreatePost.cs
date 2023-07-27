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
using PJWSTK.SCAIML.BE.Utils;
using System.Collections;
using System.Collections.Generic;

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
            var photos = GetPhotos(req);

            var createPostDto = new CreatePostDto()
            {
                MemberIndex = req.Form["MemberIndex"],
                Title = req.Form["Title"],
                Description = req.Form["Description"],
                Content = req.Form.Files["Content"],
                ContentPhotos = photos,
                MainPhoto = req.Form.Files["MainPhoto"]
            };

            ValidateRequest(createPostDto);

            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = createPostDto.Title,
                Content = await HtmlConverter.ChangePhotosInHTMLToBase64(createPostDto.ContentPhotos, createPostDto.Content),
                Description = createPostDto.Description,
                MainPhoto = await HtmlConverter.ChangeIFormFileToBase64(createPostDto.MainPhoto),
                Member = _dataContext.Member.FirstOrDefault(member => member.Index == createPostDto.MemberIndex),
            };

            _dataContext.Post.Add(post);
            _dataContext.SaveChanges();

            return new OkObjectResult(post); // TODO change to OkResult()
        }

        private void ValidateRequest(CreatePostDto createPostDto)
        {
            Validator.Validate(() => !string.IsNullOrWhiteSpace(createPostDto.Title), "Title is empty");
            Validator.Validate(() => !string.IsNullOrWhiteSpace(createPostDto.Description), "Description is empty");

            Validator.Validate(() => !string.IsNullOrWhiteSpace(createPostDto.MemberIndex), "MemberIndex is empty");
            Validator.Validate(() => _dataContext.Member.FirstOrDefault(x => x.Index == createPostDto.MemberIndex) is not null, "Member not found");

            Validator.Validate(() => createPostDto.Content is not null, "Content is empty");
            Validator.Validate(() => createPostDto.ContentPhotos is not null, "Content photos are empty");
            Validator.Validate(() => createPostDto.MainPhoto is not null, "Main photo is empty");
        }

        private List<IFormFile> GetPhotos(HttpRequest req)
        {
            var numberOfPhotos = req.Form.Files.Count - 2;
            List<IFormFile> photos = new();

            for (var i = 0; i < numberOfPhotos; i++)
            {
                photos.Add(req.Form.Files[$"ContentPhoto[{i}]"]);
            }

            return photos;
        }
    }
}
