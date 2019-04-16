import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PanelBuscadorComponent } from './panel-buscador.component';

describe('PanelBuscadorComponent', () => {
  let component: PanelBuscadorComponent;
  let fixture: ComponentFixture<PanelBuscadorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PanelBuscadorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PanelBuscadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
