import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UiKitModule } from 'src/app/ui-kit/ui-kit.module';

@Component({
  standalone: true,
  imports: [ CommonModule, UiKitModule, RouterModule ],
  selector: 'app-account-page',
  templateUrl: './account-page.component.html',
  styleUrls: ['./account-page.component.scss']
})
export class AccountPageComponent implements OnInit {
  activeTabId?: string;

  constructor(
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.activeTabId = this.router.url.split('/')[2];
  }
}





