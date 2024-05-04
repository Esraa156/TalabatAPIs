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
		Task<Order> CreateOrderAsync(string buyerEmail, string basketId, Address shippingAddress, string deliveryMethodId);
		Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

		Task<Order> GetOrderById(string buyerEmail, int orderId);
		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();


	}
}
