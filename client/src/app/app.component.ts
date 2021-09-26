import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/IPagination';
import { IProduct } from './models/IProduct';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
public products: IProduct[];
constructor (private http: HttpClient) {

}
  ngOnInit(): void {
   this.http.get("https://localhost:5001/api/products").subscribe((response:IPagination) => {
     this.products = response.data;
   } , 
   error => {
    console.log(error);
   })
  }

}
