using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    //Entity will bind our class to the table  in the database
    //Some value could be nullable ()
    public class History : GenericModel
    {
      public int PostId {get; set;}
      public int AccountId {get; set;}
      public System.DateTime CreationDate {get; set;}
      public bool Marked {get; set;}
      public string Note {get; set;}

      
    }
  }
