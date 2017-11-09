
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    //Entity will bind our class to the table  in the database
    //Some value could be nullable (CreationDate, Location, Age)
    public class User : GenericModel
    {
      public string DisplayName {get; set;}
      public System.DateTime? CreationDate {get; set;}
      public string Location {get; set;}
      public int? Age {get; set;}
      
    }
  }
