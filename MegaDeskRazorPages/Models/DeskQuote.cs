using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRazorPages.Models
{
    public class DeskQuote
    {
        public int ID { get; set; }

        public Desk Desk { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        public string CustomerName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Quote")]
        public DateTime QuoteDate { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public double QuoteTotal { get; set; }

        [Range(24, 96)]
        public int Width { get; set; }
        [Range(12, 48)]
        public int Depth { get; set; }

        [Display(Name = "Number of Drawers")]
        [Range(0, 7)]
        public int NumberOfDrawers { get; set; }

        [Display(Name = "Desk Material")]
        [Required]
        public string DeskMaterial { get; set; }


        [Display(Name = "Shipping Option")]
        public int RushDays { get; set; }

        //Constants
        public const int PRICE_BASE = 200;
        public const int LOWER_SIZE_LIMIT = 1000;
        public const int UPPER_SIZE_LIMIT = 2000;
        public const int PRICE_PER_AREA = 1;
        public const int PRICE_PER_DRAWER = 50;
        public List<DeskQuote> deskQuotes = new List<DeskQuote>();
    }
}
