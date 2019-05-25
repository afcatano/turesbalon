import { TestBed } from '@angular/core/testing';

import { PasarelaService } from './pasarela.service';

describe('PasarelaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PasarelaService = TestBed.get(PasarelaService);
    expect(service).toBeTruthy();
  });
});
