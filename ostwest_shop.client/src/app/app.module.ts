import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
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
@NgModule({
  declarations: [
    AppComponent,
    AdminDashboardComponent,
    AddEditItemComponent,
    CategoryManagmentPageComponent,
    CategoryItemComponent,
    CategoryListComponent,
    AddCategoryPanelComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, CommonModule, ReactiveFormsModule,FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
