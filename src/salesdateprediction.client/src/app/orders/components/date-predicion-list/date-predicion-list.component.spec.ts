import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatePredicionListComponent } from './date-predicion-list.component';

describe('DatePredicionListComponent', () => {
  let component: DatePredicionListComponent;
  let fixture: ComponentFixture<DatePredicionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DatePredicionListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DatePredicionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
