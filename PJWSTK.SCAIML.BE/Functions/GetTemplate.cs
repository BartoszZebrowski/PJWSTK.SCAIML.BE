using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace PJWSTK.SCAIML.BE.Functions
{
    public static class GetTemplate
    {
        [FunctionName("GetTemplate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var templatePath = ".\\Template.zip";
            var contentType = "application/zip";
            string fileName = "template.zip";

            var file = File.ReadAllBytes(templatePath);

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(file)
            };

            response.Content.Headers.ContentDisposition = contentDisposition;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return new FileContentResult(file, contentType)
            {
                FileDownloadName = fileName
            };
        }
    }
}
