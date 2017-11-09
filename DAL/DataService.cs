using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using DataAccessLayer.Repository;
using DataAccessLayer;
using Models;

namespace DataAccessLayer
{
    //See IDataService.cs for explanation on Get<Models>Repository 
    public class DataService : IDataService
    {
        //If you don't know what is the context, you should read DataBaseContext.cs
        private DatabaseContext context = new DatabaseContext();
        public GenericWritableRepository<Account> accountRepository {get;}
        public GenericReadableRepository<Comment> commentRepository {get;}
        public GenericWritableRepository<History> historyRepository {get;}
        public GenericReadableRepository<LinkPost> linkPostRepository {get;}
        public GenericReadableRepository<LTagsPost> ltagsPostRepository {get;}
        public GenericReadableRepository<Post> postRepository {get;}
        public GenericReadableRepository<PostType> postTypeRepository {get;}
        public GenericWritableRepository<QueryHistory> queryHistoryRepository {get;}
        public GenericReadableRepository<Tag> tagRepository {get;}
        public GenericReadableRepository<User> userRepository {get;}


        public DataService() {
            //The constructor must instanciate everyRepository
            this.accountRepository = new GenericWritableRepository<Account>(context);
            this.commentRepository = new GenericReadableRepository<Comment>(context);
            this.historyRepository = new GenericWritableRepository<History>(context);
            this.linkPostRepository = new GenericReadableRepository<LinkPost>(context);
            this.ltagsPostRepository = new GenericReadableRepository<LTagsPost>(context);
            this.postRepository = new GenericReadableRepository<Post>(context);
            this.postTypeRepository = new GenericReadableRepository<PostType>(context);
            this.queryHistoryRepository = new GenericWritableRepository<QueryHistory>(context);
            this.tagRepository = new GenericReadableRepository<Tag>(context);
            this.userRepository = new GenericReadableRepository<User>(context);
            //See DAL/GenericRepository.cs for more explanation
        }
        public GenericWritableRepository<Account> GetAccountRepository() {
            //At some point, you should read Models/Account.cs
            return this.accountRepository;
        }
        public GenericReadableRepository<Comment> GetCommentRepository() {
            //At some point, you should read Models/Comment.cs
            return this.commentRepository;
        }
        public GenericWritableRepository<History> GetHistoryRepository() {
            //At some point, you should read Models/History.cs
            return this.historyRepository;
        }
        public GenericReadableRepository<LinkPost> GetLinkPostRepository() {
            //At some point, you should read Models/LinkPost.cs
            return this.linkPostRepository;
        }
        public GenericReadableRepository<LTagsPost> GetLTagsPostRepository() {
            //At some point, you should read Models/LTagsPost.cs
            return this.ltagsPostRepository;
        }
        public GenericReadableRepository<Post> GetPostRepository() {
            //At some point, you should read Models/Post.cs
            return this.postRepository;
        }

        public GenericReadableRepository<PostType> GetPostTypeRepository() {
            //At some point, you should read Models/PostType.cs
            return this.postTypeRepository;
        }
        public GenericWritableRepository<QueryHistory> GetQueryHistoryRepository() {
            //At some point, you should read Models/QueryHistory.cs
            return this.queryHistoryRepository;
        }
        public GenericReadableRepository<Tag> GetTagRepository() {
            //At some point, you should read Models/Tag.cs
            return this.tagRepository;
        }
        public GenericReadableRepository<User> GetUserRepository() {
            //At some point, you should read Models/User.cs
            return this.userRepository;
        }

        public List<int> SearchInPosts(string query, int questionOnly, int numberLimit) {
            return Procedures.SearchInPosts(query, questionOnly, numberLimit);
        }

        public List<int> GetPostsByUser(string user)
        {
            return Procedures.GetPostsByUser(user);
        }

        public List<int> GetPostsByTag(string tag)
        {
            return Procedures.GetPostsByTag(tag);
        }

        public List<int> SearchHistoryForAccount(string user, int limitNumber)
        {
            return Procedures.SearchHistoryForAccount(user, limitNumber);
        }
        
        public List<int> SearchQueryHistoryForAccount(string user, int limitNumber)
        {
            return Procedures.SearchQueryHistoryForAccount(user, limitNumber);
        }
    }
}