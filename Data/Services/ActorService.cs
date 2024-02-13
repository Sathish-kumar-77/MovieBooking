using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieBooking.Data.BaseRepository;
using MovieBooking.Models;

namespace MovieBooking.Data.Services
{
    public class ActorService :EntityRepository<Actor>, IActorsService


    {   

         public ActorService(AppDbContext context):base (context){
           
         }
    }}