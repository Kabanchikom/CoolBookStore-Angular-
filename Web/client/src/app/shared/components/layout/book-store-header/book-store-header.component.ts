import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/shared/account/services/account.service';
import { HeaderMenuItemComponent } from './header-menu-item/header-menu-item.component';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { HeaderNavItemComponent } from './header-nav-item/header-nav-item.component';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [HeaderMenuItemComponent, HeaderNavItemComponent, CommonModule, UiKitModule],
  selector: 'app-book-store-header',
  templateUrl: './book-store-header.component.html',
  styleUrls: ['./book-store-header.component.scss']
})
export class BookStoreHeaderComponent implements OnDestroy {
  isLoggedIn = false;
  isAccountPopupActive = false;

  logoutSubscription: Subscription | null = null;

  constructor(
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.accountService._accessToken.subscribe(
      (accessToken) => {
        this.isLoggedIn = !!accessToken;
      }
    );
  }

  ngOnDestroy(): void {
    this.logoutSubscription?.unsubscribe();
  }

  onLoginClick() {
    this.accountService.isLoginModalActiveSubject.next(true);
  }

  onLogoutClick(e: Event) {
    e.stopPropagation();

    this.accountService.isLoadingSubject.next(true);

    this.logoutSubscription = this.accountService.logout().subscribe();
    this.isAccountPopupActive = false;
  }

  onPopupClick() {
    this.isAccountPopupActive = true;
  }

  navigateTo(path: string[]) {
    this.router.navigate(path);
  }
}