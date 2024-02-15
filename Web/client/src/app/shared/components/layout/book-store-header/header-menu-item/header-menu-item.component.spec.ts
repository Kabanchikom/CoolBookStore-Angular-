import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderMenuItemComponent } from './header-menu-item.component';

describe('HeaderMenuItemComponent', () => {
  let component: HeaderMenuItemComponent;
  let fixture: ComponentFixture<HeaderMenuItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HeaderMenuItemComponent]
    });
    fixture = TestBed.createComponent(HeaderMenuItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
