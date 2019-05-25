import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatalleProductoComponent } from './datalle-producto.component';

describe('DatalleProductoComponent', () => {
  let component: DatalleProductoComponent;
  let fixture: ComponentFixture<DatalleProductoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatalleProductoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatalleProductoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
