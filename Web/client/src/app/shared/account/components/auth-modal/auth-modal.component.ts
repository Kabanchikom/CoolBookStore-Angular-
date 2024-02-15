import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { RegisterFormComponent } from './components/register-form/register-form.component';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [UiKitModule, CommonModule, LoginFormComponent, RegisterFormComponent],
  selector: 'app-auth-modal',
  templateUrl: './auth-modal.component.html',
  styleUrls: ['./auth-modal.component.scss']
})
export class AuthModalComponent implements OnInit {
  isLoading = false;

  @Input() isActive: boolean = false;

  @Output() closeClick = new EventEmitter<void>();
  @Output() success = new EventEmitter<void>();
  @Output() failure = new EventEmitter<void>();

  constructor(
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.accountService.isLoadingSubject.subscribe(
      (isLoading: boolean) => {
        this.isLoading = isLoading;
      }
    );
  }
}