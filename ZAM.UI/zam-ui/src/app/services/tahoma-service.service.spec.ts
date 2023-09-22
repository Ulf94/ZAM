import { TestBed } from '@angular/core/testing';

import { TahomaServiceService } from './tahoma-service.service';

describe('TahomaServiceService', () => {
  let service: TahomaServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TahomaServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
