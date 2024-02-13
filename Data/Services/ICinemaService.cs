using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data.BaseRepository;
using MovieBooking.Models;

namespace MovieBooking.Data.Services
{
    public interface ICinemaService:IEntityBaseRepository<Cinema>
    {
        
    }
}