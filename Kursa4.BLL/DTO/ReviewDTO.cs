using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public int Grade { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
