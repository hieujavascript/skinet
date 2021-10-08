using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id) {
            var basket = await this._basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id)); // nếu ko có basket thì trả vê 1 CustomerBasket với list Empty
        }
        [HttpPost] 
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket customerBasket) {
            var updateBasket = await this._basketRepository.UpdatebasketAsync(customerBasket);
            return updateBasket;
        }
        [HttpDelete] 
        public async Task<bool> DeleteBasket(string id) {   
            var state =  await this._basketRepository.DeleteBasketAsync(id);
            return state;
        }
    }
}