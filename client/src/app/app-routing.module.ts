import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  {path: "" , component: HomeComponent , data : {breadcrumb: 'Home'}},
  {path: 'server-error' , component:ServerErrorComponent , data : {breadcrumb: 'Server Error'} },
  {path: 'not-found' , component:NotFoundComponent ,  data : {breadcrumb: 'Not Found'} },
  {path: 'test-error' , component:TestErrorComponent ,  data : {breadcrumb: 'Test Error'} },
  {path: 'shop' , loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule) ,  data : {breadcrumb: 'Shop'}} ,
  {path: 'basket' , loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule) , data : {breadcrumb: 'Basket'} },
  {path: 'account' , loadChildren: () => import('./account/account.module').then(m => m.AccountModule) , data : {breadcrumb: {skip: true} } },
  // {path: "shop/:id" , component: ProductDetailsComponent},
  {path: 'checkout' , 
  canActivate: [AuthGuard] ,
  loadChildren:() => import('./checkout/checkout.module').then(m => m.CheckoutModule) , data: {breadcrumb: 'checkout'} },
  {path: "**" , redirectTo:'not-found',pathMatch: "full" }
];
// Dang lam toi viec them account module router

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
