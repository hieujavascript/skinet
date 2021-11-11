import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketTotal, IBasketTotal } from '../../models/BasketTotal';

@Component({
  selector: 'app-order-total',
  templateUrl: './order-total.component.html',
  styleUrls: ['./order-total.component.css']
})
export class OrderTotalComponent implements OnInit , OnDestroy {

  basketTotal$: Observable<IBasketTotal>;
  basketTotal:BasketTotal  = new BasketTotal();
  componentDestroyed$: Subject<boolean> = new Subject()

  constructor(private basketService: BasketService) { 
  }
  ngOnInit(): void {
  this.basketService.basketTotal$.pipe(takeUntil(this.componentDestroyed$)).subscribe(classTotal => {
      this.basketTotal = classTotal;
    })   
  }
  ngOnDestroy(): void {
    this.componentDestroyed$.unsubscribe();
    //this.componentDestroyed$.complete();
  }
}
