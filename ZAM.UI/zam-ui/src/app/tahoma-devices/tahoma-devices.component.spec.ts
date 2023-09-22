import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TahomaDevicesComponent } from './tahoma-devices.component';

describe('TahomaDevicesComponent', () => {
  let component: TahomaDevicesComponent;
  let fixture: ComponentFixture<TahomaDevicesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TahomaDevicesComponent]
    });
    fixture = TestBed.createComponent(TahomaDevicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
