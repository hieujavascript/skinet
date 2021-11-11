import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { take } from 'rxjs/operators';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';
import { IPagination } from './shared/models/IPagination';
import { IProduct } from './shared/models/IProduct';
import { ShopService } from './shop/shop.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

constructor (private basketService: BasketService , private acccountService: AccountService) {

}
  ngOnInit(): void {
  this.loadBasket();
  this.loadCurrentUser();
  }
  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(() => {
        console.log('initialised basket');
      }, error => {
        console.log(error);
      })
    }
  }
  loadCurrentUser() {
    let token = localStorage.getItem("token");
    this.acccountService.loadCurrentUser(token).pipe(take(1)).subscribe(() => {
      console.log("'login successful");
    } , error => {
      console.log(error)
    })
  }
}
