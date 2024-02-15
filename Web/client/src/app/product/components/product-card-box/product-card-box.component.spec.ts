import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCardBoxComponent } from './product-card-box.component';

describe('ProductCardBoxComponent', () => {
  let component: ProductCardBoxComponent;
  let fixture: ComponentFixture<ProductCardBoxComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductCardBoxComponent]
    });
    fixture = TestBed.createComponent(ProductCardBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
