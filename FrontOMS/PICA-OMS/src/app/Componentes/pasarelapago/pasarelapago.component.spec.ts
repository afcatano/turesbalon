import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PasarelapagoComponent } from './pasarelapago.component';

describe('PasarelapagoComponent', () => {
  let component: PasarelapagoComponent;
  let fixture: ComponentFixture<PasarelapagoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PasarelapagoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PasarelapagoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
