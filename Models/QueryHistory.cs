
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    //Entity will bind our class to the table  in the database
    //Some value could be nullable ()
    public class QueryHistory : GenericModel
    {
      public int AccountId {get; set;}
      public string Query {get; set;}
      public System.DateTime CreationDate {get; set;}
      
    }
  }
