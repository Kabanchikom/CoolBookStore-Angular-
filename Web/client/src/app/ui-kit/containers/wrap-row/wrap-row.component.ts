import { Component, Input } from '@angular/core';
import { Gap, JustifyContent } from '../../shared';

@Component({
  selector: 'app-wrap-row',
  templateUrl: './wrap-row.component.html',
  styleUrls: ['./wrap-row.component.scss']
})
export class WrapRowComponent {
  @Input() gap?: string = "10px";
  @Input() justifyContent?: string = "space-between";

  Gap = Gap;
}