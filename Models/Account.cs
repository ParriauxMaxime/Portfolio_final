
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    //Entity will bind our class to the table  in the database
    //Some value could be nullable ()
    public class Account
    {
      [Key]
      public int Id {get; set;}
      public string Name {get; set;}
      public System.DateTime CreationDate {get; set;}
      
    }
  }
