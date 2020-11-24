using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingControl.API.Models
{
    public class InstalledSpend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime StartDate { get; set; }
        public int QtyInstallments { get; set; }
        public int WhereId { get; set; }
        public WhereSpent WhereSpent { get; set; }
    }
}
