import { Component, OnInit,inject } from '@angular/core';
import { FormGroup,ReactiveFormsModule, FormControl,Validators } from '@angular/forms';
import { dataSharingService } from '../admin-dashboard/DataSharingService/data-sharing.service';
import { Product } from '../../Intefraces/product';
import { Router } from '@angular/router';
import {ProductsService} from '../admin-dashboard/ProductService/products.service';
@Component({
  selector: 'app-add-edit-item',
  standalone: false,
  templateUrl: './add-edit-item.component.html',
  styleUrls: ['./add-edit-item.component.css'],
})
export class AddEditItemComponent implements OnInit {

  productForm!:FormGroup;
  product: Product | null = null;
  router= inject(Router);
  constructor(
    private dataSharingService: dataSharingService,
    private productService: ProductsService
  ) {
  }

  ngOnInit(): void {
    const product = this.dataSharingService.getProduct();
    if (product) {
      this.product = product;

      this.productForm = new FormGroup({
        productName: new FormControl(this.product.name || '', Validators.required),
        productPrice: new FormControl(this.product.price || 0, [Validators.required, Validators.min(0)]),
        productQuantity: new FormControl( 0, [Validators.required, Validators.min(0)]),
        productCategory: new FormControl(this.product?.categories || '', Validators.required),
      },
        {
          updateOn: 'blur'
        });
    } else {
      this.productForm = new FormGroup({
        productName: new FormControl('', [Validators.required, Validators.minLength(3)]),
        productPrice: new FormControl(0, [Validators.required, Validators.min(0)]),
        productQuantity: new FormControl(0, [Validators.required, Validators.min(0)]),
        productCategory: new FormControl('', Validators.required),
      },
        {
          updateOn: 'blur'
        });
    }
  }

  navigate (path: string){
    this.router.navigate([path]);
  }
  onSubmit(): void {
    if (this.productForm.valid) {
      const productData = {
        name: this.productForm.value.productName,
        price: this.productForm.value.productPrice,
        magazine: {
          quantity: this.productForm.value.productQuantity
        }
      }
      this.productService.createProduct(productData).subscribe({
        next: (response) => {
          console.log('Produkt został utworzony:', response);
          this.router.navigate(['dashboard']);
        },
        error: (err) => {
          console.error('Błąd tworzenia produktu:', err);
        }
      });
    }
  }
}
