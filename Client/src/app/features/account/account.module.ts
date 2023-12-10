import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
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



@NgModule({
  declarations: [
    AccountComponent,
    LoginComponent,
    RegisterComponent,
    SendEmailConfirmationComponent,
    ConfirmEmailComponent,
    SendEmailResetPasswordComponent,
    ResetPasswordComponent,
    EditImageComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    ReactiveFormsModule,
    CoreModule
  ]
})
export class AccountModule { }
