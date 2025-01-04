import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import {Product} from '../../Intefraces/product';

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
    //this.productSource.next(null);
    return product;
  }
  clearService(){
    this.productSource.next(null)
  }
}
