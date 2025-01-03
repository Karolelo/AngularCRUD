import { Component } from '@angular/core';
import {Product} from '../../../../Intefraces/product';
import { HttpClient } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';
import {ProductsService} from '../../../../services/ProductService/products.service';
@Component({
  selector: 'app-card-list',
  standalone: false,

  templateUrl: './card-list.component.html',
  styleUrl: './card-list.component.css'
})
export class CardListComponent {

  products!: Product[];
  pageSize = 10;
  pageIndex = 0;
  totalItems = 0;

  constructor(private http: HttpClient,private productService: ProductsService) { }

  ngOnInit(): void {
    this.loadCards();
  }

  loadCards() {
    this.productService.getProductPage(this.pageIndex, this.pageSize).subscribe(response => {
      console.log(response);
      this.products = response.data;
      this.totalItems = response.count;
    })
  }

  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadCards();
  }
}
