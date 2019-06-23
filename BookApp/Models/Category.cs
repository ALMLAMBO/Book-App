using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models {
	public class Category {
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<BookCategory> BookCategories { get; set; }
	}
}
