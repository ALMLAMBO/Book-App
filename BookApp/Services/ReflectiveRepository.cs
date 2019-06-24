using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Services
{
    public class ReflectiveRepository<T> where T : class
    {
        static private readonly Dictionary<string, string> repoNames = new Dictionary<string, string>()
        {
            { "Book", "Books" },
            { "Author", "Authors" },
            { "Review", "Reviews" },
            { "BookAuthor", "BookAuthors" },
            { "BookCategory", "BookCategories" },
            { "Country", "Countries" },
            { "Category", "Categories" },
            { "Reviewer", "Reviewers" }
        };

        private BookDbContext repository;

        private string fieldName;

        public ReflectiveRepository(BookDbContext database)
        {
            repository = database;
            repoNames.TryGetValue(typeof(T).Name, out fieldName);
        }

        public bool Save()
        {
            int saved = repository.SaveChanges();

            return saved >= 0 ? true : false;
        }

        public bool Exists(int id)
        {
            return ((DbSet<T>)repository.GetType().GetProperty(fieldName).GetValue(repository)).Any(i => (int)i.GetType().GetProperty("Id").GetValue(i) == id);
        }

        public bool Create(T item)
        {
            repository.AddAsync(item);
            return Save();
        }

        public bool Delete(T item)
        {
            repository.Remove(item);
            return Save();
        }

        public ICollection<T> Get()
        {
            return ((DbSet<T>)repository.GetType().GetProperty(fieldName).GetValue(repository))
                .OrderBy(i => i.GetType().GetProperty("Id").GetValue(i))
                .ToList();
        }

        public T GetById(int id)
        {
            return ((DbSet<T>)repository.GetType().GetProperty(fieldName).GetValue(repository))
                .Where(c => (int)c.GetType().GetProperty("Id").GetValue(c) == id)
                .FirstOrDefault();
        }

        public bool Update(T item)
        {
            repository.Update(item);
            return Save();
        }
    }
}
