import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";


export function password(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

        if (!control.value)
            return null;

        return /(?=(.*[0-9]))(?=.*[^a-zA-Z0-9])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{6,}/g.test(control.value) ? null : { password: true };
    }
}