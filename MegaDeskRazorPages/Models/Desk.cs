using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRazorPages.Models
{

    public class Desk
    {
        public int ID { get; set; }
        //constants
        public double quotePrice;
        public int area { get; set; }
    }
}
