import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardNameComponent } from './card-name.component';

describe('CardNameComponent', () => {
  let component: CardNameComponent;
  let fixture: ComponentFixture<CardNameComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CardNameComponent]
    });
    fixture = TestBed.createComponent(CardNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
