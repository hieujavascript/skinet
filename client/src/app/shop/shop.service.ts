import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBrand } from '../shared/models/IBrand';
import { IPagination } from '../shared/models/IPagination';
import { IType } from '../shared/models/ProductType';
import {map} from 'rxjs/operators';
import {ShopParams} from "../shared/models/shopparams";
import { IProduct } from '../shared/models/IProduct';

@Injectable({
  providedIn: 'root'
})

export class ShopService {
  baseUrl = "https://localhost:5001/api/";
  constructor(private http: HttpClient) { }

  getProducts( shopParams : ShopParams):Observable<IPagination> {    
    let params = new HttpParams();
    if(shopParams.typeId != 0)
    params = params.append("typeId" , shopParams.typeId.toString());

    if(shopParams.brandId != 0)
    params = params.append("brandId" , shopParams.brandId.toString());

    //if(shopParams.sortId) da comac dinh la sort = name nen ko can kiem tra
    params = params.append("sort" , shopParams.sort.toString());
    
    if(shopParams.search != null) {
      params = params.append('search', shopParams.search);
    }

    params = params.append("pageIndex" , shopParams.pageNumber.toString());
    params = params.append("pageIndex" , shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + "products" , {observe:"response" , params})
                    .pipe(map(response => {
                      return response.body; // lay body trong http response
                    }))
  }
  getBrand() {
    return this.http.get<IBrand[]>(this.baseUrl + "products/brands"); // lay trong body response
  }
  getType() {
    return this.http.get<IType[]>(this.baseUrl + "products/types");
  }
  getProduct(id: number) {
    var productDetails = this.http.get<IProduct>(this.baseUrl  + "products/" + id);
    return productDetails;
  }
}
