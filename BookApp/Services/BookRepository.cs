using BookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Services
{
    public class BookRepository : ReflectiveRepository<Book>
    {
        public BookRepository(BookDbContext db) : base(db)
        {

        }
    }
}
