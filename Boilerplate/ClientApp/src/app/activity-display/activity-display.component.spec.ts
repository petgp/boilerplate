import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityDisplayComponent } from './activity-display.component';

describe('ActivityDisplayComponent', () => {
  let component: ActivityDisplayComponent;
  let fixture: ComponentFixture<ActivityDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActivityDisplayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
