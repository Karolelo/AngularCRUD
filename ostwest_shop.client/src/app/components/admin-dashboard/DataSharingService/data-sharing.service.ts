import { Injectable } from '@angular/core';
import {Product} from '../../../Intefraces/product';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class dataSharingService {
  private productSource = new BehaviorSubject<Product | null>(null);
  currentProduct = this.productSource.asObservable();

  setProduct(product: Product) {
    this.productSource.next(product);
  }

  getProduct() {
    const product = this.productSource.getValue();
    this.productSource.next(null);
    return product;
  }
}
