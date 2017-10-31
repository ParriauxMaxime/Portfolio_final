using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WebService.Models
{
    public class Comment
    {
        [Key]
        public int Id {get; set;}

        public int PostId {get; set;}
        public System.DateTime creationDate { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
    }
}