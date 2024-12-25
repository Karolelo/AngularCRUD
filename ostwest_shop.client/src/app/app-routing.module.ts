import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import {AddEditItemComponent} from './components/add-edit-item/add-edit-item.component';
import {CategoryManagmentPageComponent} from './components/category-managment-page/category-managment-page.component';
import {
  ItemViewPageForClientsComponent
} from './components/item-view-page-for-clients/item-view-page-for-clients.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: AdminDashboardComponent },
  { path: 'dashboard/add-edit-item', component: AddEditItemComponent},
  { path: 'categories', component: CategoryManagmentPageComponent},
  { path: 'productList', component: ItemViewPageForClientsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
