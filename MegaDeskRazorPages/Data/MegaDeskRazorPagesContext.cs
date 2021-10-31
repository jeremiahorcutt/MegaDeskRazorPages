using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaDeskRazorPages.Models;

namespace MegaDeskRazorPages.Data
{
    public class MegaDeskRazorPagesContext : DbContext
    {
        public MegaDeskRazorPagesContext (DbContextOptions<MegaDeskRazorPagesContext> options)
            : base(options)
        {
        }

        public DbSet<MegaDeskRazorPages.Models.DeskQuote> DeskQuote { get; set; }
    }
}
