import { Component } from '@angular/core';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [UiKitModule],
  selector: 'app-book-store-footer',
  templateUrl: './book-store-footer.component.html',
  styleUrls: ['./book-store-footer.component.scss']
})
export class BookStoreFooterComponent {

}
