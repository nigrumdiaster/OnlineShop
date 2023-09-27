using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineShop.Models
{
    public partial class StatusOrder
    {
        public StatusOrder()
        {
            Orders = new HashSet<Order>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int IsDeleted { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
