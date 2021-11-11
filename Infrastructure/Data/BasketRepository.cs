using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entity;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis) // Su Dung Redis
        {
            // doc du lieu tu database luu tru o o cung client
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            Boolean state =  await _database.KeyDeleteAsync(basketId);
            return state;
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
           var data = await _database.StringGetAsync(basketId);
           var readJson = data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data); // Deserialize đọc dữ liệu Json ra thành string có kiểu CustomerBasket 
           return readJson;
        }

        public async Task<CustomerBasket> UpdatebasketAsync(CustomerBasket customerBasket)
        {
         var created = await _database.StringSetAsync(
                                     customerBasket.Id , 
                                     JsonSerializer.Serialize(customerBasket) , 
                                     TimeSpan.FromDays(30)); // du lieu ton tai trong 30 ngay
            // nếu lỗi chưa tạo đc thì trả về null                                 
            if(!created) return null;
            
            return await GetBasketAsync(customerBasket.Id);
        }
    }
}