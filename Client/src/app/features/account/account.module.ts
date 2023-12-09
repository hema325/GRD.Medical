import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
<<<<<<< HEAD
import { HttpClientModule } from '@angular/common/http';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { AccountComponent } from './account.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
=======
import { AccountComponent } from './account.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CoreModule } from 'src/app/core/core.module';
import { SendEmailConfirmationComponent } from './send-email-confirmation/send-email-confirmation.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { SendEmailResetPasswordComponent } from './send-email-reset-password/send-email-reset-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { EditImageComponent } from './edit-image/edit-image.component';
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf



@NgModule({
  declarations: [
<<<<<<< HEAD
    LoginRegisterComponent,
    AccountComponent
=======
    AccountComponent,
    LoginComponent,
    RegisterComponent,
    SendEmailConfirmationComponent,
    ConfirmEmailComponent,
    SendEmailResetPasswordComponent,
    ResetPasswordComponent,
    EditImageComponent
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule,
<<<<<<< HEAD
    SharedModule
=======
    SharedModule,
    ReactiveFormsModule,
    CoreModule
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
  ]
})
export class AccountModule { }
