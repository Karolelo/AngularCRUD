import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCategoryPanelComponent } from './add-category-panel.component';

describe('AddCategoryPanelComponent', () => {
  let component: AddCategoryPanelComponent;
  let fixture: ComponentFixture<AddCategoryPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddCategoryPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCategoryPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
