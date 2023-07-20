using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Data.Dto
{
    public class CreatePostDto
    {
        public string MemberIndex { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Content { get; set; }
        public IEnumerable<IFormFile> ContentPhotos { get; set; }
        public IFormFile MainPhoto { get; set; }
    }
}
