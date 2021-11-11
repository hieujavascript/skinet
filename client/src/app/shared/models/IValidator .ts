import { AbstractControl, ValidationErrors } from "@angular/forms";

export interface IValidator {
    validate(c: AbstractControl): ValidationErrors | null;
    registerOnValidatorChange?(fn: () => void): void;
}