import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

var routes:Routes = [
  {path: "login" , component: LoginComponent} ,
  {path: "register" , component: RegisterComponent}
]

@NgModule({
  declarations: [],
  imports: [
   // CommonModule ko can o day
   // lazy loding
   RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
