import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderingPageComponent } from './ordering-page.component';

describe('OrderingPageComponent', () => {
  let component: OrderingPageComponent;
  let fixture: ComponentFixture<OrderingPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrderingPageComponent]
    });
    fixture = TestBed.createComponent(OrderingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
