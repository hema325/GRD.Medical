import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './account.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoginComponent } from './login/login.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { CoreModule } from 'src/app/core/core.module';
import { SendEmailConfirmationComponent } from './send-email-confirmation/send-email-confirmation.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { SendEmailResetPasswordComponent } from './send-email-reset-password/send-email-reset-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { UpdateAccountDetailsComponent } from './update-user-details/update-user-details.component';
import { ChangeAccountPasswordComponent } from './change-account-password/change-account-password.component';
import { EditImageBottomSheetComponent } from './edit-image-bottom-sheet/edit-image-bottom-sheet.component';
import { PostsModule } from '../posts/posts.module';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { RegisterDoctorComponent } from './register-doctor/register-doctor.component';
import { LanguageSelectInputComponent } from './language-select-input/language-select-input.component';
import { UpdateDoctorDetailsComponent } from './update-doctor-details/update-doctor-details.component';
import { NotificationListComponent } from './notification-list/notification-list.component';
import { SpecialitiesModule } from '../specialities/specialities.module';



@NgModule({
  declarations: [
    AccountComponent,
    LoginComponent,
    RegisterUserComponent,
    SendEmailConfirmationComponent,
    ConfirmEmailComponent,
    SendEmailResetPasswordComponent,
    ResetPasswordComponent,
    UpdateAccountDetailsComponent,
    ChangeAccountPasswordComponent,
    EditImageBottomSheetComponent,
    UserProfileComponent,
    RegisterDoctorComponent,
    LanguageSelectInputComponent,
    UpdateDoctorDetailsComponent,
    NotificationListComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    ReactiveFormsModule,
    CoreModule,
    PostsModule,
    SpecialitiesModule
  ]
})
export class AccountModule { }
