import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import SummaryCardRowVariant from 'src/app/ordering/shared/enums/SummaryCardRowVariant';
import { Gap } from 'src/app/ui-kit/shared';
import ISummaryCardItem from '../../types/ISummaryCardItem';
import { CommonModule } from '@angular/common';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { SummaryCardRowComponent } from '../summary-card-row/summary-card-row.component';

@Component({
  standalone: true,
  imports: [ CommonModule, UiKitModule, SummaryCardRowComponent ],
  selector: 'app-summary-card',
  templateUrl: './summary-card.component.html',
  styleUrls: ['./summary-card.component.scss']
})
export class SummaryCardComponent implements OnChanges {
  @Input() items: ISummaryCardItem[] = [];
  @Input() showNextButton?: boolean;
  @Input() nextButtonText: string | null = null;
  @Input() disabled: boolean = false;
  @Input() buttonType?: 'submit' | 'button' = 'button';
  @Output() nextClick = new EventEmitter<void>();
  
  SummaryCardRowVariant = SummaryCardRowVariant;

  content: {
      quantity: number,
      sum: number,
      discount: number,
      total: number,
  } = {
      sum: 0,
      discount: 0,
      total: 0,
      quantity: 0,
  };

  ngOnChanges(changes: SimpleChanges) {
    let items = changes['items']?.currentValue;

    if(!items) {
      return;
    }
    
    this.content = {
      sum: 0,
      discount: 0,
      total: 0,
      quantity: 0,
  };
    items.forEach((x: ISummaryCardItem) => {
      this.content.quantity += x.quantity;
      this.content.sum += (x.oldPrice ?? 0) * x.quantity;
      this.content.discount += (x.oldPrice - (x.newPrice ?? 0)) * x.quantity;
      this.content.total += (x.newPrice ?? 0) * x.quantity;
    });
  }
}