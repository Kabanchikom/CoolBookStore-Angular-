import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [UiKitModule],
  selector: 'app-product-card-thumb',
  templateUrl: './product-card-thumb.component.html',
  styleUrls: ['./product-card-thumb.component.scss']
})
export class ProductCardThumbComponent {
  @Input() imgSrc?: string;
}
