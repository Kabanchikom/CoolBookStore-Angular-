import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card-name',
  templateUrl: './card-name.component.html',
  styleUrls: ['./card-name.component.scss']
})
export class CardNameComponent {
  @Input() name?: string;
}
