using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repository;
using DataAccessLayer;
using WebService.Models;

namespace DataAccessLayer
{
    public class DataService : IDataService
    {
        private DatabaseContext context = new DatabaseContext();
        public GenericReadableRepository<Post> postRepository {get;}
        public GenericReadableRepository<Comment> commentRepository {get;}
        public GenericReadableRepository<Post> GetPostRepository() {
            return this.postRepository;
        }
        public GenericReadableRepository<Comment> GetCommentRepository() {
            return this.commentRepository;
        }
        public DataService() {
            this.postRepository = new GenericReadableRepository<Post>(context);
            this.commentRepository = new GenericReadableRepository<Comment>(context);
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}