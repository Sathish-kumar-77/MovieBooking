using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBooking.Models
{
    public class OrderItem
    {
        [Key]

        public int Id{get;set;}

        public int Amount {get;set;}

        public int Price{get; set;}

        public int MovieId{get;set;}
        [ForeignKey("MovieId")]

        public virtual Movie Movie{get;set;}

         public int OrderId{get;set;}
        [ForeignKey("MovieId")]

        public  Order Order{get;set;}

    }
}