using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieBooking.Data;
using MovieBooking.Data.Services;
using MovieBooking.Data.User;
using MovieBooking.Data.ViewModels;
using MovieBooking.Models;

namespace MovieBooking.Controllers
{  [Authorize(Roles =UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service=service;
        }
     
     [AllowAnonymous]
       public async Task<IActionResult> Index(){

            var allMovies=await _service.GetAllAsync(n=>n.Cinema);

            return View(allMovies);

       }
       [AllowAnonymous]
       public async Task<IActionResult> Filter( string searchString){

            var allMovies=await _service.GetAllAsync(n=>n.Cinema);

            if(!string.IsNullOrEmpty(searchString)){
                
                var filterResult=allMovies.Where(n=>n.Name.Contains(searchString )|| n.Description.Contains(searchString)).ToList();
                return View("Index",filterResult);
            }

            return View(allMovies);
       }
       
       [AllowAnonymous]
       public async Task<IActionResult>Details(int id){
        var movieDetail=await _service.GetMovieByIdAsync(id);
        return View(movieDetail);
       }
       public async Task <IActionResult> Create(){
            
            var MoviedropDown=await _service.GetNewMoviewDropdown();

            ViewBag.Cinemas=new SelectList(MoviedropDown.cinemas,"Id","Name");
            ViewBag.Actors=new SelectList(MoviedropDown.actors,"Id","FullName");
            ViewBag.Producers=new SelectList(MoviedropDown.Producers,"Id","FullName");
            return View();
       
       }

       [HttpPost]
       public async Task<IActionResult>Create(NewMovieVm movie){

        if(!ModelState.IsValid){

             var MoviedropDown=await _service.GetNewMoviewDropdown();

            ViewBag.Cinemas=new SelectList(MoviedropDown.cinemas,"Id","Name");
            ViewBag.Actors=new SelectList(MoviedropDown.actors,"Id","FullName");
            ViewBag.Producers=new SelectList(MoviedropDown.Producers,"Id","FullName");
            return View(movie);
        }

        await _service.AddNewMovieAsync(movie);

        return  RedirectToAction(nameof(Index));

       }    

       /// edit
       
       public async Task <IActionResult> Edit( int id){

           var MovieDetails=await _service.GetMovieByIdAsync(id);

           if(MovieDetails==null) return View("NotFound");

           var response=new NewMovieVm (){

            Id=MovieDetails.Id,
            Name=MovieDetails.Name,
            Description=MovieDetails.Description,
            Price=MovieDetails.Price,
            StartDate=MovieDetails.StartDate,
            EndDate=MovieDetails.EndDate,
            ImageUrl=MovieDetails.ImageUrl,
            MovieCategory=MovieDetails.MovieCategory,
            CinemaId=MovieDetails.CinemaId,
            ProducerId=MovieDetails.ProducerId,
            ActorIds=MovieDetails.Actor_Movies.Select(n=>n.ActorId).ToList(),
            

           };
            
            var MoviedropDown=await _service.GetNewMoviewDropdown();

            ViewBag.Cinemas=new SelectList(MoviedropDown.cinemas,"Id","Name");
            ViewBag.Actors=new SelectList(MoviedropDown.actors,"Id","FullName");
            ViewBag.Producers=new SelectList(MoviedropDown.Producers,"Id","FullName");
            return View(response);
       
       }

       [HttpPost]
       public async Task<IActionResult>Edit( int id,NewMovieVm movie)
       {
         if(id != movie.Id)  return View("NotFound");


        if(!ModelState.IsValid){

             var MoviedropDown=await _service.GetNewMoviewDropdown();

            ViewBag.Cinemas=new SelectList(MoviedropDown.cinemas,"Id","Name");
            ViewBag.Actors=new SelectList(MoviedropDown.actors,"Id","FullName");
            ViewBag.Producers=new SelectList(MoviedropDown.Producers,"Id","FullName");
            return View(movie);
        }

        await _service.UpdateMovieAsync(movie);

        return  RedirectToAction(nameof(Index));

       }    
    }
}