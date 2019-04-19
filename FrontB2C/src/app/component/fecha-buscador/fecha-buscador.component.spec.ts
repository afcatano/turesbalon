import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FechaBuscadorComponent } from './fecha-buscador.component';

describe('FechaBuscadorComponent', () => {
  let component: FechaBuscadorComponent;
  let fixture: ComponentFixture<FechaBuscadorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FechaBuscadorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FechaBuscadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
