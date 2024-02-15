import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-sort-block',
  templateUrl: './sort-block.component.html',
  styleUrls: ['./sort-block.component.scss']
})
export class SortBlockComponent {
  @Input() total: number = 0;
}