import { Component, Input } from '@angular/core';
import { Gap } from '../../shared';

@Component({
  selector: 'app-flex-container',
  templateUrl: './flex-container.component.html',
  styleUrls: ['./flex-container.component.scss'],
  // host: {
  //   '[class.flex-container]': 'true',
  //   '[class.gap-5]': 'gap === Gap.Gap5',
  //   '[class.gap-10]': 'gap === Gap.Gap10',
  //   '[class.gap-20]': 'gap === Gap.Gap20',
  //   '[class.gap-30]': 'gap === Gap.Gap30'
  // },
})
export class FlexContainerComponent {
  @Input() gap?: string = "20px";
}