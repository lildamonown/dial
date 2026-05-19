using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.DAL.Entities
{
    public class Report
    {
        public int Id { get; set; }

        public Order Order { get; set; }
        public int OrderId {  get; set; }

        public double FinitePrice { get; set; }

        public DateTime DateCompleted { get; set; }

        public string NameMaster { get; set; } = string.Empty;

        public string SurnameMaster { get; set; } = string.Empty;
    }
}
