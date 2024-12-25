import { HttpClientModule } from '@angular/common/http';
import { NgModule,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { AddEditItemComponent } from './components/add-edit-item/add-edit-item.component';
import { ReactiveFormsModule,FormBuilder,FormsModule } from '@angular/forms';
import { CategoryManagmentPageComponent } from './components/category-managment-page/category-managment-page.component';
import { CategoryItemComponent } from './components/category-managment-page/CategoryComponents/category-item/category-item.component';
import { CategoryListComponent } from './components/category-managment-page/CategoryComponents/category-list/category-list.component';
import { AddCategoryPanelComponent } from './components/category-managment-page/CategoryComponents/add-category-panel/add-category-panel.component';
import { FooterComponent } from './SharedComponets/footer/footer.component';
import { HeaderComponent } from './SharedComponets/header/header.component';
import {MatIconModule} from '@angular/material/icon';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ItemViewPageForClientsComponent } from './components/item-view-page-for-clients/item-view-page-for-clients.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { RouterModule } from '@angular/router';
import { ItemCardComponent } from './components/item-view-page-for-clients/item-view-components/item-card/item-card.component';
import { CardListComponent } from './components/item-view-page-for-clients/item-view-components/card-list/card-list.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
@NgModule({
  declarations: [
    AppComponent,
    AdminDashboardComponent,
    AddEditItemComponent,
    CategoryManagmentPageComponent,
    CategoryItemComponent,
    CategoryListComponent,
    AddCategoryPanelComponent,
    FooterComponent,
    HeaderComponent,
    ItemViewPageForClientsComponent,
    ItemCardComponent,
    CardListComponent,
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, CommonModule, ReactiveFormsModule,FormsModule,
    MatIconModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatButtonModule,
    DragDropModule,
    RouterModule,
    MatPaginatorModule,BrowserAnimationsModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
