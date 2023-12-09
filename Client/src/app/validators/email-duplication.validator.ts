import { AbstractControl, AsyncValidatorFn, ValidationErrors } from "@angular/forms";
<<<<<<< HEAD
import { Observable, map } from "rxjs";
import { AccountService } from "../services/account.service";



export function emailDuplicated(accountService: AccountService): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
        return accountService.isEmailDuplicated(control.value)
            .pipe(map(result => result ? { emailDuplication: true } : null));
=======
import { Observable, finalize, map } from "rxjs";
import { AccountService } from "../services/account.service";
import { LoaderService } from "../services/loader.service";



export function emailDuplicated(accountService: AccountService, loader: LoaderService): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
        loader.deactivate();
        return accountService.isEmailDuplicated(control.value)
            .pipe(map(result => result ? { emailDuplication: true } : null), finalize(() => loader.activate()));
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
    }
}