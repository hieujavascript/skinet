import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from 'src/app/account/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService:AccountService , private router:Router) {

  }
  canActivate(
    route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
     
    return this.accountService.currentUser$.pipe(map(auth => {
      if(auth)
      { 
        return true;
      }
      console.log( state.url);
      this.router.navigate(['account/login'] , {queryParams: {returnUrls: state.url}});
      // THEM URL checkout vào trang login dạng https:5001/account/login?returnUrls=checkout      
      // returnUrls: state.url trả về /checkout cho trang login
      // neu tu trang checkout chung ta login thi chungta thanh toan
      // neu tu trang khac login thi chung ta se chuyen ve trang shop
      // vao trang login xem
    })
    )
  }
}
