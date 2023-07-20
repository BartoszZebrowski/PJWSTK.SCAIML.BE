using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJWSTK.SCAIML.BE.Data.Entities
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public Member Member { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string MainPhoto { get; set; }
    }
}
