import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCardListItemComponent } from './product-card-list-item.component';

describe('ProductCardListItemComponent', () => {
  let component: ProductCardListItemComponent;
  let fixture: ComponentFixture<ProductCardListItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductCardListItemComponent]
    });
    fixture = TestBed.createComponent(ProductCardListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
