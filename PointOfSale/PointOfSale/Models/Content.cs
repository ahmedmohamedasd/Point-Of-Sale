using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Content
    {
        public int Id { get; set; }
        
        public int? BarId { get; set; }
        public BarItem BarItem2 { get; set; }
        [ForeignKey("BarItem2")]
        [Display(Name = "Content Name")]
        public int? ContentId { get; set; }
    
}
}
