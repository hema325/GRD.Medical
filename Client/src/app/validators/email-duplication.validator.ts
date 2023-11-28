import { AbstractControl, AsyncValidatorFn, ValidationErrors } from "@angular/forms";
import { Observable, map } from "rxjs";
import { AccountService } from "../services/account.service";



export function emailDuplicated(accountService: AccountService): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
        return accountService.isEmailDuplicated(control.value)
            .pipe(map(result => result ? { emailDuplication: true } : null));
    }
}