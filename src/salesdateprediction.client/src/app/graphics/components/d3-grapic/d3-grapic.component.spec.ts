import { ComponentFixture, TestBed } from '@angular/core/testing';

import { D3GrapicComponent } from './d3-grapic.component';

describe('D3GrapicComponent', () => {
  let component: D3GrapicComponent;
  let fixture: ComponentFixture<D3GrapicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [D3GrapicComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(D3GrapicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
