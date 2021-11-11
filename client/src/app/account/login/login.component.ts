import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
 public loginForm: FormGroup;
  constructor(private accoutService: AccountService , private router: Router , private activeRoute: ActivatedRoute ) { }
  returnUrl: string;
  ngOnInit(): void {
  // neu Url đang đứng là checkout thi chung ta se thanh toan 
  // neu url la cac trang khac thi chung ta se chuyen qua trang /shop
   this.returnUrl =  this.activeRoute.snapshot.queryParams.returnUrls || '/shop'
    this.createForm();
  }
  createForm() {
   this.loginForm = new FormGroup({
      email : new FormControl('' ,[Validators.required , Validators.pattern("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$")]),
      password: new FormControl('' , Validators.required)
   });
  }
  submitForm() {
    // console.log(this.loginForm.value);
    this.accoutService.login(this.loginForm.value).pipe(take(1)).subscribe(() => {
       console.log(this.returnUrl);
      this.router.navigate([this.returnUrl]);
    } , error => {
      console.log(error);
    })
  }

}
