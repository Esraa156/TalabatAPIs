﻿using System.ComponentModel.DataAnnotations;

namespace TalabatProject.APIs.DTOs
{
	public class AddressDto
	{

		[Required]
		public string FirstName { get; set; } = null!;
		[Required]


		public string LnameName { get; set; } = null!;
		[Required]

		public string Street { get; set; } = null!;
		[Required]

		public string Country { get; set; } = null!;
		[Required]

		public string City { get; set; } = null!;


	}
}