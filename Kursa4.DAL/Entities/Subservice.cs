using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.DAL.Entities
{
    public class Subservice
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TimeSpan LeadTime { get; set; }

        public bool FixPrice { get; set; }

        public double Price { get; set; }

        public bool Visible { get; set; }

        public Service Service { get; set; }
        public int ServiceId { get; set; }

        public List<Order> Orders { get; set; } = [];
    }
}
