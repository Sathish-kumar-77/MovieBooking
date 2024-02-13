using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Data;
using MovieBooking.Data.Services;
using MovieBooking.Data.User;
using MovieBooking.Models;

namespace MovieBooking.Controllers
{   
    [Authorize(Roles =UserRoles.Admin)]
    public class ActorsController : Controller
    {
       private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service=service;
        }
      
      [AllowAnonymous]
        public async Task< IActionResult> index(){

            var data=await _service.GetAllAsync();

            return View(data);
            
        }

        public IActionResult Create(){
            return View ();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")] Actor actor)
        {

            if (!ModelState.IsValid)
            {

                return View(actor);

            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(index));

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult>Details(int id){

             var actorDetails=await _service.GetByIdAsync(id);

             if(actorDetails==null) return View("NotFound");

             return View(actorDetails);
        }
        public async Task<IActionResult> Edit(int id){
              var actorDetails=await _service.GetByIdAsync(id);

             if(actorDetails==null) return View("NotFound");
             return View (actorDetails);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureUrl,Bio")] Actor actor)
        {

            if (!ModelState.IsValid)
            {

                return View(actor);

            }
            await _service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(index));

        }
         public async Task<IActionResult> Delete(int id){
              var actorDetails=await _service.GetByIdAsync(id);

             if(actorDetails==null) return View("NotFound");
             return View (actorDetails);
        }

        [HttpPost,ActionName("Delete")]

        public async Task<IActionResult> Deleteconfirmed(int id)
        {  
             var actorDetails=await _service.GetByIdAsync(id);

             if(actorDetails==null) return View("NotFound");

             await _service.DeleteAsync(id);

            return RedirectToAction(nameof(index));

    
    }
}
}