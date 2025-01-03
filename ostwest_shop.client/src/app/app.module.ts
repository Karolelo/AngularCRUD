import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ItemViewPageForClientsComponent } from './components/item-view-page-for-clients/item-view-page-for-clients.component';
import { RouterModule } from '@angular/router';
import { ItemCardComponent } from './components/item-view-page-for-clients/item-view-components/item-card/item-card.component';
import { CardListComponent } from './components/item-view-page-for-clients/item-view-components/card-list/card-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegistrationPageComponent } from './components/registration-page/registration-page.component';
import {MaterialModule} from './modules/material/material.module';
import {AuthInterceptor} from './interceptors/auth.interceptor';
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
    LoginPageComponent,
    RegistrationPageComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    BrowserAnimationsModule,
    MaterialModule
  ],
  providers: [
    provideAnimationsAsync(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
