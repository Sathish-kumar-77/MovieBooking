using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data.BaseRepository;

namespace MovieBooking.Models
{
        public class Actor :IEntityBase
        {
                [Key]
                public int Id { get; set; }

                [Display(Name = "Profile Picture")]
                [Required(ErrorMessage = "Profile Picture is required")]
                public string ProfilePictureUrl { get; set; }
                [Display(Name = "Full Name")]
                [Required(ErrorMessage = "FullName is required")]
                [StringLength(50,MinimumLength =3,ErrorMessage ="FullName must be Minimum 3 and maximum 50")]
                public string FullName { get; set; }

                [Display(Name = "Biography")]
                [Required(ErrorMessage = "Biography is required")]
                public string Bio { get; set; }

                //relationship
                public List<Actor_movie> Actor_Movies { get; set; }
        }
}