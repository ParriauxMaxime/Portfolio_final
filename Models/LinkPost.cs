
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    //Entity will bind our class to the table  in the database
    //Some value could be nullable ()
    public class LinkPost : GenericModel
    {
      public int PostId {get; set;}
      public int LinkPostId {get; set;}
      
    }
  }
