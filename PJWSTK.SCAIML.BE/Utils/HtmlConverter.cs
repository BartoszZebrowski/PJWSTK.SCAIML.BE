using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PJWSTK.SCAIML.BE.Exceptions;
using System.IO;

namespace PJWSTK.SCAIML.BE.Utils
{
    public class HtmlConverter
    {
        public static async Task<string> ChangePhotosInHTMLToBase64(IEnumerable<IFormFile> photos, IFormFile html)
        {
            var htmlContent = await ChangeIFormFileToString(html);

            var document = new HtmlDocument();
            document.LoadHtml(htmlContent);
            var imgNodes = document.DocumentNode.SelectNodes("//img");

            if(imgNodes.Count < 0)
                return htmlContent;

            foreach (HtmlNode imgNode in imgNodes)
            {
                var imgName = imgNode.GetAttributeValue("src", null)
                    ?? throw new BadRequestException("Not all imgs src attributtes in html file are filled");
                
                var photo = photos.FirstOrDefault(photo => photo.FileName == imgName)
                    ?? throw new BadRequestException($"Photo: {imgName} is not attached");

                var photoBase64 = await ChangeIFormFileToBase64(photo);

                imgNode.SetAttributeValue("src", "data:image/png;base64," + photoBase64);
            }

            return document.DocumentNode.OuterHtml;
        }

        public static async Task<string> ChangeIFormFileToBase64(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static async Task<string> ChangeIFormFileToString(IFormFile file)
        {
            using var streamReader = new StreamReader(file.OpenReadStream());
            return await streamReader.ReadToEndAsync();
        }
    }
}
