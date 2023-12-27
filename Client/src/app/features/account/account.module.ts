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
import { UpdateAccountDetailsComponent } from './update-account-details/update-account-details.component';
import { ChangeAccountPasswordComponent } from './change-account-password/change-account-password.component';
import { EditImageBottomSheetComponent } from './edit-image-bottom-sheet/edit-image-bottom-sheet.component';
import { PostsModule } from '../posts/posts.module';
import { UserProfileComponent } from './user-profile/user-profile.component';



@NgModule({
  declarations: [
    AccountComponent,
    LoginComponent,
    RegisterComponent,
    SendEmailConfirmationComponent,
    ConfirmEmailComponent,
    SendEmailResetPasswordComponent,
    ResetPasswordComponent,
    UpdateAccountDetailsComponent,
    ChangeAccountPasswordComponent,
    EditImageBottomSheetComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    ReactiveFormsModule,
    CoreModule,
    PostsModule
  ]
})
export class AccountModule { }
