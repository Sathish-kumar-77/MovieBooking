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
    public class NewMovieVm 
    {
        public int Id{get;set;}
       [Display(Name ="Movie Name")]
       [Required(ErrorMessage ="Name is Required")]
        public string Name{get;set;}
       
        [Display(Name ="Movie Description")]
       [Required(ErrorMessage ="Description is Required")]
        public string Description{get;set;}
        
         [Display(Name ="Price in Rs")]
       [Required(ErrorMessage ="Price is Required")]
        public int Price{get;set;}
         
          [Display(Name ="Movie Poster Url ")]
       [Required(ErrorMessage ="Url  is Required")]
        public string  ImageUrl{get;set;}
        
         [Display(Name ="startDate")]
       [Required(ErrorMessage ="StartDate is Required")]
        public DateTime StartDate{get;set;}

          [Display(Name ="EndDate")]
       [Required(ErrorMessage ="EndDate is Required")]
        public DateTime EndDate{get;set;}
         
          [Display(Name ="Select Category")]
       [Required(ErrorMessage ="MovieCategory is Required")]
        public MovieCategory ?MovieCategory{get;set;}
        //relationship
          [Display(Name ="Select ActorId ")]
       [Required(ErrorMessage ="ActorId is Required")]
        public List<int>ActorIds{get;set;}

        //cinema 
         [Display(Name ="Select CinemaId")]
       [Required(ErrorMessage ="CinemaId is Required")]
         public int CinemaId{get;set;}
         

        //producer
        //cinema 
         [Display(Name ="Select ProducerId")]
       [Required(ErrorMessage ="Producer is Required")]
         public int ProducerId{get;set;}

       
    }
}