using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
	public class Order:BaseEntity
	{
		public string BuyerEmail { get; set; } = null!;
		 
		public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.UtcNow;

		public OrderStatus Status { get; set; } = OrderStatus.Pending;

		public Address ShippingAddress { get; set; }= null!;

		//public int DeliveryMethodId {  get; set; }	 // Foreign Key
		
		public DeliveryMethod? DeliveryMethod { get; set; } = null!; //Navigational Property [ONE]


		public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); //Navigational Property	

		public decimal SubTotal {  get; set; }

		//[NotMapped]
		//public decimal  Total =>SubTotal + DeliveryMethod.Cost; 


		public decimal GetTotal()=> SubTotal + DeliveryMethod.Cost;

		public string PaymentIntentId { get; set;} = string.Empty;





	}
}
