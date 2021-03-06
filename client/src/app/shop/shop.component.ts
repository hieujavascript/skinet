import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/IBrand';
import { IPagination } from '../shared/models/IPagination';
import { IProduct } from '../shared/models/IProduct';
import { IType } from '../shared/models/ProductType';
import { ShopService } from './shop.service';
import {ShopParams} from "../shared/models/shopparams";
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { BreadcrumbService } from 'xng-breadcrumb';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
 products: IProduct[];
 brands: IBrand[];
 types: IType[]
 totalCount = 0;
 public shopParam = new ShopParams();
 sortOptions = [
    { name: 'Alphabetical', value:  'name' },
    { name: 'Price: Low To High' , value: 'priceAsc' },
    { name: 'Price: Hight To Low' , value: 'priceDesc'}
 ]

  constructor(
    private shopService : ShopService , 
    private cdr: ChangeDetectorRef , // fix lỗi dữ liệu load trước template    
    ) {
      
    }
  ngOnInit(): void {
   this.getProduct();
   this.getBrand();
   this.getType();
  }
  getProduct(){
    this.shopService.getProducts(this.shopParam).subscribe((response:IPagination) => {
      this.products = response.data;
      this.totalCount = response.count;
      this.shopParam.totalCountParams = response.count;
      //console.log("total :"+this.totalCount + ", param : " + this.shopParam.totalCountParams);
     
    } , 
    error => {
     console.log(error);
    })
  }
  getBrand() {
    this.shopService.getBrand().subscribe(response => {
      // {id:0 , name: "All"}, gia tri mac dinh khi chay
      this.brands = [{id:0 , name: "All"},...response]; // array object
    }, 
    error => {
     console.log(error);
    });
  }
  getType() {
    this.shopService.getType().subscribe(response => {
      this.types = [{id:0 , name: "All"},...response];
    }, 
    error => {
     console.log(error);
    });
  }

  onBrandSelected(brandid: number) {
    this.shopParam.brandId = brandid;
    this.getProduct();
  }
  onTypeSelected(typeid: number) {
    this.shopParam.typeId = typeid;
    this.getProduct();
  }
  onSortSelected(event:any) {
    this.shopParam.sort = event.target.value;
    this.getProduct();
  }
  // onPageChange(event: PageChangedEvent ) {
  //   this.shopParam.pageNumber = event.page;
  //   this.getProduct();
  // }
  onPageChanged(event: any) {
   if(this.shopParam.pageNumber !== event) {
     this.shopParam.pageNumber = event;
     this.getProduct();
     this.cdr.detectChanges();
   }
  }
  onSearch() {
    this.shopParam.search  = this.searchTerm.nativeElement.value;
    this.getProduct();
   
  }
  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParam = new ShopParams();// khởi tạo lại tham số mac định tat ca deu null chi co name ,pageindex , pagesize co du lieu mac dinh
    this.getProduct();
  }
}
