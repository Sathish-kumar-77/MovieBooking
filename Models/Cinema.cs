using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data.BaseRepository;

namespace MovieBooking.Models
{
    public class Cinema : IEntityBase
    {  
        [Key]
        public int Id{get;set;}  

          
       public string  Logo{get;set;}


        public string  Name{get;set;}


   
        public string  Description{get;set;}
        //relationship

        public List<Movie>Movies{get;set;}
    }
}