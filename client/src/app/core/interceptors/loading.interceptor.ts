import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { delay, finalize} from "rxjs/operators";
import { BusyServiceService } from "../busy-service.service";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
    constructor(private busyService: BusyServiceService){}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        if(!req.url.includes("emailexists"))
        {
            this.busyService.busy(); // xuat hien man hinh pacman loading
        }
        
        return next.handle(req).pipe(
            delay(500),
            finalize(()=> {               
                this.busyService.idle();
            })  
        );
    }

}
