import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookStoreHeaderComponent } from './book-store-header.component';

describe('BookStoreHeaderComponent', () => {
  let component: BookStoreHeaderComponent;
  let fixture: ComponentFixture<BookStoreHeaderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookStoreHeaderComponent]
    });
    fixture = TestBed.createComponent(BookStoreHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
