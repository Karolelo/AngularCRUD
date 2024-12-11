import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { AddEditItemComponent } from './components/add-edit-item/add-edit-item.component';
import { ReactiveFormsModule,FormBuilder } from '@angular/forms';
@NgModule({
  declarations: [
    AppComponent,
    AdminDashboardComponent,
    AddEditItemComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, CommonModule, ReactiveFormsModule,
    FormBuilder
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
