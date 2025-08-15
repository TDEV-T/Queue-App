import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QueueRequestPage } from './queue-request-page';

describe('QueueRequestPage', () => {
  let component: QueueRequestPage;
  let fixture: ComponentFixture<QueueRequestPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QueueRequestPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QueueRequestPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
