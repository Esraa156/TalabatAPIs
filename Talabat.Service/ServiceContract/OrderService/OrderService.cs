using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Service.ServiceContract.OrderService
{
    public class OrderService:IOrderService
    {
		private readonly IBasketRepository _basketRepo;
		private readonly IGenericRepository<Product> _ProductRepo;
		private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
		private readonly IGenericRepository<Order> _orderRepo;

		public OrderService(IBasketRepository basketRepo,
			IGenericRepository<Product> Products,
			IGenericRepository<DeliveryMethod> deliveryMethodRepo,
			IGenericRepository<Order> orderRepo



			)

		{
			_basketRepo = basketRepo;
			_ProductRepo = Products;
			_deliveryMethodRepo= deliveryMethodRepo;
			_orderRepo = orderRepo;
		}


        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, Address shippingAddress, int deliveryMethodId)
		{
			//1. Get Basket From Basket Repo

			var basket = await _basketRepo.GetBasketAsync(basketId);

			//2. Get Selected Items at Basket from Products Repo

			var OrderItems=new List<OrderItem>();
			if(basket?.Items?.Count()>0)
			{
				foreach (var item in basket.Items)
				{
					var Product = await _ProductRepo.GetAsync(item.Id);
					var ProductItemOrdered = new ProductItemOrdered(item.Id, Product.Name, Product.PictureUrl);
					var OrderItem = new OrderItem(ProductItemOrdered,item.Quantity, Product.Price);

					OrderItems.Add(OrderItem);
				}
			}

			//3. Calculate SubTotal

			var subTotal= OrderItems.Sum(item=>item.Price*item.Quantity);



			//4. Get Delivery Method From Delivery Method Repo

			//var deliveryMethod = _deliveryMethodRepo.GetAsync(deliveryMethodId);

			//5.Create Order

			var order = new Order(
				
				buyerEmail: buyerEmail,
				deliveryMethodId: deliveryMethodId,
				shippingAddress: shippingAddress,
				items: OrderItems,
				subTotal: subTotal
		
				);

			//var Order = new Order();

			// Save To Database



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
