import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderingCompletedComponent } from './ordering-completed.component';

describe('OrderingCompletedComponent', () => {
  let component: OrderingCompletedComponent;
  let fixture: ComponentFixture<OrderingCompletedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [OrderingCompletedComponent]
    });
    fixture = TestBed.createComponent(OrderingCompletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
