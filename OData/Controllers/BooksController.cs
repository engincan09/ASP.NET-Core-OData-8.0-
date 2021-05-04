using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Controllers
{
    public class BooksController : ODataController
    {
        private ExampleContext _db;

        public BooksController(ExampleContext context)
        {
            _db = context;
            if (context.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Press.Add(b.Press);
                }
                context.SaveChanges();
            }
        }
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Books);
        }
        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_db.Books.FirstOrDefault(c => c.Id == key));
        }
    }
}
