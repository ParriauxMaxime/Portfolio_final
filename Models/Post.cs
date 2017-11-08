using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WebService.Models
{
    //Entity will bind our class to the table l_posts in the database
    //Some value could be nullable (closedDate, parentId, acceptedAnswerId)
    public class Post : GenericModel
    {
        public System.DateTime creationDate { get; set; }
        public System.DateTime? closedDate { get; set; }
        public string title { get; set; }
        public int score { get; set; }
        public string body { get; set; }
        public int? parentId { get; set; }
        public int postTypeId { get; set; }
        public int? acceptedAnswerId { get; set; }
        public int userId { get; set; }
    }
}