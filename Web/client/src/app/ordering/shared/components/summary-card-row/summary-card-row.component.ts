import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import SummaryCardRowVariant from 'src/app/ordering/shared/enums/SummaryCardRowVariant';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [ CommonModule, UiKitModule ],
  selector: 'app-summary-card-row',
  templateUrl: './summary-card-row.component.html',
  styleUrls: ['./summary-card-row.component.scss']
})
export class SummaryCardRowComponent {
    @Input() variant?: SummaryCardRowVariant;
    @Input() param?: string;
    @Input() value?: string;

    SummaryCardRowVariant = SummaryCardRowVariant;
}