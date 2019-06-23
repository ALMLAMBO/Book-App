﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models {
	public class Reviewer {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[MaxLength(100, ErrorMessage = "First Name cannot be more than 100 characters")]
		public string FisrtName { get; set; }
		[Required]
		[MaxLength(200, ErrorMessage = "Last Name cannot be more than 200 characters")]
		public string LastName { get; set; }
		public virtual ICollection<Review> Reviews { get; set; }
	}
}
