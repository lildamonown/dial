using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.DTO
{
    public class ReportDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public double FinitePrice { get; set; }

        public DateTime DateCompleted { get; set; }

        public string NameMaster { get; set; } = string.Empty;

        public string SurnameMaster { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;
    }
}
