import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";


export function precisionScale(precision: number, scale: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

        if (!control.value)
            return null;

        const regex = new RegExp(`^\\d{1,${(precision - scale)}}(\\.\\d{1,${scale}})?$`);
        return regex.test(control.value) ? null : { precisionScale: { precision, scale } };
    }
}