import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderNavUserComponent } from './header-nav-user.component';

describe('HeaderNavUserComponent', () => {
  let component: HeaderNavUserComponent;
  let fixture: ComponentFixture<HeaderNavUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeaderNavUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderNavUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
