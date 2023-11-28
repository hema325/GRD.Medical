import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AccountComponent } from './account.component';
import { LoginRegisterComponent } from './login-register/login-register.component';

const routes = [
  { path: '', component: AccountComponent },
  { path: 'login-register', component: LoginRegisterComponent }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
