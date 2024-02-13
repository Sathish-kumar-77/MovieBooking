using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data;
using MovieBooking.Data.BaseRepository;

namespace MovieBooking.Models
{
    public class Movie :IEntityBase
    {
        [Key]
        public int Id{get;set;}

        public string Name{get;set;}

        public string Description{get;set;}

        public int Price{get;set;}

        public string  ImageUrl{get;set;}

        public DateTime StartDate{get;set;}

        public DateTime EndDate{get;set;}

        public MovieCategory ?MovieCategory{get;set;}
        //relationship

        public List<Actor_movie>Actor_Movies{get;set;}

        //cinema 
         public int CinemaId{get;set;}
         [ForeignKey("CinemaId")]
        public Cinema Cinema{get;set;}

        //producer
        //cinema 
         public int ProducerId{get;set;}
         [ForeignKey("ProducerId")]
        public Producer Producer{get;set;}

       
    }
}