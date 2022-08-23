﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Models.ViewModel
{
    public class RoleClaimViewModel
    {
        public RoleClaimViewModel()
        {
            Claims = new List<RoleClaim>();
        }
        [Required]
        public string RoleName { get; set; }
        public List<RoleClaim> Claims { get; set; }
    }
}
