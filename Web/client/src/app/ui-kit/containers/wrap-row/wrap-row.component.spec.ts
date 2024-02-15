import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WrapRowComponent } from './wrap-row.component';

describe('WrapRowComponent', () => {
  let component: WrapRowComponent;
  let fixture: ComponentFixture<WrapRowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WrapRowComponent]
    });
    fixture = TestBed.createComponent(WrapRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
