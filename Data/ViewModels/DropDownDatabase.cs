using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Models;

namespace MovieBooking.Data.ViewModels
{
    public class DropDownDatabase
    {

        public  DropDownDatabase()
        {
            Producers=new List<Producer>();
            cinemas=new List<Cinema>();
            actors=new List<Actor>();
        }
        public List<Producer> Producers{get;set;}
        public List<Cinema> cinemas{get;set;}

        public List<Actor> actors{get;set;}
    }
}