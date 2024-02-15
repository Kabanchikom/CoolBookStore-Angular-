import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookStoreFooterComponent } from './book-store-footer.component';

describe('BookStoreFooterComponent', () => {
  let component: BookStoreFooterComponent;
  let fixture: ComponentFixture<BookStoreFooterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookStoreFooterComponent]
    });
    fixture = TestBed.createComponent(BookStoreFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
