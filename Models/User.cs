
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    //Entity will bind our class to the table  in the database
    //Some value could be nullable (CreationDate, Location, Age)
    public class User
    {
      [Key]
      public int Id {get; set;}
      public string DisplayName {get; set;}
      public System.DateTime? CreationDate {get; set;}
      public string Location {get; set;}
      public int? Age {get; set;}
      
    }
  }
