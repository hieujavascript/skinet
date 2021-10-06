import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
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
  constructor(private shopService: ShopService , 
    private activeRoute: ActivatedRoute,
    private breadcrumb : BreadcrumbService) {
      this.breadcrumb.set("@ProductDetails" , " ");
    }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    this.shopService.getProduct(+this.activeRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.product = response;
    //  this.breadcrumb.set("@ProductItems" , response.name);
    })
  }

}
