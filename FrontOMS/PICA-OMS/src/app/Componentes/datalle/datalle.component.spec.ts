import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatalleComponent } from './datalle.component';

describe('DatalleComponent', () => {
  let component: DatalleComponent;
  let fixture: ComponentFixture<DatalleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatalleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatalleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
