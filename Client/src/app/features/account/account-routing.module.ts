import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountComponent } from './account.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { authGuard } from 'src/app/guards/auth.guard';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { SendEmailConfirmationComponent } from './send-email-confirmation/send-email-confirmation.component';
import { SendEmailResetPasswordComponent } from './send-email-reset-password/send-email-reset-password.component';

const routes = [
  { path: '', component: AccountComponent, canActivate: [authGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'send-email-verification', component: SendEmailConfirmationComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: 'send-email-reset-password', component: SendEmailResetPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: ':id', component: UserProfileComponent, canActivate: [authGuard] }
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
