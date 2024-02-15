import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/shared/account/services/account.service';
import { IUser } from 'src/app/shared/account/types/IUser';
import { BookStoreHeaderComponent } from '../book-store-header/book-store-header.component';
import { BookStoreFooterComponent } from '../book-store-footer/book-store-footer.component';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { AuthModalComponent } from 'src/app/shared/account/components/auth-modal/auth-modal.component';

@Component({
  standalone: true,
  imports: [BookStoreHeaderComponent, BookStoreFooterComponent, AuthModalComponent, UiKitModule],
  selector: 'app-book-store-layout',
  templateUrl: './book-store-layout.component.html',
  styleUrls: ['./book-store-layout.component.scss']
})
export class BookStoreLayoutComponent implements OnInit, OnDestroy {
  isLoginModalActive = false;
  user: IUser | null = null;

  getUserSubscription: Subscription | null = null;
  successSubscription: Subscription | null = null;
  failureSubscription: Subscription | null = null;

  constructor(
    private accountService: AccountService
  ) {}
  
  ngOnInit(): void {
    this.accountService.isLoginModalActiveSubject.subscribe(
      (isActive: boolean) => {
        this.isLoginModalActive = isActive;
      }
    )

    this.accountService.autoLogin();

    this.getUserSubscription = this.accountService.getUser().subscribe({
      next: (user: IUser) => {
        this.user = user;
      },
      error: (e: Error) => {
        this.user = null;
      }});

    this.successSubscription = this.accountService.success.subscribe(
      () => {
        this.isLoginModalActive = false;
        alert('Вы успешно вошли в систему!');
      }
    );

    this.failureSubscription = this.accountService.failure.subscribe(
      (message: string) => {
        alert(message);
      }
    );
  }

  ngOnDestroy(): void {
    this.getUserSubscription?.unsubscribe();
    this.successSubscription?.unsubscribe();
    this.failureSubscription?.unsubscribe();
  }

  onLoginModalCloseClick() {
    this.isLoginModalActive = false;
    this.accountService.isLoginModalActiveSubject.next(false);
  }
}