import { Component,inject,OnInit } from '@angular/core';
import {Product} from '../../Intefraces/product';
import { Router } from '@angular/router';
import {Category} from '../../Intefraces/category';
import {CategoryService} from '../category-managment-page/CategoryService/category.service';
import {ProductsService} from '../../services/ProductService/products.service';
import {dataSharingService} from '../../services/DataSharingService/data-sharing.service';
@Component({
  selector: 'app-admin-dashboard',
  standalone: false,
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent implements OnInit{
  Products: Product[] = [];
  Categories: Category[] = [];
  router = inject(Router);
  constructor(private productsService: ProductsService,private dataSharingService:  dataSharingService) {}
  ngOnInit() {
    this.productsService.getProducts().subscribe(
      (products) => {
        this.Products = products;
        console.log(this.Products);
      }
    );

  }
  navigate(){
    this.router.navigate(['dashboard/add-edit-item']);
  }

  onEdit(product: Product){
    this.dataSharingService.setProduct(product);
    console.log(this.dataSharingService.currentProduct);
    this.router.navigate(['dashboard/add-edit-item']);
  }

  onDelete(product: Product){
    this.productsService.deleteProduct(product.id).subscribe({
      next: () => {
        this.Products = this.Products.filter(p => p !== product);
        console.log("Produkt usunięty" + product.name);

    }}
    );
  }

    protected readonly encodeURIComponent = encodeURIComponent;
}
