import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabNavLinkComponent } from './tab-nav-link.component';

describe('TabNavLinkComponent', () => {
  let component: TabNavLinkComponent;
  let fixture: ComponentFixture<TabNavLinkComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TabNavLinkComponent]
    });
    fixture = TestBed.createComponent(TabNavLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
