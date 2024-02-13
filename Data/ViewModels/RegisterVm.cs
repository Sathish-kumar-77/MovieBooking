using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBooking.Data.ViewModels
{
    public class RegisterVm
    {
        
        [Display(Name =" FullName ")]
         [Required(ErrorMessage="FullName required")]
        public string FullName{get;set;}

        [Display(Name =" Email Address")]
         [Required(ErrorMessage="Email Adress is required")]
        public string EmailAddress{get;set;}
         

         
         [Required]
         [DataType(DataType.Password)]
        public string Password{get;set;}

        [Display(Name ="confirm Password")]
         [Required(ErrorMessage ="Confirm Password is required")]
         [DataType(DataType.Password)]
         [Compare("Password",ErrorMessage ="Password do not match")]
        public string ConfirmPassword{get;set;}
    }
}