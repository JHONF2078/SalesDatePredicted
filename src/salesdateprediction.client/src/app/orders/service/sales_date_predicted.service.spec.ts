import { TestBed } from '@angular/core/testing';

import { SalesDatePredictedService } from './sales_date_predicted.service';

describe('SalesDatePredictionService', () => {
  let service: SalesDatePredictedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalesDatePredictedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
