import { TestBed } from '@angular/core/testing';

import { SourcesServiceService } from './sources-service.service';

describe('SourcesServiceService', () => {
  let service: SourcesServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SourcesServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
