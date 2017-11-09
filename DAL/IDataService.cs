using System.Collections.Generic;
using DataAccessLayer.Repository;
using Models;
namespace DataAccessLayer
{
    public interface IDataService
    {
        //The IDataService interface must provide a way to getRepository linked to a Table
        GenericWritableRepository<Account> GetAccountRepository();
        GenericReadableRepository<Comment> GetCommentRepository();
        GenericWritableRepository<History> GetHistoryRepository();
        GenericReadableRepository<LinkPost> GetLinkPostRepository();
        GenericReadableRepository<LTagsPost> GetLTagsPostRepository();
        GenericReadableRepository<Post> GetPostRepository();
        GenericReadableRepository<PostType> GetPostTypeRepository();
        GenericWritableRepository<QueryHistory> GetQueryHistoryRepository();
        GenericReadableRepository<Tag> GetTagRepository();
        GenericReadableRepository<User> GetUserRepository();
        List<int> SearchInPosts(string query, int questionOnly, int numberLimit);
        List<int> GetPostsByUser(string user);
        List<int> GetPostsByTag(string tag);
    }
}