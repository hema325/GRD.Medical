import { AbstractControl, AsyncValidatorFn, ValidationErrors } from "@angular/forms";
import { Observable, finalize, map } from "rxjs";
import { AccountService } from "../services/account.service";
import { LoaderService } from "../services/loader.service";



export function emailDuplicated(accountService: AccountService, loader: LoaderService): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
        loader.deactivate();
        return accountService.isEmailDuplicated(control.value)
            .pipe(map(result => result ? { emailDuplication: true } : null), finalize(() => loader.activate()));
    }
}