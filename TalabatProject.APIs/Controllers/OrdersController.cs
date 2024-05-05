using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Service.ServiceContract;
using TalabatProject.APIs.DTOs;
using TalabatProject.APIs.Errors;
using Order = Talabat.Core.Entities.Order_Aggregate.Order;

namespace TalabatProject.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService,
			IMapper mapper
			)
		{
			_orderService = orderService;
			_mapper = mapper;
		}


		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
		{

			var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, address, orderDto.DeliveryMethodId);

			if (order is null)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(_mapper.Map<Order,OrderToReturnDto>(order));
		}

		[HttpGet] //GET:  /Api/Orders?email=ahmed.nasr@linkdev.com
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string email)
		{
			var orders = await _orderService.GetOrdersForUserAsync(email);
			return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
		}

		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpGet("{id}")] //GET:  /Api/Orders/1?email=ahmed.nasr@linkdev.com

		public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id,string email)
		{
			var order = await _orderService.GetOrderByIdForUserAsync(email,id);
			if (order is null) return NotFound(new ApiResponse(404));
			return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
		}


	}
}
