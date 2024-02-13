using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MovieBooking.Data.User;
using MovieBooking.Models;

namespace MovieBooking.Data
{
    public class AddDbInitializer

    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context=serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //cinema
                if(!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>(){

                    new Cinema(){
                        Name="Cinema1",
                        Logo="http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                        Description="this is the description of first cinema"
                    },
                     new Cinema(){
                        Name="Cinema2",
                        Logo="http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                        Description="this is the description of second cinema"
                    },
                     new Cinema(){
                        Name="Cinema3",
                        Logo="http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                        Description="this is the description of third cinema"
                    }

                    });
                  context.SaveChanges();
                }
                //actors
                 if(!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>(){

                        new Actor(){
                            FullName="Actor 1",
                            Bio="this is Bio of the First Actor",
                           ProfilePictureUrl="http://dotnethow.net/images/actor/actor-1.jpeg"
                        },
                         new Actor(){
                            FullName="Actor 2",
                            Bio="this is Bio of the Second Actor",
                           ProfilePictureUrl="http://dotnethow.net/images/actor/actor-1.jpeg"
                        },
                         new Actor(){
                            FullName="Actor 2",
                            Bio="this is Bio of the thrid Actor",
                           ProfilePictureUrl="http://dotnethow.net/images/actor/actor-1.jpeg"
                        }

                    });

                    context.SaveChanges();
                }
                //Producers
                   if(!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>(){

                        new Producer(){

                            FullName="Producer 1",
                            Bio="this is the Bio of first Producer ",
                            ProfilePictureUrl="http://dotnethow.net/images/actor/producer-1.jpeg"
                        },
                         new Producer(){

                            FullName="Producer 2",
                            Bio="this is the Bio of second Producer ",
                            ProfilePictureUrl="http://dotnethow.net/images/actor/producer-1.jpeg"
                        },
                        new Producer(){

                            FullName="Producer 3",
                            Bio="this is the Bio of Third Producer ",
                            ProfilePictureUrl="http://dotnethow.net/images/actor/producer-1.jpeg"
                        }

                    });
                    context.SaveChanges();

                }
                  
                //Movies
                   if(!context.Movies.Any())

                {
                    context.Movies.AddRange(new List<Movie>(){

                        new Movie(){
                            Name="Race",
                            Description="This is the Race Movie Describition",
                            Price=39,
                            ImageUrl="http://dotnethow.net/images/movies/movie-6.jpeg",
                            StartDate=DateTime.Now,
                            EndDate=DateTime.Now.AddDays(7),
                            CinemaId=1,
                            ProducerId=1,
                            MovieCategory=MovieCategory.Documentary
                        },
                         new Movie(){
                            Name="Ghost",
                            Description="This is the Ghost Movie Describition",
                            Price=39,
                            ImageUrl="http://dotnethow.net/images/movies/movie-6.jpeg",
                            StartDate=DateTime.Now,
                            EndDate=DateTime.Now.AddDays(7),
                            CinemaId=1,
                            ProducerId=1,
                            MovieCategory=MovieCategory.Documentary
                        },
                         new Movie(){
                            Name="Wolf",
                            Description="This is the wolf Movie Describition",
                            Price=45,
                            ImageUrl="http://dotnethow.net/images/movies/movie-6.jpeg",
                            StartDate=DateTime.Now,
                            EndDate=DateTime.Now.AddDays(7),
                            CinemaId=1,
                            ProducerId=1,
                            MovieCategory=MovieCategory.Documentary
                        }

                    });
                    context.SaveChanges();
                }
                //Actors& Movies
                   if(!context.Actor_Movies.Any())
                {
                    context.Actor_Movies.AddRange(new List<Actor_movie>(){


                        new Actor_movie(){

                            ActorId=1,
                            MovieId=2
                        },

                        new Actor_movie(){

                            ActorId=1,
                            MovieId=1


                        },
                        
                        new Actor_movie(){

                            ActorId=2,
                            MovieId=2


                        },

                    });

                  
                }

          }
        }
    
    public static async Task SeedUserAndRoleAsync(IApplicationBuilder applicationBuilder){

        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()){
            
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if(!await roleManager.RoleExistsAsync(UserRoles.Admin)){
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

             if(!await roleManager.RoleExistsAsync(UserRoles.User)){
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User)); 


             //users
             var UserManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
             var adminUser=await UserManager.FindByEmailAsync("admin@gmail.com");
             if(adminUser ==null){

                var newAdminUser=new ApplicationUser(){

                    FullName="Admin User",
                    UserName="admin",
                    Email="admin@gmail.com",
                    EmailConfirmed=true
                };
                await UserManager.CreateAsync(newAdminUser,"Coding@1234?");
                await UserManager.AddToRoleAsync(newAdminUser,UserRoles.Admin);
             }
              var AppUser=await UserManager.FindByEmailAsync("user@gmail.com");
             if(AppUser ==null){

                var newAppUser=new ApplicationUser(){

                    FullName="Application User",
                    UserName="User",
                    Email="user@gmail.com",
                    EmailConfirmed=true
                };
                await UserManager.CreateAsync(newAppUser,"Coding@1234?");
                await UserManager.AddToRoleAsync(newAppUser,UserRoles.User);

             }
            

            }
        }
    }
    }

}}