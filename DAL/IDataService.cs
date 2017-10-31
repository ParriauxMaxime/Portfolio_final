using System.Collections.Generic;
using DataAccessLayer.Repository;
using WebService.Models;
namespace DataAccessLayer
{
    public interface IDataService
    {
        //The IDataService interface must provide a way to getRepository linked to a Table
        GenericReadableRepository<Post> GetPostRepository();
        GenericReadableRepository<Comment> GetCommentRepository();
    }
}