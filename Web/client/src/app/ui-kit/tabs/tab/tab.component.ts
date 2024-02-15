import { Component, Input, OnInit } from '@angular/core';
import { TabsService } from '../tabs.service';

@Component({
  selector: 'app-tab',
  templateUrl: './tab.component.html',
  styleUrls: ['./tab.component.scss']
})
export class TabComponent implements OnInit {
  @Input() tabId?: string;
  isActive: boolean = false;

  constructor(
    private tabsService: TabsService
  ) {}

  ngOnInit(): void {
    this.isActive = this.tabsService.selectedId === this.tabId;

    this.tabsService.selectedIdChanged.subscribe(
      (selectedId: string) => {
        this.isActive = this.tabsService.selectedId === this.tabId;
      }
    )
  }
}