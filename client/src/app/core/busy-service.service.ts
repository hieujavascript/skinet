import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyServiceService {
  busyRequestCount = 0;
  constructor(private spinnerService: NgxSpinnerService) { }
  busy() {
    this.busyRequestCount++;
    this.spinnerService.show(undefined , {
      type: "pacman" ,
      bdColor: "rgba(51,51,51,0.8)",
      color: "#333333",
    })
  }
  idle() {
    this.busyRequestCount--;
    if(this.busyRequestCount <=0)
    this.busyRequestCount = 0;
    this.spinnerService.hide();
  }
}
