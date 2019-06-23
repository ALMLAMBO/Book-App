using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models {
	public class Review {
		public int Id { get; set; }
		public string HeadLine { get; set; }
		public string ReviewText { get; set; }
		public int Rating { get; set; }
	}
}
