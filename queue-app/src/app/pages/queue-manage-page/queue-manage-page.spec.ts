import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QueueManagePage } from './queue-manage-page';

describe('QueueManagePage', () => {
  let component: QueueManagePage;
  let fixture: ComponentFixture<QueueManagePage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QueueManagePage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QueueManagePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
