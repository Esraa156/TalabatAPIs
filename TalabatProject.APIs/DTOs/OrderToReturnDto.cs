using Talabat.Core.Entities.Order_Aggregate;

namespace TalabatProject.APIs.DTOs
{
	public class OrderToReturnDto
	{
		public int Id { get; set; }
		public string BuyerEmail { get; set; } = null!;


		public string Status { get; set; } 

		public Address ShippingAddress { get; set; } = null!;

		public string DeliveryMethod { get; set; } = null!; 

		public decimal DeliveryMethodCost { get; set; } 

		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>(); //Navigational Property	

		public decimal SubTotal { get; set; }

	
		public decimal Total{ get; set; }

		public string PaymentIntentId { get; set; } = string.Empty;



	}
}
