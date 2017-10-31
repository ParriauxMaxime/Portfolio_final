using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccessLayer
{

    //Abstract class for Table
    //Every Table object will contain an id
    public abstract class ITable
    {
        public int id { get; set; }
    }

    //Abstract class for read access Table
    //Every TableReadable will contain a GetAll, Get, GetNumber, function 
}