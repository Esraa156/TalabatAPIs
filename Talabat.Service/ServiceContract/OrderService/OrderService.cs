using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.OrderSpecifications;

namespace Talabat.Service.ServiceContract.OrderService
{
    public class OrderService: IOrderService
    {
		private readonly IBasketRepository _basketRepo;
		//private readonly IGenericRepository<Product> _ProductRepo;
		//private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
		//private readonly IGenericRepository<Order> _orderRepo;

		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IBasketRepository basketRepo,IUnitOfWork unitOfWork
			//IGenericRepository<Product> Products,
			//IGenericRepository<DeliveryMethod> deliveryMethodRepo,
			//IGenericRepository<Order> orderRepo



			)

		{
			_unitOfWork = unitOfWork;
			_basketRepo = basketRepo;
			//_ProductRepo = Products;
			//_deliveryMethodRepo= deliveryMethodRepo;
			//_orderRepo = orderRepo;
		}


        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, Address shippingAddress, int deliveryMethodId)
		{
			//1. Get Basket From Basket Repo

			var basket = await _basketRepo.GetBasketAsync(basketId);

			//2. Get Selected Items at Basket from Products Repo

			var OrderItems=new List<OrderItem>();
			if(basket?.Items?.Count()>0)
			{
				foreach (var item in basket.Items)
				{
					var Product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
					var ProductItemOrdered = new ProductItemOrdered(item.Id, Product.Name, Product.PictureUrl);
					var OrderItem = new OrderItem(ProductItemOrdered,item.Quantity, Product.Price);

					OrderItems.Add(OrderItem);
				}
			}

			//3. Calculate SubTotal

			var subTotal= OrderItems.Sum(item=>item.Price*item.Quantity);



			//4. Get Delivery Method From Delivery Method Repo

			var deliveryMethod = _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

			//5.Create Order

			var order = new Order(
				
				buyerEmail: buyerEmail,
				deliveryMethodId: deliveryMethodId,
				shippingAddress: shippingAddress,
				items: OrderItems,
				subTotal: subTotal
		
				);

			 _unitOfWork.Repository<Order>().Add(order);

			//var Order = new Order();

			//6. Save To Database

			var result=await _unitOfWork.CompleteAsync();

			if (result <= 0)
			{
				return null;
			}
			return order;

			throw new NotImplementedException();

		}

		async Task<IReadOnlyList<Order>> IOrderService.GetOrdersForUserAsync(string buyerEmail)
		{
			var OrderRepo =  _unitOfWork.Repository<Order>();

			var spec = new OrderSpecification(buyerEmail);
			var orders = await OrderRepo.GetAllAsyncSpec(spec);
			return orders;

		}

		async Task<Order?> IOrderService.GetOrderByIdForUserAsync(string buyerEmail, int orderId)
		{
			var OrderRepo = _unitOfWork.Repository<Order>();
			var orderSpec = new OrderSpecification(buyerEmail, orderId);


			var Order = await OrderRepo.GetAsyncSpec(orderSpec);

			return Order;
		}

		async Task<IReadOnlyList<DeliveryMethod>> IOrderService.GetDeliveryMethodsAsync()
		
			=>await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
		


	}
}
