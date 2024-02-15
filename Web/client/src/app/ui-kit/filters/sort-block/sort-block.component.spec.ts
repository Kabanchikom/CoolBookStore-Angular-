import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SortBlockComponent } from './sort-block.component';

describe('SortBlockComponent', () => {
  let component: SortBlockComponent;
  let fixture: ComponentFixture<SortBlockComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SortBlockComponent]
    });
    fixture = TestBed.createComponent(SortBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
