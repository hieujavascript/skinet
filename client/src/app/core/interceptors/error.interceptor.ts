import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, throwError } from "rxjs";
import { catchError, delay } from "rxjs/operators";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private route: Router , private toast: ToastrService ) {}
    intercept(req: HttpRequest<any>, next: HttpHandler):  Observable<HttpEvent<any>>  {
        
        return next.handle(req).pipe(
            delay(1000),
            catchError((error: HttpErrorResponse) => {
            
                if(error) {
                    if(error.status === 400) {
                        if(error.error.errors) {
                            throw error.error ; // dc tra ve component
                        } 
                        else 
                        {
                            this.toast.error(error.error.message ,error.error.statusCode);
                        }
                    }
                    if(error.status === 401) {
                        this.toast.error(error.error.message ,error.error.statusCode);
                    }
                   if(error.status === 404) {
                    this.route.navigateByUrl('/not-found');
                   }
                   if(error.status === 500) {
                       
                       this.route.navigateByUrl('/server-error' , { state: { error: 'day la loi ko biet' }} );
                   }
                }
                return throwError(error);
            })
        );
    }

}