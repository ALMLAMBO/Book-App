using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models {
	public class Reviewer {
		public int Id { get; set; }
		public string FisrtName { get; set; }
		public string LastName { get; set; }
		public virtual ICollection<Review> Reviews { get; set; }
	}
}
