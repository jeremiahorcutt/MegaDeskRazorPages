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
    public class ViewAllQuotesModel : PageModel
    {
        private readonly MegaDeskRazorPages.Data.MegaDeskRazorPagesContext _context;

        public ViewAllQuotesModel(MegaDeskRazorPages.Data.MegaDeskRazorPagesContext context)
        {
            _context = context;
        }
        public string CustomerSort { get; set; }
        public string DateSort { get; set; }
        public IList<DeskQuote> DeskQuote { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            CustomerSort = String.IsNullOrEmpty(sortOrder) ? "cust_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            var quotes = from q in _context.DeskQuote
                         select q;

            switch (sortOrder)
            {
                case "cust_desc":
                    quotes = quotes.OrderBy(s => s.CustomerName);
                    break;
                case "Date":
                    quotes = quotes.OrderBy(s => s.QuoteDate);
                    break;
                case "date_desc":
                    quotes = quotes.OrderByDescending(s => s.QuoteDate);
                    break;
                default:
                    quotes = quotes.OrderByDescending(s => s.CustomerName);
                    break;
            }

            DeskQuote = await quotes.AsNoTracking().ToListAsync();
        }
    }
}