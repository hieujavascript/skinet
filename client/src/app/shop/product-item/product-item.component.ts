import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/IProduct';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent implements OnInit {
  @Input() product : IProduct;
  constructor(private baskService: BasketService) {

  }

  ngOnInit(): void {
    
  }
  addProductInBasket() {
    this.baskService.AddItemToBasket(this.product)
  }
}
