using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repository;
using DataAccessLayer;
using WebService.Models;

namespace DataAccessLayer
{
    //See IDataService.cs for explanation on Get<Models>Repository 
    public class DataService : IDataService
    {
        //If you don't know what is the context, you should read DataBaseContext.cs
        private DatabaseContext context = new DatabaseContext();
        public GenericReadableRepository<Post> postRepository {get;}
        public GenericReadableRepository<Comment> commentRepository {get;}

        public DataService() {
            //The constructor must instanciate everyRepository
            this.postRepository = new GenericReadableRepository<Post>(context);
            this.commentRepository = new GenericReadableRepository<Comment>(context);
            //See DAL/GenericRepository.cs for more explanation
        }


        public GenericReadableRepository<Post> GetPostRepository() {
            //At some point, you should read Models/Post.cs
            return this.postRepository;
        }
        public GenericReadableRepository<Comment> GetCommentRepository() {
            //At some point, you should read Models/Comment.cs
            return this.commentRepository;
        }
    }
}