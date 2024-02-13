using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data.BaseRepository;
using MovieBooking.Data.ViewModels;
using MovieBooking.Models;

namespace MovieBooking.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
         Task<Movie>GetMovieByIdAsync(int id);
        Task<DropDownDatabase>GetNewMoviewDropdown();

        Task AddNewMovieAsync(NewMovieVm data);
        Task UpdateMovieAsync(NewMovieVm data);
    }
}