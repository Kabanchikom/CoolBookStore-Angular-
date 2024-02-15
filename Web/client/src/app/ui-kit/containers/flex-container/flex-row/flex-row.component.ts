import { Component, Input } from '@angular/core';
import { AlignItems, Gap, JustifyContent } from 'src/app/ui-kit/shared';

@Component({
  selector: 'app-flex-row',
  templateUrl: './flex-row.component.html',
  styleUrls: ['./flex-row.component.scss']
})
export class FlexRowComponent {
  @Input() gap?: string = "20px";
  @Input() alignItems?: string = "start";
  @Input() justifyContent?: string = "flex-start";
  @Input() isAdaptive?: boolean = true;
}
