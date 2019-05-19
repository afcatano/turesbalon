import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatalleCampanaComponent } from './datalle-campana.component';

describe('DatalleCampanaComponent', () => {
  let component: DatalleCampanaComponent;
  let fixture: ComponentFixture<DatalleCampanaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatalleCampanaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatalleCampanaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
