using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Data;
using MovieBooking.Data.User;
using MovieBooking.Data.ViewModels;
using MovieBooking.Models;

namespace MovieBooking.Controllers
{
    public class AccountController : Controller
        {
        private readonly UserManager<ApplicationUser>_userManager;

        private readonly SignInManager<ApplicationUser>_singInManager;

        private readonly AppDbContext _context;

     public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> singInManager,AppDbContext context)

     {
        _userManager=userManager;
        _singInManager=singInManager;
        _context=context;
        
     }
      public IActionResult Login(){

        var response= new LoginVm();

        return View(response);
      }
      
      [HttpPost]
      public async Task<IActionResult>Login(LoginVm loginVm){

        if(!ModelState.IsValid) return View (loginVm);
        
        var user=await _userManager.FindByEmailAsync(loginVm.EmailAddress);
        if(user !=null){


          var PasswordCheck=await _userManager.CheckPasswordAsync(user,loginVm.Password);
          if(PasswordCheck){
            var result=await _singInManager.PasswordSignInAsync(user,loginVm.Password,false,false);
            if(result.Succeeded){
                return RedirectToAction("Index","Movies");
            }
          }
          TempData["Error"]="Incorrect Credentials.please ! try again";
       return View(loginVm);
        }
       
       TempData["Error"]="Incorrect Credentials.please ! try again";
       return View(loginVm);

      }

      public IActionResult Register(){

        var response= new RegisterVm();

        return View(response);
      }
        
        [HttpPost]

        public async Task< IActionResult> Register(RegisterVm registerVm){

            if(!ModelState.IsValid) return View(registerVm);
             var user=await _userManager.FindByEmailAsync(registerVm.EmailAddress);

             if(user!= null){

                TempData["Error"]="Email Address is already in use";

                return View(registerVm);

             }

             var newuser=new ApplicationUser(){
                FullName=registerVm.FullName,
                Email=registerVm.EmailAddress,
                UserName=registerVm.EmailAddress,

             };

             var newUserResponse=await _userManager.CreateAsync(newuser,registerVm.Password);
             if(newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newuser,UserRoles.User);

                return View("RegisterCompleted");
             

        }

        [HttpPost]

        public async Task<IActionResult> Logout(){
          await _singInManager.SignOutAsync();

          return RedirectToAction("Index","Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl){

          return View();
        }
    }
}