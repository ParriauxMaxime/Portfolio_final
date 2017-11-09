using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

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
            //string connection = "server=localhost;database=stackoverflow_sample_universal;uid=stackoverflow;pwd=password";
            string connection = "server=wt-220.ruc.dk;database=raw3;uid=raw3;pwd=raw3";
            try {
                optionsBuilder.UseMySql(connection);            
            }
            catch (Exception) {
                throw new Exception("Cannot join the Database.\nLook that your bdd accept this connection string :" + connection);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //This is the best quickfix on human history
            //Just override the name of the table our class must represent
            modelBuilder.Entity<Account>().ToTable("accounts");
            modelBuilder.Entity<Comment>().ToTable("l_comments");
            modelBuilder.Entity<History>().ToTable("history");
            modelBuilder.Entity<LinkPost>().ToTable("linkPosts");
            modelBuilder.Entity<LTagsPost>().ToTable("l_tags_posts");
            modelBuilder.Entity<Post>().ToTable("l_posts");
            modelBuilder.Entity<PostType>().ToTable("postType");
            modelBuilder.Entity<QueryHistory>().ToTable("queryHistory");
            modelBuilder.Entity<Tag>().ToTable("tags").Property(p => p.TagName).HasColumnName("tag");
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}