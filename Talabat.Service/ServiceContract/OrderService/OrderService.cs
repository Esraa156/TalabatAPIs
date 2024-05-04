using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Service.ServiceContract.OrderService
{
    public class OrderService:IOrderService
    {
       

		Task<Order> IOrderService.CreateOrderAsync(string buyerEmail, string basketId, Address shippingAddress, string deliveryMethodId)
		{
			throw new NotImplementedException();
		}

		Task<IReadOnlyList<Order>> IOrderService.GetOrdersForUserAsync(string buyerEmail)
		{
			throw new NotImplementedException();
		}

		Task<Order> IOrderService.GetOrderById(string buyerEmail, int orderId)
		{
			throw new NotImplementedException();
		}

		Task<IReadOnlyList<DeliveryMethod>> IOrderService.GetDeliveryMethodsAsync()
		{
			throw new NotImplementedException();
		}
	}
}
