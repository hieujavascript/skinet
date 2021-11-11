import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket.component';

var routes:Routes = [
  // // path:"" tuogn duong localhost://basket/ , ngay tai model nay no la goc
  {path : "" , component: BasketComponent}
]

@NgModule({
  declarations: [],
  imports: [
   // CommonModule
   RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class BasketRoutingModule { }
