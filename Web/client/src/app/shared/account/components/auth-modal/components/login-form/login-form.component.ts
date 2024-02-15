import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/shared/account/services/account.service';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [UiKitModule, CommonModule, FormsModule],
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit, OnDestroy {
  @ViewChild('loginForm') loginForm?: NgForm;

  loginSubscription: Subscription | null = null;
  
  username = '';
  password = '';

  constructor(
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    if (!this.accountService.isLoadingSubject) {
      this.loginSubscription?.unsubscribe();
    }
  }

  onSubmit() {
    this.username = this.loginForm?.value.username;
    this.password = this.loginForm?.value.password;

    this.loginSubscription = this.accountService
      .login({userName: this.username, password: this.password}).subscribe();

    this.loginForm?.reset();
  }
}