import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, forwardRef, OnInit, Provider } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
const COUNTRY_CONTROL_VALUE_ACCESSOR: Provider = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => ImagesButtonComponent),
  multi: true,
};
@Component({
  selector: 'app-images-button',
  templateUrl: './images-button.component.html',
  styleUrls: ['./images-button.component.css'],
  providers: [COUNTRY_CONTROL_VALUE_ACCESSOR],
  animations: [
    trigger('selected', [
      state('*', style({ opacity: 1, transform: 'scale(1)' })),
      state('void', style({ opacity: 0, transform: 'scale(0)' })),
      transition(':enter', animate('300ms ease-in-out')),
      transition(':leave', animate('300ms ease-in-out')),
    ]),
  ],
})
export class ImagesButtonComponent implements ControlValueAccessor  {

  countries = [
    { code: 'IN', name: 'India' },
    { code: 'US', name: 'United States' },
    { code: 'GB-ENG', name: 'England' },
    { code: 'NL', name: 'Netherlands' },
  ];
  selected!: string;
  disabled = false;
  private onTouched!: Function;
  private onChanged!: Function;

  selectCountry(code: string) {
    this.onTouched();
    this.selected = code;
    this.onChanged(code);
  //  this.disabled = true;
  }



  constructor() { }
  writeValue(obj: any): void {
    this.selected = obj ?? 'IN';
    
  }
  registerOnChange(fn: any): void {
    this.onChanged = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

 

}
