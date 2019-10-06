import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleDisplayComponent } from './sample-display.component';

describe('UserDisplayComponent', () => {
  let component: SampleDisplayComponent;
  let fixture: ComponentFixture<SampleDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SampleDisplayComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SampleDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
