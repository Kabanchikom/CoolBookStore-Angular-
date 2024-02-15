import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TabsService } from '../../tabs.service';

@Component({
  selector: 'app-tab-nav-link',
  templateUrl: './tab-nav-link.component.html',
  styleUrls: ['./tab-nav-link.component.scss']
})
export class TabNavLinkComponent {
  @Input() title?: string;
  @Input() tabId?: string;
  @Output() click = new EventEmitter<void>();

  isSelected = false;

  constructor(
    private tabsService: TabsService
  ) { }

  ngOnInit() {
    if (this.tabsService.defaultId === this.tabId) {
      this.tabsService.selectedId = this.tabId;
    }

    if (this.tabId) {
      this.tabsService.addTabId(this.tabId);
    }

    this.isSelected = this.tabsService.selectedId === this.tabId;
    this.tabsService.selectedIdChanged.subscribe((id: string) => {
      this.isSelected = id === this.tabId;
    });
  }

  handleOnClick() {
    this.tabsService.selectedId = this.tabId;
    //this.click?.emit();
  }
}
