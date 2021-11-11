using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class CustomerBasket
    {
         public string Id { get; set; } 
        public List<BasketItem> items { get; set; } = new List<BasketItem>();
         // khi khởi tạo Initial CustomerBasket chúng ta phải truyền vào Constructor với id
         // và mặc định là chúng ta sẽ có Basket với Id và List<BasketItem> mà ko cần truyền list đó vào contructor
        public CustomerBasket(string id)
        {
            Id = id;
        }
      
    }
}