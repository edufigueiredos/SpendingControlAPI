using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingControl.API.Models
{
    public class FixedSpend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WhereId { get; set; }
        public WhereSpent WhereSpent { get; set; }

    }
}
