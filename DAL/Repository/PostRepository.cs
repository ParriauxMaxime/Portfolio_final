/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IPostRepository : IDisposable
    {
        IEnumerable<Post> GetPosts();
        Post GetPostByID(int PostId);
        void InsertPost(Post Post);
        void DeletePost(int PostID);
        void UpdatePost(Post Post);
        void Save();
    }

    public class PostRepository : IPostRepository, IDisposable
    {
        private DatabaseContext context;
        public PostRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Post> GetPosts(int page = 0, int pageSize = 50)
        {
            return this.context.Posts.OrderBy(x => x.Id)
                        // .Skip(page * pageSize)
                         //.Take(pageSize)
                         .Take(50)
                         .ToList(); ;
        }
        public Post GetPostByID(int PostId)
        {
            return this.context.Posts.Find(PostId);
        }
        public void InsertPost(Post Post)
        {

        }
        public void DeletePost(int PostID)
        {

        }
        public void UpdatePost(Post Post)
        {

        }
        public void Save()
        {

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
}*/