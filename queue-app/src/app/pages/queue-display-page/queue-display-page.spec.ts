import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QueueDisplayPage } from './queue-display-page';

describe('QueueDisplayPage', () => {
  let component: QueueDisplayPage;
  let fixture: ComponentFixture<QueueDisplayPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QueueDisplayPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QueueDisplayPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
