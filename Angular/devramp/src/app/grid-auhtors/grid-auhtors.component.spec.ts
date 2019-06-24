import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GridAuhtorsComponent } from './grid-auhtors.component';

describe('GridAuhtorsComponent', () => {
  let component: GridAuhtorsComponent;
  let fixture: ComponentFixture<GridAuhtorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GridAuhtorsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GridAuhtorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
