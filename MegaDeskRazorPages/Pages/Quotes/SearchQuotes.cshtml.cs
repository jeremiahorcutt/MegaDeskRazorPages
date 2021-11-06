using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskRazorPages.Data;
using MegaDeskRazorPages.Models;

namespace MegaDeskRazorPages.Pages.Quotes
{
    public class SearchQuotesModel : PageModel
    {
        private readonly MegaDeskRazorPages.Data.MegaDeskRazorPagesContext _context;

        public SearchQuotesModel(MegaDeskRazorPages.Data.MegaDeskRazorPagesContext context)
        {
            _context = context;
        }

        public string CurrentFilter { get; set; }
        public IList<DeskQuote> DeskQuote { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString;

            var quotes = from q in _context.DeskQuote
                          select q;
            if (!string.IsNullOrEmpty(searchString))
            {
                quotes = quotes.Where(s => s.CustomerName.Contains(searchString));
            }

            DeskQuote = await quotes.AsNoTracking().ToListAsync();
        }
    }
}
