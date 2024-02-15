import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutletTabComponent } from './outlet-tab.component';

describe('OutletTabComponent', () => {
  let component: OutletTabComponent;
  let fixture: ComponentFixture<OutletTabComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OutletTabComponent]
    });
    fixture = TestBed.createComponent(OutletTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
