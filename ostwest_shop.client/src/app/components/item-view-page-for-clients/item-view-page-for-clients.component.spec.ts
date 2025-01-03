import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemViewPageForClientsComponent } from './item-view-page-for-clients.component';

describe('ItemViewPageForClientsComponent', () => {
  let component: ItemViewPageForClientsComponent;
  let fixture: ComponentFixture<ItemViewPageForClientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ItemViewPageForClientsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemViewPageForClientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
