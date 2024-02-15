import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-flex',
  templateUrl: './flex.component.html',
  styleUrls: ['./flex.component.scss']
})
export class FlexComponent {
  @Input() flexDirection?: string = "flex-row";
  @Input() alignItems?: string = "normal";
  @Input() justifyContent?: string = "flex-start";
  @Input() flexWrap?: string = "nowrap";
  @Input() gap?: string = "none";
}