using BookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Services
{
    public class AuthorRepository : ReflectiveRepository<Author>
    {
        public AuthorRepository(BookDbContext db) : base(db)
        {
            
        }
    }
}
