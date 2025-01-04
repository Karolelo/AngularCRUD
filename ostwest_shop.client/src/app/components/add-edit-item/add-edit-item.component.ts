import { Component, OnInit,inject } from '@angular/core';
import { FormGroup,ReactiveFormsModule, FormControl,Validators } from '@angular/forms';
import { Product } from '../../Intefraces/product';
import { Router } from '@angular/router';
import {Category} from '../../Intefraces/category';
import {CategoryService} from '../category-managment-page/CategoryService/category.service';
import {ProductsService} from '../../services/ProductService/products.service';
import {dataSharingService} from '../../services/DataSharingService/data-sharing.service';
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
  Categories!: Category[];
  editMode = false;
  file!: File;
  constructor(
    private dataSharingService: dataSharingService,
    private productService: ProductsService,
    private categoryService: CategoryService
  ) {
  }

  ngOnInit(): void {
    const product = this.dataSharingService.getProduct();
    if (product) {
      this.product = product;


      this.productForm = new FormGroup({
        productName: new FormControl(this.product.name || '', Validators.required),
        productPrice: new FormControl(this.product.price || 0, [Validators.required, Validators.min(0)]),
        productQuantity: new FormControl( this.product?.magazine.quanity, [Validators.required, Validators.min(0)]),
          productCategory: new FormControl([], Validators.required)
      },
        {
          updateOn: 'blur'
        });
      this.editMode = true;
    }
    else {
      this.productForm = new FormGroup({
        productName: new FormControl('', [Validators.required, Validators.minLength(3)]),
        productPrice: new FormControl(0, [Validators.required, Validators.min(0)]),
        productQuantity: new FormControl(0, [Validators.required, Validators.min(0)]),
        productCategory: new FormControl([], Validators.required),
      },
        {
          updateOn: 'blur'
        });
    }

    this.categoryService.getCategories().subscribe(category => {this.Categories=category});

  }

  navigate (path: string){
    this.router.navigate([path]);
  }

  onFileChange(event: Event) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files[0]) {
      this.file = target.files[0];
    }
  }

  createProduct() {
    if (this.productForm.valid) {

      const productForm = new FormData();
      productForm.append('name',this.productForm.value.productName);
      productForm.append('price',this.productForm.value.productPrice);
      if(this.file)
      productForm.append('Img',this.file)
      productForm.append('quantity',this.productForm.value.productQuantity);

      this.productForm.value.productCategory.forEach((categoryId: number) => {
        productForm.append('categoriesIDs[]', categoryId.toString());
      });


      console.log('FormData Values:');
      productForm.forEach((value, key) => {
        console.log(`${key}:`, value);
      });

      this.productService.createProduct(productForm).subscribe({
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

  updateProduct() {
    if (this.productForm.valid) {
      const productForm = new FormData();
      productForm.append('id',this.product!.id.toString());
      productForm.append('name',this.productForm.value.productName);
      productForm.append('price',this.productForm.value.productPrice);
      if(this.file)
        productForm.append('Img',this.file)
      productForm.append('quantity',this.productForm.value.productQuantity);

      this.productForm.value.productCategory.forEach((categoryId: number) => {
        productForm.append('categoriesIDs[]', categoryId.toString());
      });


      console.log('FormData Values:');
      productForm.forEach((value, key) => {
        console.log(`${key}:`, value);
      });
      this.productService.updateProduct(productForm).subscribe({
        next: (response) => {
          console.log('Produkt został zaktualizowany:');
          this.router.navigate(['dashboard']);
          this.dataSharingService.clearService();
        },
        error: (err) => {
          console.error('Błąd przy aktualizacji produktu produktu:', err);
        }
      });
    }
  }
  onSubmit(): void {
    if(this.editMode){
      this.updateProduct();
    }
    else{
      this.createProduct();
    }
  }
}
