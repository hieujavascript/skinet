import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { switchMap, take , map  } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  myerror: string[] = [];
  constructor(private fb: FormBuilder , 
              private accountService: AccountService,
              private router: Router
              ) {}

  ngOnInit(): void {
    this.createRegisterForm();
  } 
   createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: ['' , [Validators.required]],
      email: ['' , [Validators.required , Validators.pattern("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$")],
                   [this.validateEmailNotTaken()] 
                  ],
      password: ['' , [Validators.required]] , 
      server: ['IN', Validators.required],
    });
  }  
  submitForm() {
    console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).pipe(take(1)).subscribe(
      () => {
        console.log("Register successful");
        this.router.navigateByUrl("/shop");
      } , error => {
        this.myerror = error.errors;
        console.log(error);        
      }
    )
  }
  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
         switchMap(() => {
          if(!control.value)
            return of(null);
          
           return this.accountService.checkEmailExists(control.value).pipe(
              map( res => {
                  return res ? {emailExists: true} : false;
              })
            );
        })
      );
  };
 }

}
