using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MovieBooking.Models;

namespace MovieBooking.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Actor_movie>().HasKey(am=>new {
                am.ActorId,
                am.MovieId
             });

             modelBuilder.Entity<Actor_movie>().HasOne(m=>m.Movie).WithMany(am=>am.Actor_Movies).HasForeignKey(m=>
             m.MovieId);
             modelBuilder.Entity<Actor_movie>().HasOne(m=>m.Actor).WithMany(am=>am.Actor_Movies).HasForeignKey(m=>
             m.ActorId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Actor>Actors{get;set;}
        public DbSet<Movie>Movies{get;set;}
        public DbSet<Actor_movie>Actor_Movies{get;set;}
        public DbSet<Cinema>Cinemas{get;set;}
        public DbSet<Producer>Producers{get;set;}

       public DbSet<Order>Orders{get;set;}
       public DbSet<OrderItem>OrderItems{get;set;}

       public DbSet<ShoppingCartItem>ShoppingCartItems{get;set;}



        
    }
}
