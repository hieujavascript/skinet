import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { AbstractControl, ControlValueAccessor, NgControl, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';


@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements OnInit , ControlValueAccessor  {

  // static : true ko dùng *ngIf , *ngfor để thay đổi dữ liệu , nếu có dùng thì Static= false mac định
  @ViewChild('input', {static: true}) input: ElementRef;
  @Input() type = 'text';
  @Input() label = 'string';
   disabled;
  constructor(@Self() public controlDir: NgControl) { 
    this.controlDir.valueAccessor = this;
  } 
  ngOnInit(): void {
    const control = this.controlDir.control;
    const validators = control.validator ? [control.validator] : [];
    const asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];

    control.setValidators(validators);
    control.setAsyncValidators(asyncValidators);
    control.updateValueAndValidity();
  }

  registerOnValidatorChange?(fn: () => void): void {
    throw new Error('Method not implemented.');
  }


  onChange(event) {}

  onTouched() {}
  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }
  registerOnChange(fn: any): void {
   this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
