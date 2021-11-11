import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketItem } from '../shared/models/Basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket> = null;

  constructor(private basketService: BasketService ) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }
  RemoveItemFromBasket(item: IBasketItem) {
    this.basketService.RemoveItemFromBasket(item);

  }
  IncrementTotalBasket(item: IBasketItem) {
  //  console.log(item);
    this.basketService.IncrementTotalBasket(item);
  }
  DecrementTotalBasket(item: IBasketItem) {
    this.basketService.DecrementTotalBasket(item);
  }

}
