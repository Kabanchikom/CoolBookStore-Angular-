import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card-wrapper',
  templateUrl: './card-wrapper.component.html',
  styleUrls: ['./card-wrapper.component.scss']
})
export class CardWrapperComponent {
  @Input() scaleOnHover?: boolean;
  @Input() enablePadding?: boolean;
  @Input() enableBorder?: boolean;
}