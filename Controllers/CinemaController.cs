using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBooking.Data;
using MovieBooking.Data.Services;
using MovieBooking.Data.User;
using MovieBooking.Models;

namespace MovieBooking.Controllers
{   [Authorize(Roles =UserRoles.Admin)]
    public class CinemaController : Controller
    {
        private readonly ICinemaService _service;
        public CinemaController(ICinemaService service)
        {
            _service=service;
        }
       
      [AllowAnonymous]
        public  async Task<ActionResult> index(){

            var allCinema=await _service.GetAllAsync();

            return View(allCinema);
        }
         
       
        public IActionResult create(){
            return View();
        }

        [HttpPost]

        public async Task<IActionResult>create([Bind("Logo,Name,Description")] Cinema cinema){

            if(!ModelState.IsValid){
                return View(cinema);
            }

            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(index));

        }

        [AllowAnonymous]
        public async Task<IActionResult>Details(int id){

             var cinemaDetails=await _service.GetByIdAsync(id);

             if(cinemaDetails==null) return View("NotFound");

             return View(cinemaDetails);
        }
        public async Task<IActionResult> Edit(int id){
                var cinemaDetails=await _service.GetByIdAsync(id);

             if(cinemaDetails==null) return View("NotFound");

             return View(cinemaDetails);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Logo,Description")] Cinema cinema)
        {

            if (!ModelState.IsValid)
            {

                return View(cinema);

            }
            await _service.UpdateAsync(id,cinema);
            return RedirectToAction(nameof(index));

        }
         public async Task<IActionResult> Delete(int id){
              var cinemaDetails=await _service.GetByIdAsync(id);

             if(cinemaDetails==null) return View("NotFound");
             return View (cinemaDetails);
        }

        [HttpPost,ActionName("Delete")]

        public async Task<IActionResult> Deleteconfirmed(int id)
        {  
             var cinemaDetails=await _service.GetByIdAsync(id);

             if(cinemaDetails==null) return View("NotFound");

             await _service.DeleteAsync(id);

            return RedirectToAction(nameof(index));
    }
}}