import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SummaryCardRowComponent } from './summary-card-row.component';

describe('SummaryCardRowComponent', () => {
  let component: SummaryCardRowComponent;
  let fixture: ComponentFixture<SummaryCardRowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SummaryCardRowComponent]
    });
    fixture = TestBed.createComponent(SummaryCardRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
