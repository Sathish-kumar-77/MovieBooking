using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data.BaseRepository;
using MovieBooking.Models;

namespace MovieBooking.Data.Services
{
    public class ProducersService :EntityRepository<Producer>,IProducersService
    {
        public ProducersService(AppDbContext context):base (context)
        {
            
        }
    }
}