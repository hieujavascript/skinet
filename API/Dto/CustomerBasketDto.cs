using System.Collections.Generic;
namespace API.Dto
{
    public class CustomerBasketDto
    {
        public string Id { get; set; } 
        public List<BasketItemDto> items { get; set; } 
    }
}