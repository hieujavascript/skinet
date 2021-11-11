import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/IProduct';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  productid: number = 0;
  product: IProduct;
  Quantity = 1;
  constructor(private shopService: ShopService , 
    private activeRoute: ActivatedRoute,
    private breadcrumb : BreadcrumbService , 
    private basketService: BasketService
    ) {
      this.breadcrumb.set("@ProductDetails" , " ");
    }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    this.shopService.getProduct(+this.activeRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.product = response;
      this.breadcrumb.set("@productDetails" , ' '); //set("@productDetails" , ' co khoảng trắng ') 
    })
  }
  AddItemToBasket() {
    this.basketService.AddItemToBasket(this.product , this.Quantity);
  }
  IncrementTotalBasket() {
  
    this.Quantity ++;
  }
  DecrementTotalBasket() {
  this.Quantity > 1 ? this.Quantity-- : this.Quantity = 1;
  }

}
