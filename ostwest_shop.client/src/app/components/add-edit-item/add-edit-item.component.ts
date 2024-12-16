import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { dataSharingService } from '../admin-dashboard/DataSharingService/data-sharing.service';
import { Product } from '../../Intefraces/product';

@Component({
  selector: 'app-add-edit-item',
  standalone: false,
  templateUrl: './add-edit-item.component.html',
  styleUrls: ['./add-edit-item.component.css'],
})
export class AddEditItemComponent implements OnInit {

  productForm!:FormGroup;
  product: Product | null = null;
  constructor(
    private dataSharingService: dataSharingService,
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
      });
    } else {
      this.productForm = new FormGroup({
        productName: new FormControl('', Validators.required),
        productPrice: new FormControl(0, [Validators.required, Validators.min(0)]),
        productQuantity: new FormControl(0, [Validators.required, Validators.min(0)]),
        productCategory: new FormControl('', Validators.required),
      });
    }
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      console.log('Form submitted:', this.productForm.value);
    }
  }
}
