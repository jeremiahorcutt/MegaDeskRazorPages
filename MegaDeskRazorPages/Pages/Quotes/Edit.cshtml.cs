using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDeskRazorPages.Data;
using MegaDeskRazorPages.Models;

namespace MegaDeskRazorPages.Pages.Quotes
{
    public class EditModel : PageModel
    {
        private readonly MegaDeskRazorPages.Data.MegaDeskRazorPagesContext _context;

        public EditModel(MegaDeskRazorPages.Data.MegaDeskRazorPagesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; }

        [BindProperty]
        public Desk Desk { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeskQuote = await _context.DeskQuote.FirstOrDefaultAsync(m => m.ID == id);

            if (DeskQuote == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            DeskQuote.QuoteTotal = GetQuote(DeskQuote.RushDays);
            _context.Attach(DeskQuote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ViewAllQuotes");
        }

        private bool DeskQuoteExists(int id)
        {
            return _context.DeskQuote.Any(e => e.ID == id);
        }
        //method for calculating quote price
        public double GetQuote(int rush)
        {
            Desk.area = DeskQuote.Width * DeskQuote.Depth;
            Desk.quotePrice = DeskQuote.PRICE_BASE + CalculateAreaCost() + CalculateDrawerCost() + GetMaterialCost() + CalculateRushOrderCost(rush);
            return Desk.quotePrice;
        }


        //method for calculating the area cost
        private int CalculateAreaCost()
        {
            if (Desk.area > DeskQuote.LOWER_SIZE_LIMIT)
            {
                return (Desk.area - DeskQuote.LOWER_SIZE_LIMIT) * DeskQuote.PRICE_PER_AREA;
            }
            else
            {
                return 0;
            }
        }

        //method for calculating the drawer cost
        private int CalculateDrawerCost()
        {
            int DrawerCost = DeskQuote.NumberOfDrawers * DeskQuote.PRICE_PER_DRAWER;
            return DrawerCost;
        }

        //method for determining the rush costs
        private int CalculateRushOrderCost(int rush)
        {
            int cost = 0;
            switch (rush)
            {
                case 7:
                    if (Desk.area < DeskQuote.LOWER_SIZE_LIMIT)
                    {
                        cost = 30;
                    }
                    else if (Desk.area > DeskQuote.UPPER_SIZE_LIMIT)
                    {
                        cost = 40;
                    }
                    else
                    {
                        cost = 35;
                    }
                    break;
                case 5:
                    if (Desk.area < DeskQuote.LOWER_SIZE_LIMIT)
                    {
                        cost = 40;
                    }
                    else if (Desk.area > DeskQuote.UPPER_SIZE_LIMIT)
                    {
                        cost = 60;
                    }
                    else
                    {
                        cost = 50;
                    }
                    break;
                case 3:
                    if (Desk.area < DeskQuote.LOWER_SIZE_LIMIT)
                    {
                        cost = 60;
                    }
                    else if (Desk.area > DeskQuote.UPPER_SIZE_LIMIT)
                    {
                        cost = 80;
                    }
                    else
                    {
                        cost = 70;
                    }
                    break;
            }
            return cost;
        }

        private int GetMaterialCost()
        {
            int cost = 0;
            if (DeskQuote.DeskMaterial == "Pine" || DeskQuote.DeskMaterial == "pine")
            {
                cost = 50;
            }
            else if (DeskQuote.DeskMaterial == "Laminate" || DeskQuote.DeskMaterial == "laminate")
            {
                cost = 100;
            }
            else if (DeskQuote.DeskMaterial == "Veneer" || DeskQuote.DeskMaterial == "veneer")
            {
                cost = 125;
            }
            else if (DeskQuote.DeskMaterial == "Oak" || DeskQuote.DeskMaterial == "oak")
            {
                cost = 200;
            }
            else if (DeskQuote.DeskMaterial == "Rosewood" || DeskQuote.DeskMaterial == "rosewood")
            {
                cost = 300;
            }
            return cost;
        }
    }
}
