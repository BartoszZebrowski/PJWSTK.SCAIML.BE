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
using PJWSTK.SCAIML.BE.Exceptions;
using System.Linq;

namespace PJWSTK.SCAIML.BE.Functions
{
    public class DeletePost
    {
        private readonly DataContext _dataContext;
        public DeletePost(DataContext dataContext) => _dataContext = dataContext;

        [FunctionName("DeletePost")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var postId = new Guid(requestBody);

            var post = _dataContext.Post.FirstOrDefault(x => x.Id == postId) ??
                throw new ResourceNotFoundException("This post can't exist");

            _dataContext.Post.Remove(post);
            _dataContext.SaveChanges();

            return new OkResult();
        }
    }
}
