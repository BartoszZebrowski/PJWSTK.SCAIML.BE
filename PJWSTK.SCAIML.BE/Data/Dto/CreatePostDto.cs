using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Data.Dto
{
    public class CreatePostDto
    {
        public string UserIndex { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string PhotoBlobUrl { get; set; }

    }
}
