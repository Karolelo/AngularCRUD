import { Component,OnInit } from '@angular/core';
import {Category} from '../../../../Intefraces/category';
import {CategoryService} from '../../CategoryService/category.service';

@Component({
  selector: 'app-category-list',
  standalone: false,

  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit{

  Categories!: Category[];
  constructor(private categoryService: CategoryService) {
  }
  ngOnInit() {
    this.categoryService.getCategories().subscribe(category => {
      this.Categories = category;
      console.log(category)})
  }

  removeCategory(id: number) {
    this.categoryService.deleteCategory(id).subscribe(() => {
      this.Categories = this.Categories.filter(category => category.id !== id);
    });
  }
}
