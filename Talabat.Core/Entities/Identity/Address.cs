namespace Talabat.Core.Entities.Identity
{
	public class Address
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = null!;
		public string LnameName { get; set; } = null!;
		public string Street { get; set; } = null!;
		public string Country { get; set; } = null!;
		public string City { get; set; } = null!;

		public string ApplicationUserId { get; set; }//Foreign Key
		public ApplicationUser User { get; set; } = null!;//Navigational Property [ONE]



	}
}