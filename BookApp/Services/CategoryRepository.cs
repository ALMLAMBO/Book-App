using BookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Services
{
    public class CategoryRepository : ReflectiveRepository<Category>
    {
        public CategoryRepository(BookDbContext db) : base(db)
        {

        }
    }
}
