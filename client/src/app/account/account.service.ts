import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource$ = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource$.asObservable();
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient , private router: Router) { }
  login(formValue: any) {
   return this.http.post(this.baseUrl + "account/login" , formValue).pipe(
    // this.http.post(this.baseUrl + "login" , formValue) tra ve Use:IUser cho map
      map((user:IUser) => {
        if(user) {
          this.currentUserSource$.next(user);
          localStorage.setItem("token" , user.token);
          this.router.navigateByUrl("/shop");
        }
      })
    );
  }
  //đã làm 1 trang điều khiển lỗi
  // nên khi Register không đc , lỗi sẽ trả về cho trang register.TS
  register(formValue: any) {
      return this.http.post(this.baseUrl + "account/register" , formValue).pipe(
        map((user:IUser) => {
         if(user)
         localStorage.setItem("token" , user.token);
         this.currentUserSource$.next(user);
        })
      );
  }

  checkEmailExists(email:string) {
    return this.http.get<boolean>(this.baseUrl + "account/emailexists?email=" + email);
  }
  logout() {
    localStorage.removeItem("token");
    this.currentUserSource$.next(null);
    this.router.navigateByUrl("/");
  }

  loadCurrentUser(token: string) {
    
    if (token === null) {
        this.currentUserSource$.next(null);
        return of(null);
    }
    let headers =  new HttpHeaders().set(
      "Authorization", // Controller yeu cau phai xac thuc Authoriration
      `Bearer ${token}`
    );
    //thêm Header vào API thì Controller vẫn lấy ra từ [FromQuery]
    // sau đó thông  qua Token có chứa Claim chúng ta sẽ có đc User HIện Tại Đamg Login
    return this.http.get(this.baseUrl + "account" , {headers}).pipe(
      map((user:IUser) => {
          if(user) 
          {
            this.currentUserSource$.next(user);
            localStorage.setItem("token" , user.token); // cho ta biet user nao dang login
          }
      })
    )
  }
}
