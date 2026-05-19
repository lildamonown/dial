using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int StatusId { get; set; }

        public DateTime? VisitDate { get; set; }

        public DateTime CreateAt { get; set; }

        public int CarId { get; set; }

        public List<SubserviceDTO> Subservices { get; set; }
    }
}
