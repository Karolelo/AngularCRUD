import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Product} from '../../Intefraces/product';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  private baseUrl = 'https://localhost:7200/Product';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/all`);
  }

  getProductPage(page: number, pageSize: number): Observable<{ data: Product[], count: number }> {
    return this.http.get<{ data: Product[], count: number }>(
      `${this.baseUrl}/page/${page + 1}/size/${pageSize}`
    );
  }
  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
  createProduct(product: any): Observable<Product> {
   return this.http.post<any>(this.baseUrl, product);
  }
  updateProduct(product: any): Observable<Product> {
    return this.http.put<any>(`${this.baseUrl}`, product);
  }

}

