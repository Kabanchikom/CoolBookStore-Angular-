import { EventEmitter, Injectable } from '@angular/core';

@Injectable()
export class TabsService {
  private _tabIds: string[] = [];
  private _selectedId?: string = this.defaultId;

  defaultId?: string;

  tabIdsChanged = new EventEmitter<string[]>();
  selectedIdChanged = new EventEmitter<string>();

  addTabId(id: string) {
    this._tabIds.push(id);
    this.tabIdsChanged.emit(this._tabIds);
  }
  
  get tabIds() {
    return this._tabIds.slice();
  }

  get selectedId(): string | undefined {
    return this._selectedId;
  }

  set selectedId(value: string | undefined) {
    this._selectedId = value;
    this.selectedIdChanged.emit(value);
  }
}