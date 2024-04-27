﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using TalabatProject.APIs.DTOs;
using TalabatProject.APIs.Errors;

namespace TalabatProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet] //GET : /api/basket?id=
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost] //POST : /api/basket
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdatedBasket is null) { return BadRequest(new ApiResponse(400)); }
            return Ok(createdOrUpdatedBasket);
        }
        [HttpDelete] //DELETE : /api/basket?id
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }

    }
}

