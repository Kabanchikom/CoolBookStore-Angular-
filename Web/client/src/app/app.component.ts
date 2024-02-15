import { Component } from '@angular/core';
import { BookStoreLayoutComponent } from './shared/components/layout/book-store-layout/book-store-layout.component';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [BookStoreLayoutComponent, RouterModule],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';
}