using System.Collections.Generic;
using DataAccessLayer.Repository;
using WebService.Models;
namespace DataAccessLayer
{
    public interface IDataService
    {
        GenericReadableRepository<Post> GetPostRepository();
        GenericReadableRepository<Comment> GetCommentRepository();
    }
}