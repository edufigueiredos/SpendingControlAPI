using Microsoft.EntityFrameworkCore;
using SpendingControl.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingControl.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<FixedSpend> FixedSpends{ get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<InstalledSpend> InstalledSpends { get; set; }
        public DbSet<WhereSpent> WhereSpents { get; set; }
    }
}
