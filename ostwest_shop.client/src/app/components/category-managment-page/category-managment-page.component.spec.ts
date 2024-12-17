import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryManagmentPageComponent } from './category-managment-page.component';

describe('CategoryManagmentPageComponent', () => {
  let component: CategoryManagmentPageComponent;
  let fixture: ComponentFixture<CategoryManagmentPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CategoryManagmentPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CategoryManagmentPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
