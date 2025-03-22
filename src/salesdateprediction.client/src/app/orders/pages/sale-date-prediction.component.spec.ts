import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleDatePredictionComponent } from './sale-date-prediction.component';

describe('SaleDatePredictionComponent', () => {
  let component: SaleDatePredictionComponent;
  let fixture: ComponentFixture<SaleDatePredictionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleDatePredictionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleDatePredictionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
