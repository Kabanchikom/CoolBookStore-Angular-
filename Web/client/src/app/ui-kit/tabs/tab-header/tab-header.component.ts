import { Component, Input, OnInit } from '@angular/core';
import { TabsService } from '../tabs.service';

@Component({
  selector: 'app-tab-header',
  templateUrl: './tab-header.component.html',
  styleUrls: ['./tab-header.component.scss']
})
export class TabHeaderComponent implements OnInit {
  @Input() defaultId?: string;

  constructor(
    private tabsService: TabsService
  ) { }

  ngOnInit(): void {
    this.tabsService.defaultId = this.defaultId;
  }
}