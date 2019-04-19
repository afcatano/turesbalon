import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatalleEventoComponent } from './datalle-evento.component';

describe('DatalleEventoComponent', () => {
  let component: DatalleEventoComponent;
  let fixture: ComponentFixture<DatalleEventoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatalleEventoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatalleEventoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
