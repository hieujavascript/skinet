import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { IPagination } from './shared/models/IPagination';
import { IProduct } from './shared/models/IProduct';
import { ShopService } from './shop/shop.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

constructor (private spinerService: NgxSpinnerService) {

}
  ngOnInit(): void {
   
  }

}
