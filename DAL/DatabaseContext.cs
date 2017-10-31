using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebService.Models;

namespace DataAccessLayer
{
    //Shit happens, magic occure, please don't panic
    public class DatabaseContext : DbContext
    {
        //See Microsoft.EntityFrameworkCore.DbContext to understand overrided method OnConfiguring ant on OnModelCreating
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //We just say to Entity to get the stuff from the MySql database with a string connection
            optionsBuilder.UseMySql("server=localhost;database=stackoverflow_sample_universal;uid=stackoverflow;pwd=password");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //This is the best quickfix on human history
            //Just override the name of the table our class must represent
            modelBuilder.Entity<Post>().ToTable("l_posts");
            modelBuilder.Entity<Comment>().ToTable("l_comments");
        }
    }
}