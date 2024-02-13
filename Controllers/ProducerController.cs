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
    public class ProducerController:Controller
    {
          private readonly IProducersService _service;
        public ProducerController(IProducersService service)
        {
            _service=service;
        }
         [AllowAnonymous]
         public async Task<IActionResult> Index(){

            var allProducer=await _service.GetAllAsync();

            return View(allProducer);
        }
         public IActionResult Create(){
        return View();
      }

      [HttpPost]

      public async Task<IActionResult> create([Bind("FullName,ProfilePictureUrl,Bio")] Producer producer){

        if(!ModelState.IsValid){
            return View(producer);
        }
        await _service.AddAsync(producer);
        return RedirectToAction(nameof(Index));

      }

     
      [AllowAnonymous]
        public async Task<IActionResult>Details(int id){
            var ProducerDetails=await _service.GetByIdAsync(id);
            if(ProducerDetails  ==null) return View("NotFound");

            return View(ProducerDetails);
        }
    

    public async Task<IActionResult> Edit(int id){
              var ProducerDetails=await _service.GetByIdAsync(id);

             if(ProducerDetails==null) return View("NotFound");
             return View (ProducerDetails);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureUrl,Bio")] Producer producer)
        {

            if (!ModelState.IsValid)
            {

                return View(producer);

            }
            if(id == producer.Id){
             await _service.UpdateAsync(id,producer);
            return RedirectToAction(nameof(Index));
            }

            return View(producer);
           

        }
         public async Task<IActionResult> Delete(int id){
              var producerDetails=await _service.GetByIdAsync(id);

             if(producerDetails==null) return View("NotFound");
             return View (producerDetails);
        }

        [HttpPost,ActionName("Delete")]

        public async Task<IActionResult> Deleteconfirmed(int id)
        {  
             var actorDetails=await _service.GetByIdAsync(id);

             if(actorDetails==null) return View("NotFound");

             await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

    
    }
}

}