using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using AutoMapper;
using Core.Entity;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id) {
            var basket = await this._basketRepository.GetBasketAsync(id);
            // nếu ko có basket thì trả vê 1 CustomerBasket với id và list<ItemBasket> Empty
            return Ok(basket ?? new CustomerBasket(id)); 
        }
        [HttpPost] 
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasket) {            
            var basket = _mapper.Map<CustomerBasketDto , CustomerBasket>(customerBasket);
            var updateBasket = await this._basketRepository.UpdatebasketAsync(basket);
            return updateBasket;
        }
        [HttpDelete] 
        public async Task<bool> DeleteBasket(string id) {   
            var state =  await this._basketRepository.DeleteBasketAsync(id);
            return state;
        }
    }
}