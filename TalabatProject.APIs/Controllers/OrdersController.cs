﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Service.ServiceContract;
using TalabatProject.APIs.DTOs;
using TalabatProject.APIs.Errors;

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


		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
		{

			var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, address, orderDto.DeliveryMethodId);

			if (order is null)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(order);
		}
	}
}