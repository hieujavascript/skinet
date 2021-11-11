import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket, IBasketItem, IBasket } from '../shared/models/Basket';
import { BasketTotal, IBasketTotal } from '../shared/models/BasketTotal';
import { IProduct } from '../shared/models/IProduct';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.baseUrl;
  basketSource = new BehaviorSubject<IBasket>(null);
  basketTotalSource = new BehaviorSubject<IBasketTotal>(null);
  // dung basket$ này truy xuất ở mọi nơi dưa trên dữ liệu đc gán cho basketsource.next
  basket$ = this.basketSource.asObservable();
  basketTotal$ = this.basketTotalSource.asObservable();
  constructor(private http: HttpClient) { }
  getBasket(id: string) {
    return this.http.get(this.baseUrl + "basket?id="+id).pipe(map((basket:IBasket) => {
      // nơi đây để gán this.basketSource.next để basket$ có dữ liệu thay đổi ở mọi  nơi
      this.basketSource.next(basket);
      this.CaculatorBasketTotal(); // tinh  tong tien so basket
    }))
  }
  setBasket(basket:IBasket) {
    // sau khi truyen dữ liệu basket vào thân Body Request 
    // chung ta câp nhận lại response là basket mới đc cập nhật.
    const obj = {
      id: basket.id,
      items : basket.items
    }
    return this.http.post(this.baseUrl + 'basket', obj).subscribe((response: IBasket) => {
      this.basketSource.next(response);
      this.CaculatorBasketTotal(); // tinh  tong tien so basket
    }, error => {
      console.log(error);
    })

  }
  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  CaculatorBasketTotal() {
    let  basket = this.getCurrentBasketValue();
      var objBasket = new BasketTotal();
      objBasket.subTotal = basket.items.reduce<number>((a , b) => b.quantity * b.price + a  , 0);
      objBasket.total = objBasket.subTotal + objBasket.shipCode;
      this.basketTotalSource.next(objBasket);
  }

  // add Product vao trong BasketItem
  AddItemToBasket(product:IProduct , Quantity:number = 1) {
    // Mục đích KIEM TRA XEM GIO HANG BASKET CO HAY CHUA
    // nếu GIỎ HÀNG basket đã tồn tại thì trả về , còn ko thì tạo mới giỏ hàng basket với id và basketitems = null theo mặc định
    const basket = this.getCurrentBasketValue() ?? this.CreateBasket();
   
    // MÔ TA THÔNG TIN sản phẩm cụ thể trong basketItems
    const productInBasketItems: IBasketItem = this.mapProductToBasketProduct( product , Quantity );

    // nếu Sản Phẩm tồn ta trong giỏ hàng basket thì CỘNG Quantity , còn không thì thêm mới
    basket.items = this.addOrUpdateProductItem(basket.items , productInBasketItems , Quantity);
    this.setBasket(basket);
  }
  // hàm này trả về IBasket nhưng do Class Basket thực thi implement IBasket nên trả về Class Basket vẫn đc
  private CreateBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private addOrUpdateProductItem(items: IBasketItem[], productInBasketItems: IBasketItem, Quantity: number): IBasketItem[] {
   
    // kiểm tra xem sản phẩm có trong giỏ basketitems hay không
      const index = items.findIndex(x => x.id === productInBasketItems.id);           
      // neu chua co
      if(index === -1) {
        // thêm product và + quanlity vào basketitem
        productInBasketItems.quantity = Quantity;
        items.push(productInBasketItems)       
      }
      else // nếu đã tồn tại sản phẩm trong giỏ basketitems thì chỉ + Quantity cho cái Product trong baskitems đó
      {              
        console.log(items[index].quantity);
        items[index].quantity += Quantity;
      }
      return items;
  }

  private mapProductToBasketProduct(product: IProduct, Quantity: number): IBasketItem {
   return {
    id: product.id ,
    productname: product.name,
    price: product.price ,
    quantity: Quantity,
    pictureUrl: product.pictureUrl,
    brand: product.productBrand,
    type: product.productType
   }
  }
  public IncrementTotalBasket(item:IBasketItem ) {
    let basket = this.getCurrentBasketValue();
    const productIndexInBasket = basket.items.findIndex(x => x.id === item.id);
    basket.items[productIndexInBasket].quantity++;
    this.setBasket(basket);
  }
  public DecrementTotalBasket(item:IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const productIndexInBasket = basket.items.findIndex(x => x.id === item.id);
    if(basket.items[productIndexInBasket].quantity > 1 )
    {
      basket.items[productIndexInBasket].quantity--;
      this.setBasket(basket);
    }
    else {
      this.RemoveItemFromBasket(item); // remove Product in basket
    }
  }
  public RemoveItemFromBasket(item:IBasketItem) {
    // neu ko con product nao trong basket thi xoa luon basket
    const basket = this.getCurrentBasketValue();
    //Nếu vẫn con Product trong Items , kiem tra ID 1 product còn khớp với Product id trong giỏ hàng hay ko
    if(basket.items.some(x => x.id === item.id)) { // nếu còn
       basket.items = basket.items.filter(x => x.id != item.id); // gán lai BakstItem ko co ID Product bi xoa
       if(basket.items.length > 0)
       {
         this.setBasket(basket);
       }
       else { 
        console.log("vo tới đây")
        this.http.get(this.baseUrl + "basket?id="+basket.id).subscribe(() => {
          this.basketSource.next(null);
          this.basketTotalSource.next(null);
          localStorage.removeItem("basket_id");
          console.log("day la : " + this.getCurrentBasketValue);
        }, error => {
          console.log(error);
        })
      }

    }
    // đây là sản phẩm cuối cùng trong basket , Quanlity la 0 ,
    // nhấn DecrementTotalBasket để giảm Quantity nữa  . vậy ta sẽ xóa luôn basket

  }

}



