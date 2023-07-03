using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Data.Dto
{
    public class GetPostsDto
    {
        public IList<GetPostDto> Posts { get; set; }
    }
}
