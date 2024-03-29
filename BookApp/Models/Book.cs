﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models {
	public class Book {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[MaxLength(200, ErrorMessage = "Title cannot be more than 200 characters")]
		public string Title { get; set; }
		public DateTime? DatePublished { get; set; }
		public virtual ICollection<BookAuthor> BookAuthors { get; set; }
		public virtual ICollection<BookCategory> BookCategories { get; set; }
	}
}
