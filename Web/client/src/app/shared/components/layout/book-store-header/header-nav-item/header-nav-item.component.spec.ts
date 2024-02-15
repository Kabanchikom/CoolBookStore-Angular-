import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderNavItemComponent } from './header-nav-item.component';

describe('HeaderNavItemComponent', () => {
  let component: HeaderNavItemComponent;
  let fixture: ComponentFixture<HeaderNavItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HeaderNavItemComponent]
    });
    fixture = TestBed.createComponent(HeaderNavItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
