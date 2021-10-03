import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { IProduct } from 'src/app/shared/models/IProduct';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  productid: number = 0;
  product: IProduct;
  constructor(private shopService: ShopService , activeRoute: ActivatedRoute) { 
   activeRoute.paramMap.subscribe(param => {
      this.productid = +param.get("id");
    })
  }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
   
    this.shopService.getProduct(this.productid).pipe(take(1)).subscribe(response => {
      this.product = response;
      console.log(this.product);
    })
  }

}
