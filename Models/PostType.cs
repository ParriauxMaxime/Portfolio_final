
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    //Entity will bind our class to the table  in the database
    //Some value could be nullable ()
    public class PostType : GenericModel
    {
      public string Type {get; set;}
    }
  }
