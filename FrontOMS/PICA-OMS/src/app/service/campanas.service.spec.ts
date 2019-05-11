import { TestBed } from '@angular/core/testing';

import { CampanasService } from './campanas.service';

describe('CampanasService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CampanasService = TestBed.get(CampanasService);
    expect(service).toBeTruthy();
  });
});
