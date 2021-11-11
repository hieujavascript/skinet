import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import {PagerComponent} from './components/pager/pager.component';
import { OrderTotalComponent } from './components/order-total/order-total.component';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';
import { ImagesButtonComponent } from './components/images-button/images-button.component';
@NgModule({
  declarations: [PagingHeaderComponent , PagerComponent, OrderTotalComponent, TextInputComponent, ImagesButtonComponent ],
  imports: [
    CommonModule , 
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot()
  ], // cau hinh cho share model dhcy trong file ts
  exports:[
   
    ReactiveFormsModule , 
    PaginationModule,
    PagingHeaderComponent ,
    PagerComponent,
    CarouselModule,
    OrderTotalComponent , 
    BsDropdownModule , 
    TextInputComponent,
    ImagesButtonComponent
  ]
})
export class SharedModule { }
