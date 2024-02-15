import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookStoreLayoutComponent } from './book-store-layout.component';

describe('BookStoreLayoutComponent', () => {
  let component: BookStoreLayoutComponent;
  let fixture: ComponentFixture<BookStoreLayoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookStoreLayoutComponent]
    });
    fixture = TestBed.createComponent(BookStoreLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
