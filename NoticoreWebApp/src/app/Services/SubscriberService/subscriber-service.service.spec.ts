import { TestBed } from '@angular/core/testing';

import { SubscriberServiceService } from './subscriber-service.service';

describe('SubscriberServiceService', () => {
  let service: SubscriberServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubscriberServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
