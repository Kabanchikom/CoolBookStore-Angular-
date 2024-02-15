import { Component, OnInit } from '@angular/core';
import { TabsService } from './tabs.service';

@Component({
  selector: 'app-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.scss'],
  providers: [TabsService]
})
export class TabsComponent implements OnInit {
  tabIds: string[] = [];

  constructor(
    private tabsService: TabsService
  ) {}

  ngOnInit(): void {
    this.tabIds = this.tabsService.tabIds;
    this.tabsService.tabIdsChanged.subscribe(
      (tabIds: string[]) => {
        this.tabIds = tabIds;
      }
    );
  }
}