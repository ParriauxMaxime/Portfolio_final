using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{

    //Entity will bind our class to the table l_comments in the database
    public class Comment : GenericModel
    {
        public int PostId {get; set;}
        public System.DateTime creationDate { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
    }
}