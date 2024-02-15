import { CommonModule } from '@angular/common';
import { Component, Input, OnInit, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, RadioControlValueAccessor } from '@angular/forms';

@Component({
  standalone: true,
  imports: [CommonModule],
  selector: 'app-payment-type',
  templateUrl: './payment-type.component.html',
  styleUrls: ['./payment-type.component.scss'],
  providers: [
    { 
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => PaymentTypeComponent),
    }
  ]
})
export class PaymentTypeComponent extends RadioControlValueAccessor {
  @Input() disabled = false;

  //override onChange = () => {};
  onBlur = (_: any) => {};

  override ngOnInit() {
    console.log(this.value);
  }

  onChanged() {
    console.log(this.value);
    this.onChange();
  }

  override writeValue(obj: any): void {
    if (obj !== undefined) {
      this.value = obj;
    }
  }
  
  override registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  override registerOnTouched(fn: any): void {
    this.onBlur = fn;
  }

  override setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}