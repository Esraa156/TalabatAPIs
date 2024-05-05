using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Service.ServiceContract
{
	public interface IOrderService
	{
		public Task<Order> CreateOrderAsync(string buyerEmail, string basketId, Address shippingAddress, int deliveryMethodId);
		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

		public Task<Order> GetOrderById(string buyerEmail, int orderId);
		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();


	}
}
