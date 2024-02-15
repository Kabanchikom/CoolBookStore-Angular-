import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCardThumbComponent } from './product-card-thumb.component';

describe('ProductCardThumbComponent', () => {
  let component: ProductCardThumbComponent;
  let fixture: ComponentFixture<ProductCardThumbComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductCardThumbComponent]
    });
    fixture = TestBed.createComponent(ProductCardThumbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
