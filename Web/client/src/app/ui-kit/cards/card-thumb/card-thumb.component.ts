import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card-thumb',
  templateUrl: './card-thumb.component.html',
  styleUrls: ['./card-thumb.component.scss']
})
export class CardThumbComponent {
  @Input() imgSrc?: string;
}
