﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Data.Dto
{
    internal class GetPostsDto
    {
        public Guid Id { get; set; }
        public string MemberIndex { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainPhoto { get; set; }
    }
}
