using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data.BaseRepository;

namespace MovieBooking.Models
{
    public class Producer :IEntityBase
    {
        [Key]
        public int Id {get;set;}

        [Display(Name ="Profile Picture")]
        public string ProfilePictureUrl{get;set;}

           [Display(Name ="Full Name")]
        public string  FullName{get;set;}

    [Display(Name ="Biography")]
        public string Bio{get;set;}

        //relationship

        public  List<Movie>Movies{get;set;}
    }
}