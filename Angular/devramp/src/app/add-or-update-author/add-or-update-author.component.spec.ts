import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOrUpdateAuthorComponent } from './add-or-update-author.component';

describe('AddOrUpdateAuthorComponent', () => {
  let component: AddOrUpdateAuthorComponent;
  let fixture: ComponentFixture<AddOrUpdateAuthorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddOrUpdateAuthorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOrUpdateAuthorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
