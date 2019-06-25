using BookApp.Models;
using BookApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp {
	public static class DbSeedingClass {
		public static void SeedDataContext(this BookDbContext context) {
			var booksAuthors = new List<BookAuthor>() {
				new BookAuthor() {
					Book = new Book() {
						Title = "The Call Of The Wild",
						DatePublished = new DateTime(1903,1,1),
						BookCategories = new List<BookCategory>() {
							new BookCategory { Category = new Category() { Name = "Action"}}
						}
					}
				},
				new BookAuthor() {
					Book = new Book() {
						Title = "Winnetou",
						DatePublished = new DateTime(1878,10,1),
						BookCategories = new List<BookCategory>() {
							new BookCategory { Category = new Category() { Name = "Western"}}
						}
					},
					Author = new Author() {
						FirstName = "Karl",
						LastName = "May",
					}
				},
				new BookAuthor() {
					Book = new Book() {
						Title = "Pavols Best Book",
						DatePublished = new DateTime(2019, 2, 2),
						BookCategories = new List<BookCategory>() {
							new BookCategory { Category = new Category() { Name = "Educational"}},
							new BookCategory { Category = new Category() { Name = "Computer Programming"}}
						},
					},
					Author = new Author() {
						FirstName = "Pavol",
						LastName = "Almasi",
					}
				},
				new BookAuthor() {
					Book = new Book() {
						Title = "Three Musketeers",
						DatePublished = new DateTime(2019,2,2),
						BookCategories = new List<BookCategory>() {
							new BookCategory { Category = new Category() { Name = "Action"}},
							new BookCategory { Category = new Category() { Name = "History"}}
						}
					},
					Author = new Author() {
						FirstName = "Alexander",
						LastName = "Dumas",
					}
				},
				new BookAuthor() {
					Book = new Book() {
						Title = "Big Romantic Book",
						DatePublished = new DateTime(1879,3,2),
						BookCategories = new List<BookCategory>() {
							new BookCategory { Category = new Category() { Name = "Romance"}}
						},
					},
					Author = new Author() {
						FirstName = "Anita",
						LastName = "Powers"
					}
				}
			};

			context.BookAuthors.AddRange(booksAuthors);
			context.SaveChanges();
		}
        
	}
}
