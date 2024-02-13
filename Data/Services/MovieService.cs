using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MovieBooking.Data.BaseRepository;
using MovieBooking.Data.ViewModels;
using MovieBooking.Models;

namespace MovieBooking.Data.Services
{
    public class MovieService: EntityRepository<Movie>,IMoviesService
    {

        private readonly AppDbContext _context;
        public MovieService(AppDbContext context) :base(context)
        {
             _context=context;
        }

        public  async Task AddNewMovieAsync(NewMovieVm data)
        {
            var newMovie=new Movie(){
                Name=data.Name,
                Description=data.Description,
                Price=data.Price,
                ImageUrl=data.ImageUrl,
                CinemaId=data.CinemaId,
                StartDate=data.StartDate,
                EndDate=data.EndDate,
                MovieCategory=data.MovieCategory,
                ProducerId=data.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //add movie actor
            foreach (var actorId in data.ActorIds){
                var newActorMovie=new Actor_movie{
                    MovieId=newMovie.Id,
                    ActorId=actorId
                }  ;

                await _context.Actor_Movies.AddAsync(newActorMovie);   
                
           }
           await _context.SaveChangesAsync();


        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails=await _context.Movies.Include(c=>c.Cinema)
            .Include(p=>p.Producer)
            . Include(am=>am.Actor_Movies).ThenInclude(a=>a.Actor).FirstOrDefaultAsync(n=>n.Id == id);

            return  movieDetails;
        }

        public async Task<DropDownDatabase> GetNewMoviewDropdown()
        {
            var response =new DropDownDatabase()
            {            
            actors=await _context.Actors.OrderBy(n=>n.FullName).ToListAsync(),

            cinemas=await _context.Cinemas.OrderBy(n=>n.Name).ToListAsync(),

            Producers=await _context.Producers.OrderBy(n=>n.FullName).ToListAsync()

            
             };
        return response;
    }

        public async Task UpdateMovieAsync(NewMovieVm data)
       
        {   var dbMovie=await _context.Movies.FirstOrDefaultAsync(n=>n.Id==data.Id);
        if(dbMovie !=null){

           
                dbMovie.Name=data.Name;
                dbMovie.Description=data.Description;
                dbMovie.Price=data.Price;
                dbMovie.ImageUrl=data.ImageUrl;
                dbMovie.CinemaId=data.CinemaId;
                dbMovie.StartDate=data.StartDate;
                dbMovie.EndDate=data.EndDate;
                dbMovie.MovieCategory=data.MovieCategory;
                dbMovie.ProducerId=data.ProducerId;
                  await _context.SaveChangesAsync();
            }
           
          

        
           

            //remove  actor
            var existingactor = _context.Actor_Movies.Where(n =>n.MovieId == data.Id).ToList();
             _context.Actor_Movies.RemoveRange(existingactor);
            await _context.SaveChangesAsync();
                   

            foreach (var actorId in data.ActorIds){
                var newActorMovie=new Actor_movie{
                    MovieId=data.Id,
                    ActorId=actorId
                }  ;

                await _context.Actor_Movies.AddAsync(newActorMovie);   
                
           }
           await _context.SaveChangesAsync();


            
        }
    }
}