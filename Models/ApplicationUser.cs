using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MovieBooking.Models
{
    public class ApplicationUser :IdentityUser
    {  
        [Display(Name= "FullName")]
        public string FullName{get;set;}
    }
}